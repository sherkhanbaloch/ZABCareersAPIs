using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            this. db = db;
        }

        [HttpGet("GetAllMessages")]
        public IActionResult GetAllMessages()
        {
            var data = db.Tbl_Messages.ToList();
            return Ok(data);
        }

        [HttpPost("AddMessage")]
        public IActionResult AddMessage([FromForm] Message message)
        {
            if (message == null)
            {
                return BadRequest();
            }
            else
            {
                db.Tbl_Messages.Add(message);
                db.SaveChanges();
                return Created();
            }
        }

        [HttpDelete("DeleteMessage/{Id}")]
        public IActionResult DeleteMessage(int Id)
        {
            var data = db.Tbl_Messages.Find(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Messages.Remove(data);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpGet("GetMessageByID/{Id}")]
        public IActionResult GetMessageByID(int Id)
        {
            var data = db.Tbl_Messages.Find(Id);

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
