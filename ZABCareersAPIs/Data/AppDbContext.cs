using Microsoft.EntityFrameworkCore;
using ZABCareersAPIs.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZABCareersAPIs.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Campus> Tbl_Campus { get; set; }
        public DbSet<User> Tbl_Users { get; set; }
        public DbSet<Role> Tbl_Roles { get; set; }
        public DbSet<Department> Tbl_Departments { get; set; }
        public DbSet<Job> Tbl_Jobs { get; set; }
        public DbSet<Candidate> Tbl_Candidates { get; set; }
        public DbSet<Message> Tbl_Messages { get; set; }
        public DbSet<AppliedJob> Tbl_AppliedJobs { get; set; }
        public DbSet<ResumeAnalysis> Tbl_ResumeAnalysis { get; set; }

    }
}
