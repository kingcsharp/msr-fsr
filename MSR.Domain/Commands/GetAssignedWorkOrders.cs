using System;
using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetAssignedWorkOrders: Command
    {
        public int AssignedUserId { get; set; }
    }
}
