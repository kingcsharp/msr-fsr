using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class WorkOrderHistoryViewQueries
    {
        public static IQueryable<EntityFramework.Entities.WorkOrderHistoryView> CreateWorkOrderHistoryViewQuery(this IQueryable<EntityFramework.Entities.WorkOrderHistoryView> query, GetWorkOrderHistory command, bool forRowCount = false)
        {

            query = query.Where(command.PurchaseId, s => s.PurchaseId == command.PurchaseId);
            query = query.Where(command.WorkOrderItemNumber, s => (s.Customer + "-" + s.WorkOrderId).Contains(command.WorkOrderItemNumber));
            query = query.Where(command.Customer, s => s.Customer.Contains(command.Customer));
            query = query.Where(command.Location, s => s.Location.Contains(command.Location));
            query = query.Where(command.SerialNumber, s => s.SerialNumber.Contains(command.SerialNumber));
            query = query.Where(command.PurchaseOrderNumber, s => s.PurchaseOrderNumber.Contains(command.PurchaseOrderNumber));
            query = query.Where(command.Qty, s => s.Qty == command.Qty);
            query = query.Where(command.ScheduledStartDate, s => DateTime.Compare(s.ScheduledStartDate.Value.Date,command.ScheduledStartDate.Value.Date) == 0);
            query = query.Where(command.ScheduledEndDate, s => DateTime.Compare(s.ScheduledEndDate.Value.Date, command.ScheduledEndDate.Value.Date) == 0);
            query = query.Where(command.ActualStartDate, s => DateTime.Compare(s.ActualStartDate.Value.Date, command.ActualStartDate.Value.Date) == 0);
            query = query.Where(command.ActualEndDate, s => DateTime.Compare(s.ActualEndDate.Value.Date, command.ActualEndDate.Value.Date) == 0);
            query = query.Where(command.Product, s => s.Product.Contains(command.Product));
            query = query.Where(command.Procedure, s => s.Procedure.Contains(command.Procedure));
            query = query.Where(command.Status, s => command.Status.Contains(s.Status));
            query = query.Where(command.Dispostion, s => s.Dispostion.Contains(command.Dispostion));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.PurchaseId), s => s.PurchaseId);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.WorkOrderItemNumber), s => s.Customer);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.Customer), s => s.Customer);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.Location), s => s.Location);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.SerialNumber), s => s.SerialNumber);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.PurchaseOrderNumber), s => s.PurchaseOrderNumber);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.Qty), s => s.Qty);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.ScheduledStartDate), s => s.ScheduledStartDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.ScheduledEndDate), s => s.ScheduledEndDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.ActualStartDate), s => s.ActualStartDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.ActualEndDate), s => s.ActualEndDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.Product), s => s.Product);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.Procedure), s => s.Procedure);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.Status), s => s.Status);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkOrderHistoryViewSortFields.Dispostion), s => s.Dispostion);
            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
