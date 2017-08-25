using Msr.Models.ApprovalStages;
using Msr.Services.ApprovalGroups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.ApprovalStages.VIewModels
{
    public class EditApprovalStagesViewModel
    {
        public EditApprovalStagesViewModel()
        {
            Groups = new List<string>();
        }
        public string Id { get; set; }

        [Required]
        [Display(Name = "WF Stage Name :")]
        public string Name { get; set; }

        [Display(Name = "Stage Member Group :")]
        public List<string> Groups { get; set; }

        public List<SelectListItem> ListGroups { get; set; }

        public string NTLogin { get; set; }

        public void Setup(ApprovalGroupsService approvalGroupsService, ApprovalStagesService approvalStagesService,string ntlogin)
        {
            ListGroups = approvalGroupsService.GetApprovalGroupsQueryable().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).OrderBy(o => o.Text).ToList();

            Groups = approvalStagesService.GetStageMemberGroups(id: Id,ntlog:ntlogin).Select(x => x.Id).ToList();
        }

        public EditApprovalStagesViewModel MapToDto(ApprovalStagesView model)
        {
            return new EditApprovalStagesViewModel
            {
                Id = model.Id,
                Name = model.StageName
            };
        }
    }
}
