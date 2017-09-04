using Msr.Services.Procedures.Messages;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Msr.Services.Procedures.ViewModels
{
   public class EditProcedureObjectViewModel
    {
        [DisplayName("Object Name")]
        public string APPROVED_OBJECT_ID { get; set; }
        public string PROCEDURE_ID { get; set; }
        public string ObjId { get; set; }
        public string ID { get; set; }

        [DisplayName("Quantity")]
        public string QTY { get; set; }

        [DisplayName("Ordering Unit")]
        public string QTY_TYPE { get; set; }
        public string ProcID { get; set; }
        public string action { get; set; }
        public string isSaving { get; set; }
        public string LaberRole { get; set; }
        public string RELATIONSHIP { get; set; }
        public string STEP_ID { get; set; }
        public string NTLogin { get; set; }
        public IEnumerable<SelectListItem> OrderingUnits { get; set; }
        public List<SelectListItem> ApprovedObjectNameList { get; set; }

        public void Setup(ProceduresService ProceduresService)
        {
            OrderingUnits = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Ordering Unit Item",
                    Value = "UNIT",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Ounces",
                    Value = "WT_OZ"
                },
                new SelectListItem
                {
                    Text = "Pounds",
                    Value = "WT_LBS"
                },
                new SelectListItem
                {
                    Text = "Kilograms",
                    Value = "WT_KG"
                },
                new SelectListItem
                {
                    Text = "Gallons",
                    Value = "VOL_GALLONS"
                },
                new SelectListItem
                {
                Text = "SYS_SECONDS",
                Value = "TIME_SYS_SECONDS"
            },
            new SelectListItem
            {
                Text = "SYS_MINUTES",
                Value = "TIME_SYS_MINUTES"
            },
            new SelectListItem
            {
                Text = "SYS_HOURS",
                Value = "TIME_SYS_HOURS"
            },
                new SelectListItem
                {
                Text = "SYS_DAYS",
                Value = "TIME_SYS_DAYS"
            },
            new SelectListItem
            {
                Text = "SYS_WEEKS",
                Value = "TIME_SYS_WEEKS"
            }
          };


            ApprovedObjectNameList = ProceduresService.GetProcedureEditObjects().Select(x => new SelectListItem
            {
                Text = x.OBJ_DESC,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();

        

        }

        public EditProcedureObjectViewModel MapToDto(ProcedureEditObjectView model)
        {
            return new EditProcedureObjectViewModel
            {
                ID = model.ID,
                QTY = model.QTY.ToString(),
                QTY_TYPE = model.QTY_TYPE,
                APPROVED_OBJECT_ID = model.APPROVED_OBJECT_ID,
               
            };
        }
    }
}
