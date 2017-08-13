
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Msr.Services.Orders.Procedures
{
    public class MonitorTemplateResult
    {
        public int FillId { get; set; }

        public string Id { get; set; }

        public int Step_Id { get; set; }

        public int Opinion { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Target { get; set; }
        
        public string Monitor_Type { get; set; }

        public string Should_Be { get; set; }

        public string Releated_Object_Type { get; set; }

        public string Releated_Object_Description { get; set; }

        public int Hide_Target { get; set; }

        public int Use_Result { get; set; }

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

        public string Highest_Threshold { get; set; }

        public string High_Threshold { get; set; }

        public string Low_Threshold { get; set; }

        public string Lowest_Threshold { get; set; }

        public bool? Is_Passing { get; set; }

        public string Mult_Choice_Answer { get; set; }

        public string Target_Object_Type { get; set; }

        public List<SelectListItem> ResultList { get; set; }

        public List<SelectListItem> FailActionList { get; set; }

        public List<SelectListItem> MonitorTemplateMultiChoices { get; set; }

        public IEnumerable<SelectListItem> MonitorTypes { get; set; }

        public List<SelectListItem> ShouldBeItems { get; set; }

        public List<SelectListItem> TargetObjectTypes { get; set; }

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

            MonitorTypes = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "User Number",
                    Value = "USER_NUMBER"
                },
                new SelectListItem
                {
                    Text = "Multiple",
                    Value = "MULTIPLE"
                },
                new SelectListItem
                {
                    Text = "Yes/No",
                    Value = "YES_NO"
                },new SelectListItem
                {
                    Text = "Number",
                    Value = "NUMBER"
                },new SelectListItem
                {
                    Text = "Text",
                    Value = "TEXT"
                }
            };

            ShouldBeItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "EQUAL",
                    Value = "EQUAL"
                },
                new SelectListItem
                {
                    Text = "ABOVE",
                    Value = "ABOVE"
                },
                new SelectListItem
                {
                    Text = "BELOW",
                    Value = "BELOW"
                },
                new SelectListItem
                {
                    Text = "BETWEEN",
                    Value = "BETWEEN"
                }
            };

            TargetObjectTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Anything",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = "Person",
                    Value = "A_PEOPLE_HISTORY"
                },
                new SelectListItem
                {
                    Text = "Company",
                    Value = "A_COMPANIES_HISTORY"
                },
                new SelectListItem
                {
                    Text = "Part",
                    Value = "A_PARTS_HISTORY"
                },
                new SelectListItem
                {
                    Text = "Actual Part",
                    Value = "A_ACTUAL_PARTS_HISTORY"
                }
            };
        }
    }
}
