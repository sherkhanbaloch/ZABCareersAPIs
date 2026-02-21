using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext db;

        public MessagesController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetAllMessages")]
        public async Task<IActionResult> GetAllMessages()
        {
            var data = await db.Tbl_Messages.ToListAsync();
            return Ok(data);
        }

        [HttpPost("AddMessage")]
        public async Task<IActionResult> AddMessage([FromBody] Message message)
        {
            if (message == null)
            {
                return BadRequest();
            }
            else
            {
                await db.Tbl_Messages.AddAsync(message);
                await db.SaveChangesAsync();
                return Created();
            }
        }

        [HttpDelete("DeleteMessage/{Id}")]
        public async Task<IActionResult> DeleteMessage(int Id)
        {
            var data = await db.Tbl_Messages.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Messages.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetMessageByID/{Id}")]
        public async Task<IActionResult> GetMessageByID(int Id)
        {
            var data = await db.Tbl_Messages.FindAsync(Id);

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
