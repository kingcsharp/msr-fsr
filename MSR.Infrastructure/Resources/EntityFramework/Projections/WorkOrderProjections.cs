using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MSR.Infrastructure.Resources.EntityFramework.Projections
{
    public static class WorkOrderProjections
    {
        public static Expression<Func<WorkOrder, dynamic>> InvoiceableWorkOrderView => i => new 
        {
            CustomerName = i.Purchase.PurchaseOrder.Customer.Name,
            CustomerId = i.Purchase.PurchaseOrder.CustomerId,
            Id = i.Id,
            ReferencePO = i.Purchase.PurchaseOrder.ReferencePO,
            CustomerPurchaseNumber = i.Purchase.CustomerPurchaseNumber,
            CustomerLineNumber = i.Purchase.CustomerLineNumber,
            SerialNumber = i.Purchase.SerialNumber,
            LocationId = i.LocationId.Value,
            LocationName = i.Location.Name,
            ProductId = i.ProductId,
            ProductName = i.Product.Name,
            ActualEndDate = i.ActualEndDate,
            TotalSalePrice = i.Product.TotalSalePrice
        };
    }
}
