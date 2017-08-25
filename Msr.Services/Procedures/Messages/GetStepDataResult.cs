using System;

namespace Msr.Services.Procedures.Messages
{
    public class GetStepDataResult
    {
        private string DurationType;
        public string Id { get; set; }
        public string Step_Text { get; set; }
        public double? Duration { get; set; }
        public string Duration_Type {
            get { return DurationType; }
            set {
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
