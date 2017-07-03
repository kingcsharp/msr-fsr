using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using Msr.Models.Orders;
using Msr.Repositories;
using Msr.Services.Orders.Messaging;
using System.Data.SqlClient;
using System.Web.Configuration;
using Dapper;
using Msr.Services.Orders.Procedures;
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
            var endPoint = WebConfigurationManager.AppSettings["DocApiEndPoint"] + string.Format("doc/getfilebyid?filePath={0}&height={1}",filePath, height);

            var client = new RestClient(endPoint);

            var request = new RestRequest(Method.GET);

            var response = client.Execute<DocResponse>(request);

            var content = response.Data.DocData;

            return content;
        }

       public WorkOrderDetailsResponse GetPurchaseItemDetails(int fillId)
       {
           var detailsResponse = new WorkOrderDetailsResponse();
           detailsResponse.FillId = fillId;

           using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
           {
               var p = new DynamicParameters();

               p.Add("@fileId", fillId ,DbType.Int32 , ParameterDirection.Input);

               using (var multi = conn.QueryMultiple("GetPurchaseItemDetails", p, commandType: CommandType.StoredProcedure))
               {
                   detailsResponse.FileSearchResult = multi.Read<FileSearchResult>().Single();

                    detailsResponse.Parts = multi.Read<string>().ToList();

                   detailsResponse.TaskStepResults = multi.Read<TaskStepResult>().ToList();
               }
           }

           return detailsResponse;
       }

        public TsrDetailsResponse GetTsrDetails(int fillId)
        {
            try
            {
                var detailsResponse = new TsrDetailsResponse();

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    var p = new DynamicParameters();

                    p.Add("@fileId", fillId, DbType.Int32, ParameterDirection.Input);

                    using (var multi = conn.QueryMultiple("GetTsrDetails", p, commandType: CommandType.StoredProcedure))
                    {
                        detailsResponse.TsrTaskResults = multi.Read<TsrTaskResult>().ToList();
                    }
                }

                FormatHtml(detailsResponse);

                return detailsResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

       public WipHistoryTsrResponse GetWipHistoryTsrDetail(int fillId)
       {
           var detailsResponse = new WipHistoryTsrResponse
           {
               FillId = fillId
           };

           using (
               IDbConnection conn =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
           {
               var p = new DynamicParameters();

               p.Add("@fillID", fillId.ToString(), DbType.String, ParameterDirection.Input);

               using (
                   var multi = conn.QueryMultiple("Portal_GetTsrWipHistory", p, commandType: CommandType.StoredProcedure)
                   )
               {
                   detailsResponse.WipHistoryDetailResult = multi.Read<WipHistoryDetailResult>().Single();

                   detailsResponse.WipTaskResult = multi.Read<WipTaskResult>().ToList();

                   detailsResponse.WipSubTaskResult = multi.Read<WipSubTaskResult>().ToList();

                   foreach (var wipTask in detailsResponse.WipTaskResult)
                   {
                       wipTask.WipSubTasks =
                           detailsResponse.WipSubTaskResult.Where(x => x.TaskId == wipTask.Id).ToList();
                   }
               }
           }

           return detailsResponse;
       }

       private void FormatHtml(TsrDetailsResponse response)
       {
           foreach (var taskResult in response.TsrTaskResults)
           {
                taskResult.Des = taskResult.Des.Replace("<<bb>>", "<br/><h4>").Replace("<</bb>>", "</h4>").Replace("<<nl/>>", "<br/>");
            }
       }
    }
}