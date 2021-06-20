using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class PurchaseOrderQueries
    {
        public static IQueryable<PurchaseOrderView> CreatePurchaseOrderViewQuery(this IQueryable<PurchaseOrderView> query, GetPurchaseOrder command, bool forRowCount = false)
        {
            query = query.Where(command.Balance, s => s.Balance == command.Balance);
            query = query.Where(command.CloseDate, s => DateTime.Compare(s.CloseDate.Value.Date,command.CloseDate.Value.Date) == 0);
            query = query.Where(command.CustomerName, s => !string.IsNullOrEmpty(s.CustomerName) && s.CustomerName.ToLower().Contains(command.CustomerName.ToLower()));
            query = query.Where(command.CustomerReferencePO, s => s.CustomerReferencePO.ToLower().Contains(command.CustomerReferencePO.ToLower()));
            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.InvoicedBalance, s => s.InvoicedBalance == command.InvoicedBalance);
            query = query.Where(command.Name, s => !string.IsNullOrEmpty(s.Name) && s.Name.ToLower().Contains(command.Name.ToLower()));
            query = query.Where(command.OpenDate, s => DateTime.Compare(s.OpenDate.Date, command.OpenDate.Value.Date) == 0);
            query = query.Where(command.Revision, s => s.Revision == command.Revision);
            query = query.Where(command.Status, s => !string.IsNullOrEmpty(s.Status) && command.Status.Contains(s.Status));
            query = query.Where(command.TotalPurchaseLimit, s => s.TotalPurchaseLimit == command.TotalPurchaseLimit);
            query = query.Where(command.UninvoicedBalance, s => s.UninvoicedBalance == command.UninvoicedBalance);
            query = query.Where(command.UnusedAmount, s => s.UnusedAmount == command.UnusedAmount);

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Balance), s => s.Balance);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.CloseDate), s => s.CloseDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.CustomerName), s => s.CustomerName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.CustomerReferencePO), s => s.CustomerReferencePO);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.InvoicedBalance), s => s.InvoicedBalance);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.OpenDate), s => s.OpenDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Revision), s => s.Revision);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Status), s => s.Status);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.TotalPurchaseLimit), s => s.TotalPurchaseLimit);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.UninvoicedBalance), s => s.UninvoicedBalance);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.UnusedAmount), s => s.UnusedAmount);

            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }

        public static IQueryable<EntityFramework.Entities.PurchaseOrderDBView> CreatePurchaseOrderDBViewQuery(this IQueryable<EntityFramework.Entities.PurchaseOrderDBView> query, GetPurchaseOrderDBView command, bool forRowCount = false)
        {
            query = query.Where(command.Balance, s => s.Balance == command.Balance);
            query = query.Where(command.CloseDate, s => DateTime.Compare(s.CloseDate.Value.Date, command.CloseDate.Value.Date) == 0);
            query = query.Where(command.CustomerName, s => !string.IsNullOrEmpty(s.CustomerName) && s.CustomerName.ToLower().Contains(command.CustomerName.ToLower()));
            query = query.Where(command.CustomerReferencePO, s => s.CustomerReferencePO.ToLower().Contains(command.CustomerReferencePO.ToLower()));
            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.InvoicedBalance, s => s.InvoicedBalance == command.InvoicedBalance);
            query = query.Where(command.Name, s => !string.IsNullOrEmpty(s.Name) && s.Name.ToLower().Contains(command.Name.ToLower()));
            query = query.Where(command.OpenDate, s => DateTime.Compare(s.OpenDate.Date, command.OpenDate.Value.Date) == 0);
            query = query.Where(command.Revision, s => s.Revision == command.Revision);
            query = query.Where(command.Status, s => !string.IsNullOrEmpty(s.Status) && command.Status.Contains(s.Status));
            query = query.Where(command.TotalPurchaseLimit, s => s.TotalPurchaseLimit == command.TotalPurchaseLimit);
            query = query.Where(command.UninvoicedBalance, s => s.UninvoicedBalance == command.UninvoicedBalance);
            query = query.Where(command.UnusedAmount, s => s.UnusedAmount == command.UnusedAmount);

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Balance), s => s.Balance);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.CloseDate), s => s.CloseDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.CustomerName), s => s.CustomerName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.CustomerReferencePO), s => s.CustomerReferencePO);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.InvoicedBalance), s => s.InvoicedBalance);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.OpenDate), s => s.OpenDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Revision), s => s.Revision);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.Status), s => s.Status);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.TotalPurchaseLimit), s => s.TotalPurchaseLimit);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.UninvoicedBalance), s => s.UninvoicedBalance);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseOrderSortFields.UnusedAmount), s => s.UnusedAmount);

            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
