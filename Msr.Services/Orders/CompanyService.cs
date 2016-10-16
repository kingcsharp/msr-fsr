using System.ComponentModel;
using System.Linq;
using Msr.Models.Orders;
using Msr.Repositories;
using Msr.Services.Orders.Messaging;
using System.Data.SqlClient;

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

        public IQueryable<BuyerView> GetBuyerWorkOrderQueryable()
        {
            return _dbContext.BuyerViews;
        }

       public NcrReportResponse GetNcrDetails(string fileId)
       {
            var response = new NcrReportResponse();

            var fileIdParm = new SqlParameter("@FileId", fileId);

           response.Details = _dbContext.Database.SqlQuery<NcrDetails>("Portal_GetNcrReport @FileId", fileIdParm).Single();


           //SELECT * FROM A_TASK_COMMENT WHERE TASK_ID IN  (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = '109815')
            return response;
       }
    }
}
