using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Msr.Services.Procedures.ViewModels
{
    public class AddMonitorForProcedureViewModel
    {
        public AddMonitorForProcedureViewModel()
        {
            
            MonitorTypesList = new List<SelectListItem>();

            ShouldBeList = new List<SelectListItem>();

            BasedOPionList = new List<SelectListItem>();
            
            UseResultList = new List<SelectListItem>();
            
            HideTargetList = new List<SelectListItem>();
            
            FailNextActionList = new List<SelectListItem>();
            
            ForceEndActionList = new List<SelectListItem>();
            
            AlwaysPassList = new List<SelectListItem>();
            
        }
        public string NewId { get; set; }

        public string Messages { get; set; }
        
        public string Id { get; set; }

        [DisplayName("Monitor Type")]
        public string Monitor_Type { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
        
        public string Start_System_Task { get; set; }
        
        public string Start_Type { get; set; }

        public string Stop_System_Task { get; set; }
        
        public string Stop_Type { get; set; }
        
        public string Counter_Or_Clock { get; set; }
        
        public string Clock_Unit { get; set; }

        [DisplayName("Highest Threshold")]
        public string Highest_Threshold { get; set; }

        [DisplayName("Highest Threashold")]
        public string High_Threshold { get; set; }

        [DisplayName("Target")]
        public string Target { get; set; }

        [DisplayName("Low Threshold")]
        public string Low_Threshold { get; set; }

        [DisplayName("Lowest Threshold")]
        public string Lowest_Threshold { get; set; }

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
        
        public string Correct_Answer { get; set; }
        
        public string Text_Target { get; set; }
        
        public string Task_Id { get; set; }
        
        public string Tolerance { get; set; }
        
        public string Related_Object_Id { get; set; }

        [DisplayName("Fault Handling")]
        public string Fail_Action { get; set; }
        
        public string Target_Object_Type { get; set; }
        
        public string Target_Object { get; set; }
        
        public string Skip_Mode { get; set; }

        [DisplayName("Force specified 'Fault handling' on task assignee?")]
        public string Cant_Change { get; set; }

        [DisplayName("Always Pass? (Just collecting data.. not judging)")]
        public string Always_Pass { get; set; }
        
        public string StrNTLogin { get; set; }

        public List<SelectListItem> MonitorTypesList { get; set; }

        public List<SelectListItem> ShouldBeList { get; set; }

        public List<SelectListItem> BasedOPionList { get; set; }

        public List<SelectListItem> UseResultList { get; set; }

        public List<SelectListItem> HideTargetList { get; set; }

        public List<SelectListItem> FailNextActionList { get; set; }

        public List<SelectListItem> ForceEndActionList { get; set; }

        public List<SelectListItem> AlwaysPassList { get; set; }

        public void Setup()
        {

            MonitorTypesList = new List<SelectListItem>
            {
                new SelectListItem {Text = "NUMBER", Value = "NUMBER"},
                new SelectListItem {Text = "USER_NUMBER", Value = "USER_NUMBER"},
                new SelectListItem {Text = "YES_NO", Value = "YES_NO"},
                new SelectListItem {Text = "MULTIPLE", Value = "MULTIPLE"},
                new SelectListItem {Text = "TEXT", Value = "TEXT"},
                new SelectListItem {Text = "OBJECT", Value = "OBJECT"}
            };

            ShouldBeList = new List<SelectListItem>
            {
                new SelectListItem {Text = "EQUAL", Value = "EQUAL"},
                new SelectListItem {Text = "ABOVE", Value = "ABOVE"},
                new SelectListItem {Text = "BELOW", Value = "BELOW"},
                new SelectListItem {Text = "BETWEEN", Value = "BETWEEN"}
            };

            BasedOPionList = new List<SelectListItem>
            {
                new SelectListItem {Text = "NO", Value = "0"},
                new SelectListItem {Text = "YES", Value = "1"}
            };

            UseResultList = new List<SelectListItem>
            {
                new SelectListItem {Text = "NO", Value = "0"},
                new SelectListItem {Text = "YES", Value = "1"}
            };

            HideTargetList = new List<SelectListItem>
            {
                new SelectListItem {Text = "NO", Value = "0"},
                new SelectListItem {Text = "YES", Value = "1"}
            };

            FailNextActionList = new List<SelectListItem>
            {
                new SelectListItem {Text = "CONTINUE", Value = "CONTINUE"},
                new SelectListItem {Text = "DONOTCLOSE", Value = "DONOTCLOSE"},
                new SelectListItem {Text = "DNR", Value = "DNR"},
                new SelectListItem {Text = "DNR", Value = "DNR"}
            };

            ForceEndActionList = new List<SelectListItem>
            {
                new SelectListItem {Text = "NO", Value = "0"},
                new SelectListItem {Text = "YES", Value = "1"}
            };

            AlwaysPassList = new List<SelectListItem>
            {
                new SelectListItem {Text = "NO", Value = "0"},
                new SelectListItem {Text = "YES", Value = "1"}
            };

        }
        public AddMonitorForProcedureViewModel MapToDto(GetMoniterViewModel model)
        {
            return new AddMonitorForProcedureViewModel
            {
                Id = model.Id,

                Monitor_Type = model.Monitor_Type,

                Description = model.Description,

                Start_System_Task = model.Start_System_Task,

                Start_Type = model.Start_Type,

                Stop_System_Task = model.Stop_System_Task,

                Stop_Type = model.Stop_Type,

                Counter_Or_Clock = model.Counter_Or_Clock,

                Clock_Unit = model.Clock_Unit,

                Highest_Threshold = model.Highest_Threshold.ToString(),

                High_Threshold = model.High_Threshold.ToString(),

                Target = model.Target.ToString(),

                Low_Threshold = model.Low_Threshold.ToString(),

                Lowest_Threshold = model.Lowest_Threshold.ToString(),

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

                Cant_Change = model.Cant_Change.ToString(),

                Always_Pass = model.Always_Pass.ToString(),

                StrNTLogin = model.StrNTLogin
            };
        }
    }
}
