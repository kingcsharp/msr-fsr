using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Msr.Services.ProductionPlanning.ViewModels
{
    public class RequirementStatusViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Requirement Status")]
        public string Status { get; set; }

        public List<SelectListItem> StatusList { get; set; }

        public void Setup()
        {
            StatusList = new List<SelectListItem>
            {               
                 new SelectListItem
                {
                    Text = "Please Select Status",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = "Received",
                    Value = "RECEIVED"
                },
                new SelectListItem
                {
                    Text = "In Progress",
                    Value = "IN_PROGRESS"
                },
                new SelectListItem
                {
                    Text = "In Appproval",
                    Value = "IN_APPROVAL"
                },
                new SelectListItem
                {
                    Text = "Approved",
                    Value = "APPROVED"
                }
            };
        }
    }
}
