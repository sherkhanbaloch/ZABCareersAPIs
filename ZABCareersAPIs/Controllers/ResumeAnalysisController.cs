using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAllResumeAnalysis()
        {
            var data = await db.Tbl_ResumeAnalysis.ToListAsync();
            return Ok(data);
        }
    }
}
