namespace ZABCareersAPIs.ViewModels
{
    public class ResumeAnalysisDetailsVM
    {
        public int AppliedJobId { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string CandidateEmail { get; set; } = string.Empty;
        public string CandidateMobile { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string CampusName { get; set; } = string.Empty;
        public string? ResumeUsedUrl { get; set; }
        public string? ResumeFileType { get; set; }
        public string ApplicationStatus { get; set; } = string.Empty;

        public float MatchedScore { get; set; }
        public bool AnalysisSuccess { get; set; }
        public string AnalysisStatus { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }

        public string Experience { get; set; } = string.Empty;
        public string KeySkills { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public List<string> SkillsMatched { get; set; } = new();
        public List<string> MissingSkills { get; set; } = new();
        public List<string> AISuggestions { get; set; } = new();

        public DateTime? AnalyzedOn { get; set; }
        public string? ResumeHash { get; set; }

        public MatchScoringBreakdownVM? Scoring { get; set; }
        public ModelAnalysisVM? ModelAnalysis { get; set; }
    }

    public class MatchScoringBreakdownVM
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

    public class ModelAnalysisVM
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int MatchPercentage { get; set; }
        public string Experience { get; set; } = string.Empty;
        public List<string> MatchedSkills { get; set; } = new();
        public List<string> MissingSkills { get; set; } = new();
        public List<string> Suggestions { get; set; } = new();
        public MatchScoringBreakdownVM? Scoring { get; set; }
    }
}
