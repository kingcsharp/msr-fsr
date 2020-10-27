using System;
using System.Collections.Generic;
using System.Text;
using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetWorkOrderPart: Command
    {
        public int? Id { get; set; }
        public int? WorkOrderId { get; set; }
    }
}
