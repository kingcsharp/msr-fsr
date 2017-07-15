using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Services.Companies;
using Msr.Services.Orders;
using Msr.Services.TimeZones;
using Msr.Services.Users;

namespace Answer.Web.ViewModel
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
                new SelectListItem {Text = RolesConstants.ClientBuyer, Value = RolesConstants.ClientBuyer ,  Selected = UserSummary.RoleName == RolesConstants.ClientBuyer},
                new SelectListItem {Text = RolesConstants.ClientEngineer, Value = RolesConstants.ClientEngineer,Selected = UserSummary.RoleName == RolesConstants.ClientEngineer},
                new SelectListItem {Text = RolesConstants.ClientAdmin, Value = RolesConstants.ClientAdmin,Selected = UserSummary.RoleName == RolesConstants.ClientAdmin},
            };

            Companies = companyService.GetCompaniesQueryable().ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
        }
    }
}