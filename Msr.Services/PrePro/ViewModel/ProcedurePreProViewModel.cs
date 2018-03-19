using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.PrePro;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.Users.Messages;

namespace Msr.Services.PrePro.ViewModel
{
    public class ProcedurePreProViewModel
    {
        public ProcedurePreProViewModel()
        {
            ReferenceProceduresList = new List<SelectListItem>();
            ReferenceFilesList = new List<SelectListItem>();
            ApplicationObjectsList = new List<SelectListItem>();
            ReferenceObjectsList = new List<SelectListItem>();
            ReferenceTheoriesList = new List<SelectListItem>();
            ReferenceVerbList = new List<SelectListItem>();
            Labors = new List<LaborObjectsView>();
        }

        [Required]
        public string Title { get; set; }

        public string Id { get; set; }

        public string ObjectId { get; set; }

        [DisplayName("Text :")]
        public string StepText { get; set; }

        public string ProcObjId { get; set; }

        [DisplayName("Comments :")]
        public string Comments { get; set; }

        [DisplayName("Base Start on Counter :")]
        public int? StartOnCounter { get; set; }

        [DisplayName("System Task :")]
        public string SystemTask { get; set; }

        [DisplayName("DESTINATION :")]
        public string Destination { get; set; }

        [DisplayName("Reference Procedure Type:")]
        public string ReferenceVerb { get; set; }

        [DisplayName("Application Objects:")]
        public string ApplicationObjects { get; set; }

        [DisplayName("Reference Object:")]
        public string ReferenceObject { get; set; }

        public List<string> ReferenceTheories { get; set; }

        [DisplayName("Reference Procedure:")]
        public List<string> ReferenceProcedures { get; set; }

        [DisplayName("precedingSteps :")]
        public string PrecedingSteps { get; set; }

        [DisplayName("Estimated Step Duration :")]
        public double? Duration { get; set; }


        [DisplayName("Duration Type:")]
        public string DurationType { get; set; }

        [DisplayName("Labor :")]
        public List<ProcedureObjectsLaborStepView> Labor { get; set; }

        [DisplayName("Labor :")]
        public List<LaborObjectsView> Labors { get; set; }

        [DisplayName("Number Of Questions to use :")]
        public string NumTestQuestion { get; set; }

        [DisplayName("Reference Files :")]
        public string ReferenceFiles { get; set; }

        public string NTLogin { get; set; }

        public string CreatingCo { get; set; }

        public List<SelectListItem> BaseStartOnCounterList { get; set; }

        public List<SelectListItem> StepDurationTypeList { get; set; }

        public List<SelectListItem> SystemTaskList { get; set; }

        public List<SelectListItem> ReferenceVerbList { get; set; }

        public List<SelectListItem> ReferenceObjectsList { get; set; }

        public List<SelectListItem> ApplicationObjectsList { get; set; }

        public IList<SelectListItem> ReferenceProceduresList { get; set; }

        public IList<SelectListItem> ReferenceFilesList { get; set; }


        public IList<SelectListItem> ReferenceTheoriesList { get; set; }

        public List<DocLink> DocLinks { get; set; }

        public void Setup(PreProServices preProServices, DocumentFilesService documentFilesService, LoggedUserIdResult currentUser)
        {
            BaseStartOnCounterList = LookupItems.YesNo();

            StepDurationTypeList = LookupItems.DurationType();

            SystemTaskList = LookupItems.System();

            ReferenceProceduresList = preProServices.GetSelectedRefProcedures(id: Id, ntlogin: currentUser.Id).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();


            ReferenceFilesList = preProServices.GetSelectedRefFiles(id: Id, ntlogin: currentUser.Id).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();


            ReferenceTheoriesList = preProServices.GetSelectedRefTheories(id: Id).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();

            ReferenceVerbList.Add(new SelectListItem { Value = "", Text = @"--Select--" });

            ReferenceVerbList.AddRange(preProServices.GetApprovedVerbsByCreatingCo(currentUser.Id).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList());

            ReferenceObjectsList.Add(new SelectListItem { Value = "", Text = @"--Select--" });

            ReferenceObjectsList.AddRange(preProServices.GetReferenceObjectsByCreatingCo(currentUser.Id).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList());

            Labors.AddRange(preProServices.GetProcedureStepLaborList(ProcObjId, currentUser.Id).Select(x => new LaborObjectsView
            {
                RoleName = x.RoleName,
                ObjDesc = x.Qty + " " + x.QtyType
            }).OrderBy(o => o.RoleName).ToList());

            DocLinks = documentFilesService.GetDocByObjectId(id: ObjectId);
        }

        public ProcedurePreProViewModel MapToDto(PrePropSearchView model)
        {
            return new ProcedurePreProViewModel
            {
                Id = model.Id,
                ObjectId = model.ObjectId,
                ProcObjId = model.ProcStepId,
                StartOnCounter = model.StartOnCounter,
                Duration = model.Duration,
                DurationType = model.DurationType,
                //Labor = model,
                StepText = model.StepText,
                SystemTask = model.SystemTask,
                Title = model.Title,
                ReferenceVerb = model.ReferenceVerb,
                ReferenceObject = model.ReferenceObject,
                Comments = model.Comments
            };
        }
    }
}
