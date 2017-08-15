using Msr.Services.Users.Messages;

namespace Msr.Services
{
    public class BaseNotificationRequest
    {
        public LoggedUserIdResult LoggedUserIdResult { get; set; }

        public string ReturnUrl { get; set; }
    }
}
