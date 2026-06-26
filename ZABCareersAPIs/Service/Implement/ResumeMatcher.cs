using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ZABCareersAPIs.Configuration;
using ZABCareersAPIs.Service.Interface;

namespace ZABCareersAPIs.Service.Implement
{
    public class ResumeMatcher : IResumeMatcher
    {
        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly IArtificialIntelligence _aiService;
        private readonly MistralSettings _settings;
        private readonly ILogger<ResumeMatcher> _logger;

        public ResumeMatcher(
            IArtificialIntelligence aiService,
            IOptions<MistralSettings> settings,
            ILogger<ResumeMatcher> logger)
        {
            _aiService = aiService;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<ResumeMatchResult> MatchResumeAsync(string resumeText, string jobDescription)
        {
            var normalizedResume = NormalizeText(resumeText);

            if (normalizedResume.Length < _settings.MinResumeChars)
            {
                _logger.LogWarning(
                    "Resume text too short ({Length} chars, minimum {Min}).",
                    normalizedResume.Length,
                    _settings.MinResumeChars);

                return CreateFailedResult(
                    $"Resume text could not be extracted reliably ({normalizedResume.Length} characters). " +
                    "Please upload a text-based PDF or Word document.");
            }

            if (normalizedResume.Length > _settings.MaxResumeChars)
            {
                normalizedResume = normalizedResume[.._settings.MaxResumeChars] +
                    "\n\n[Resume truncated due to length limits.]";
            }

            var userContent = BuildUserContent(normalizedResume, jobDescription);
            var systemPrompt = CreateResumeMatchingPrompt();

            var completion = await _aiService.CompleteChatAsync(systemPrompt, userContent);

            if (!completion.Success)
            {
                _logger.LogError("Resume matching AI call failed: {Error}", completion.ErrorMessage);
                return CreateFailedResult(completion.ErrorMessage ?? "AI analysis failed.");
            }

            var parsed = ParseAIResponse(completion.Content!);
            if (!parsed.Success)
            {
                _logger.LogError("Failed to parse AI JSON: {Error}", parsed.ErrorMessage);
                return CreateFailedResult(parsed.ErrorMessage ?? "Failed to parse AI analysis result.");
            }

            ResumeScoreCalculator.ApplyWeightedScore(parsed);

            _logger.LogInformation(
                "Resume score calculated: {Final}% (skills coverage {Skills}%, matched {M}, gaps {G})",
                parsed.MatchPercentage,
                parsed.Scoring?.SkillsCoverageScore,
                parsed.MatchedSkills.Count,
                parsed.MissingSkills.Count);

            return parsed;
        }

        private static string BuildUserContent(string resumeText, string jobDescription)
        {
            return $"""
                JOB REQUIREMENTS:
                {jobDescription}

                CANDIDATE RESUME:
                {resumeText}

                Compare only skills and experience relevant to this job. Ignore unrelated resume content.
                """;
        }

        private static string NormalizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            return string.Join(
                ' ',
                text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
        }

        private string CreateResumeMatchingPrompt()
        {
            return """
                You are a senior HR recruiter and ATS evaluator. Analyze how well the candidate resume matches the job requirements.

                STRICT RULES:
                1. Only count skills, experience, and qualifications directly relevant to the job.
                2. Ignore unrelated technologies and generic soft skills.
                3. Do not assume skills unless explicitly stated in the resume.
                4. Do not list every keyword from the job post — only CRITICAL requirements.

                SKILL LIST LIMITS (important):
                - matchedSkills: max 15 items — only skills clearly evidenced in the resume.
                - missingSkills: max 15 items — only mandatory/critical gaps (not nice-to-have fluff).
                - Do not duplicate similar items (e.g. "Python" and "Python programming" count once).

                SUB-SCORES (each 0-100, evidence-based):
                - subScores.skills: coverage of critical technical requirements (not the final match %).
                - subScores.experience: years and relevance vs required seniority.
                - subScores.qualifications: degrees, certifications required for the role.
                - subScores.domain: project/industry alignment with the role.

                The server computes matchPercentage using this weighted formula — you must still return consistent subScores:
                - Skills coverage from lists: 50% weight (matched ÷ (matched + missing) × 100)
                - Experience: 25% weight
                - Qualifications: 15% weight
                - Domain/projects: 10% weight

                Example: 5 matched + 10 critical gaps → skills coverage ≈ 33%. If experience=60, qualifications=40, domain=50:
                final ≈ round(33×0.5 + 60×0.25 + 40×0.15 + 50×0.1) ≈ 42%.

                Return ONLY valid JSON (no markdown):
                {
                  "matchPercentage": 0,
                  "experience": "",
                  "matchedSkills": [],
                  "missingSkills": [],
                  "suggestions": [],
                  "subScores": {
                    "skills": 0,
                    "experience": 0,
                    "qualifications": 0,
                    "domain": 0
                  }
                }

                Field rules:
                - matchPercentage: your estimate before weighting (server recalculates from formula)
                - experience: short comparison of candidate vs required experience
                - matchedSkills / missingSkills: follow limits above
                - suggestions: max 8 actionable items for this role
                """;
        }

        private ResumeMatchResult ParseAIResponse(string responseText)
        {
            try
            {
                var jsonStart = responseText.IndexOf('{');
                var jsonEnd = responseText.LastIndexOf('}');

                if (jsonStart < 0 || jsonEnd <= jsonStart)
                {
                    return CreateFailedResult("AI response did not contain JSON.");
                }

                var jsonString = responseText.Substring(jsonStart, jsonEnd - jsonStart + 1);
                var result = JsonConvert.DeserializeObject<ResumeMatchResult>(jsonString, JsonSettings);

                if (result == null)
                {
                    return CreateFailedResult("AI response JSON was empty.");
                }

                result.Success = true;
                result.MatchedSkills ??= new List<string>();
                result.MissingSkills ??= new List<string>();
                result.Suggestions ??= new List<string>();

                if (string.IsNullOrWhiteSpace(result.Experience))
                {
                    result.Experience = "Not specified";
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while parsing AI response JSON.");
                return CreateFailedResult("Invalid JSON in AI response.");
            }
        }

        private static ResumeMatchResult CreateFailedResult(string errorMessage)
        {
            return new ResumeMatchResult
            {
                Success = false,
                ErrorMessage = errorMessage,
                MatchPercentage = -1,
                Experience = errorMessage,
                MatchedSkills = new List<string>(),
                MissingSkills = new List<string>(),
                Suggestions = new List<string>()
            };
        }
    }
}
