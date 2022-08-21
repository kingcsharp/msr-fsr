using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Comparers
{
    public class WorkOrderStatusComparer : IEqualityComparer<WorkOrderStatus>
    {
        public bool Equals(WorkOrderStatus x, WorkOrderStatus y)
        {
            return x?.WorkOrderSummary?.WorkOrderId == y?.WorkOrderSummary?.WorkOrderId;
        }

        public int GetHashCode(WorkOrderStatus obj)
        {
            return obj.WorkOrderSummary.WorkOrderId.GetHashCode();
        }
    }
}
