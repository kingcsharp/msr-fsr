using System.ComponentModel;
using System.Linq;
using Msr.Models.Orders;
using Msr.Repositories;

namespace Msr.Services.Orders
{
   public class OrderService
    {
        private readonly MsrDbContext _dbContext;

        public OrderService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<WorkOrderView> GetWorkOrderQueryable()
        {
            return _dbContext.WorkOrders;
        }
    }
}
