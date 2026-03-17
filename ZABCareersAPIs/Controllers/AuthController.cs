using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Helpers;
using ZABCareersAPIs.Models;
using ZABCareersAPIs.ViewModels;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly IConfiguration configuration;

        public AuthController(AppDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginVM login)
        {
            var user = await db.Tbl_Users.FirstOrDefaultAsync(u => u.UserName == login.UserName);

            if (user != null)
            {
                bool isHashMatched = BCrypt.Net.BCrypt.EnhancedVerify(login.Password, user.UserPassword);

                if (isHashMatched == true)
                {
                    string Token = CreateToken(user.UserId, user.UserName, "Admin");
                    return Ok(Token);
                }
                else
                {
                    return Unauthorized("Invalid User Name or Password.");
                }
            }
            else
            {
                return Unauthorized("Invalid User Name or Password.");
            }
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginVM login)
        {
            var user = await db.Tbl_Candidates.FirstOrDefaultAsync(u => u.CandidateEmail == login.UserName);

            if (user != null)
            {
                bool isHashMatched = BCrypt.Net.BCrypt.EnhancedVerify(login.Password, user.CandidatePassword);

                if (isHashMatched == true)
                {
                    if (user.IsEmailVerified == true)
                    {
                        string Token = CreateToken(user.CandidateId, user.CandidateName, "Candidate");
                        return Ok(Token);
                    }
                    else
                    {
                        return BadRequest("Email Not Verified.");
                    }
                }
                else
                {
                    return Unauthorized("Invalid User Name or Password.");
                }
            }
            else
            {
                return Unauthorized("Invalid User Name or Password.");
            }
        }

        [HttpPost("UserOTPVerify")]
        public async Task<IActionResult> UserOTPVerify([FromBody] OTPVerificationVM verify)
        {
            var user = await db.Tbl_Candidates.Where(c => c.CandidateEmail == verify.Email && c.OTP == verify.OTP).FirstOrDefaultAsync();

            if (user != null)
            {
                user.IsEmailVerified = true;
                await db.SaveChangesAsync();

                return Ok(user.CandidateId);
            }
            return BadRequest("Email and OTP does not match.");
        }

        // Custom Methods

        private string CreateToken(int UserId, string UserName, string Role)
        {
            string ISSUER = configuration.GetValue<string>("AppSettings:ISSUER")!;
            string AUDIENCE = configuration.GetValue<string>("AppSettings:AUDIENCE")!;
            string SECRET_KEY = configuration.GetValue<string>("AppSettings:SECRET_KEY")!;
            DateTime EXPIRY = DateTime.UtcNow.AddDays(1);

            byte[] EncodedKey = Encoding.UTF8.GetBytes(SECRET_KEY);
            var key = new SymmetricSecurityKey(EncodedKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("id", UserId.ToString()),
                new Claim("name", UserName),
                new Claim("role", Role)
            };

            var token = new JwtSecurityToken(
                issuer: ISSUER,
                audience: AUDIENCE,
                claims: claims,
                expires: EXPIRY,
                signingCredentials: creds
            );

            string FinalToken = new JwtSecurityTokenHandler().WriteToken(token);

            return FinalToken;
        }
    }
}
