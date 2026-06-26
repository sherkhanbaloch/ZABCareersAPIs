using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using ZABCareersAPIs.Configuration;
using ZABCareersAPIs.Service.Interface;

namespace ZABCareersAPIs.Service.Implement
{
    public class ArtificialIntelligence : IArtificialIntelligence
    {
        private readonly HttpClient _httpClient;
        private readonly MistralSettings _settings;
        private readonly ILogger<ArtificialIntelligence> _logger;

        public ArtificialIntelligence(
            HttpClient httpClient,
            IOptions<MistralSettings> settings,
            ILogger<ArtificialIntelligence> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<ChatCompletionResult> CompleteChatAsync(string systemPrompt, string userContent)
        {
            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
            {
                return new ChatCompletionResult
                {
                    Success = false,
                    ErrorMessage = "Mistral API key is not configured."
                };
            }

            var requestData = new
            {
                model = _settings.Model,
                temperature = _settings.Temperature,
                response_format = new { type = "json_object" },
                messages = new object[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userContent }
                }
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);

            using var request = new HttpRequestMessage(HttpMethod.Post, _settings.ApiUrl);
            request.Headers.Add("Authorization", $"Bearer {_settings.ApiKey}");
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mistral API request failed.");
                return new ChatCompletionResult
                {
                    Success = false,
                    ErrorMessage = "Failed to reach Mistral API."
                };
            }

            var rawResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Mistral API returned {StatusCode}: {Response}",
                    response.StatusCode,
                    rawResponse);

                var apiError = TryExtractApiError(rawResponse);
                return new ChatCompletionResult
                {
                    Success = false,
                    RawResponse = rawResponse,
                    ErrorMessage = apiError ?? $"Mistral API error ({(int)response.StatusCode})."
                };
            }

            try
            {
                var json = JObject.Parse(rawResponse);
                var content = json["choices"]?[0]?["message"]?["content"]?.ToString();

                if (string.IsNullOrWhiteSpace(content))
                {
                    _logger.LogWarning("Mistral API returned an empty message content.");
                    return new ChatCompletionResult
                    {
                        Success = false,
                        RawResponse = rawResponse,
                        ErrorMessage = "Mistral API returned an empty response."
                    };
                }

                return new ChatCompletionResult
                {
                    Success = true,
                    Content = content,
                    RawResponse = rawResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to parse Mistral API response.");
                return new ChatCompletionResult
                {
                    Success = false,
                    RawResponse = rawResponse,
                    ErrorMessage = "Failed to parse Mistral API response."
                };
            }
        }

        private static string? TryExtractApiError(string rawResponse)
        {
            try
            {
                var json = JObject.Parse(rawResponse);
                return json["message"]?.ToString() ?? json["error"]?["message"]?.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
