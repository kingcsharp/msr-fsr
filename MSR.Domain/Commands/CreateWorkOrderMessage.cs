using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class CreateWorkOrderMessage: Command
    {
        public int WorkOrderId { get; set; }
        public string Message { get; set; }
    }
}
