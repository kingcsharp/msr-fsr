using Answer.Web.ViewModel.Help;
using Mvc.Mailer;

namespace Answer.Web.UserMailer
{
    internal interface IUserMailer
    {
        MvcMailMessage SendSupportRequest(SupportRequestModel model);
    }
}