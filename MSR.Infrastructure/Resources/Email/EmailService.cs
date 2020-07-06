using Microsoft.EntityFrameworkCore.Internal;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Models.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Email
{
    public class EmailService: IEmailService
    {
        private readonly EmailInformation _emailInformation;

        public EmailService(EmailInformation emailInformation)
        {
            _emailInformation = emailInformation;
        }

        public async Task<bool> SendEmailAsync(string fromEmail, string toEmail, string subject, string body, List<string> ccList,
           bool isHtml, List<Attachment> attachments = null)
        {
            try
            {
                using (var message = new MailMessage())
                {

                    // During testing send all emails to one account
                    if (!string.IsNullOrWhiteSpace(_emailInformation.SendEmailsTo))
                    {
                        toEmail = _emailInformation.SendEmailsTo;

                        if (ccList != null)
                        {
                            foreach (var ccEmail in ccList)
                            {
                                message.CC.Add(new MailAddress(toEmail));
                            }
                            ccList = null;
                        }

                    }


                    message.From = new MailAddress(fromEmail);
                    message.To.Add(new MailAddress(toEmail));
                    if (ccList != null)
                    {
                        foreach (var cc in ccList)
                        {
                            if (!string.IsNullOrWhiteSpace(cc))
                            {
                                message.CC.Add(new MailAddress(cc));
                            }
                        }
                    }
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = isHtml;
                    message.Priority = MailPriority.High;
                    message.BodyEncoding = Encoding.GetEncoding("utf-8");

                    foreach (var attachment in attachments ?? new List<Attachment>())
                    {
                        message.Attachments.Add(attachment);
                    }
                    
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Port = _emailInformation.Port;
                        smtp.Host = _emailInformation.Host.ToString(CultureInfo.InvariantCulture);
                        smtp.Credentials = new System.Net.NetworkCredential(_emailInformation.UserName, _emailInformation.Password);
                        smtp.EnableSsl = _emailInformation.EnableSsl;

                        await smtp.SendMailAsync(message);
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
