using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Workflows.Messages;

namespace Msr.Services.Workflows.ViewModels
{
    public class SubmitWorkflowViewModel : BaseNotificationRequest
    {
        public SubmitWorkflowViewModel()
        {
            WorkflowsList = new List<SelectListItem>();
            DeleteAllRevisionsList = new List<SelectListItem>();
        }

        public string ObjectId { get; set; }

        public string LoginId { get; set; }

        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Display(Name = "Revision Comment")]
        public string Comment { get; set; }

        [Display(Name = "Approval Workflow")]
        [Required]
        public string ApprovalWorflowId { get; set; }

        public IList<SelectListItem> WorkflowsList { get; set; }

        public IList<SelectListItem> DeleteAllRevisionsList { get; set; }

        public string CompletionStart { get; set; }

        [Display(Name = "Delete All Revs")]
        public string AllRevs { get; set; }

        public void SetUp(List<ShowApplicableWorkflowsResult> workflows)
        {
            WorkflowsList = workflows.Select(x => new SelectListItem {Text = x.WF_Name, Value = x.Wf_Id}).ToList();
            DeleteAllRevisionsList = Commons.Lookups.LookupItems.YesNo();
        }
    }
}