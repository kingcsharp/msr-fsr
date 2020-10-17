using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(CycleCountHistory))]
    public class CycleCountHistory:Entity
    {
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; }
        public int CycleCount { get; set; }
    }
}
