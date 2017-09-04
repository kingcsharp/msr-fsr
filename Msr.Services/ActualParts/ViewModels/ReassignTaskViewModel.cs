using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Orders;
using Msr.Services.Roles;

namespace Msr.Services.ActualParts.ViewModels
{
    public class ReassignTaskViewModel
    {
        public ReassignTaskViewModel()
        {
            PeopleList = new List<SelectListItem>();
            GroupList = new List<SelectListItem>();
        }

        public string Id { get; set; }

        [DisplayName("Person To Re-Assign To :")]
        public string PersonToReAssign { get; set; }

        [DisplayName("Group to ReAssign To :")]
        public string GroupToReassign { get; set; }

        [DisplayName("ReAssignment Comments :")]
        public string ReAssignComments { get; set; }

        public string NTLogin { get; set; }


        public List<SelectListItem> PeopleList { get; set; }

        public List<SelectListItem> GroupList { get; set; }

        public void SetUp(PeopleService peopleService, RoleService roleService)
        {
            PeopleList.Add(new SelectListItem { Value = "", Text = "--Select People--" });

            PeopleList.AddRange(peopleService.GetPeople().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.FirstName + " " + x.LastName
            }).Distinct().OrderBy(o => o.Text));

            GroupList.Add(new SelectListItem { Value = "", Text = "--Select Group--" });

            GroupList.AddRange(roleService.GetUserRolesQueryable().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.RoleName
            }).Distinct().OrderBy(o => o.Text));
        }
    }
}