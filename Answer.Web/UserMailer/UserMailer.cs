using Answer.Web.ViewModel.Help;
using Mvc.Mailer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Answer.Web.UserMailer
{
    public class UserMailer : MailerBase, IUserMailer
    {
        public MvcMailMessage SendSupportRequest(SupportRequestModel model)
        {
            var mailMessage = new MvcMailMessage
            {
                Subject = $"{model.FirstName} {model.LastName} has requested Help"
            };

            var from = new MailAddress(model.Email);
            mailMessage.From = from;
            mailMessage.Sender = from;
            mailMessage.To.Add(ConfigurationManager.AppSettings["SupportEmail"]);
            ViewData.Model = model;
            PopulateBody(mailMessage, "SupportRequest");

            return mailMessage;
        }
    }
}