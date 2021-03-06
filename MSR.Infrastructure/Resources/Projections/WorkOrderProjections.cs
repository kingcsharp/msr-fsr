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
            LocationId = i.LocationId,
            LocationName = i.Location.Name,
            ProductId = i.ProductId,
            ProductName = i.Product.Name,
            ActualEndDate = i.ActualEndDate,
            TotalSalePrice = i.Product.TotalSalePrice,
            Status = GetWorkOrderStatusFromTasks(i.WorkOrderTasks)
        };

        public static Expression<Func<WorkOrder, dynamic>> WorkOrderGridSummaryView => i => new
        {
            Id = i.Id,
            PurchaseId = i.PurchaseId,
            WorkOrderItemNumber = $"{i.Purchase.PurchaseOrder.Customer.Name}-{i.Id}",
            CustomerName = i.Purchase.PurchaseOrder.Customer.Name,
            LocationId = i.LocationId,
            LocationName = i.Location.Name,
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
                ProcedureName = j.ProcedureStep.Procedure.Name,
                TaskStepOrder = j.TaskStepOrder,
                Title = j.ProcedureStep.Title,
                ProcedureStepTypeId = j.ProcedureStepTypeId
            }),
            WorkOrderPart = i.WorkOrderParts.Select(j => new
            {
                SerialNumber = j.SerialNumber,
                Qty = j.Qty
            }).FirstOrDefault(),
            HasNcr = i.HasNCR,
            WorkOrderMessages = i.WorkOrderMessages
        };

        public static Expression<Func<WorkOrderStatusSummary, dynamic>> WorkOrderStatusView => i => new
        {
            ProductName = i.ProductName,
            PartNumber = i.WorkOrderPartSerialNumber,
            ProcedureName = i.ProcedureName,
            LocationName = i.LocationName,
            WorkOrderSummary = new
            {
                WorkOrderId = i.WorkOrderId,
                WorkOrderItemNumber = i.WorkOrderItem,
                PurchaseOrderLineNumber = i.PurchaseOrderLineNumber,
                WorkOrderPartSerialNumber = i.WorkOrderPartSerialNumber,
                WorkOrderStatus = i.WorkOrderStatus,
                WorkOrderAssignedTo = i.WorkOrderAssignedTo,
                AssignedTo = i.AssignedTo,
                WorkOrderHasNcr = i.WorkOrderHasNCR,
                WorkOrderScheduledEndDate = i.ScheduledEndDate
            }
        };

        public static Expression<Func<WorkOrderMenu, dynamic>> WorkOrderMenuView => i => new
        {
            Id = i.Id,
            PurchaseId = i.PurchaseId,
            WorkOrderItemNumber = i.WorkOrderItemNumber,
            CustomerName = i.CustomerName,
            LocationName = i.LocationName,
            SerialNumber = i.SerialNumber,
            PurchaseOrderNumber = i.PurchaseOrderNumber,
            ReferencePO = i.ReferencePO,
            Quantity = i.Quantity,
            ScheduledStartDate = i.ScheduledStartDate,
            ScheduledEndDate = i.ScheduledEndDate,
            ActualStartDate = i.ActualStartDate,
            ActualEndDate = i.ActualEndDate,
            ProductName = i.ProductName,
            ProcedureName = i.ProcedureName,
            Status = i.Status,
            Disposition = i.Disposition,
            CurrentActiveTaskName = i.CurrentActiveTaskName,
            PercentageOfTasksCompleted = i.PercentageOfTasksCompleted,
            PercentageOfTasksCompletedNumerator = i.PercentageOfTasksCompletedNumerator,
            PercentageOfTasksCompletedDenominator = i.PercentageOfTasksCompletedDenominator,
            PercentageOfExpectedDurationTimeLogged = i.PercentageOfExpectedDurationTimeLogged,
            PercentageOfExpectedDurationTimeLoggedNumerator = i.PercentageOfExpectedDurationTimeLoggedNumerator,
            PercentageOfExpectedDurationTimeLoggedDenominator = i.PercentageOfExpectedDurationTimeLoggedDenominator,
            HasNcr = i.HasNcr,
            SegregationType = i.SegregationType == null ? (EnumSegregationType?)null: EnumUtils.GetValueFromDescription<EnumSegregationType>(i.SegregationType)
        };

        public static Expression<Func<PortalWorkOrderMenu, dynamic>> PortalWorkOrderMenuView => i => new
        {
            CompanyPartNumber = i.CompanyPartNumber,
            CustomerId = i.CustomerId,
            CycleCount = i.CycleCount,
            Disposition = i.Disposition,
            DueDate = i.DueDate,
            HasFiles = i.HasFiles,
            HasMonitors = i.HasMonitors,
            HasNCRs = i.HasNCRs,
            HasPhotos = i.HasPhotos,
            Id = i.Id,
            InvoiceAmount = i.InvoiceAmount,
            InvoiceDate = i.InvoiceDate,
            InvoiceName = i.InvoiceName,
            PartName = i.PartName,
            PartId = i.PartId,
            PercentageOfExpectedDurationTimeLogged = i.PercentageOfExpectedDurationTimeLogged,
            PercentageOfExpectedDurationTimeLoggedDenominator = i.PercentageOfExpectedDurationTimeLoggedDenominator,
            PercentageOfExpectedDurationTimeLoggedNumerator = i.PercentageOfExpectedDurationTimeLoggedNumerator,
            PercentageOfTasksCompleted = i.PercentageOfTasksCompleted,
            PercentageOfTasksCompletedDenominator = i.PercentageOfTasksCompletedDenominator,
            PercentageOfTasksCompletedNumerator = i.PercentageOfTasksCompletedNumerator,
            Price = i.Price,
            ProcedureName = i.ProcedureName,
            ProductName = i.ProductName,
            PurchaseOrderNumber = i.PurchaseOrderNumber,
            Qty = i.Qty,
            SerialNumber = i.SerialNumber,
            StartDate = i.StartDate,
            Status = i.Status,
            SubParts = i.SubParts,
            WorkOrderId = i.WorkOrderId,
            CreatedOn = i.CreatedOn
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
