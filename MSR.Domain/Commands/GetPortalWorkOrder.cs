using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class GetPortalWorkOrder: Command
    {
        public int CustomerId { get; set; }
        public string PartName { get; set; }
        public int? PartId { get; set; }
    }
}
