namespace ZABCareersAPIs.Service.Interface
{
    public interface IArtificialIntelligence
    {
        Task<string> OpenAITurboModelAsync(string prompt, dynamic Data);
    }
}
