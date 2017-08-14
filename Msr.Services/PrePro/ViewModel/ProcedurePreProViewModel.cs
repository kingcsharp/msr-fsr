using Msr.Services.Documents;
using System;
using Msr.Services.Documents.Procedures;
using Msr.Services.Documents.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Msr.Models;
using Msr.Models.PrePro;
using Msr.Services.ProcedureVerbs;
using Msr.Services.TheoryParagraph;

namespace Msr.Services.PrePro.ViewModel
{
    public class ProcedurePreProViewModel
    {
        public ProcedurePreProViewModel()
        {
            ProcFilesList = new List<SelectListItem>();
            PictureRefFilesList = new List<SelectListItem>();
            //1Reference_Verb = new List<SelectListItem>();
            ReferenceObjectsList = new List<SelectListItem>();
            ListReferenceTheories = new List<SelectListItem>();
            PictureFiles = new List<string>();
        }

        public string Id { get; set; }

        [DisplayName("Text :")]
        public string StepText { get; set; }

        public string ProcObjId { get; set; }

        [DisplayName("Comments :")]
        public string Comments { get; set; }

        [DisplayName("Base Start on Counter :")]
        public string StartOnCounter { get; set; }

        public string CounterValue { get; set; }

        public string CounterUnit { get; set; }

        public string FromStartOrStop { get; set; }

        public string RelOrAbs { get; set; }

        [DisplayName("System Task :")]
        public string SystemTask { get; set; }

        [DisplayName("DESTINATION :")]
        public string Destination { get; set; }

        public string SpecificLocation { get; set; }

        public List<string> ReferenceVerb { get; set; }

        public List<string> ReferenceObject { get; set; }

        public List<string> ReferenceTheories { get; set; }

        public string GotoStep { get; set; }

        public string GotoStepId { get; set; }

        public string Cycles { get; set; }

        public string CycleOnCounter { get; set; }

        public string CycleCount { get; set; }

        public string CycleUnit { get; set; }

        [DisplayName("ReferenceProcs :")]
        public List<string> ReferenceProcs { get; set; }

        [DisplayName("precedingSteps :")]
        public string PrecedingSteps { get; set; }

        [DisplayName("Estimated Step Duration :")]
        public float? Duration { get; set; }

        [DisplayName("Step Duration Type :")]
        public string DurationType { get; set; }

        [DisplayName("Step Duration Type :")]
        public string Labor { get; set; }

        [DisplayName("Number Of Questions to use :")]
        public string NumTestQuestion { get; set; }

        [DisplayName("Picture Files :")]
        public List<string> PictureFiles { get; set; }

        public string NTLogin { get; set; }

        public List<SelectListItem> BaseStartOnCounterList { get; set; }

        public List<SelectListItem> StepDurationTypeList { get; set; }

        public List<SelectListItem> SystemTaskList { get; set; }

        //public IList<SelectListItem> Reference_Verb { get; set; }

        public List<SelectListItem> ReferenceObjectsList { get; set; }


        public IList<SelectListItem> ProcFilesList { get; set; }

        public IList<SelectListItem> PictureRefFilesList { get; set; }


        public IList<SelectListItem> ListReferenceTheories { get; set; }

        public void Setup(PreProServices preProServices, ProcedureVerbsService procedureVerbsService, DocumentService documentService, DocumentFilesService documentFilesService, TheoryParagraphService TheoryParagraphService)
        {
            BaseStartOnCounterList = new List<SelectListItem>
            {
                    new SelectListItem
                    { Text = "NO",
                        Value = "0",
                        Selected = true
                    },
                new SelectListItem
                {
                    Text = "YES",
                    Value = "1"

                }

            };

            StepDurationTypeList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "SYS_SECONDS",
                    Value = "TIME_SYS_SECONDS"
                },
                new SelectListItem
                {
                    Text = "SYS_MINUTES",
                    Value = "TIME_SYS_MINUTES",
                    Selected = true
                },
                 new SelectListItem
                {
                    Text = "SYS_HOURS",
                    Value = "TIME_SYS_HOURS"
                },
                new SelectListItem
                {
                    Text = "SYS_DAYS",
                    Value = "TIME_SYS_DAYS",

                },
                new SelectListItem
                {
                    Text = "SYS_WEEKS",
                    Value = "TIME_SYS_WEEKS",

                }

            };

