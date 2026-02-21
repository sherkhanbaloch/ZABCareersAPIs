using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;
using ZABCareersAPIs.ViewModels;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;

        public JobsController(AppDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        [HttpGet("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var data = await db.Tbl_Jobs.Include(x => x.Department).Include(x => x.Campus).ToListAsync();
            return Ok(data);
        }

        [HttpGet("ViewJobsForAdmin")]
        public async Task<IActionResult> ViewJobsForAdmin()
        {
            var data = await db.Tbl_Jobs.Select(j => new
            {
                j.JobId,
                j.JobTitle,
                j.EmploymentStatus,
                j.PublishedOn,
                j.ApplicationDeadline,
                j.Department.DepartmentName,
                j.Campus.CampusName
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("ViewJobsForUsers")]
        public async Task<IActionResult> ViewJobsForUsers()
        {
            var data = await db.Tbl_Jobs.Select(j => new
            {
                j.JobId,
                j.Campus.CampusLogoUrl,
                j.JobTitle,
                j.Department.DepartmentName,
                j.Campus.CampusName,
                j.Salary,
                j.EmploymentStatus,
                j.PublishedOn
            }).ToListAsync();

            return Ok(data);
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> AddJob([FromForm] Job job)
        {
            if (job == null)
            {
                return BadRequest();
            }
            else
            {
                var folder = Path.Combine(env.WebRootPath, "Images", "JobImages");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(job.FeaturedImage.FileName);
                var filePath = Path.Combine(folder, fileName);
                job.FeaturedImage.CopyTo(new FileStream(filePath, FileMode.Create));

                job.FeaturedImageUrl = "/Images/JobImages/" + fileName;
                job.FeaturedImage = null;

                await db.Tbl_Jobs.AddAsync(job);
                await db.SaveChangesAsync();
                return Created();
            }
        }

        [HttpPut("UpdateJob/{Id}")]
        public async Task<IActionResult> UpdateJob(int Id, [FromForm] Job job)
        {
            var data = await db.Tbl_Jobs.FindAsync(Id);

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
                data.OtherBenefits = job.OtherBenefits;
                data.CampusId = job.CampusId;
                data.DepartmentId = job.DepartmentId;

                if (job.FeaturedImage != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(job.FeaturedImage.FileName);
                    var path = Path.Combine(env.WebRootPath, "Images", "JobImages", fileName);

                    job.FeaturedImage.CopyTo(new FileStream(path, FileMode.Create));

                    data.FeaturedImageUrl = "/Images/CampusLogos/" + fileName;
                }

                await db.SaveChangesAsync();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteJob/{Id}")]
        public async Task<IActionResult> DeleteJob(int Id)
        {
            var data = await db.Tbl_Jobs.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Jobs.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetJobByID/{Id}")]
        public async Task<IActionResult> GetJobByID(int Id)
        {
            var data = await db.Tbl_Jobs.Where(u => u.JobId == Id).Select(j => new
            {
                j.JobId,
                j.JobTitle,
                j.FeaturedImage,
                j.FeaturedImageUrl,
                j.Vacancy,
                j.EmploymentStatus,
                j.Experience,
                j.JobLocation,
                j.Salary,
                j.Gender,
                PublishedOn = j.PublishedOn.ToString("yyyy-MM-dd"),
                ApplicationDeadline = j.ApplicationDeadline.ToString("yyyy-MM-dd"),
                j.JobDescription,
                j.Responsibilities,
                j.EducationAndExperience,
                j.OtherBenefits,
                j.Department.DepartmentId,
                j.Department.DepartmentName,
                j.Campus.CampusId,
                j.Campus.CampusName
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

        [HttpGet("GetJobDetailsForUser/{Id}")]
        public async Task<IActionResult> GetJobDetailsForUser(int Id)
        {
            var data = await db.Tbl_Jobs.Where(u => u.JobId == Id).Select(j => new
            {
                j.JobId,
                j.JobTitle,
                j.FeaturedImage,
                j.FeaturedImageUrl,
                j.Vacancy,
                j.EmploymentStatus,
                j.Experience,
                j.JobLocation,
                j.Salary,
                j.Gender,
                j.PublishedOn,
                j.ApplicationDeadline,
                j.JobDescription,
                j.Responsibilities,
                j.EducationAndExperience,
                j.OtherBenefits,
                j.Department.DepartmentId,
                j.Department.DepartmentName,
                j.Campus.CampusId,
                j.Campus.CampusName,
                j.Campus.CampusLogoUrl
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
