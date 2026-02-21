using System.ComponentModel.DataAnnotations.Schema;

namespace ZABCareersAPIs.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public int UserStatus { get; set; }

        // Foreign Keys
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public int CampusId { get; set; }
        public Campus? Campus { get; set; }
    }
}
