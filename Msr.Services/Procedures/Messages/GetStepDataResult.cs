using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProcedureVerbs;
using System.Linq;
using Msr.Services.Objects;

namespace Msr.Services.Procedures.Messages
{
    public class GetStepDataResult
    {
        public GetStepDataResult()
        {
            GetStepLaborsList = new List<SelectListItem>();
            GetStepLabors = new List<SelectListItem>();
            AddMonitorForProcedureViewModel = new AddMonitorForProcedureViewModel();
            GetMoniterViewModels = new List<GetMoniterViewModel>();


            ReferenceProcedureTypes = new List<SelectListItem>();
            ListReferenceObjects = new List<SelectListItem>();
            DurationTypeList = new List<SelectListItem>();
            SystemTasks = new List<SelectListItem>();
            ReferenceProcedureTypes = new List<SelectListItem>();
            SelectedReferenceProcedures = new List<string>();
            SelectedPrecedingSteps = new List<string>();

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
        public string REFERENCE_THEORIES { get; set; }
        public int? GOTO_STEP { get; set; }
        public string GOTO_STEP_ID { get; set; }
        public string Cycles { get; set; }
        public int? Cycle_On_Counter { get; set; }
        public int? Cycle_Count { get; set; }
        public string Cycle_Unit { get; set; }
        public double? Duration { get; set; }

        public double? Print_Order { get; set; }
        public string Pre_Step { get; set; }

        public string Title { get; set; }
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
        public IList<SelectListItem> GetStepLabors { get; set; }
        public IList<SelectListItem> GetStepLaborsList { get; set; }
        public List<GetMoniterViewModel> GetMoniterViewModels { get; set; }

        public List<SelectListItem> DurationTypeList { get; set; }
        public List<SelectListItem> SystemTasks { get; set; }
        public List<string> SelectedReferenceProcedureTypes { get; set; }
        public List<SelectListItem> ReferenceProcedureTypes { get; set; }
        public List<string> ReferenceObject { get; set; }
        public IList<SelectListItem> ListReferenceObjects { get; set; }
        public List<string> SelectedReferenceProcedures { get; set; }
        public List<string> SelectedPrecedingSteps { get; set; }

        public void SetUp(ProceduresService proceduresService, ProcedureVerbsService procedureVerbsService, ObjectsService objectsService)
        {

            ReferenceProcedureTypes = new List<SelectListItem>();

            ListReferenceObjects = objectsService.GetObjectsQueryable().Where(x => x.Id == REFERENCE_OBJECT).Select(x => new SelectListItem
            {
                Text = x.ObjectTable,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            DurationTypeList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = @"Seconds",
                    Value = "TIME_SYS_SECONDS"
                },
                new SelectListItem()
                {
                    Text = @"Minutes",
                    Value = "TIME_SYS_MINUTES"
                },
                new SelectListItem()
                {
                    Text = @"Hours",
                    Value = "TIME_SYS_HOURS"
                },
                new SelectListItem()
                {
                    Text = @"Days",
                    Value = "TIME_SYS_DAYS"
                },
                new SelectListItem()
                {
                    Text = @"Weeks",
                    Value = "TIME_SYS_WEEKS"
                }
            };
            SystemTasks = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = @"SYS_COMP_TEST",
                    Value = "SYS_COMP_TEST"
                },
                new SelectListItem
                {
                    Text = @"SYS_CONSUME",
                    Value = "SYS_CONSUME"
                },
                new SelectListItem
                {
                    Text = @"SYS_CREATE",
                    Value = "SYS_CREATE"
                },
                new SelectListItem
                {
                    Text = @"SYS_DNR",
                    Value = "SYS_DNR"
                },
                new SelectListItem
                {
                    Text = @"SYS_E_ACCESS",
                    Value = "SYS_E_ACCESS"
                },
                new SelectListItem
                {
                    Text = @"SYS_INSTALL",
                    Value = "SYS_INSTALL"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_AND_CONSUMED",
                    Value = "SYS_PROVIDE_AND_CONSUMED"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_AND_STAY",
                    Value = "SYS_PROVIDE_AND_STAY"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_TAKE_BACK",
                    Value = "SYS_PROVIDE_TAKE_BACK"
                },
                new SelectListItem
                {
                    Text = @"SYS_RECEIVE",
                    Value = "SYS_RECEIVE"
                },
                new SelectListItem
                {
                    Text = @"SYS_REMOVE",
                    Value = "SYS_REMOVE"
                },
                new SelectListItem
                {
                    Text = @"SYS_SEND",
                    Value = "SYS_SEND"
                },
                new SelectListItem
                {
                    Text = @"SYS_SERIALIZE",
                    Value = "SYS_SERIALIZE"
                },
                new SelectListItem
                {
                    Text = @"SYS_SHIPPING",
                    Value = "SYS_SHIPPING"
                }
            };

            ReferenceProcedureTypes = procedureVerbsService.ProceduresVerbsList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();
        }
    }
}
