namespace ZABCareersAPIs.Models
{
    public class AppliedJob
    {
        public int AppliedJobId { get; set; }

        // Foreign Keys
        public int JobId { get; set; }
        public Job? Job { get; set; }

        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }

        public int ApplicationStatus { get; set; }
    }
}
