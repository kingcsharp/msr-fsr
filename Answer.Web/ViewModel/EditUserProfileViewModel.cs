
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Services.Orders;
using Msr.Services.Users;

namespace Msr.Web.ViewModel
{
    public class UserProfileViewModel
    {
        public UserView UserSummary { get; set; }
    }
}