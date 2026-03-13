using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Service.Interface;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeAnalysisController : ControllerBase
    {
        private readonly AppDbContext db;

        public ResumeAnalysisController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetResumeAnalysis/{AppliedJobId}")]
        public async Task<IActionResult> GetResumeAnalysis(int AppliedJobId)
        {
            var result = await db.Tbl_ResumeAnalysis.Where(ra => ra.AppliedJobId == AppliedJobId).Select(ra => new
            {
                ra.AppliedJob.Candidate.CandidateName,
                ra.AppliedJob.Candidate.CandidateEmail,
                ra.AppliedJob.Candidate.CandidateMobile,
                ra.AppliedJob.Job.JobTitle,
                ra.AppliedJob.Job.Campus.CampusName,
                ra.AppliedJobId,
                ra.AppliedJob.ResumeUsedUrl,
                ra.AppliedJob.ApplicationStatus,
                ra.MatchedScore,
                ra.Experience,
                SkillsMatched = ra.SkillsMatched.Split(',').ToList(),
                MissingSkills = ra.MissingSkills.Split(',').ToList(),
                AISuggestions = ra.AISuggestions.Split(',').ToList(),
                ra.AnalyzedOn
            }).FirstOrDefaultAsync();

            return Ok(result);
        }
    }
}

