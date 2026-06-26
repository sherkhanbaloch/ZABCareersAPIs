namespace ZABCareersAPIs.Service.Interface
{
    public class ChatCompletionResult
    {
        public bool Success { get; set; }
        public string? Content { get; set; }
        public string? RawResponse { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
