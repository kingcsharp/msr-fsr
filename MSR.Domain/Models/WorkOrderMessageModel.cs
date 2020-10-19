using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class WorkOrderMessageModel
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
