using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Administration.Messages;
using Msr.Services.Locations;
using Msr.Services.Roles;


namespace Answer.Web.ViewModel.Administration
{
    public class AssignRoleToJobViewModel
    {
        public AssignRoleToJobViewModel()
        {
            Jobs = new List<SelectListItem>();
            Companies = new List<RoleForJobItem>();
            Roles = new List<SelectListItem>();
        }

        public IList<SelectListItem> Jobs { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public string SelectedJobId { get; set; }

        public IList<RoleForJobItem> Companies { get; set; }

        public IList<UpdateAssignRoleToJobItem> CompaniesWithRoles { get; set; }

        public void SetUp(RoleService roleService)
        {

            if (!string.IsNullOrWhiteSpace(SelectedJobId))
            {
                Roles.Add(new SelectListItem{Value = "",Text = "--Select Role--"});

                Roles.AddRange(roleService.GetActiveRoles().Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).OrderBy(o => o.Text));
            }
        }
    }
}