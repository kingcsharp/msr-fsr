using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.DTOs
{
    public class WorkOrderPartDTO
    {
        public int? Qty { get; set; }
        public string SerialNumber { get; set; }
        public string PartNumber { get; set; }
    }
}
