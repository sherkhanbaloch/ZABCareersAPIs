using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppliedJobController : ControllerBase
    {
        private readonly AppDbContext db;

        public AppliedJobController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllApplications")]
        public IActionResult GetAllApplications()
        {
            var data = db.Tbl_AppliedJobs.ToList();
            return Ok(data);
        }

        [HttpPost("AddApplication")]
        public IActionResult AddApplication([FromForm] AppliedJob appliedJob)
        {
            if (appliedJob == null)
            {
                return BadRequest();
            }
            else
            {
                db.Tbl_AppliedJobs.Add(appliedJob);
                db.SaveChanges();
                return Created();
            }
        }

        [HttpDelete("DeleteApplication/{Id}")]
        public IActionResult DeleteApplication(int Id)
        {
            var data = db.Tbl_AppliedJobs.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_AppliedJobs.Remove(data);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpGet("GetApplicationByID/{Id}")]
        public IActionResult GetApplicationByID(int Id)
        {
            var data = db.Tbl_AppliedJobs.Find(Id);

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
