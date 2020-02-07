using Msr.Services.EquipmentMaintenances;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Sensor;
using System.ComponentModel.DataAnnotations.Schema;
using iTextSharp.text.pdf;
using Msr.Models.EquipmentMaintenances;

namespace Msr.Services.Orders.Procedures
{
    public class MonitorTemplateResult
    {

        public List<SensorDataModel> SensorDataModels { get; set; }

        public string Monitor_Result_ID { get; set; }

        public int FillId { get; set; }

        public string Id { get; set; }

        public string List_Source { get; set; }

        public string Step_Id { get; set; }

        public string Ph_Step_Id { get; set; }

        public int Opinion { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Target { get; set; }

        public string Monitor_Type { get; set; }

        public string Input_Type { get; set; }

        public string Should_Be { get; set; }

        public string Releated_Object_Type { get; set; }

        public string Releated_Object_Description { get; set; }

        public int Hide_Target { get; set; }

        public int Use_Result { get; set; }

        public string Fail_Stop { get; set; }

        public string Yes_No_Answer { get; set; }

        public string Correct_Anser_Id { get; set; }

        public string Text_Target { get; set; }

        public string TASK_ID { get; set; }

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

        public string Target_Object { get; set; }
        //TODO: Replace when you do database update
        public bool SendEmailClient { get; set; } = true;

        //[Column("SENSOR_MAPPING_ID")]

        public int? SensorMappingID { get; set; }

        public int? SENSOR_MAPPING_ID { get; set; }

        public List<SelectListItem> NCRCategoryList { get; set; }

        public List<SelectListItem> EquipmentMaintenanceList { get; set; }

        public List<SelectListItem> ResultList { get; set; }

        public List<SelectListItem> FailActionList { get; set; }

        public List<SelectListItem> MonitorTemplateMultiChoices { get; set; }

        public void Setup(List<EquipmentMaintenanceView> equipmentMaintenanceView)
        {
            NCRCategoryList = Msr.Commons.Lookups.LookupItems.NCRCategories();

            ResultList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"YES",
                    Value = "1",
                    Selected = this.Print_Result == "1"
                },
                new SelectListItem
                {
                    Text = @"NO",
                    Value = "0",
                    Selected = this.Print_Result == "0"
                }
            };

            FailActionList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Continue to next step.",
                    Value = "CONTINUE"
                },
                new SelectListItem
                {
                    Text = @"Stay at this step until passing result is entered.",
                    Value = "DONOTCLOSE"
                },
                new SelectListItem
                {
                    Text = @"Start Diagnose & Repair Tool.",
                    Value = "DNR"
                },
                new SelectListItem
                {
                    Text = @"Skip all steps and end procedure.",
                    Value = "ENDPROCEDURE"
                }

            };

            EquipmentMaintenanceList = equipmentMaintenanceView.Select(x => new SelectListItem
            {
                Text = x.RoomEquipment + " (" + x.Id.ToString() + ")",
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
        }

        public void Setup(EquipmentMaintenanceService equipmentMaintenanceService, OrderService orderService)
        {
            NCRCategoryList = Msr.Commons.Lookups.LookupItems.NCRCategories();

            ResultList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"YES",
                    Value = "1",
                    Selected = this.Print_Result == "1"
                },
                new SelectListItem
                {
                    Text = @"NO",
                    Value = "0",
                    Selected = this.Print_Result == "0"
                }
            };

            FailActionList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Continue to next step.",
                    Value = "CONTINUE"
                },
                new SelectListItem
                {
                    Text = @"Stay at this step until passing result is entered.",
                    Value = "DONOTCLOSE"
                },
                new SelectListItem
                {
                    Text = @"Start Diagnose & Repair Tool.",
                    Value = "DNR"
                },
                new SelectListItem
                {
                    Text = @"Skip all steps and end procedure.",
                    Value = "ENDPROCEDURE"
                }

            };

            MonitorTemplateMultiChoices = orderService.GetMultiChoiceAnswers(id: Id).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            EquipmentMaintenanceList = equipmentMaintenanceService.GetEquipmentsQueryable().ToList().Select(x => new SelectListItem
            {
                Text = x.RoomEquipment + " (" + x.Id.ToString() + ")",
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
        }
    }
}