            SystemTaskList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = "SYS_COMP_TEST",
                    Value = "SYS_COMP_TEST"
                },
                new SelectListItem
                {
                    Text = "SYS_CONSUME",
                    Value = "SYS_CONSUME",
                    Selected = true
                },
                 new SelectListItem
                {
                    Text = "SYS_CREATE",
                    Value = "SYS_CREATE"
                },
                new SelectListItem
                {
                    Text = "SYS_DNR",
                    Value = "SYS_DNR",

                },
                new SelectListItem
                {
                    Text = "SYS_E_ACCESS",
                    Value = "SYS_E_ACCESS",

                },
                new SelectListItem
                {
                    Text = "SYS_INSTALL",
                    Value = "SYS_INSTALL"
                },
                new SelectListItem
                {
                    Text = "SYS_PROVIED_AND_CONSUMED",
                    Value = "SYS_PROVIED_AND_CONSUMED",

                },
                new SelectListItem
                {
                    Text = "SYS_PROVIED_AND_STAY",
                    Value = "SYS_PROVIED_AND_STAY",

                },
                 new SelectListItem
                {
                    Text = "SYS_PROVIDE_TAKE_BACK",
                    Value = "SYS_PROVIDE_TAKE_BACK",

                },
                new SelectListItem
                {
                    Text = "SYS_RECEIVE",
                    Value = "SYS_RECEIVE"
                },
                new SelectListItem
                {
                    Text = "SYS_REMOVE",
                    Value = "SYS_REMOVE",

                },
                new SelectListItem
                {
                    Text = "SYS_SEND",
                    Value = "SYS_SEND",

                },
                 new SelectListItem
                {
                    Text = "SYS_SERIALIZE",
                    Value = "SYS_SERIALIZE",

                },
                new SelectListItem
                {
                    Text = "SYS_SHIPPING",
                    Value = "SYS_SHIPPING",

                }

            };


            //Reference_Verb = ProcedureTypesService.GetProceduresTypes().ToList().Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id
            //}).OrderBy(o => o.Text).ToList();

            ReferenceObjectsList = documentService.GetSelectedObjects(id: Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();

            ProcFilesList = documentFilesService.GetSelectedFiles(id: Id, type: null).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();

            ProcFilesList = documentFilesService.GetSelectedFiles(id: Id, type: "PICTURE").Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();
            ListReferenceTheories = preProServices.GetPreProTheoryExceptions(id: Id).Select(x => new SelectListItem
            {
                Text = x.NAME,
                Value = x.ID.ToString(),
            }).OrderBy(o => o.Text).ToList();




        }
        public ProcedurePreProViewModel MapToDto(PrePropSearchView model)
        {
            return new ProcedurePreProViewModel
            {
                //ID = model.Id,
                //PROC_OBJ_ID = model.OBJ_ID,
                //STEP_TEXT = model.StepText,
                //COMMENTS = model.Comments,
                // DURATION = model.Duration.ToString(),
                //   DURATION_TYPE = model.DurationType,
                //   START_ON_COUNTER = model.StartOnCounter.ToString(),
                //SYSTEM_TASK = model.SystemTask
                // = model.CompanyPartNumber,
                //Name = model.Name,
                //PartType = model.PartType,
                //Spare = model.Spare,
                //Consumable = model.Consumable,
                //Unit = model.Unit,
                //UnitShippingWeight = model.UnitShippingWeight,
                //CustomerSeeAvailability = model.CustomerSeeAvailability,
                //SupplierSeeAvailability = model.SupplierSeeAvailability,
                //SupplierSeeInstallBase = model.SupplierSeeInstallBase,
                //WeightType = model.WeightType,
                //CreateProd = model.CreateProd,
                //SupplierCo = model.SupplierCo,
                //ProductType = model.ProductType,
                //ProcVerb = model.ProcVerb

            };
        }
    }
}
