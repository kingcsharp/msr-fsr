using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MSR.Infrastructure.Resources.EntityFramework.Projections
{
    public static class WorkOrderProjections
    {
        public static Expression<Func<WorkOrder, dynamic>> InvoiceableWorkOrderView
        {
            get
            {
                return i => new
                {
                    i.Id,
                    CustomerName = i.Purchase.PurchaseOrder.Customer.Name,
                    i.ProductId,
                    i.Price,
                    i.ScheduledStartDate,
                    i.ScheduledEndDate,
                    i.ActualStartDate,
                    i.ActualEndDate,
                    i.HasNCR,
                    i.LocationId,
                    LocationName = i.Location.Name,
                    ProductName = i.Product.Name,
                    i.Purchase,
                    PurchaseOrderNumber = i.Purchase.PurchaseOrder.ReferencePO,
                    WorkOrderTasks = i.WorkOrderTasks.ToList(),
                    WorkOrderParts = i.WorkOrderParts.ToList(),
                    Status = GetStatus(i.WorkOrderTasks.ToList())
                };
            }
        }

        private static string GetStatus(ICollection<WorkOrderTask> tasks)
        {
            // Status ['Waiting to Start', 'In Progress', 'Cancelled', 'Completed']
            // This field is calculated based on the summation of the statuses
            // of the steps.
            // 1   Approved
            // 2   In Progress
            // 3   Complete
            // 4   Cancelled
            // 5   Pending
            // 6   Rejected
            // 7   Open
            // 8   Closed
            // 9   Requested
            // 10  Assigned
            // 11  Waiting to Start
            int[] completed = { 3, 6, 8 };

            if (tasks.All(x => completed.Contains(x.StatusId)))
            {
                return EnumUtils.GetDescription(EnumStatusSteps.Complete);
            }

            if (tasks.Any(x => x.StatusId == (int)EnumStatusSteps.Cancelled))
            {
                return EnumUtils.GetDescription(EnumStatusSteps.Cancelled);
            }

            if (tasks.Any(x => x.StatusId == (int)EnumStatusSteps.InProgress))
            {
                return EnumUtils.GetDescription(EnumStatusSteps.InProgress);
            }
            
            return EnumUtils.GetDescription(EnumStatusSteps.WaitingtoStart);
        }
    }
}
