using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Comman;
using Msr.Services.Administration.Messages;
using Msr.Services.Administration.ViewModels;
using Msr.Services.Companies;
using Msr.Services.Roles;
using Msr.Services.Roles.Messages;

namespace Answer.Web.ViewModel.Administration
{
    public class ModuleAccessViewViewModel
    {
        public ModuleAccessViewViewModel()
        {
            Roles = new List<SelectListItem>();
            CompaniesToView = new List<ModuleAccessResult>();
            RoleId = new List<SelectListItem>();
        }

        public List<ModuleAccessResult> CompaniesToView { get; set; }

        public List<XmlContentViewModel> GlobalSettings { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public List<SelectListItem> RoleId { get; set; }

        public List<SelectListItem> Id { get; set; }

        public List<RoleApprovedData> SelectedRoles { get; set; }

        public void SetUp(RoleService companyService)
        {

            Roles.AddRange(companyService.GetActiveRoles().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name
            }).Distinct().OrderBy(o => o.Text));
           
        }
        
    }
}