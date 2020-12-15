using Microsoft.EntityFrameworkCore;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Queries
{
    public static class WorkOrderQueries
    {
        public static async Task<dynamic> GetInvoiceableWorkOrders(this DbSet<WorkOrder> dbSet, Expression<Func<WorkOrder, dynamic>> projection)
        {
            return await dbSet.Select(projection).ToListAsync();
        }
    }
}
