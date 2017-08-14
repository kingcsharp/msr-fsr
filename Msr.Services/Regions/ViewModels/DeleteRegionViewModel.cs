using System.Collections.Generic;
using Msr.Models.Regions;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Msr.Services.Regions.ViewModels
{
    public class DeleteRegionViewModel
    {
        public DeleteRegionViewModel()
        {
            WorkflowsList = new List<SelectListItem>();
        }

        public string ObjectId { get; set; }

        [Required]
        [Display(Name = "Region Name")]
        public string Name { get; set; }

        [Display(Name = "Delete Comment")]
        public string Comment { get; set; }

        [Display(Name = "Approval Workflow")]
        [Required]
        public string ApprovalWorflow { get; set; }

        [Display(Name = "Delete All Revisions?")]
        [Required]
        public string DeleteAllRevisions { get; set; }

        public string LoginId { get; set; }

        public IList<SelectListItem> WorkflowsList { get; set; }
        public IList<SelectListItem> DeleteAllRevisionsList { get; set; }

        public void MapFromDto(RegionsView model)
        {
            ObjectId = model.ObjectId;
            Name = model.Name;
        }

        public void SetUp()
        {
            WorkflowsList = GetWorkflowsList();
            DeleteAllRevisionsList = GetDeleteAllRevisions();
        }

        private List<SelectListItem> GetWorkflowsList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "--Select--", Value = ""},
                new SelectListItem {Text = "Admin Workflow", Value = "37"}
            };
        }

        private List<SelectListItem> GetDeleteAllRevisions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "--Select--", Value = ""},
                new SelectListItem {Text = "Yes", Value = "1"},
                new SelectListItem {Text = "No", Value = "0"}
            };
        }
    }
    
}
