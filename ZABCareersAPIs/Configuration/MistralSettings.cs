namespace ZABCareersAPIs.Configuration
{
    public class MistralSettings
    {
        public const string SectionName = "Mistral";

        public string ApiKey { get; set; } = string.Empty;
        public string ApiUrl { get; set; } = "https://api.mistral.ai/v1/chat/completions";
        public string Model { get; set; } = "mistral-large-latest";
        public double Temperature { get; set; } = 0.1;
        public int MaxResumeChars { get; set; } = 12000;
        public int MinResumeChars { get; set; } = 200;
    }
}
