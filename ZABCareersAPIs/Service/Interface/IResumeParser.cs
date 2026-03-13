namespace ZABCareersAPIs.Service.Interface
{
    public interface IResumeParser
    {
        Task<string> ExtractTextFromFileAsync(IFormFile file);
    }
}
