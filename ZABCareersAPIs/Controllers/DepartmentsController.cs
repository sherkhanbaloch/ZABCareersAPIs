using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext db;

        public DepartmentsController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var data = await db.Tbl_Departments.Select(d => new
            {
                d.DepartmentId,
                d.DepartmentName
            }).ToListAsync();

            return Ok(data);
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            else
            {
                await db.Tbl_Departments.AddAsync(department);
                await db.SaveChangesAsync();
                return Created();
            }
        }

        [HttpPut("UpdateDepartment/{Id}")]
        public async Task<IActionResult> UpdateDepartment(int Id, [FromBody] Department department)
        {
            var data = await db.Tbl_Departments.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.DepartmentName = department.DepartmentName;
                await db.SaveChangesAsync();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteDepartment/{Id}")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            var data = await db.Tbl_Departments.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Departments.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDepartmentByID(int Id)
        {
            var data = await db.Tbl_Departments.Where(d => d.DepartmentId == Id).Select(d => new
            {
                d.DepartmentId,
                d.DepartmentName
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
