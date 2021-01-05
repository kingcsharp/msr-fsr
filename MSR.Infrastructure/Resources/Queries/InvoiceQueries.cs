using Microsoft.EntityFrameworkCore;
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
    }
}
