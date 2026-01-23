using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllUsers()
        {
            var data = db.Tbl_Users.ToList();
            return Ok(data);
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser([FromForm] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                db.Tbl_Users.Add(user);
                db.SaveChanges();
                return Created();
            }
        }

        [HttpPut("UpdateUser/{Id}")]
        public IActionResult UpdateUser(int Id, [FromForm] User user)
        {
            var data = db.Tbl_Users.Find(Id);

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
                db.SaveChanges();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            var data = db.Tbl_Users.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Users.Remove(data);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpGet("GetUserByID/{Id}")]
        public IActionResult GetUserByID(int Id)
        {
            var data = db.Tbl_Users.Find(Id);

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
