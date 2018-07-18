using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProcedureVerbs;
using System.Linq;
using System.Web.Script.Serialization;
using Msr.Commons.Files;
using Msr.Commons.Lookups;
using Msr.Models.PrePro;
using Msr.Services.Documents;
using Msr.Services.Objects;
using Msr.Services.Roles;
using Msr.Services.TheoryParagraph;

namespace Msr.Services.Procedures.Messages
{
    public class GetStepDataResult
    {
        public GetStepDataResult()
        {
            StepLaborsList = new List<SelectListItem>();
            StepLabors = new List<SelectListItem>();
            AddMonitorForProcedureViewModel = new AddMonitorForProcedureViewModel();
            GetMoniterViewModels = new List<GetMoniterViewModel>();
            ListReferenceTheories = new List<SelectListItem>();
            ReferenceProcedureTypes = new List<SelectListItem>();
            ListReferenceObjects = new List<SelectListItem>();
            DurationTypeList = new List<SelectListItem>();
            SystemTasks = new List<SelectListItem>();
            ReferenceProcedureTypes = new List<SelectListItem>();
            SelectedReferenceProcedures = new List<string>();
            SelectedPrecedingSteps = new List<string>();
            ReferenceTheories = new List<string>();
            Role = new List<string>();
            RolesList = new List<SelectListItem>();
        }

        private string DurationType;
        public string Procedure_Id { get; set; }
        public int? Start_On_Counter { get; set; }
        public string Id { get; set; }
        [AllowHtml]
        public string Step_Text { get; set; }
        public string System_Task { get; set; }
        [AllowHtml]
        public string Comments { get; set; }
        public decimal? Counter_Value { get; set; }
        public string Counter_Unit { get; set; }
        public string FROM_START_OR_STOP { get; set; }
        public string REL_OR_ABS { get; set; }
        public string Destination { get; set; }
        public string Specific_Location { get; set; }
        public string REFERENCE_VERB { get; set; }
        public string REFERENCE_OBJECT { get; set; }
        public int? GOTO_STEP { get; set; }
        public string GOTO_STEP_ID { get; set; }
        public string Cycles { get; set; }
        public int? Cycle_On_Counter { get; set; }
        public int? Cycle_Count { get; set; }
        public string Cycle_Unit { get; set; }
        public double? Duration { get; set; }
        public double? EquipmentTime { get; set; }
        public double? Print_Order { get; set; }
        public string Pre_Step { get; set; }
        public string Title { get; set; }
        public string PrevStepName { get; set; }
        public string StepSystemTask { get; set; }
        public string Duration_Type
        {
            get { return DurationType; }
            set
            {
                SetDurationType(value);
            }
        }

        private void SetDurationType(string value)
        {
            switch (value)
            {
                case "TIME_SYS_HOURS":
                    {
                        DurationType = "Hours";
                        break;
                    }
                default:
                    {
                        DurationType = String.Empty;
                        break;
                    }
            }
        }

        public string ProcObjId { get; set; }
        public int? Index { get; set; }
        public AddMonitorForProcedureViewModel AddMonitorForProcedureViewModel { get; set; }
        public List<GetStepDataResult> GetStepDataResults { get; set; }
        public IList<SelectListItem> StepLabors { get; set; }
        public IList<SelectListItem> StepLaborsList { get; set; }
        public List<GetMoniterViewModel> GetMoniterViewModels { get; set; }

        public List<SelectListItem> DurationTypeList { get; set; }
        public List<SelectListItem> SystemTasks { get; set; }
        public List<string> SelectedReferenceProcedureTypes { get; set; }
        public List<SelectListItem> ReferenceProcedureTypes { get; set; }
        public List<string> ReferenceObject { get; set; }
        public IList<SelectListItem> ListReferenceObjects { get; set; }
        public List<string> ReferenceTheories { get; set; }
        public IList<SelectListItem> ListReferenceTheories { get; set; }
        public List<string> SelectedReferenceProcedures { get; set; }
        public List<string> SelectedPrecedingSteps { get; set; }
        public List<PreProDockLink> DocLinks { get; set; }
        public string Preview { get; set; }
        public string PreviewConfig { get; set; }
        public string Roles { get; set; }
        [Required]
        public List<string> Role { get; set; }
        public List<SelectListItem> RolesList { get; set; }
        public decimal? ReplacementCost { get; set; }
        public decimal? Utilization { get; set; }
        public int? UsefulLife { get; set; }

        public void SetUp(ProcedureVerbsService procedureVerbsService, ObjectsService objectsService, DocumentFilesService documentFilesService, TheoryParagraphService theoryParagraphService, RoleService roleService)
        {

            ReferenceProcedureTypes = new List<SelectListItem>();

            ListReferenceObjects = objectsService.GetObjectsQueryable().Where(x => x.Id == REFERENCE_OBJECT).Select(x => new SelectListItem
            {
                Text = x.ObjectTable,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            ListReferenceTheories = theoryParagraphService.GetStepTheories(Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            DurationTypeList = LookupItems.DurationType();
            SystemTasks = LookupItems.System();

            ReferenceProcedureTypes = procedureVerbsService.ProceduresVerbsList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();

            DocLinks = documentFilesService.GetPreProRefFileDocLinks(Id, "");



            var preview = string.Join(",", DocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'", x.Server_Path)));
            Preview = preview;

            var jsonSerialiser = new JavaScriptSerializer();

            var previewConfig = jsonSerialiser.Serialize(DocLinks.Select(x => new
            {
                caption = x.Name,
                type = MimeTypes.GetContentType(x.Contenttype),
                size = 6666,
                url = $"/Doc/DeletePreProImageById?id={Id}&fileId={ x.Value}",
                downloadUrl = x.Server_Path,
                key = x.Value
            }));

            PreviewConfig = previewConfig;

            RolesList = roleService.GetActiveRoles().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
        }
    }
}
