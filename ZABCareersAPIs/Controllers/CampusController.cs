using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusController : ControllerBase
    {
        private readonly AppDbContext db;

        public CampusController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllCampuses")]
        public IActionResult GetAllCampuses()
        {
            var data = db.Tbl_Campus.ToList();
            return Ok(data);
        }

        [HttpPost("AddCampus")]
        public IActionResult AddCampus([FromForm] Campus campus)
        {
            if (campus == null)
            {
                return BadRequest();
            }
            else
            {
                db.Tbl_Campus.Add(campus);
                db.SaveChanges();
                return Created();
            }
        }

        [HttpPut("UpdateCampus/{Id}")]
        public IActionResult UpdateCampus(int Id, [FromForm] Campus campus)
        {
            var data = db.Tbl_Campus.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.CampusName = campus.CampusName;
                data.CampusLogo = campus.CampusLogo;
                data.CampusLocation = campus.CampusLocation;
                db.SaveChanges();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteCampus/{Id}")]
        public IActionResult DeleteCampus(int Id)
        {
            var data = db.Tbl_Campus.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Campus.Remove(data);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpGet("GetCampusByID/{Id}")]
        public IActionResult GetCampusByID(int Id)
        {
            var data = db.Tbl_Campus.Find(Id);

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
