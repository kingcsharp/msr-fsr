using Msr.Services.Procedures.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Msr.Services.Procedures.ViewModels
{
    public class PartsProvideTakeBackViewModel
    {
        public string APPROVED_OBJECT_ID { get; set; }
        public string PROCEDURE_ID { get; set; }
        public string ObjId { get; set; }
        public string ID { get; set; }
        public string QTY { get; set; }
        public string QTY_TYPE { get; set; }
        public string RELATIONSHIP { get; set; }
        public string Procedure_STEP_ID { get; set; }
        public string NTLogin { get; set; }
        public IEnumerable<SelectListItem> OrderingUnits { get; set; }
        public List<SelectListItem> ApprovedObjectNameList { get; set; }

        public void Setup(ProceduresService proceduresService)
        {
            OrderingUnits = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"SYS_MINUTES",
                    Value = "TIME_SYS_MINUTES"
                },
                new SelectListItem
                {
                    Text = @"SYS_HOURS",
                    Value = "TIME_SYS_HOURS"
                },
                new SelectListItem
                {
                    Text = @"SYS_DAYS",
                    Value = "TIME_SYS_DAYS"
                },
                new SelectListItem
                {
                    Text = @"SYS_WEEKS",
                    Value = "TIME_SYS_WEEKS"
                }
            };
            ApprovedObjectNameList = proceduresService.GetProcedureEditObjects().Select(x => new SelectListItem
            {
                Text = x.OBJ_DESC,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
        }

        public PartsProvideTakeBackViewModel MapToDto(ProcedureEditObjectView model)
        {
            return new PartsProvideTakeBackViewModel
            {
                ID = model.ID,
                QTY = model.QTY.ToString(),
                QTY_TYPE = model.QTY_TYPE,
                APPROVED_OBJECT_ID = model.APPROVED_OBJECT_ID,

            };
        }
    }
}
