using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Msr.Models.Orders;
using Msr.Repositories;
using Msr.Services.Orders.Messaging;
using System.Data.SqlClient;
using System.Web.Configuration;
using RestSharp;

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

           response.StepPics = GetStepPics(fileId);

           //SELECT * FROM A_TASK_COMMENT WHERE TASK_ID IN  (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = '109815')
            return response;
       }

        public List<DocumentView> GetDocuments(string acctualPartId)
        {
            var fileIdParm = new SqlParameter("@ActualPartId", acctualPartId);

            var result = _dbContext.Database.SqlQuery<DocumentView>("Portal_GetDocuments @ActualPartId", fileIdParm).ToList();

            return result;
        }


        public List<DocumentView> GetStepPics(string fileId)
        {
            var fileIdParm = new SqlParameter("@FileId", fileId);

            var result = _dbContext.Database.SqlQuery<DocumentView>("Portal_GetStepPics @FileId", fileIdParm).ToList();

            return result;
        }

        public string GetDocumentBase64(string filePath, int? height)
        {
            var endPoint = WebConfigurationManager.AppSettings["DocApiEndPoint"] + $"doc/getfilebyid?filePath={filePath}&height={height}";

            var client = new RestClient(endPoint);

            var request = new RestRequest(Method.GET);

            var response = client.Execute<DocResponse>(request);

            var content = response.Data.DocData;

            return content;
        }
    }
}
