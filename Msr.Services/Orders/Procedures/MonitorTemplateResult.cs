
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Msr.Services.Orders.Procedures
{
    public class MonitorTemplateResult
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Target { get; set; }
        
        public string Monitor_Type { get; set; }

        public string Should_Be { get; set; }

        public string Releated_Object_Type { get; set; }

        public string Releated_Object_Description { get; set; }

        public string Hide_Target { get; set; }

        public string Use_Result { get; set; }

        public string Fail_Stop { get; set; }

        public string Yes_No_Answer { get; set; }

        public string Correct_Anser_Id { get; set; }

        public string Text_Target { get; set; }

        public string TaskId { get; set; }

        public string Roll_Up_Id { get; set; }

        public string Num_Val { get; set; }

        public string Text_Val { get; set; }

        public string Print_Result { get; set; }

        public string Comment { get; set; }

        public string My_Anser { get; set; }

        public string Tolerance { get; set; }

        public string Fail_Action { get; set; }
        public string Print_Order { get; set; }
        public string Cant_Change { get; set; }

        public string Always_Pass { get; set; }

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
                    Selected = this.Print_Result == "1"
                },
                new SelectListItem
                {
                    Text = "NO",
                    Value = "0",
                    Selected = this.Print_Result == "0"
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
