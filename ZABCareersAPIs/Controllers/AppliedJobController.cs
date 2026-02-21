using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAllApplications()
        {
            var data = await db.Tbl_AppliedJobs.ToListAsync();
            return Ok(data);
        }

        [HttpPost("AddApplication")]
        public async Task<IActionResult> AddApplication([FromForm] AppliedJob appliedJob)
        {
            if (appliedJob == null)
            {
                return BadRequest();
            }
            else
            {
                await db.Tbl_AppliedJobs.AddAsync(appliedJob);
                await db.SaveChangesAsync();
                return Created();
            }
        }

        [HttpDelete("DeleteApplication/{Id}")]
        public async Task<IActionResult> DeleteApplication(int Id)
        {
            var data = await db.Tbl_AppliedJobs.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_AppliedJobs.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetApplicationByID/{Id}")]
        public async Task<IActionResult> GetApplicationByID(int Id)
        {
            var data = await db.Tbl_AppliedJobs.FindAsync(Id);

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
