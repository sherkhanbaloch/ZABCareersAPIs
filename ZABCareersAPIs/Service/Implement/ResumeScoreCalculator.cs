using ZABCareersAPIs.Service.Interface;

namespace ZABCareersAPIs.Service.Implement
{
    public static class ResumeScoreCalculator
    {
        public const int SkillsWeightPercent = 50;
        public const int ExperienceWeightPercent = 25;
        public const int QualificationsWeightPercent = 15;
        public const int DomainWeightPercent = 10;

        public static void ApplyWeightedScore(ResumeMatchResult result)
        {
            result.MatchedSkills = NormalizeList(result.MatchedSkills, 15);
            result.MissingSkills = NormalizeList(result.MissingSkills, 15);

            var skillsFromLists = CalculateSkillsCoverageScore(
                result.MatchedSkills.Count,
                result.MissingSkills.Count);

            var experienceScore = ClampSubScore(result.SubScores?.Experience, result.MatchPercentage);
            var qualificationsScore = ClampSubScore(result.SubScores?.Qualifications, skillsFromLists);
            var domainScore = ClampSubScore(result.SubScores?.Domain, skillsFromLists);

            var weightedTotal = (int)Math.Round(
                skillsFromLists * (SkillsWeightPercent / 100.0) +
                experienceScore * (ExperienceWeightPercent / 100.0) +
                qualificationsScore * (QualificationsWeightPercent / 100.0) +
                domainScore * (DomainWeightPercent / 100.0));

            var finalScore = Math.Clamp(weightedTotal, 0, 100);

            result.MatchPercentage = finalScore;
            result.Scoring = new MatchScoringBreakdown
            {
                SkillsCoverageScore = skillsFromLists,
                ExperienceScore = experienceScore,
                QualificationsScore = qualificationsScore,
                DomainScore = domainScore,
                SkillsWeightPercent = SkillsWeightPercent,
                ExperienceWeightPercent = ExperienceWeightPercent,
                QualificationsWeightPercent = QualificationsWeightPercent,
                DomainWeightPercent = DomainWeightPercent,
                MatchedCount = result.MatchedSkills.Count,
                MissingCount = result.MissingSkills.Count,
                FinalScore = finalScore,
                FormulaSummary =
                    $"({skillsFromLists}×{SkillsWeightPercent}% + {experienceScore}×{ExperienceWeightPercent}% + " +
                    $"{qualificationsScore}×{QualificationsWeightPercent}% + {domainScore}×{DomainWeightPercent}%) = {finalScore}%"
            };
        }

        public static int CalculateSkillsCoverageScore(int matchedCount, int missingCount)
        {
            var total = matchedCount + missingCount;
            if (total == 0)
            {
                return 0;
            }

            return (int)Math.Round(100.0 * matchedCount / total);
        }

        private static int ClampSubScore(int? value, int fallback)
        {
            if (!value.HasValue)
            {
                return Math.Clamp(fallback, 0, 100);
            }

            return Math.Clamp(value.Value, 0, 100);
        }

        private static List<string> NormalizeList(List<string>? items, int maxItems)
        {
            if (items == null || items.Count == 0)
            {
                return new List<string>();
            }

            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var list = new List<string>();

            foreach (var item in items)
            {
                var trimmed = item?.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || !seen.Add(trimmed))
                {
                    continue;
                }

                list.Add(trimmed);
                if (list.Count >= maxItems)
                {
                    break;
                }
            }

            return list;
        }
    }
}
