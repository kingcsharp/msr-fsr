using System;
using System.Collections.Generic;

namespace Msr.Services.Procedures.ViewModels
{
    public class ProcedureStepMonitorsViewModel
    {
        public string Id { get; set; }

        public string Related_Object_Id { get; set; }

        public string Procedure_Step_Id { get; set; }

        public string Task_Id { get; set; }

        public string StrNTLogin { get; set; }

        public string Monitor_Type { get; set; }

        public string Description { get; set; }

        public string Should_Be { get; set; }

        public int MonitorNumber { get; set; }

        public string Highest_Threshold { get; set; }

        public string High_Threshold { get; set; }

        public string Target { get; set; }

        public string Low_Threshold { get; set; }

        public string Lowest_Threshold { get; set; }

        public string Opinion { get; set; }

        public string Hide_Target { get; set; }

        public string Use_Result { get; set; }

        public string Fail_Action { get; set; }

        public string Cant_Change { get; set; }

        public string Always_Pass { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ClientName { get; set; }
    }
}
