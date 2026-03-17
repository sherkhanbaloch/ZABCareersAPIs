using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Helpers;
using ZABCareersAPIs.Models;
using ZABCareersAPIs.ViewModels;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmailAccountController : ControllerBase
    {
        private readonly AppDbContext db;

        public EmailAccountController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllEmails")]
        public async Task<IActionResult> GetAllEmails()
        {
            var data = await db.Tbl_EmailAccounts.Select(e => new
            {
                e.EmailAccountId,
                e.EmailHost,
                e.EmailPort,
                e.EmailUsername,
                e.EmailPassword,
                e.IsDefault
            }).ToListAsync();

            return Ok(data);
        }

        [HttpPost("AddEmail")]
        public async Task<IActionResult> AddEmail([FromBody] EmailAccount email)
        {
            if (email == null)
            {
                return BadRequest();
            }
            else
            {
                await db.Tbl_EmailAccounts.AddAsync(email);
                await db.SaveChangesAsync();
                return Created();
            }
        }

        [HttpPut("UpdateEmail/{Id}")]
        public async Task<IActionResult> UpdateEmail(int Id, [FromBody] EmailAccount email)
        {
            var data = await db.Tbl_EmailAccounts.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.EmailHost = email.EmailHost;
                data.EmailPort = email.EmailPort;
                data.EmailUsername = email.EmailUsername;
                data.EmailPassword = email.EmailPassword;
                data.IsDefault = email.IsDefault;

                await db.SaveChangesAsync();
                return Ok(data);
            }
        }

        [HttpDelete("DeleteEmail/{Id}")]
        public async Task<IActionResult> DeleteEmail(int Id)
        {
            var data = await db.Tbl_EmailAccounts.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_EmailAccounts.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetEmailByID/{Id}")]
        public async Task<IActionResult> GetEmailByID(int Id)
        {
            var data = await db.Tbl_EmailAccounts.Where(r => r.EmailAccountId == Id).Select(e => new
            {
                e.EmailAccountId,
                e.EmailHost,
                e.EmailPort,
                e.EmailUsername,
                e.EmailPassword,
                e.IsDefault
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

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequestVM request)
        {
            var account = await db.Tbl_EmailAccounts.Where(e => e.IsDefault == true).FirstOrDefaultAsync();

            if (account == null)
            {
                return BadRequest("Email Account Not Found.");
            }

            EmailSystem email = new(account.EmailHost, account.EmailPort, account.EmailUsername, account.EmailPassword);

            bool flag = await email.SendEmailAsync(request.ToEmail, request.Subject, request.Body);

            return Ok(flag);

        }
    }
}
