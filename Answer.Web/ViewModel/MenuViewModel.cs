using System.Collections.Generic;
using Msr.Models.Menus;
using Msr.Services.Users.Messages;

namespace Answer.Web.ViewModel
{
    public class MenuViewModel
    {
        public LoggedUserIdResult LoggedUserIdResult { get; set; }
        public List<MenuView> UserModules { get; set; }
    }
}