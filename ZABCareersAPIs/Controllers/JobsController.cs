using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext db;

        public JobsController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllJobs")]
        public IActionResult GetAllJobs()
        {
            var data = db.Tbl_Jobs.ToList();
            return Ok(data);
        }

        [HttpPost("AddJob")]
        public IActionResult AddJob([FromForm] Job job)
        {
            if (job == null)
            {
                return BadRequest();
            }
            else
            {
                db.Tbl_Jobs.Add(job);
                db.SaveChanges();
                return Created();
            }
        }

        [HttpPut("UpdateJob/{Id}")]
        public IActionResult UpdateJob(int Id, [FromForm] Job job)
        {
            var data = db.Tbl_Jobs.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.JobTitle = job.JobTitle;
                data.FeaturedImage = job.FeaturedImage;
                data.Vacancy = job.Vacancy;
                data.EmploymentStatus = job.EmploymentStatus;
                data.Experience = job.Experience;
                data.JobLocation = job.JobLocation;
                data.Salary = job.Salary;
                data.Gender = job.Gender;
                data.PublishedOn = job.PublishedOn;
                data.ApplicationDeadline = job.ApplicationDeadline;
                data.JobDescription = job.JobDescription;
                data.Responsibilities = job.Responsibilities;
                data.EducationAndExperience = job.EducationAndExperience;
                data.OtherBenifits = job.OtherBenifits;
                data.CampusId = job.CampusId;
                data.DepartmentId = job.DepartmentId;
                db.SaveChanges();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteJob/{Id}")]
        public IActionResult DeleteJob(int Id)
        {
            var data = db.Tbl_Jobs.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Jobs.Remove(data);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpGet("GetJobByID/{Id}")]
        public IActionResult GetJobByID(int Id)
        {
            var data = db.Tbl_Jobs.Find(Id);

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
