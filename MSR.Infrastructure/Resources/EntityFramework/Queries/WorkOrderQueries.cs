using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using Newtonsoft.Json;
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
        public static async Task<ICollection<InvoiceableWorkOrderView>> GetInvoiceableWorkOrders(this DbSet<WorkOrder> dbSet, Expression<Func<WorkOrder, dynamic>> projection, List<int> invoicedWorkOrderIds)
        {
            try
            {
                var dynamicData =  await dbSet.Select(projection).ToListAsync();

                var workOrderViews = JsonConvert.DeserializeObject<ICollection<InvoiceableWorkOrderView>>(JsonConvert.SerializeObject(dynamicData));
                var invoiceableWorkOrderViews = workOrderViews.Where(i => invoicedWorkOrderIds.Contains(i.Id) == false && i.Status == EnumStatusSteps.Complete).ToList();

                return invoiceableWorkOrderViews;
            }
            catch(Exception ex)
            {
                throw new DomainException(ex.Message, DomainError.InternalServerError);
            }
        }
    }
}
