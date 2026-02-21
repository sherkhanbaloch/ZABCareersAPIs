using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Data;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext db;

        public DashboardController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            int TotalJobs = await db.Tbl_Jobs.CountAsync();
            int TotalJobUsers = await db.Tbl_Candidates.CountAsync();
            int TotalUserQueries = await db.Tbl_Messages.CountAsync();
            int TotalCampuses = await db.Tbl_Campus.CountAsync();
            int TotalPortalUsers = await db.Tbl_Users.CountAsync();
            int TotalRoles = await db.Tbl_Roles.CountAsync();
            int TotalDepartments = await db.Tbl_Departments.CountAsync();

            var dashboardData = new
            {
                TotalJobs,
                TotalJobUsers,
                TotalUserQueries,
                TotalCampuses,
                TotalPortalUsers,
                TotalRoles,
                TotalDepartments
            };

            return Ok(dashboardData);
        }

    }
}
