using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext db;

        public UsersController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await db.Tbl_Users.Select(u => new
            {
                u.UserId,
                u.UserName,
                u.UserEmail,
                u.UserPassword,
                u.Role.RoleName,
                u.Campus.CampusName
            }).ToListAsync();

            return Ok(data);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                await db.Tbl_Users.AddAsync(user);
                await db.SaveChangesAsync();
                return Created();
            }
        }

        [HttpPut("UpdateUser/{Id}")]
        public async Task<IActionResult> UpdateUser(int Id, [FromBody] User user)
        {
            var data = await db.Tbl_Users.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.UserName = user.UserName;
                data.RoleId = user.RoleId;
                data.CampusId = user.CampusId;
                data.UserEmail = user.UserEmail;
                data.UserPassword = user.UserPassword;
                await db.SaveChangesAsync();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var data = await db.Tbl_Users.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Users.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetUserByID/{Id}")]
        public async Task<IActionResult> GetUserByID(int Id)
        {
            var data = await db.Tbl_Users.Where(u => u.UserId == Id).Select(u => new
            {
                u.UserId,
                u.UserName,
                u.UserEmail,
                u.UserPassword,
                u.RoleId,
                u.Role.RoleName,
                u.CampusId,
                u.Campus.CampusName
            }).FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
    }
}
