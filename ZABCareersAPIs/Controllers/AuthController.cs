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
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginVM login)
        {
            var user = await db.Tbl_Users.FirstOrDefaultAsync(u => u.UserName == login.UserName && u.UserPassword == login.Password);

            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized("Invalid credentials.");
        }
    }
}
