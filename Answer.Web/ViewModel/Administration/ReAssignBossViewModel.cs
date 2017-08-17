using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Administration;

namespace Answer.Web.ViewModel.Administration
{
    public class ReAssignBossViewModel
    {
        public ReAssignBossViewModel()
        {
            BossList = new List<SelectListItem>();
        }
        [DisplayName("Mave all workers that report to this Boss :")]
        public string Id { get; set; }

        public string FullName { get; set; }
        
        public string LastName { get; set; }

        public string Name { get; set; }

        public string NTLogin { get; set; }

        [DisplayName("Now report to this Boss :")]
        public string ToBossId { get; set; }

        public List<SelectListItem> BossList { get; set; }



        public void SetUp(AdministrationService administrationService)
        {
            BossList.Add(new SelectListItem { Value = "", Text = "--Select Boss--" });

            BossList.AddRange(administrationService.GetBossListByLoginId(NTLogin).Select(x => new SelectListItem
            {
                Value = x.Value,
                Text = x.Text
            }).Distinct().OrderBy(o => o.Text));
        }
    }
}
