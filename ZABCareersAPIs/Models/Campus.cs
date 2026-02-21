using System.ComponentModel.DataAnnotations.Schema;

namespace ZABCareersAPIs.Models
{
    public class Campus
    {
        public int CampusId { get; set; }
        public string CampusName { get; set; }
        [NotMapped]
        public IFormFile? CampusLogo { get; set; }
        public string? CampusLogoUrl { get; set; }
        public string CampusLocation { get; set; }
        public int CampusStatus { get; set; }
    }
}
