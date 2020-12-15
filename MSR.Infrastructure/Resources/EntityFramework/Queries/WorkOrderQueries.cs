using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;
using MSR.Domain.Views;
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
        public static async Task<ICollection<dynamic>> GetInvoiceableWorkOrders(this DbSet<WorkOrder> dbSet, Expression<Func<WorkOrder, dynamic>> projection)
        {
            try
            {
                return await dbSet.Select(projection).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new DomainException(ex.Message, DomainError.InternalServerError);
            }
        }
    }
}
