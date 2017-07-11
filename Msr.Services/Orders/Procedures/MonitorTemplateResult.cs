
using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Services.Orders.Procedures
{
    public class MonitorTemplateResult
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string PrintResult { get; set; }

        public string Target { get; set; }

        public string MonitorType { get; set; }

        public string FailAction { get; set; }

        public string Tolerance { get; set; }

        public string TheSaurusId { get; set; }

        public string StrNtLogin { get; set; }

        public List<SelectListItem> ResultList { get; set; }

        public List<SelectListItem> FailActionList { get; set; }

        public void Setup()
        {
            ResultList = new List<SelectListItem>
            { 
                new SelectListItem
                {
                    Text = "YES",
                    Value = "1",
                    Selected = this.PrintResult == "1"
                },
                new SelectListItem
                {
                    Text = "NO",
                    Value = "0",
                    Selected = this.PrintResult == "0"
                }
            };

            FailActionList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Continue to next step.",
                    Value = "CONTINUE"
                },
                new SelectListItem
                {
                    Text = "Stay at this step until passing result is entered.",
                    Value = "DONOTCLOSE"
                },
                new SelectListItem
                {
                    Text = "Start Diagnose & Repair Tool.",
                    Value = "DNR"
                },
                new SelectListItem
                {
                    Text = "Skip all steps and end procedure.",
                    Value = "ENDPROCEDURE"
                }
            };
        }
    }
}
