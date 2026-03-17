using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;
using ZABCareersAPIs.Service.Interface;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AppliedJobController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly IResumeParser _resumeParser;
        private readonly IResumeMatcher _resumeMatcher;

        public AppliedJobController(AppDbContext db, IResumeParser resumeParser, IResumeMatcher resumeMatcher)
        {
            this.db = db;
            _resumeParser = resumeParser;
            _resumeMatcher = resumeMatcher;
        }

        [HttpGet("GetJobApplications")]
        public async Task<IActionResult> GetJobApplications()
        {
            var result = await db.Tbl_AppliedJobs.GroupBy(a => new
            {
                a.JobId,
                a.Job.JobTitle,
                a.Job.PublishedOn,
                a.Job.ApplicationDeadline,
                a.Job.Department.DepartmentName
            }).Select(g => new
            {
                JobId = g.Key.JobId,
                JobTitle = g.Key.JobTitle,
                DepartmentName = g.Key.DepartmentName,
                PublishedOn = g.Key.PublishedOn,
                ApplicationDeadline = g.Key.ApplicationDeadline,
                TotalApplications = g.Count()
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetApplicationsByJob/{JobId}")]
        public async Task<IActionResult> GetApplicationsByJob(int JobId)
        {
            var totalApplications = await db.Tbl_AppliedJobs.CountAsync(x => x.JobId == JobId);

            var result = await db.Tbl_AppliedJobs.Where(a => a.JobId == JobId).Select(a => new
            {
                AppliedJobId = a.AppliedJobId,
                JobId = a.JobId,

                JobTitle = a.Job.JobTitle,
                DepartmentName = a.Job.Department.DepartmentName,
                ApplicationDeadline = a.Job.ApplicationDeadline,

                TotalApplications = totalApplications,

                CandidateName = a.Candidate.CandidateName,
                CandidateEmail = a.Candidate.CandidateEmail,

                ResumeUsedUrl = a.ResumeUsedUrl,

                MatchedScore = db.Tbl_ResumeAnalysis
                        .Where(r => r.AppliedJobId == a.AppliedJobId)
                        .Select(r => r.MatchedScore)
                        .FirstOrDefault(),

                ApplicationStatus = a.ApplicationStatus
            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetIsJobApplied/{JobId}/{CandidateId}")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> GetIsJobApplied(int JobId, int CandidateId)
        {
            var exists = await db.Tbl_AppliedJobs.AnyAsync(a => a.JobId == JobId && a.CandidateId == CandidateId);

            return Ok(exists);
        }

        [HttpPost("AddApplication")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> AddApplication([FromBody] AppliedJob appliedJob)
        {
            if (appliedJob == null)
            {
                return BadRequest();
            }

            var alreadyApplied = await db.Tbl_AppliedJobs.AnyAsync(x => x.JobId == appliedJob.JobId && x.CandidateId == appliedJob.CandidateId);

            if (alreadyApplied)
            {
                return BadRequest("Already applied for this job");
            }

            var resumeUrl = await db.Tbl_Candidates.Where(c => c.CandidateId == appliedJob.CandidateId).Select(c => c.CandidateResumeUrl).FirstOrDefaultAsync();

            var job = await db.Tbl_Jobs.Where(j => j.JobId == appliedJob.JobId).FirstOrDefaultAsync();

            var data = new AppliedJob
            {
                JobId = appliedJob.JobId,
                CandidateId = appliedJob.CandidateId,
                ResumeUsedUrl = resumeUrl,
                IsPrimaryResume = true,
                ApplicationStatus = "Pending"
            };

            await db.Tbl_AppliedJobs.AddAsync(data);
            await db.SaveChangesAsync();

            // ---------- Resume Analysis ----------

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", resumeUrl.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
            {
                var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);

                var formFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "ResumeFile", Path.GetFileName(fullPath));

                var resumeText = await _resumeParser.ExtractTextFromFileAsync(formFile);

                var result = await _resumeMatcher.MatchResumeAsync(resumeText, job.JobDescription);

                var analysis = new ResumeAnalysis
                {
                    AppliedJobId = data.AppliedJobId,
                    MatchedScore = result.MatchPercentage,
                    KeySkills = "",
                    RequiredSkills = "",
                    Experience = result.Experience,
                    SkillsMatched = string.Join(",", result.MatchedSkills),
                    MissingSkills = string.Join(",", result.MissingSkills),
                    AISuggestions = string.Join(",", result.Suggestions),
                    AnalyzedOn = DateTime.UtcNow,
                    ResumeHash = ""
                };

                await db.Tbl_ResumeAnalysis.AddAsync(analysis);
                await db.SaveChangesAsync();
            }

            return Ok(data);
        }

        [HttpPut("ChangeApplicationStatus/{appliedJobId}/{status}")]
        public async Task<IActionResult> ChangeApplicationStatus(int appliedJobId, string status)
        {
            var data = await db.Tbl_AppliedJobs.FindAsync(appliedJobId);

            if (data == null)
            {
                return NotFound();
            }

            data.ApplicationStatus = status;

            await db.SaveChangesAsync();

            return Ok();
        }
    }
}