using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Services.Administration.Messages;
using Msr.Services.Administration.ViewModels;
using Msr.Services.Locations;
using Msr.Services.Roles;

namespace Answer.Web.ViewModel.Administration
{
    public class AssignAccRecievableRoleViewModel
    {
        public AssignAccRecievableRoleViewModel()
        {
            Roles = new List<SelectListItem>();
            Locations = new List<SelectListItem>();
            Companies = new List<AssignAccRecievableRoleResult>();

        }

        public List<AssignAccRecievableRoleResult> Companies { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public List<SelectListItem> Locations { get; set; }

        public void SetUp(RoleService roleService, LocationService locationService)
        {
            Roles.Add(new SelectListItem {Value = "", Text = "--Select Role--"});

            Roles.AddRange(roleService.GetUserRolesQueryable().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.RoleName
            }).Distinct().OrderBy(o => o.Text));

            Locations.Add(new SelectListItem { Value = "", Text = "--Select Location--" });

            Locations.AddRange(locationService.GetLocationsQueryable().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name
            }).Distinct().OrderBy(o => o.Text));
        }
    }
}