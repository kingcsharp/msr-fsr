using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Administration.Messages;
using Msr.Services.Companies;

namespace Answer.Web.ViewModel.Administration
{
    public class AssignCompaniesToViewViewModel
    {
        public AssignCompaniesToViewViewModel()
        {
            Companies = new List<SelectListItem>();
            CompaniesToView = new List<AssignCompaniesToViewResult>();
        }

        public List<AssignCompaniesToViewResult> CompaniesToView { get; set; }

        public List<SelectListItem> Companies { get; set; }

        public void SetUp(CompanyService companyService)
        {
            Companies.Add(new SelectListItem {Value = "", Text = "--Select Role--"});

            Companies.AddRange(companyService.GetLocationsQueryable().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name
            }).Distinct().OrderBy(o => o.Text));
        }
    }
}