using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.ViewModels
{
    public class GetMoniterViewModel
    {
        public GetMoniterViewModel()
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
       
        public string Monitor_Type { get; set; }
      
        public string Description { get; set; }
        
        public string Start_System_Task { get; set; }
        
        public string Start_Type { get; set; }

        public string Stop_System_Task { get; set; }
        
        public string Stop_Type { get; set; }
        
        public string Counter_Or_Clock { get; set; }
        
        public string Clock_Unit { get; set; }
        
        public Single? Highest_Threshold { get; set; }
        
        public Single? High_Threshold { get; set; }

        public Single? Target { get; set; }
        
        public Single? Low_Threshold { get; set; }
        
        public Single? Lowest_Threshold { get; set; }
      
        public string Should_Be { get; set; }

        public Int16? Opinion { get; set; }
        
        public Int16? Hide_Target { get; set; }

        public Int16? Use_Result { get; set; }

        public Int16? Fail_Stop { get; set; }
        
        public string Step_Id { get; set; }
        
        public string Correct_Answer { get; set; }
        
        public string Text_Target { get; set; }
        
        public string Task_Id { get; set; }
        
        public string Tolerance { get; set; }
        
        public string Related_Object_Id { get; set; }
        
        public string Fail_Action { get; set; }
        
        public string Target_Object_Type { get; set; }
        
        public string Target_Object { get; set; }
        
        public string Skip_Mode { get; set; }
        
        public byte Cant_Change { get; set; }
        
        public byte Always_Pass { get; set; }
        
        public string StrNTLogin { get; set; }

        public int MonitorNumber { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ClientName { get; set; }

        public string Procedure_Step_Id { get; set; }

        public string OpinionText { get; set; }

        public string Hide_Target_Text { get; set; }

        public string Use_Result_Text { get; set; }

        public string Fail_Stop_Text { get; set; }

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
        public AddMonitorForProcedureViewModel MapToDto(AddMonitorForProcedureViewModel model)
        {
            return new AddMonitorForProcedureViewModel
            {
                Id = model.Id,
                //ObjectId = model.ObjectId,
                //Name = model.Name,
                //CreatingCompany = model.CreatingCo,
                //Verb = model.Verb,
                //SystemId = model.SystemId,
                //Comments = model.Comments,
                //SecurityLevel = model.SecurityLevel,
                //StepInAp = model.StepInAp,
                //Duration = model.Duration,
                //DurationType = model.DurationType,
                //WipMsg = model.WipMsg,
                //Threshold = model.Threshold
            };
        }
    }
}
