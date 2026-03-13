using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;
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
            var user = await db.Tbl_Users.FirstOrDefaultAsync(u => u.UserName == login.UserName && u.UserPassword == login.Password);

            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized("Invalid credentials.");
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginVM login)
        {
            var user = await db.Tbl_Candidates.FirstOrDefaultAsync(u => u.CandidateEmail == login.UserName && u.CandidatePassword == login.Password);

            if (user != null)
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
            return Unauthorized("Invalid credentials.");
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
