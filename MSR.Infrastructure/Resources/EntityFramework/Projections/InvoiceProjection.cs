using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MSR.Infrastructure.Resources.EntityFramework.Projections
{
    public static class InvoiceProjection
    {
        public static Expression<Func<Invoice, dynamic>> InvoiceView
        {
            get
            {
                return i => new
                {
                   i.Id,
                   i.CustomerId,
                   CustomerName = i.Customer.Name,
                   i.Description,
                   i.InvoiceNumber,
                   Amount = i.Total,
                   i.TaxPercentage,
                   DueDate = i.InvoiceDate,
                   i.CreatedOn,
                   CreatedByName = i.Created.GetFullName(),
                   i.LastUpdatedOn,
                   LastUpdatedByName = i.LastUpdated.GetFullName(),
                   i.StatusId,
                   InvoiceItems = i.InvoiceItems.ToList()
                };
            }
        }
    }
}
