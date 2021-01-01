using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using MSR.Domain.Views;
using MSR.Infrastructure.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MSR.Infrastructure.Resources.Projections
{
    public static class WorkOrderProjections
    {
        public static Expression<Func<WorkOrder, dynamic>> InvoiceableWorkOrderView => i => new 
        {
            CustomerName = i.Purchase.PurchaseOrder.Customer.Name,
            CustomerId = i.Purchase.PurchaseOrder.CustomerId,
            Id = i.Id,
            PurchaseOrderId = i.Purchase.PurchaseOrderId,
            ReferencePO = i.Purchase.PurchaseOrder.ReferencePO,
            CustomerPurchaseNumber = i.Purchase.CustomerPurchaseNumber,
            CustomerLineNumber = i.Purchase.CustomerLineNumber,
            SerialNumber = i.Purchase.SerialNumber,
            LocationId = i.LocationId.Value,
            LocationName = i.Location.Name,
            ProductId = i.ProductId,
            ProductName = i.Product.Name,
            ActualEndDate = i.ActualEndDate,
            TotalSalePrice = i.Product.TotalSalePrice,
            Status = GetWorkOrderStatusFromTasks(i.WorkOrderTasks)
        };

        public static Expression<Func<WorkOrder, dynamic>> WorkOrders => i => new
        {
            Id = i.Id,
            PurchaseId = i.PurchaseId,
            WorkOrderItemNumber = $"{i.Purchase.PurchaseOrder.Customer.Name}-{i.Purchase.CustomerPurchaseNumber}",
            CustomerName = i.Purchase.PurchaseOrder.Customer.Name,
            LocationId = i.LocationId.Value,
            LocationName = i.Location.Name,
            SerialNumber = i.Purchase.SerialNumber,
            PurchaseOrderNumber = i.Purchase.PurchaseOrderId,
            ReferencePO = i.Purchase.PurchaseOrder.ReferencePO,
            ScheduledStartDate = i.ScheduledStartDate,
            ScheduledEndDate = i.ScheduledEndDate,
            ActualStartDate = i.ActualStartDate,
            ActualEndDate = i.ActualEndDate,
            ProductName = i.Product.Name,
            WorkOrderTasks = i.WorkOrderTasks.Select(j => new
            {
                StatusId = j.StatusId,
                LaborTime = j.ProcedureStep.LaborTime,
                ProcedureName = j.ProcedureStep.Procedure.Name,
                TotalTaskTime = j.TotalTaskTime,
                TaskStepOrder = j.TaskStepOrder,
                Title = j.ProcedureStep.Title,
                ProcedureStepTypeId = j.ProcedureStepTypeId,
                WorkOrderTaskMonitors = j.WorkOrderTaskMonitors.Select(x => x.TextVal)
            }),
            WorkOrderPart = i.WorkOrderParts.FirstOrDefault(),
            HasNcr = i.HasNCR
        };

        private static EnumStatusSteps GetWorkOrderStatusFromTasks(ICollection<WorkOrderTask> tasks)
        {
            int[] completed = { 3, 6, 8 };
            if (tasks.All(x => completed.Contains(x.StatusId)))
            {
                return EnumStatusSteps.Complete;
            }

            if (tasks.Any(x => (x.StatusId == (int)EnumStatusSteps.InProgress || x.StatusId == (int)EnumStatusSteps.Complete)))
            {
                return EnumStatusSteps.InProgress;
            }

            if (tasks.Any(x => x.StatusId == (int)EnumStatusSteps.Cancelled))
            {
                return EnumStatusSteps.Cancelled;
            }

            return EnumStatusSteps.WaitingtoStart;
        }
    }
}
