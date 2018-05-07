using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Procedures;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.ProcedureVerbs;
using Msr.Services.Roles;
using Msr.Services.Procedures.Messages;

namespace Msr.Services.Procedures.ViewModels
{
    public class SaveProcedureViewModel
    {
        public SaveProcedureViewModel()
        {
            VerbList = new List<SelectListItem>();
            SystemList = new List<SelectListItem>();
            SecurityLevelList = new List<SelectListItem>();
            SetpInApList = new List<SelectListItem>();
            DurationTypeList = new List<SelectListItem>();
            WipMsgList = new List<SelectListItem>();
            RolesList = new List<SelectListItem>();
            Roles = new List<string>();
            RolesList = new List<SelectListItem>();
            StepTemplateList = new List<SelectListItem>();
        }
        public string Id { get; set; }

        public string ObjectId { get; set; }

        [Required]
        [Display(Name = "Procedure Name :")]
        public string Name { get; set; }

        public string Company { get; set; }

        public string CreatingCompany { get; set; }

        [Display(Name = "Procedure Type :")]
        public string Verb { get; set; }

        [Display(Name = "Procedure System Task :")]
        public string SystemId { get; set; }

        [Display(Name = "Comments (notes,warning,etc) :")]
        public string Comments { get; set; }

        [Display(Name = "Security Clearance Level :")]
        public string SecurityLevel { get; set; }

        [Display(Name = "Roles To View :")]
        public List<string> Roles { get; set; }

        [Display(Name = "Show Steps In AP :")]
        public decimal? StepInAp { get; set; }

        [Display(Name = "Estimated Procedure Time :")]
        public double? Duration { get; set; }

        [Display(Name = "Estimated Duration Type :")]
        public string DurationType { get; set; }

        [Display(Name = "Send Msg On Approval :")]
        public decimal? WipMsg { get; set; }

        public string NTLogin { get; set; }

        public int? Threshold { get; set; }

        public string Root { get; set; }

        public bool IsActive { get; set; }

        public GetStepDataResult GetStepDataResults { get; set; }

        public string ReferenceFiles { get; set; }

        public List<SelectListItem> VerbList { get; set; }

        public List<SelectListItem> SystemList { get; set; }

        public List<SelectListItem> SecurityLevelList { get; set; }

        public List<SelectListItem> SetpInApList { get; set; }

        public List<SelectListItem> DurationTypeList { get; set; }

        public List<SelectListItem> WipMsgList { get; set; }

        public List<SelectListItem> RolesList { get; set; }

        public string AddStepTemplateId { get; set; }
        public List<SelectListItem> StepTemplateList { get; set; }

        public List<DocLink> DocLinks { get; set; }

        public void Setup(ProceduresService proceduresService, RoleService roleService, ProcedureVerbsService procedureTypesService, DocumentFilesService documentFilesService, string ntlogin, string co)
        {
            SystemList = proceduresService.GetSystemList(co).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.System_Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            SecurityLevelList = Commons.Lookups.LookupItems.SecurityLevel();
            SetpInApList = Commons.Lookups.LookupItems.YesNo();
            WipMsgList = Commons.Lookups.LookupItems.YesNo();

            DurationTypeList = Commons.Lookups.LookupItems.DurationType();

            RolesList = roleService.GetActiveRoles().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            VerbList = procedureTypesService.ProcVerbsList(ntlogin).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
            VerbList.Insert(0, new SelectListItem { Text = @"--Select--", Value = "" });

            DocLinks = documentFilesService.GetDocByObjectId(ObjectId);
            Roles = proceduresService.GetSelectedRoles(Id, ntlogin).Select(x => x.Role_Id).ToList();
        }

        public void MapToDto(ProcedureView model)
        {
            Id = model.Id;
            ObjectId = model.ObjectId;
            Name = model.Name;
            CreatingCompany = model.CreatingCo;
            Verb = model.Verb;
            SystemId = model.SystemId;
            Comments = model.Comments;
            SecurityLevel = model.SecurityLevel;
            StepInAp = model.StepInAp;
            Duration = model.Duration;
            DurationType = model.DurationType;
            WipMsg = model.WipMsg;
            Threshold = model.Threshold;
            IsActive = model.IsActive;
        }
    }
}
