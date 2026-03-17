using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Helpers;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Candidate")]
    public class CandidatesController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;

        public CandidatesController(AppDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        [HttpGet("GetAllCandidates")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCandidates()
        {
            var data = await db.Tbl_Candidates.Select(c => new
            {
                c.CandidateId,
                c.CandidateName,
                c.CandidateEmail,
                c.CandidateMobile,
                c.CandidateResumeUrl,
                c.ResumeLastUpdated,
                c.IsEmailVerified
            }).ToListAsync();

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("AddCandidate")]
        public async Task<IActionResult> AddCandidate([FromForm] Candidate candidate)
        {
            if (candidate == null)
            {
                return BadRequest();
            }

            var exists = await db.Tbl_Candidates.AnyAsync(c => c.CandidateEmail == candidate.CandidateEmail);

            if (exists == true)
            {
                return BadRequest("User Already Exists.");
            }

            if (candidate.CandidateResume != null)
            {
                var folder = Path.Combine(env.WebRootPath, "Resumes");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(candidate.CandidateResume.FileName);
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await candidate.CandidateResume.CopyToAsync(stream);

                candidate.CandidateResumeUrl = "/Resumes/" + fileName;
                candidate.ResumeLastUpdated = DateTime.UtcNow;
            }

            candidate.CandidateResume = null;

            // Password Hashing using BCrypt
            string PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(candidate.CandidatePassword, 13);
            candidate.CandidatePassword = PasswordHash;

            // OTP Process
            candidate.IsEmailVerified = false;

            Random random = new();
            int OTPNumber = random.Next(100000, 999999);
            candidate.OTP = OTPNumber;

            await db.Tbl_Candidates.AddAsync(candidate);
            await db.SaveChangesAsync();

            // Sending Email after Saving data into database
            var account = await db.Tbl_EmailAccounts.Where(e => e.IsDefault == true).FirstOrDefaultAsync();

            if (account == null)
            {
                return BadRequest("Email Account Not Found.");
            }

            EmailSystem email = new(account.EmailHost, account.EmailPort, account.EmailUsername, account.EmailPassword);

            bool flag = await email.SendEmailAsync(candidate.CandidateEmail, "Email Verification Code - ZAB Careers", $"Your OTP for registration is: {OTPNumber}");

            if (flag == false)
            {

                return BadRequest("Email Not Sent.");
            }

            return Ok(candidate);
        }

        [HttpPut("UpdateCandidate/{Id}")]
        public async Task<IActionResult> UpdateCandidate(int Id, [FromForm] Candidate candidate)
        {
            var data = await db.Tbl_Candidates.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }

            data.CandidateName = candidate.CandidateName;
            data.CandidateEmail = candidate.CandidateEmail;

            if (!string.IsNullOrEmpty(candidate.CandidatePassword))
            {
                string PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(candidate.CandidatePassword, 13);
                data.CandidatePassword = PasswordHash;
            }

            data.CandidateMobile = candidate.CandidateMobile;

            if (candidate.CandidateResume != null)
            {
                var folder = Path.Combine(env.WebRootPath, "Resumes");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(candidate.CandidateResume.FileName);
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await candidate.CandidateResume.CopyToAsync(stream);

                data.CandidateResumeUrl = "/Resumes/" + fileName;
                data.ResumeLastUpdated = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();

            return Ok(data);
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
            var data = await db.Tbl_Candidates.Where(c => c.CandidateId == Id).Select(c => new
            {
                c.CandidateId,
                c.CandidateName,
                c.CandidateEmail,
                c.CandidateMobile,
                c.CandidateResumeUrl,
                c.ResumeLastUpdated,
                c.IsEmailVerified
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
