using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly AppDbContext db;

        public CandidatesController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllCandidates")]
        public async Task<IActionResult> GetAllCandidates()
        {
            var data = await db.Tbl_Candidates.ToListAsync();
            return Ok(data);
        }

        [HttpPost("AddCandidate")]
        public async Task<IActionResult> AddCandidate([FromForm] Candidate candidate)
        {
            if (candidate == null)
            {
                return BadRequest();
            }
            else
            {
                await db.Tbl_Candidates.AddAsync(candidate);
                await db.SaveChangesAsync();
                return Created();
            }
        }

        [HttpPut("UpdateCandidate/{Id}")]
        public async Task<IActionResult> UpdateCandidate(int Id, [FromForm] Candidate candidate)
        {
            var data = await db.Tbl_Candidates.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.CandidateName = candidate.CandidateName;
                data.CandidatePassword = candidate.CandidatePassword;
                data.CandidateEmail = candidate.CandidateEmail;
                data.CandidateMobile = candidate.CandidateMobile;
                data.CandidateResume = candidate.CandidateResume;
                await db.SaveChangesAsync();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteCandidate/{Id}")]
        public async Task<IActionResult> DeleteCandidate(int Id)
        {
            var data = await db.Tbl_Candidates.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Candidates.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetCandidateByID/{Id}")]
        public async Task<IActionResult> GetCandidateByID(int Id)
        {
            var data = await db.Tbl_Candidates.FindAsync(Id);

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
