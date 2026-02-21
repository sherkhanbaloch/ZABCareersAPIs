using System.ComponentModel.DataAnnotations.Schema;

namespace ZABCareersAPIs.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        [NotMapped]
        public IFormFile? FeaturedImage { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public int Vacancy { get; set; }
        public string EmploymentStatus { get; set; }
        public string Experience { get; set; }
        public string JobLocation { get; set; }
        public string Salary { get; set; }
        public string Gender { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public string JobDescription { get; set; }
        public string Responsibilities { get; set; }
        public string EducationAndExperience { get; set; }
        public string OtherBenefits { get; set; }
        public int JobStatus { get; set; }

        // Foreign Keys
        public int CampusId { get; set; }
        public Campus? Campus { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
