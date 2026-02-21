using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;

        public CampusController(AppDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        [HttpGet("GetAllCampuses")]
        public async Task<IActionResult> GetAllCampuses()
        {
            var data = await db.Tbl_Campus.Select(c => new
            {
                c.CampusId,
                c.CampusName,
                c.CampusLogoUrl,
                c.CampusLocation
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("GetCampusesForDropdown")]
        public async Task<IActionResult> GetCampusesForDropdown()
        {
            var data = await db.Tbl_Campus.Select(c => new
            {
                c.CampusId,
                c.CampusName
            }).ToListAsync();

            return Ok(data);
        }


        [HttpPost("AddCampus")]
        public async Task<IActionResult> AddCampus([FromForm] Campus campus)
        {
            if (campus == null)
            {
                return BadRequest();
            }
            else
            {
                var folder = Path.Combine(env.WebRootPath, "Images", "CampusLogos");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(campus.CampusLogo.FileName);
                var filePath = Path.Combine(folder, fileName);
                campus.CampusLogo.CopyTo(new FileStream(filePath, FileMode.Create));

                campus.CampusLogoUrl = "/Images/CampusLogos/" + fileName;
                campus.CampusLogo = null;

                await db.Tbl_Campus.AddAsync(campus);
                await db.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPut("UpdateCampus/{id}")]
        public async Task<IActionResult> UpdateCampus(int id, [FromForm] Campus campus)
        {
            var data = await db.Tbl_Campus.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            data.CampusName = campus.CampusName;
            data.CampusLocation = campus.CampusLocation;
            data.CampusStatus = campus.CampusStatus;

            if (campus.CampusLogo != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(campus.CampusLogo.FileName);
                var path = Path.Combine(env.WebRootPath, "Images", "CampusLogos", fileName);

                campus.CampusLogo.CopyTo(new FileStream(path, FileMode.Create));

                data.CampusLogoUrl = "/Images/CampusLogos/" + fileName;
            }

            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("DeleteCampus/{Id}")]
        public async Task<IActionResult> DeleteCampus(int Id)
        {
            var data = await db.Tbl_Campus.FindAsync(Id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                db.Tbl_Campus.Remove(data);
                await db.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("GetCampusByID/{Id}")]
        public async Task<IActionResult> GetCampusByID(int Id)
        {
            var data = await db.Tbl_Campus.Where(c => c.CampusId == Id).Select(c => new
            {
                c.CampusId,
                c.CampusName,
                c.CampusLogo,
                c.CampusLogoUrl,
                c.CampusLocation
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
