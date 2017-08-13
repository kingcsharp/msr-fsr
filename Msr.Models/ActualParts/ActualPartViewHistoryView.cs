using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ActualParts
{
    public class ActualPartViewHistoryView
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string CurPlannerCounterStart { get; set; }

        public DateTime? CurPlannerStartDate { get; set; }

        public DateTime? CurPlannedStopDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualStopDate { get; set; }

        public string ActualPartId { get; set; }

        public string OriginalRequestor { get; set; }

        public string Requester { get; set; }

        public string LatestRequestee { get; set; }

        public string GroupRequesteeId { get; set; }

        public string RequesteeId { get; set; }

        public string CompanyName { get; set; }

        public string CoId { get; set; }

        public Int16? ColourCode { get; set; }

        public DateTime? RequestDate { get; set; }

        public string TaskStetTitle { get; set; }

        public string TaskPriority { get; set; }

        public string TaskType { get; set; }

        public string DnrStatus { get; set; }
    }
}
