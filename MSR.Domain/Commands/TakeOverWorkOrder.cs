using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class TakeOverWorkOrder: Command
    {
        public int WorkOrderId { get;set;}
        public int UserId { get;set;}

    }
}
