namespace ZABCareersAPIs.Service.Interface
{
    public interface IArtificialIntelligence
    {
        Task<ChatCompletionResult> CompleteChatAsync(string systemPrompt, string userContent);
    }
}
