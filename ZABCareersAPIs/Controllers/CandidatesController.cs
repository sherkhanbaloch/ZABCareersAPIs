using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllCandidates()
        {
            var data = db.Tbl_Candidates.ToList();
            return Ok(data);
        }

        [HttpPost("AddCandidate")]
        public IActionResult AddCandidate([FromForm] Candidate candidate)
        {
            if (candidate == null)
            {
                return BadRequest();
            }
            else
            {
                db.Tbl_Candidates.Add(candidate);
                db.SaveChanges();
                return Created();
            }
        }

        [HttpPut("UpdateCandidate/{Id}")]
        public IActionResult UpdateCandidate(int Id, [FromForm] Candidate candidate)
        {
            var data = db.Tbl_Candidates.Find(Id);

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
                db.SaveChanges();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteCandidate/{Id}")]
        public IActionResult DeleteCandidate(int Id)
        {
            var data = db.Tbl_Candidates.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Candidates.Remove(data);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpGet("GetCandidateByID/{Id}")]
        public IActionResult GetCandidateByID(int Id)
        {
            var data = db.Tbl_Candidates.Find(Id);

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
