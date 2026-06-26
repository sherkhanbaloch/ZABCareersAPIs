using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Service.Implement;
using ZABCareersAPIs.Service.Interface;
using ZABCareersAPIs.ViewModels;

namespace ZABCareersAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ResumeAnalysisController : ControllerBase
    {
        private readonly AppDbContext db;

        public ResumeAnalysisController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("GetResumeAnalysis/{AppliedJobId}")]
        public async Task<IActionResult> GetResumeAnalysis(int AppliedJobId)
        {
            var row = await db.Tbl_ResumeAnalysis
                .Where(ra => ra.AppliedJobId == AppliedJobId)
                .Select(ra => new
                {
                    ra.AppliedJob.Candidate.CandidateName,
                    ra.AppliedJob.Candidate.CandidateEmail,
                    ra.AppliedJob.Candidate.CandidateMobile,
                    ra.AppliedJob.Job.JobTitle,
                    ra.AppliedJob.Job.Campus.CampusName,
                    ra.AppliedJobId,
                    ra.AppliedJob.ResumeUsedUrl,
                    ra.AppliedJob.ApplicationStatus,
                    ra.MatchedScore,
                    ra.Experience,
                    ra.KeySkills,
                    ra.RequiredSkills,
                    ra.SkillsMatched,
                    ra.MissingSkills,
                    ra.AISuggestions,
                    ra.AnalyzedOn,
                    ra.ResumeHash,
                    ra.FullAnalysisJson
                })
                .FirstOrDefaultAsync();

            if (row == null)
            {
                return NotFound(new { message = "No resume analysis found for this application." });
            }

            var analysisSuccess = row.MatchedScore >= 0;
            var modelAnalysis = DeserializeModelAnalysis(row.FullAnalysisJson);
            var errorMessage = !analysisSuccess
                ? (modelAnalysis?.ErrorMessage ?? row.Experience)
                : null;

            var skillsMatched = SplitToList(row.SkillsMatched);
            var missingSkills = SplitToList(row.MissingSkills);
            var scoring = modelAnalysis?.Scoring
                ?? BuildLegacyScoring(skillsMatched.Count, missingSkills.Count, analysisSuccess ? (int)row.MatchedScore : -1);

            if (modelAnalysis != null)
            {
                modelAnalysis.Scoring = scoring;
            }

            var response = new ResumeAnalysisDetailsVM
            {
                AppliedJobId = row.AppliedJobId,
                CandidateName = row.CandidateName,
                CandidateEmail = row.CandidateEmail,
                CandidateMobile = row.CandidateMobile,
                JobTitle = row.JobTitle,
                CampusName = row.CampusName,
                ResumeUsedUrl = row.ResumeUsedUrl,
                ResumeFileType = GetResumeFileType(row.ResumeUsedUrl),
                ApplicationStatus = row.ApplicationStatus,
                MatchedScore = analysisSuccess && scoring != null ? scoring.FinalScore : row.MatchedScore,
                AnalysisSuccess = analysisSuccess,
                AnalysisStatus = analysisSuccess ? "Completed" : "Failed",
                ErrorMessage = errorMessage,
                Experience = row.Experience,
                KeySkills = row.KeySkills,
                RequiredSkills = row.RequiredSkills,
                SkillsMatched = skillsMatched,
                MissingSkills = missingSkills,
                AISuggestions = SplitToList(row.AISuggestions),
                AnalyzedOn = row.AnalyzedOn,
                ResumeHash = row.ResumeHash,
                Scoring = scoring,
                ModelAnalysis = modelAnalysis ?? new ModelAnalysisVM
                {
                    Success = analysisSuccess,
                    ErrorMessage = errorMessage,
                    MatchPercentage = analysisSuccess ? (int)row.MatchedScore : -1,
                    Experience = row.Experience,
                    MatchedSkills = skillsMatched,
                    MissingSkills = missingSkills,
                    Suggestions = SplitToList(row.AISuggestions),
                    Scoring = scoring
                }
            };

            return Ok(response);
        }

        private static ModelAnalysisVM? DeserializeModelAnalysis(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            try
            {
                var result = JsonConvert.DeserializeObject<ResumeMatchResult>(json);
                if (result == null)
                {
                    return null;
                }

                return new ModelAnalysisVM
                {
                    Success = result.Success,
                    ErrorMessage = result.ErrorMessage,
                    MatchPercentage = result.MatchPercentage,
                    Experience = result.Experience,
                    MatchedSkills = result.MatchedSkills ?? new List<string>(),
                    MissingSkills = result.MissingSkills ?? new List<string>(),
                    Suggestions = result.Suggestions ?? new List<string>(),
                    Scoring = MapScoring(result.Scoring)
                };
            }
            catch
            {
                return null;
            }
        }

        private static MatchScoringBreakdownVM? MapScoring(MatchScoringBreakdown? scoring)
        {
            if (scoring == null)
            {
                return null;
            }

            return new MatchScoringBreakdownVM
            {
                SkillsCoverageScore = scoring.SkillsCoverageScore,
                ExperienceScore = scoring.ExperienceScore,
                QualificationsScore = scoring.QualificationsScore,
                DomainScore = scoring.DomainScore,
                SkillsWeightPercent = scoring.SkillsWeightPercent,
                ExperienceWeightPercent = scoring.ExperienceWeightPercent,
                QualificationsWeightPercent = scoring.QualificationsWeightPercent,
                DomainWeightPercent = scoring.DomainWeightPercent,
                MatchedCount = scoring.MatchedCount,
                MissingCount = scoring.MissingCount,
                FinalScore = scoring.FinalScore,
                FormulaSummary = scoring.FormulaSummary
            };
        }

        private static MatchScoringBreakdownVM? BuildLegacyScoring(int matched, int missing, int storedScore)
        {
            if (storedScore < 0)
            {
                return null;
            }

            var skillsCoverage = ResumeScoreCalculator.CalculateSkillsCoverageScore(matched, missing);
            var experienceScore = Math.Clamp(storedScore, 0, 100);
            var qualificationsScore = skillsCoverage;
            var domainScore = skillsCoverage;

            var finalScore = (int)Math.Round(
                skillsCoverage * (ResumeScoreCalculator.SkillsWeightPercent / 100.0) +
                experienceScore * (ResumeScoreCalculator.ExperienceWeightPercent / 100.0) +
                qualificationsScore * (ResumeScoreCalculator.QualificationsWeightPercent / 100.0) +
                domainScore * (ResumeScoreCalculator.DomainWeightPercent / 100.0));

            finalScore = Math.Clamp(finalScore, 0, 100);

            return new MatchScoringBreakdownVM
            {
                SkillsCoverageScore = skillsCoverage,
                ExperienceScore = experienceScore,
                QualificationsScore = qualificationsScore,
                DomainScore = domainScore,
                SkillsWeightPercent = ResumeScoreCalculator.SkillsWeightPercent,
                ExperienceWeightPercent = ResumeScoreCalculator.ExperienceWeightPercent,
                QualificationsWeightPercent = ResumeScoreCalculator.QualificationsWeightPercent,
                DomainWeightPercent = ResumeScoreCalculator.DomainWeightPercent,
                MatchedCount = matched,
                MissingCount = missing,
                FinalScore = finalScore,
                FormulaSummary =
                    $"({skillsCoverage}×{ResumeScoreCalculator.SkillsWeightPercent}% + {experienceScore}×{ResumeScoreCalculator.ExperienceWeightPercent}% + " +
                    $"{qualificationsScore}×{ResumeScoreCalculator.QualificationsWeightPercent}% + {domainScore}×{ResumeScoreCalculator.DomainWeightPercent}%) = {finalScore}% " +
                    $"(recalculated from {matched} matched / {missing} gaps)"
            };
        }

        private static List<string> SplitToList(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new List<string>();
            }

            return value
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();
        }

        private static string? GetResumeFileType(string? resumeUrl)
        {
            if (string.IsNullOrWhiteSpace(resumeUrl))
            {
                return null;
            }

            return Path.GetExtension(resumeUrl).ToLowerInvariant();
        }
    }
}
