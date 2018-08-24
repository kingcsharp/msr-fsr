using System.Collections.Generic;
using Msr.Services.Users.Messages;

namespace Answer.Web.ViewModel
{
    public class BaseViewModel
    {
        public LoggedUserIdResult LoggedUserIdResult { get; set; }
        public List<string> UserModules { get; set; }
    }
}