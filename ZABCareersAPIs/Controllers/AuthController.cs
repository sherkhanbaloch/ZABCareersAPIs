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

        public AuthController(AppDbContext db)
        {
            this.db = db;
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
                    return Ok(user);
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
                        return Ok(user.CandidateId);
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
    }
}
