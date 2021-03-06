using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class PurchaseQueries
    {
        public static IQueryable<Purchase> CreatePurchaseQuery(this IQueryable<Purchase> query, GetPurchases command, bool forRowCount = false)
        {

            if (command.Id.HasValue)
            {
                query = query.Include(x => x.Status)
                        .Include(x => x.WorkOrders)
                        .Include(x => x.Location)
                        .Include(x => x.PurchaseOrder)
                        .Include(x => x.PurchaseOrderProduct)
                        .ThenInclude(s => s.Product).AsQueryable();
            }
            else
            {

                query = query.Include(x => x.Status)
                    .Include(x => x.Location)
                    .Include(x => x.PurchaseOrder)
                    .Include(x => x.PurchaseOrderProduct)
                    .ThenInclude(s => s.Product).AsQueryable();
            }



            query = query.Where<Purchase>(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date,command.CreatedOn.Value.Date) == 0);
            query = query.Where<Purchase>(command.Id, s => s.Id == command.Id);
            query = query.Where<Purchase>(command.Mttn, s => s.MTTN.Contains(command.Mttn));
            query = query.Where<Purchase>(command.PurchaseOrderProductName, s => s.PurchaseOrderProduct.Product.Name.Contains(command.PurchaseOrderProductName));
            query = query.Where<Purchase>(command.StatusId, s => s.StatusId == command.StatusId);


            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseSortFields.Mttn), s => s.MTTN);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseSortFields.PurchaseOrderProductName), s => s.PurchaseOrderProduct.Product.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPurchaseSortFields.StatusId), s => s.Status.Name);

            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
