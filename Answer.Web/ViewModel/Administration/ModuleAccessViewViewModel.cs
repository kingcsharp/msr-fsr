using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Administration.Messages;
using Msr.Services.Administration.ViewModels;
using Msr.Services.Companies;
using Msr.Services.Roles;

namespace Answer.Web.ViewModel.Administration
{
    public class ModuleAccessViewViewModel
    {
        public ModuleAccessViewViewModel()
        {
            Roles = new List<SelectListItem>();
            CompaniesToView = new List<ModuleAccessResult>();
        }

        public List<ModuleAccessResult> CompaniesToView { get; set; }

        public List<GlobalSettingsViewModel> GlobalSettings { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public void SetUp(RoleService companyService)
        {
            Roles.Add(new SelectListItem {Value = "", Text = "--Select Role--"});

            Roles.AddRange(companyService.GetActiveRoles().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name
            }).Distinct().OrderBy(o => o.Text));
        }
    }
}