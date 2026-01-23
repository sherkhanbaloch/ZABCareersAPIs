using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllDepartments()
        {
            var data = db.Tbl_Departments.ToList();
            return Ok(data);
        }

        [HttpPost("AddDepartment")]
        public IActionResult AddDepartment([FromForm] Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            else
            {
                db.Tbl_Departments.Add(department);
                db.SaveChanges();
                return Created();
            }
        }

        [HttpPut("UpdateDepartment/{Id}")]
        public IActionResult UpdateDepartment(int Id, [FromForm] Department department)
        {
            var data = db.Tbl_Departments.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.DepartmentName = department.DepartmentName;
                db.SaveChanges();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteDepartment/{Id}")]
        public IActionResult DeleteDepartment(int Id)
        {
            var data = db.Tbl_Departments.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Departments.Remove(data);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpGet("GetDepartmentByID/{Id}")]
        public IActionResult GetDepartmentByID(int Id)
        {
            var data = db.Tbl_Departments.Find(Id);

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
