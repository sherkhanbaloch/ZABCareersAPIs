using System.Net;
using System.Net.Mail;

namespace ZABCareersAPIs.Helpers
{
    public class EmailSystem
    {
        private readonly string smtpHost;
        private readonly int smtpPort;
        private readonly string smtpUser;
        private readonly string smtpPass;
        private readonly bool smtpSsl;

        public EmailSystem(string host, int port, string user, string pass, bool ssl = true)
        {
            smtpHost = host;
            smtpPort = port;
            smtpUser = user;
            smtpPass = pass;
            smtpSsl = ssl;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    smtp.EnableSsl = smtpSsl;

                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(smtpUser),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = isHtml
                    };
                    mail.To.Add(toEmail);

                    await smtp.SendMailAsync(mail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

    }
}
