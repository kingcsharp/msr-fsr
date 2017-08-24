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
            ReferenceProceduresList = new List<SelectListItem>();
            ReferenceFilesList = new List<SelectListItem>();
            ApplicationObjectsList = new List<SelectListItem>();
            ReferenceObjectsList = new List<SelectListItem>();
            ReferenceTheoriesList = new List<SelectListItem>();
            ReferenceVerbList = new List<SelectListItem>();
            ReferenceFiles = new List<string>();
        }

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
        public string ReferenceVerb { get; set; }

        public string ApplicationObjects { get; set; }

        public string ReferenceObject { get; set; }

        public List<string> ReferenceTheories { get; set; }

        [DisplayName("ReferenceProcs :")]
        public List<string> ReferenceProcedures { get; set; }

        [DisplayName("precedingSteps :")]
        public string PrecedingSteps { get; set; }

        [DisplayName("Estimated Step Duration :")]
        public double? Duration { get; set; }


        [DisplayName("Step Duration Type :")]
        public string DurationType { get; set; }

        [DisplayName("Labor :")]
        public string Labor { get; set; }

        [DisplayName("Number Of Questions to use :")]
        public string NumTestQuestion { get; set; }

        [DisplayName("Reference Files :")]
        public List<string> ReferenceFiles { get; set; }

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

        public void Setup(PreProServices preProServices)
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

            ReferenceProceduresList = preProServices.GetSelectedRefProcedures(id: Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();


            ReferenceFilesList = preProServices.GetSelectedRefFiles(id: Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();


            ReferenceTheoriesList = preProServices.GetSelectedRefTheories(id: Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();

            ReferenceVerbList.Add(new SelectListItem { Value = "", Text = "--Select--" });

            ReferenceVerbList.AddRange(preProServices.GetApprovedVerbsByCreatingCo(CreatingCo).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList());

            ReferenceObjectsList.Add(new SelectListItem { Value = "", Text = "--Select--" });

            ReferenceObjectsList.AddRange(preProServices.GetReferenceObjectsByCreatingCo(CreatingCo).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList());

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
                //NumTestQuestion = model,
                //ApplicationObjects = model,
                ReferenceVerb = model.ReferenceVerb,
                ReferenceObject = model.ReferenceObject,
                Comments = model.Comments
            };
        }
    }
}
