using System;
using System.Web.Mvc;

namespace Msr.Services.Procedures.Messages
{
    public class GetStepEditDataResult
    {
        private string DurationType;
        public string Procedure_Id { get; set; }
        public int? Start_On_Counter { get; set; }
        public string Id { get; set; }
        [AllowHtml]
        public string Step_Text { get; set; }
        public string System_Task { get; set; }
        public string Comments { get; set; }
        public decimal? Counter_Value { get; set; }
        public string Counter_Unit { get; set; }
        public string FROM_START_OR_STOP { get; set; }
        public string REL_OR_ABS { get; set; }
        public string Destination { get; set; }
        public string Specific_Location { get; set; }
        public string REFERENCE_VERB { get; set; }
        public string REFERENCE_OBJECT { get; set; }
        public string REFERENCE_THEORIES { get; set; }
        public int? GOTO_STEP { get; set; }
        public string GOTO_STEP_ID { get; set; }
        public string Cycles { get; set; }
        public int? Cycle_On_Counter { get; set; }
        public int? Cycle_Count { get; set; }
        public string Cycle_Unit { get; set; }
        public double? Duration { get; set; }
        public string Title { get; set; }
        public string Duration_Type
        {
            get { return DurationType; }
            set
            {
                SetDurationType(value);
            }
        }

        private void SetDurationType(string value)
        {
            switch (value)
            {
                case "TIME_SYS_HOURS":
                {
                    DurationType = "Hours";
                    break;
                }
                default:
                {
                    DurationType = String.Empty;
                    break;
                }
            }
        }

        public double? Print_Order { get; set; }
        public string Pre_Step { get; set; }
    }
}
