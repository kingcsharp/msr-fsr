using System;
using Msr.Services.EquipmentMaintenances;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Msr.Services.Procedures.ViewModels
{
    public class AddMonitorForProcedureViewModel
    {
        public AddMonitorForProcedureViewModel()
        {

            MonitorTypesList = new List<SelectListItem>();

            ListSourceList = new List<SelectListItem>();

            ShouldBeList = new List<SelectListItem>();

            BasedOPionList = new List<SelectListItem>();

            UseResultList = new List<SelectListItem>();

            HideTargetList = new List<SelectListItem>();

            FailNextActionList = new List<SelectListItem>();

            ForceEndActionList = new List<SelectListItem>();

            AlwaysPassList = new List<SelectListItem>();

            EquipmentMaintenanceList = new List<SelectListItem>();

            CorrectAnsList = new List<SelectListItem>();


        }
        public string NewId { get; set; }

        public string Messages { get; set; }

        public string Id { get; set; }

        [DisplayName("Monitor Type")]
        public string Monitor_Type { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Input Type")]
        public string Input_Type { get; set; }

        [DisplayName("List Source")]
        public string List_Source { get; set; }

        public string Start_System_Task { get; set; }

        public string Start_Type { get; set; }

        public string Stop_System_Task { get; set; }

        public string Stop_Type { get; set; }

        public string Counter_Or_Clock { get; set; }

        public string Clock_Unit { get; set; }

        [Required]
        [DisplayName("High Value")]
        [Range(0, float.MaxValue, ErrorMessage = "Can only be between 0 .. max")]
        public float? Highest_Threshold { get; set; }

        [DisplayName("High Threashold")]
        [Range(0, float.MaxValue, ErrorMessage = "Can only be between 0 .. max")]
        public float? High_Threshold { get; set; }

        [DisplayName("Target")]
        [Range(0, float.MaxValue, ErrorMessage = "Can only be between 0 .. max")]
        public float? Target { get; set; }

        [DisplayName("Low Value")]
        [Range(0, float.MaxValue, ErrorMessage = "Can only be between 0 .. max")]
        public float? Low_Threshold { get; set; }

        [Required]
        [DisplayName("Low Value")]
        [Range(0, float.MaxValue, ErrorMessage = "Can only be between 0 .. max")]
        public float? Lowest_Threshold { get; set; }

        [DisplayName("Should Be")]
        public string Should_Be { get; set; }

        [DisplayName("Based On Opinion?")]
        public string Opinion { get; set; }

        [DisplayName("Hide Answer (target, thresholds, choice)")]
        public string Hide_Target { get; set; }

        [DisplayName("Use Result Value When Matching Case?")]
        public string Use_Result { get; set; }

        public string Fail_Stop { get; set; }

        public string Step_Id { get; set; }

        [Required]
        [DisplayName("Target Value")]
        public string Correct_Answer { get; set; }

        [DisplayName("Target Value")]
        public string Text_Target { get; set; }

        public string Task_Id { get; set; }

        public string Tolerance { get; set; }

        public string Related_Object_Id { get; set; }

        [DisplayName("Fault Handling")]
        public string Fail_Action { get; set; }

        public string Target_Object_Type { get; set; }

        [Required]
        [DisplayName("Target Value")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter a number")]
        public string Target_Object { get; set; }

        public Int16? YES_NO_ANSWER { get; set; }

        public string Skip_Mode { get; set; }

        [DisplayName("Force specified 'Fault handling' on task assignee?")]
        public byte? Cant_Change { get; set; }

        [DisplayName("Always Pass? (Just collecting data.. not judging)")]
        public byte? Always_Pass { get; set; }

        public string StrNTLogin { get; set; }

        [NotMapped]
        public string ProcedureName { get; set; }

        public List<SelectListItem> MonitorTypesList { get; set; }

        public List<SelectListItem> ListSourceList { get; set; }

        public List<SelectListItem> InputTypesList { get; set; }

        public List<SelectListItem> ShouldBeList { get; set; }

        public List<SelectListItem> BasedOPionList { get; set; }

        public List<SelectListItem> UseResultList { get; set; }

        public List<SelectListItem> HideTargetList { get; set; }

        public List<SelectListItem> FailNextActionList { get; set; }

        public List<SelectListItem> ForceEndActionList { get; set; }

        public List<SelectListItem> AlwaysPassList { get; set; }

        public List<SelectListItem> EquipmentMaintenanceList { get; set; }

        public List<SelectListItem> CorrectAnsList { get; set; }

        public void Setup(EquipmentMaintenanceService equipmentMaintenanceService)
        {
            InputTypesList = new List<SelectListItem>
            {
                new SelectListItem {Text = "Manual", Value = "MANUAL"},
                new SelectListItem {Text = "QR Code", Value = "QR_CODE"},
                new SelectListItem {Text = "Sensor", Value = "SENSOR"}
            };

            ListSourceList = new List<SelectListItem>
            {
                new SelectListItem {Text = "NCR Category", Value = "NCR_CATEGORY"}
            };

            MonitorTypesList = Commons.Lookups.LookupItems.MonitorTypesList();
            ShouldBeList = Commons.Lookups.LookupItems.ShouldBeList();
            BasedOPionList = Commons.Lookups.LookupItems.YesNo();
            UseResultList = Commons.Lookups.LookupItems.YesNo();
            HideTargetList = Commons.Lookups.LookupItems.YesNo();
            FailNextActionList = Commons.Lookups.LookupItems.FailNextActionList();
            ForceEndActionList = Commons.Lookups.LookupItems.YesNo();
            AlwaysPassList = Commons.Lookups.LookupItems.YesNo();
            CorrectAnsList = Commons.Lookups.LookupItems.YesNo();
        }

        public AddMonitorForProcedureViewModel MapToDto(GetMoniterViewModel model)
        {
            return new AddMonitorForProcedureViewModel
            {
                Id = model.Id,

                Monitor_Type = model.Monitor_Type,
                Input_Type = model.Input_Type,
                List_Source = model.List_Source,
                Description = model.Description,
                Start_System_Task = model.Start_System_Task,
                Start_Type = model.Start_Type,
                Stop_System_Task = model.Stop_System_Task,
                Stop_Type = model.Stop_Type,
                Counter_Or_Clock = model.Counter_Or_Clock,
                Clock_Unit = model.Clock_Unit,
                Highest_Threshold = model.Highest_Threshold,
                High_Threshold = model.High_Threshold,
                Target = model.Target,
                Low_Threshold = model.Low_Threshold,
                Lowest_Threshold = model.Lowest_Threshold,
                Should_Be = model.Should_Be,
                Opinion = model.Opinion.ToString(),
                Hide_Target = model.Hide_Target.ToString(),
                Use_Result = model.Use_Result.ToString(),
                Fail_Stop = model.Fail_Stop.ToString(),
                Step_Id = model.Step_Id,
                Correct_Answer = model.Correct_Answer,
                Text_Target = model.Text_Target,
                Task_Id = model.Task_Id,
                Tolerance = model.Tolerance,
                Related_Object_Id = model.Related_Object_Id,
                Fail_Action = model.Fail_Action,
                Target_Object_Type = model.Target_Object_Type,
                Target_Object = model.Target_Object,
                Skip_Mode = model.Skip_Mode,
                Cant_Change = model.Cant_Change,
                Always_Pass = model.Always_Pass,
                StrNTLogin = model.StrNTLogin
            };
        }
    }
}
