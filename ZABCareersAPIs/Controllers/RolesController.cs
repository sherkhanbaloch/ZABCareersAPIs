using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAllRoles()
        {
            var data = await db.Tbl_Roles.Select(r => new
            {
                r.RoleId,
                r.RoleName
            }).ToListAsync();

            return Ok(data);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest();
            }
            else
            {
                await db.Tbl_Roles.AddAsync(role);
                await db.SaveChangesAsync();
                return Created();
            }
        }

        [HttpPut("UpdateRole/{Id}")]
        public async Task<IActionResult> UpdateRole(int Id, [FromBody] Role role)
        {
            var data = await db.Tbl_Roles.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.RoleName = role.RoleName;
                await db.SaveChangesAsync();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteRole/{Id}")]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            var data = await db.Tbl_Roles.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Roles.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetRoleByID/{Id}")]
        public async Task<IActionResult> GetRoleByID(int Id)
        {
            var data = await db.Tbl_Roles.Where(r => r.RoleId == Id).Select(r => new
            {
                r.RoleId,
                r.RoleName
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
