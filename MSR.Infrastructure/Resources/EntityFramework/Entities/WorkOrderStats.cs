using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderStats))]
    public class WorkOrderStats: Entity
    {
        public int? WorkOrderId { get; set; }
        public string ActiveTitle { get; set; }
        public int? CompletedTasks { get; set; }
        public int TotalTasks { get; set; }
        public decimal? TotalTimeLogged { get; set; }
        public decimal? TotalTAskTime { get; set; }
    }
}
