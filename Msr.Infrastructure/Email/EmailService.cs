using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;

namespace Msr.Infrastructure.Email
{
    public class EmailService
    {
        public static bool SendEmail(string fromEmail, string toEmail, string subject, string body, List<string> ccList,
           bool isHtml)
        {
            try
            {
                var message = new MailMessage();

                // During testing send all emails to one account
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SendEmailsTo"]))
                {
                    toEmail = ConfigurationManager.AppSettings["SendEmailsTo"];

                    if (ccList != null)
                    {
                        foreach (var ccEmail in ccList)
                        {
                            message.CC.Add(new MailAddress(ConfigurationManager.AppSettings["SendEmailsTo"]));
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
                        if(!string.IsNullOrWhiteSpace(cc))
                        {
                            message.CC.Add(new MailAddress(cc));
                        }
                    }
                }
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                message.Priority = MailPriority.High;
                message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                var smtp = new SmtpClient();
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Host = ConfigurationManager.AppSettings["Host"].ToString(CultureInfo.InvariantCulture);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);

                smtp.Send(message);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

    }

}
