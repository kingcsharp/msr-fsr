using Msr.Models.PurchesOrder;
using Msr.Repositories;
using System.Linq;

namespace Msr.Services.PurchesOrder
{
   public class PurchesOrderService
    {
        private readonly MsrDbContext _dbContext;

        public PurchesOrderService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<PurchesOrderView> GetPurchesOrderQueryable()
        {
            return _dbContext.PurchesOrderViews;
        }
    }
}
