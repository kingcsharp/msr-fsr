
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
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            UserSummary = new UserSummary();
        }

        public UserSummary UserSummary { get; set; }

        public List<SelectListItem> TimeZones { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Status { get; set; }
        public List<SelectListItem> Companies { get; set; }

        public void Setup(UserService userService, CompanyService companyService)
        {
            TimeZones = new List<SelectListItem>
            {
                new SelectListItem {Text = "Mid-Atlantic", Value = "100"},
                new SelectListItem {Text = "International Date Line West", Value = "11436"},
                new SelectListItem {Text = "Midway Island, Samoa", Value = "11437"},
            };

            Roles = new List<SelectListItem>
            {
                new SelectListItem {Text = RolesConstants.ClientBuyer, Value = RolesConstants.ClientBuyer ,  Selected = UserSummary.RoleName == RolesConstants.ClientBuyer},
                new SelectListItem {Text = RolesConstants.ClientEngineer, Value = RolesConstants.ClientEngineer,Selected = UserSummary.RoleName == RolesConstants.ClientEngineer},
                new SelectListItem {Text = RolesConstants.ClientAdmin, Value = RolesConstants.ClientAdmin,Selected = UserSummary.RoleName == RolesConstants.ClientAdmin},
            };

            Companies = companyService.GetCompanyQueryable().ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
        }
    }
}