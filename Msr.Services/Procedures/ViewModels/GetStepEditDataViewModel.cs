using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Procedures.Messages;
using Msr.Services.ProcedureVerbs;

namespace Msr.Services.Procedures.ViewModels
{
    public class GetStepEditDataViewModel
    {
        public GetStepEditDataViewModel()
        {
            GetStepListOfOtherSteps = new List<SelectListItem>();
            ReferenceProcedures = new List<SelectListItem>();
            ReferenceProcedureTypes = new List<SelectListItem>();
            ListReferenceTheories = new List<SelectListItem>();
            ListReferenceObjects = new List<SelectListItem>();
            ListReferenceFiles = new List<SelectListItem>();
            GetStepEditData = new GetStepEditDataResult();
            BaseCounterList = new List<SelectListItem>();
            GetStepListOfOtherSteps = new List<SelectListItem>();
            DurationTypeList = new List<SelectListItem>();
            SystemTasks = new List<SelectListItem>();
            ReferenceProcedures = new List<SelectListItem>();
            ReferenceProcedureTypes = new List<SelectListItem>();
            RelativeOrAbsoluteList = new List<SelectListItem>();
            ListReferenceFiles = new List<SelectListItem>();
            ListReferenceObjects = new List<SelectListItem>();
            ListReferenceTheories = new List<SelectListItem>();
            SelectedReferenceProcedures = new List<string>();
            ReferenceFiles = new List<string>();
            SelectedPrecedingSteps = new List<string>();
        }

        public void SetUp(ProceduresService proceduresService, ProcedureVerbsService procedureVerbsService, string procedureObjectId)
        {
            GetStepListOfOtherSteps = new List<SelectListItem>();
            ReferenceProcedures = new List<SelectListItem>();
            ReferenceProcedureTypes = new List<SelectListItem>();
            ListReferenceTheories = new List<SelectListItem>();
            ListReferenceObjects = new List<SelectListItem>();
            ListReferenceFiles = new List<SelectListItem>();
            GetStepEditData = new GetStepEditDataResult();
            ReferenceFiles = new List<string>();

            BaseCounterList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = @"NO***",
                    Value = "0"
                },
                new SelectListItem()
                {
                    Text = @"YES***",
                    Value = "1"
                }
            };

            RelativeOrAbsoluteList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = @"Relative",
                    Value = "RELATIVE"
                },
                new SelectListItem()
                {
                    Text = @"Absolute",
                    Value = "ABSOLUTE"
                }
            };

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
            GetStepListOfOtherSteps = proceduresService.GetProcedureStepOtherStepsList(procedureObjectId, NtLogin).Select(x => new SelectListItem
            {
                Text = x.Step_Text,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            ReferenceProcedures = proceduresService.GetProceduresQueryable().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();

            ReferenceProcedureTypes = procedureVerbsService.ProceduresVerbsList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();
        }

        public string Id { get; set; }
        public string StepId { get; set; }
        public string ProcObjId { get; set; }
        public string NumTestQuestion { get; set; }
        public string NtLogin { get; set; }
        public GetStepEditDataResult GetStepEditData { get; set; }
        public List<SelectListItem> GetStepListOfOtherSteps { get; set; }
        public List<GetStepLaborResult> GetStepLabors { get; set; }
        public List<SelectListItem> BaseCounterList { get; set; }
        public List<SelectListItem> DurationTypeList { get; set; }
        public List<SelectListItem> SystemTasks { get; set; }
        public List<SelectListItem> ReferenceProcedures { get; set; }
        public List<SelectListItem> ReferenceProcedureTypes { get; set; }
        public List<SelectListItem> RelativeOrAbsoluteList { get; set; }
        public List<string> SelectedReferenceProcedures { get; set; }
        public List<string> SelectedReferenceProcedureTypes { get; set; }
        public List<string> SelectedPrecedingSteps { get; set; }
        public IList<SelectListItem> ListReferenceFiles { get; set; }
        public IList<SelectListItem> ListReferenceObjects { get; set; }
        public IList<SelectListItem> ListReferenceTheories { get; set; }
        public List<string> ReferenceTheory { get; set; }
        public List<string> ReferenceObject { get; set; }
        public List<string> ReferenceFiles { get; set; }
        public decimal? ReplacementCost { get; set; }
        public decimal? Utilization { get; set; }
        public decimal? UsefulLife { get; set; }
        public decimal? EquipExpensePerMinute { get; set; }
        public decimal? AnnualRM { get; set; }
        public decimal? RMPerMinute { get; set; }
    }
}
