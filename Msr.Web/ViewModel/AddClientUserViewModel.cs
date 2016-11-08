using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Services.Orders;
using Msr.Services.TimeZones;
using Msr.Services.Users;

namespace Msr.Web.ViewModel
{
    public class AddClientUserViewModel
    {
        public AddClientUserViewModel()
        {
            UserSummary = new UserSummary();
        }

        public UserSummary UserSummary { get; set; }

        public List<SelectListItem> TimeZones { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Status { get; set; }

        public void Setup(UserService userService, CompanyService companyService, TimeZoneService timeZoneService)
        {
            TimeZones = timeZoneService.GetTimeZoneQueryable().Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id,
                Selected = UserSummary.TimeZone == x.Id
            }).ToList();

            Roles = new List<SelectListItem>
            {
                new SelectListItem {Text = RolesConstants.ClientBuyer, Value = RolesConstants.ClientBuyer},
                new SelectListItem {Text = RolesConstants.ClientEngineer, Value = RolesConstants.ClientEngineer},
            };

          
        }
    }
}