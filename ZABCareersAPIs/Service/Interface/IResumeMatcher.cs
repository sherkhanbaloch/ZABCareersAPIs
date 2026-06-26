namespace ZABCareersAPIs.Service.Interface
{
    public interface IResumeMatcher
    {
        Task<ResumeMatchResult> MatchResumeAsync(string resumeText, string jobDescription);
    }

    public class ResumeMatchResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int MatchPercentage { get; set; }
        public string Experience { get; set; } = string.Empty;
        public List<string> MatchedSkills { get; set; } = new();
        public List<string> MissingSkills { get; set; } = new();
        public List<string> Suggestions { get; set; } = new();
        public MatchSubScores? SubScores { get; set; }
        public MatchScoringBreakdown? Scoring { get; set; }
    }

    public class MatchSubScores
    {
        public int Skills { get; set; }
        public int Experience { get; set; }
        public int Qualifications { get; set; }
        public int Domain { get; set; }
    }

    public class MatchScoringBreakdown
    {
        public int SkillsCoverageScore { get; set; }
        public int ExperienceScore { get; set; }
        public int QualificationsScore { get; set; }
        public int DomainScore { get; set; }
        public int SkillsWeightPercent { get; set; }
        public int ExperienceWeightPercent { get; set; }
        public int QualificationsWeightPercent { get; set; }
        public int DomainWeightPercent { get; set; }
        public int MatchedCount { get; set; }
        public int MissingCount { get; set; }
        public int FinalScore { get; set; }
        public string FormulaSummary { get; set; } = string.Empty;
    }
}
