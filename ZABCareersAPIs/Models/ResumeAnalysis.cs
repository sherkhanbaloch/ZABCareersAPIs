namespace ZABCareersAPIs.Models
{
    public class ResumeAnalysis
    {
        public int ResumeAnalysisId { get; set; }

        // Foreign Keys
        public int AppliedJobId { get; set; }
        public AppliedJob? AppliedJob { get; set; }

        public float MatchedScore { get; set; }
        public string KeySkills { get; set; }
        public string RequiredSkills { get; set; }
        public string Experience { get; set; }
        public string SkillsMatched { get; set; }
        public string MissingSkills { get; set; }
        public string AISuggestions { get; set; }
    }
}
