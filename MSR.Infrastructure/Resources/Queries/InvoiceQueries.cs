using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.QueryFilters;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class InvoiceQueries
    {
        public static async Task<dynamic> GetFilteredInvoices(this DbSet<Invoice> dbSet, Expression<Func<Invoice, dynamic>> projection, InvoiceFilter filters)
        {
            if (filters.Id.HasValue)
            {
                return await dbSet.Where(i => i.Id == filters.Id.Value).Select(projection).FirstOrDefaultAsync();
            }

            return await dbSet.Select(projection).ToListAsync();
        }

        public static IQueryable<Invoice> CreateLocationQuery(this IQueryable<Invoice> query, GetInvoicesGridView command, bool forRowCount = false)
        {
            query = query.Include(i => i.Customer).ThenInclude(c => c.Created)
                                                    .ThenInclude(c => c.LastUpdated)
                                                .Include(i => i.Status)
                                                .Include(i => i.InvoiceItems)
                                                    .ThenInclude(ii => ii.PurchaseOrder)
                                                .Include(i => i.InvoiceItems)
                                                    .ThenInclude(ii => ii.WorkOrder)
                                                    .ThenInclude(wo => wo.Purchase)
                                                .AsQueryable();

            query = query.Where(command.Amount, s => s.Total == command.Amount);
            query = query.Where(command.CreatedByName, s => s.Created.Created.FullName.Contains(command.CreatedByName));
            query = query.Where(command.CreatedOn, s => s.CreatedOn == command.CreatedOn);
            query = query.Where(command.CustomerName, s => s.Customer.Name.Contains(command.CustomerName));
            query = query.Where(command.Description, s => s.Description.Contains(command.Description));
            query = query.Where(command.DueDate, s => s.InvoiceDate == command.DueDate);
            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.InvoiceNumber, s => s.InvoiceNumber.Contains(command.InvoiceNumber));
            query = query.Where(command.LastUpdatedByName, s => s.LastUpdated.FullName.Contains(command.LastUpdatedByName));
            query = query.Where(command.LastUpdatedOn, s => s.LastUpdatedOn == command.LastUpdatedOn);
            query = query.Where(command.StatusId, s => s.StatusId == command.StatusId);

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.Amount), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.CreatedByName), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.CreatedOn), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.CustomerName), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.Description), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.DueDate), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.InvoiceNumber), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.LastUpdatedByName), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumInvoiceSortFilters.LastUpdatedOn), s => s.Id);

            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
