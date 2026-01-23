using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext db;

        public RolesController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var data = db.Tbl_Roles.ToList();
            return Ok(data);
        }

        [HttpPost("AddRole")]
        public IActionResult AddRole([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest();
            }
            else
            {
                db.Tbl_Roles.Add(role);
                db.SaveChanges();
                return Created();
            }
        }

        [HttpPut("UpdateRole/{Id}")]
        public IActionResult UpdateRole(int Id, [FromBody] Role role)
        {
            var data = db.Tbl_Roles.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.RoleName = role.RoleName;
                db.SaveChanges();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteRole/{Id}")]
        public IActionResult DeleteRole(int Id)
        {
            var data = db.Tbl_Roles.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Roles.Remove(data);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpGet("GetRoleByID/{Id}")]
        public IActionResult GetRoleByID(int Id)
        {
            var data = db.Tbl_Roles.Find(Id);

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
