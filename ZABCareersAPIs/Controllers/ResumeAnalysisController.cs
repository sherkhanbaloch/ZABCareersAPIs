using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZABCareersAPIs.Data;

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

        [HttpGet("GetAllResumeAnalysis")]
        public IActionResult GetAllResumeAnalysis()
        {
            var data = db.Tbl_ResumeAnalysis.ToList();
            return Ok(data);
        }
    }
}
