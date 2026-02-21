using System.ComponentModel.DataAnnotations.Schema;

namespace ZABCareersAPIs.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidatePassword { get; set; }
        public string CandidateMobile { get; set; }
        [NotMapped]
        public IFormFile CandidateResume { get; set; }
        public string CandidateResumeUrl { get; set; }
        public int CandidateStatus { get; set; }
    }
}
