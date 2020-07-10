using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string fromEmail, string toEmail, string subject, string body, List<string> ccList,
           bool isHtml, List<Attachment> attachments = null);
    }
}
