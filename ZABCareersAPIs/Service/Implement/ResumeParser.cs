using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using UglyToad.PdfPig;
using ZABCareersAPIs.Service.Interface;

namespace ZABCareersAPIs.Service.Implement
{
    public class ResumeParser : IResumeParser
    {
        public async Task<string> ExtractTextFromFileAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return extension switch
            {
                ".pdf" => await ExtractTextFromPdfAsync(file),
                ".doc" or ".docx" => await ExtractTextFromWordAsync(file),
                _ => throw new NotSupportedException($"File type {extension} is not supported")
            };
        }

        private static async Task<string> ExtractTextFromPdfAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var document = PdfDocument.Open(memoryStream);
            var text = new System.Text.StringBuilder();

            foreach (var page in document.GetPages())
            {
                text.AppendLine(page.Text);
            }

            return text.ToString();
        }

        private static async Task<string> ExtractTextFromWordAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var document = WordprocessingDocument.Open(memoryStream, false);
            var body = document.MainDocumentPart?.Document?.Body;

            if (body == null)
            {
                return string.Empty;
            }

            var text = new System.Text.StringBuilder();
            foreach (var paragraph in body.Elements<Paragraph>())
            {
                text.AppendLine(paragraph.InnerText);
            }

            return text.ToString();
        }
    }
}
