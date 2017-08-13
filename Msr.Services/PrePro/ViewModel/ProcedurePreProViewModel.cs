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
using Msr.Services.ProcedureVerbs;
using Msr.Services.TheoryParagraph;

namespace Msr.Services.PrePro.ViewModel
{
   public class ProcedurePreProViewModel
    {
        public ProcedurePreProViewModel()
        {
            ListProcFiles = new List<SelectListItem>();
            ListPictureRefFiles = new List<SelectListItem>();
           //1Reference_Verb = new List<SelectListItem>();
            REFERENCE_OBJECTS = new List<SelectListItem>();
            ListReferenceTheories = new List<SelectListItem>();
            PictureFiles=new List<string>();
        }
     
        public string ID { get; set; }

        [DisplayName("Step Text:")]
        public string STEP_TEXT { get; set; }

        // [Required]
        [DisplayName("Proc ObjId:")]
        public string PROC_OBJ_ID { get; set; }

        [DisplayName("COMMENTS:")]
        public string COMMENTS { get; set; }

        [DisplayName("START_ON_COUNTER:")]
        public string START_ON_COUNTER { get; set; }
        

        [DisplayName("COUNTER VALUE :")]
        public string COUNTER_VALUE { get; set; }

        [DisplayName("COUNTER UNIT :")]
        public string COUNTER_UNIT { get; set; }

        [DisplayName("FROM_START_OR_STOP:")]
        public string FROM_START_OR_STOP { get; set; }

        [DisplayName("REL_OR_ABS :")]
        public string REL_OR_ABS { get; set; }

        [DisplayName("SYSTEM_TASK:")]
        public string SYSTEM_TASK { get; set; }

        [DisplayName("DESTINATION :")]
        public string DESTINATION { get; set; }

        [DisplayName("SPECIFIC_LOCATION :")]
        public string SPECIFIC_LOCATION { get; set; }

        [DisplayName("REFERENCE_VERB :")]
        public List<string> REFERENCE_VERB { get; set; }

        [DisplayName("REFERENCE OBJECT :")] 
        public List<string> REFERENCE_OBJECT { get; set; }

        [DisplayName("REFERENCE_THEORIES :")]
        public List<string> REFERENCE_THEORIES { get; set; }

        [DisplayName("GOTO_STEP :")]
        public string GOTO_STEP { get; set; }

        [DisplayName("GOTO_STEP_ID :")]
        public string GOTO_STEP_ID { get; set; }

        [DisplayName("CYCLES:")]
        public string CYCLES { get; set; }

        [DisplayName("CYCLE_ON_COUNTER:")]
        public string CYCLE_ON_COUNTER { get; set; }

        [DisplayName("CYCLE_COUNT :")]
        public string CYCLE_COUNT { get; set; }

        [DisplayName("CYCLE_UNIT :")]
        public string CYCLE_UNIT { get; set; }

        [DisplayName("ReferenceProcs :")]
        public List<string> ReferenceProcs { get; set; }

        [DisplayName("precedingSteps :")]
        public string precedingSteps { get; set; }

        [DisplayName("DURATION :")]
        public float? DURATION { get; set; }

        [DisplayName("DURATION_TYPE :")]
        public string DURATION_TYPE { get; set; }

        [DisplayName("Picture Files :")]
        public List<string> PictureFiles { get; set; }

        public string strNTLogin { get; set; }
        public List<SelectListItem> Base_Start_on_Counter { get; set; }

        public List<SelectListItem> Step_Duration_Type { get; set; }

      public List<SelectListItem> SYSTEM_TASKS { get; set; }



        //public IList<SelectListItem> Reference_Verb { get; set; }


        public List<SelectListItem> REFERENCE_OBJECTS { get; set; }


        public IList<SelectListItem> ListProcFiles { get; set; }

        public IList<SelectListItem> ListPictureRefFiles { get; set; }


          public IList<SelectListItem> ListReferenceTheories { get; set; }

        public void Setup(PreProServices PreProServices, ProcedureVerbsService ProcedureTypesService, DocumentService documentService, DocumentFilesService documentFilesService, TheoryParagraphService TheoryParagraphService)
        {
            Base_Start_on_Counter = new List<SelectListItem>
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

            Step_Duration_Type = new List<SelectListItem>
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

            SYSTEM_TASKS = new List<SelectListItem>
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

            REFERENCE_OBJECTS = documentService.GetSelectedObjects(id: ID).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();

            ListProcFiles = documentFilesService.GetSelectedFiles(id: ID, type: null).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();

            ListPictureRefFiles = documentFilesService.GetSelectedFiles(id: ID, type: "PICTURE").Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();
            ListReferenceTheories = PreProServices.GetPreProTheoryExceptions(id:ID).Select(x => new SelectListItem
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
                STEP_TEXT = model.StepText,
                COMMENTS = model.Comments,
               // DURATION = model.Duration.ToString(),
                DURATION_TYPE = model.DurationType,
                START_ON_COUNTER = model.StartOnCounter.ToString(),
             SYSTEM_TASK = model.SystemTask
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
