using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.PrePro;

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

        public void Setup(PreProServices preProServices, string ntlogin)
        {
            BaseStartOnCounterList = new List<SelectListItem>
            {
                new SelectListItem
                { Text = @"NO",
                    Value = "0",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"YES",
                    Value = "1"

                }

            };

            StepDurationTypeList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Seconds",
                    Value = "TIME_SYS_SECONDS"
                },
                new SelectListItem
                {
                    Text = @"Minutes",
                    Value = "TIME_SYS_MINUTES",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Hours",
                    Value = "TIME_SYS_HOURS"
                },
                new SelectListItem
                {
                    Text = "Days",
                    Value = "TIME_SYS_DAYS",

                },
                new SelectListItem
                {
                    Text = @"Weeks",
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
                    Text = @"System Comp Test",
                    Value = "SYS_COMP_TEST"
                },
                new SelectListItem
                {
                    Text = @"Consume",
                    Value = "SYS_CONSUME",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Create",
                    Value = "SYS_CREATE"
                },
                new SelectListItem
                {
                    Text = @"Diagnose and Repair",
                    Value = "SYS_DNR",

                },
                new SelectListItem
                {
                    Text = @"E-Access",
                    Value = "SYS_E_ACCESS",

                },
                new SelectListItem
                {
                    Text = @"Install",
                    Value = "SYS_INSTALL"
                },
                new SelectListItem
                {
                    Text = "Provide & Consumed",
                    Value = "SYS_PROVIED_AND_CONSUMED",

                },
                new SelectListItem
                {
                    Text = @"Provide & Stay",
                    Value = "SYS_PROVIED_AND_STAY",

                },
                new SelectListItem
                {
                    Text = @"Provide & Taken Back",
                    Value = "SYS_PROVIDE_TAKE_BACK",

                },
                new SelectListItem
                {
                    Text = @"Receive",
                    Value = "SYS_RECEIVE"
                },
                new SelectListItem
                {
                    Text = @"Remove",
                    Value = "SYS_REMOVE",

                },
                new SelectListItem
                {
                    Text = "Send",
                    Value = "SYS_SEND",

                },
                new SelectListItem
                {
                    Text = @"Serialize",
                    Value = "SYS_SERIALIZE",

                },
                new SelectListItem
                {
                    Text = @"Shipping",
                    Value = "SYS_SHIPPING",
                }

            };

            ReferenceProceduresList = preProServices.GetSelectedRefProcedures(id: Id, ntlogin: ntlogin).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();


            ReferenceFilesList = preProServices.GetSelectedRefFiles(id: Id, ntlogin: ntlogin).Select(x => new SelectListItem
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

            ReferenceVerbList.AddRange(preProServices.GetApprovedVerbsByCreatingCo(CreatingCo).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList());

            ReferenceObjectsList.Add(new SelectListItem { Value = "", Text = @"--Select--" });

            ReferenceObjectsList.AddRange(preProServices.GetReferenceObjectsByCreatingCo(CreatingCo).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList());

            Labors.AddRange(preProServices.GetProcedureStepLaborList(ProcObjId, CreatingCo).Select(x => new LaborObjectsView
            {
                RoleName = x.RoleName,
                ObjDesc = x.Qty + " " + x.QtyType
            }).OrderBy(o => o.RoleName).ToList());
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
