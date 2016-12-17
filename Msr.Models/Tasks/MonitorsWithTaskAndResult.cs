
using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Tasks
{
    public class MonitorsWithTaskAndResult
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public string MonitorType { get; set; }
        public string ActualPartId { get; set; }
        public string PartDescription { get; set; }
        public string Serial { get; set; }
        public string TaskId { get; set; }
        public DateTime? TaskStopDate { get; set; }
        public string PrintResult { get; set; }
        public string Comment { get; set; }

    }
}
