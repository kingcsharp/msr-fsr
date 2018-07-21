
using System.Collections.Generic;

namespace Msr.Services.Orders.Procedures
{
    public class TaskItemPart
    {
        public string Description { get; set; }

        public string Print_Order { get; set; }

        public string Status { get; set; }

        public string LATEST_REQUESTEE_NAME { get; set; }

        public string HAS_MONITOR { get; set; }

        public string SYSTEM_TASK { get; set; }

        public string STEP_ID { get; set; }

        public bool IsEditable { get; set; }

        public List<GetActualPartsShowHierarchy> GetActualPartsShowHierarchys { get; set; }

        public int FillId { get; set; }
    }
}
