using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Reporting
{
    public class ActualPartsHistory
    {
        [Key]
        public Guid Id { get; set; }
        public string PN { get; set; }
        public string SN { get; set; }
        public string WorkOrderNumber { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int CycleCount { get; set; }
        public string NCDisposition { get; set; }

    }
}
