using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Services.Orders;
using Msr.Services.Users;

namespace Msr.Web.ViewModel
{
    public class AddUserViewModel
    {
        public AddUserViewModel()
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

            Roles = userService.GetRoles().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            }).ToList();

            Companies = companyService.GetCompanyQueryable().ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }
    }
}