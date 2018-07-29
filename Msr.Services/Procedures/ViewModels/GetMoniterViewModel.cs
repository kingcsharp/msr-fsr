using System;
using System.Collections.Generic;
using System.Web.Mvc;

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

        public string Input_Type { get; set; }

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

        public byte? Cant_Change { get; set; }

        public byte? Always_Pass { get; set; }

        public string StrNTLogin { get; set; }

        public int? MonitorNumber { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ClientName { get; set; }

        public string Procedure_Step_Id { get; set; }

        public string OpinionText { get; set; }

        public string Hide_Target_Text { get; set; }

        public string Use_Result_Text { get; set; }

        public Int16? YES_NO_ANSWER { get; set; }

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
            MonitorTypesList = Commons.Lookups.LookupItems.MonitorTypesList();
            ShouldBeList = Commons.Lookups.LookupItems.ShouldBeList();
            BasedOPionList = Commons.Lookups.LookupItems.YesNo();
            UseResultList = Commons.Lookups.LookupItems.YesNo();
            HideTargetList = Commons.Lookups.LookupItems.YesNo();
            FailNextActionList = Commons.Lookups.LookupItems.FailNextActionList();
            ForceEndActionList = Commons.Lookups.LookupItems.YesNo();
            AlwaysPassList = Commons.Lookups.LookupItems.YesNo();
        }
        public AddMonitorForProcedureViewModel MapToDto(AddMonitorForProcedureViewModel model)
        {
            return new AddMonitorForProcedureViewModel
            {
                Id = model.Id,
            };
        }
    }
}
