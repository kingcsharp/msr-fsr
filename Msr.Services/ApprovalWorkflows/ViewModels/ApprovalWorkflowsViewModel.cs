using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Msr.Models.ApprovalWorkflows;
using Msr.Models.Files;
using Msr.Services.Documents;

namespace Msr.Services.ApprovalWorkflows.ViewModels
{
    public class ApprovalWorkflowsViewModel
    {
        public ApprovalWorkflowsViewModel()
        {
            ListWf_Stages = new List<SelectListItem>();
            ListActivities = new List<SelectListItem>();
            ListPictureFiles = new List<SelectListItem>();
            Wf_Stages = new List<string>();
            Activities = new List<string>();
            PictureFiles = new List<string>();
           
        }
        public string Id { get; set; }
        [DisplayName("WF Name :")]
        public string Name { get; set; }
        public string Object_Id { get; set; }
        public string Stamp_Id { get; set; }
        [DisplayName("Approval Stamp Name :")]
        public string Stamp_Name { get; set; }  
        public string Creating_Co { get; set; }
        public string Wf_Stage_Id { get; set; }
        public string Wf_Stage_Name { get; set; }
        public string StampPictureFile { get; set; }
        public int StagePosition { get; set; }
        [DisplayName("Member Stages :")]
        public List<string> Wf_Stages { get; set; }
        public string NTLogin { get; set; }
        [DisplayName("Applicable Activities :")]
        public List<string> Activities { get; set; }
        [DisplayName("Stamp Picture File :")]
        public List<string> PictureFiles { get; set; }

        public IList<SelectListItem> ListWf_Stages { get; set; }
        public IList<SelectListItem> ListActivities { get; set; }
        [DisplayName("Stamp Picture File :")]
        public IList<SelectListItem> ListPictureFiles { get; set; }        
        public void Setup(DocumentFilesService documentFilesService, ApprovalWorkflowsService approvalWorkflowsService )
        {
            ListWf_Stages = approvalWorkflowsService.GetWorkflowStages().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).OrderBy(o => o.Text).ToList();

            ListActivities = approvalWorkflowsService.GetActivitiesList().Select(x => new SelectListItem
            {
                Text = x.Activity,
                Value = x.Id
            }).OrderBy(o => o.Text).ToList();   
            ListPictureFiles = approvalWorkflowsService.GetFilesById(id: Stamp_Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = true
            }).OrderBy(o => o.Text).ToList();            
            Wf_Stages = approvalWorkflowsService.GetApprovalWorkflowStagesByWF_Id(id: Id).Select(x => x.WF_Stage_Id).ToList();          
            Activities = approvalWorkflowsService.GetApprovalWorkflowsActivitiesByWF_Id(id: Id).Select(x => x.Act_Id).ToList();

        }
        public ApprovalWorkflowsViewModel MapToDto(ApprovalWorkflowsView model)
        {
            return new ApprovalWorkflowsViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Object_Id = model.Object_Id,
                Stamp_Name=model.Stamp_Name,
                Stamp_Id=model.Stamp_Id
            };
        }
    }
}
