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
using System.Web.Mvc;
using Dapper;
using EntityFrameworkExtras.EF6;
using Msr.Models.Tasks;
using Msr.Services.Orders.Procedures;
using Msr.Services.Orders.ViewModels;
using RestSharp;
using Msr.Models.Comman;
using Msr.Models.Procedures;
using Msr.Services.Documents;

namespace Msr.Services.Orders
{
    public class OrderService
    {
        private readonly MsrDbContext _dbContext;
        private readonly DocumentFilesService _documentFilesService;

        public OrderService()
        {
            _dbContext = new MsrDbContext();
            _documentFilesService = new DocumentFilesService();
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
            var endPoint = WebConfigurationManager.AppSettings["DocApiEndPoint"] + string.Format("doc/getfilebyid?filePath={0}&height={1}", filePath, height);

            var client = new RestClient(endPoint);

            var request = new RestRequest(Method.GET);

            var response = client.Execute<DocResponse>(request);

            var content = response.Data.DocData;

            return content;
        }

        public WorkOrderDetailsResponse GetPurchaseItemDetails(int fillId, string ntlogin)
        {
            var detailsResponse = new WorkOrderDetailsResponse { FillId = fillId };

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@fileId", fillId, DbType.Int32, ParameterDirection.Input);

                using (var multi = conn.QueryMultiple("Portal_GetPurchaseItemDetails", p, commandType: CommandType.StoredProcedure))
                {
                    detailsResponse.FileSearchResult = multi.Read<FileSearchResult>().SingleOrDefault();

                    detailsResponse.Parts = multi.Read<string>().ToList();

                    detailsResponse.TaskStepResults = multi.Read<TaskStepResult>().ToList();
                }
            }

            if (detailsResponse.FileSearchResult?.FillObjectId != null)
            {
                var ncrData = SearchNcrs(detailsResponse.FileSearchResult.FillObjectId, ntlogin);
                detailsResponse.NcrCount = ncrData.Count();
            }

            return detailsResponse;
        }

        public TsrDetailsResponse GetTsrDetails(int fillId, string ntlogin)
        {
            try
            {
                var detailsResponse = new TsrDetailsResponse { FillId = fillId };

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    var p = new DynamicParameters();

                    p.Add("@fileId", fillId, DbType.Int32, ParameterDirection.Input);

                    using (var multi = conn.QueryMultiple("Portal_GetTsrDetails", p, commandType: CommandType.StoredProcedure))
                    {
                        detailsResponse.TsrTaskResults = multi.Read<TsrTaskResult>().ToList();
                    }
                }
                SearchNcrs(fillId, ntlogin);
              //  FormatHtml(detailsResponse);

                return detailsResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DeliveryTsrDetailsResponse GetDeliveryTsrDetails(int fillId)
        {
            var detailsResponse = new DeliveryTsrDetailsResponse { FillId = fillId };

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@fillId", fillId.ToString(), DbType.String, ParameterDirection.Input);

                using (var multi = conn.QueryMultiple("Portal_GetDeliveryTsrDetails", p, commandType: CommandType.StoredProcedure))
                {
                    detailsResponse.PurchaseWithSupplierQuotesResult = multi.Read<PurchaseWithSupplierQuotesResult>().Single();

                    detailsResponse.PurchaseItemInfoResult = multi.Read<PurchaseItemInfoResult>().Single();
                }
            }

            return detailsResponse;
        }

        public NcrTsrDetailsResponse GetNcrTsrDetails(int fillId)
        {
            var detailsResponse = new NcrTsrDetailsResponse { FillId = fillId };

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@fillId", fillId.ToString(), DbType.String, ParameterDirection.Input);
                p.Add("@strNTLogin", string.Empty, DbType.String, ParameterDirection.Input);

                detailsResponse.FillsSearchResult = conn.Query<FillsSearchResult>(
                    @"SELECT CUST_NAME AS CustomerName, PROD_NAME AS ProductName, FILL_OBJ_DESC AS PartInfo, PROC_NAME AS ProcedureName, CUST_LINE_ITEM AS CustomerPo FROM A_V_FILLS_SEARCH with (noLock)  WHERE ID = @Id",
                    new { Id = fillId.ToString() })
                    .FirstOrDefault();

                detailsResponse.TasksFindForFillIdResult =
                    conn.Query<TasksFindForFillIdResult>("A_SP_TASKS_FIND_FOR_FILL_ID", p,
                        commandType: CommandType.StoredProcedure).Where(x => x.Print_Order.HasValue).ToList();

                var fillGetMonitorsForNcrResult =
                    conn.Query<FillGetMonitorsForNcrResult>("A_SP_FILL_GET_MONITORS_FOR_NCR", p,
                        commandType: CommandType.StoredProcedure).ToList();

                detailsResponse.AttachmentFileIds =
                    conn.Query<FilesForFillTaskResult>("A_SP_FILES_GET_FOR_FILL_TASKS", p,
                        commandType: CommandType.StoredProcedure).Select(x => x.File_Id).ToList();

                foreach (var tasksForFill in detailsResponse.TasksFindForFillIdResult)
                {
                    tasksForFill.FillGetMonitorsForNcrResult = fillGetMonitorsForNcrResult.Where(monitor => monitor.Stepper_Id == tasksForFill.Step_Id).ToList();
                }

            }

            return detailsResponse;
        }

        public PartLabelTsrDetailsResponse GetPartLabelTsrDetails(int fillId)
        {
            var detailsResponse = new PartLabelTsrDetailsResponse { FillId = fillId };

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@fileId", fillId, DbType.Int32, ParameterDirection.Input);

                detailsResponse.GetPartsAndKitsLabelsResult =
                    conn.Query<GetPartsAndKitsLabelsResult>("GetPartsAndKitsLabels", p,
                        commandType: CommandType.StoredProcedure).ToList();
            }

            return detailsResponse;
        }

        public MonitorLabelTsrDetailsResponse GetMonitorLabelTsrDetails(int fillId)
        {
            var detailsResponse = new MonitorLabelTsrDetailsResponse { FillId = fillId };

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@fillId", fillId, DbType.Int32, ParameterDirection.Input);

                detailsResponse.GetMonitorLabelTsrDetailsResult =
                    conn.Query<GetMonitorLabelTsrDetailsResult>("Portal_GetMonitorLabelTsrDetails", p,
                        commandType: CommandType.StoredProcedure).ToList();

            }

            return detailsResponse;
        }

        public WipHistoryTsrResponse GetWipHistoryTsrDetail(int fillId)
        {
            var detailsResponse = new WipHistoryTsrResponse
            {
                FillId = fillId
            };

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@fillID", fillId.ToString(), DbType.String, ParameterDirection.Input);

                using (var multi = conn.QueryMultiple("Portal_GetTsrWipHistory", p, commandType: CommandType.StoredProcedure))
                {
                    detailsResponse.WipHistoryDetailResult = multi.Read<WipHistoryDetailResult>().Single();

                    detailsResponse.WipTaskResult = multi.Read<WipTaskResult>().ToList();

                    var wipSubTaskResult = multi.Read<WipSubTaskResult>().ToList();

                    foreach (var wipTask in detailsResponse.WipTaskResult)
                    {
                        wipTask.WipSubTasks = wipSubTaskResult.Where(x => x.TaskId == wipTask.Id).ToList();
                    }
                }
            }

            return detailsResponse;
        }

        public bool SaveOrderItemQty(SaveWorkOrderViewModel model)
        {
            try
            {
                var saveWorkItemQtyProcedure = new SaveWorkOrderItemQtyProcedure { ItemId = model.FillId, Quanitiy = model.Value, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveWorkItemQtyProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool SaveOrderItemPunchNum(SaveWorkOrderViewModel model)
        {
            try
            {
                var saveWorkItemPunchNumProcedure = new SaveWorkOrderItemPunchNumProcedure { ItemId = model.FillId, CustPurchNum = model.Value, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveWorkItemPunchNumProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool SaveOrderItemDueDate(SaveWorkOrderViewModel model)
        {
            try
            {
                var saveOrderItemPurchaseDueDateProcedure = new SaveOrderItemPurchaseDueDateProcedure { purchItemID = model.PurchaseItemId, DueDate = model.Value, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveOrderItemPurchaseDueDateProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public string SaveOrderItemImages(SaveWorkItemImageViewModel model)
        {
            try
            {
                var saveWorkItemImagesProcedure = new SaveWorkItemImagesProcedure(model);

                _dbContext.Database.ExecuteStoredProcedure(saveWorkItemImagesProcedure);

                return saveWorkItemImagesProcedure.NewId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<WorkOrderImageView> GetOrderItemImagesById(string taskId)
        {
            var taskIdParm = new SqlParameter("@taskId", taskId);

            var result = _dbContext.Database.SqlQuery<WorkOrderImageView>("Portal_WorkItemImagesById @taskId", taskIdParm).ToList();

            return result;
        }
        public bool DeleteOrderItemImageById(string id, string loginId)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    var p = new DynamicParameters();

                    p.Add("@fileLinkId", id, DbType.String, ParameterDirection.Input);
                    p.Add("@strNTLogin", loginId, DbType.String, ParameterDirection.Input);

                    conn.Execute("Portal_DeleteWorkItemImagesById", p, commandType: CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetPurchaseItemIdByFillId(int fillId)
        {
            using (
                IDbConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var purchaseItemId = conn.Query<string>(
                    @"SELECT PURCH_ITEM_ID FROM A_FILLS WHERE ID = @Id",
                    new { Id = fillId.ToString() })
                    .FirstOrDefault();

                return purchaseItemId;
            }
        }

        public GetPurchaseWorkReportTsrDetailsResult GetPurchaseWorkReportTsrDetails(int purchaseItemId)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@purchaseItemId", purchaseItemId, DbType.String, ParameterDirection.Input);

                var getPurchaseWorkReportTsrDetailsResult =
                    conn.Query<GetPurchaseWorkReportTsrDetailsResult>("Portal_GetTsrPurchaseWorkReport", p,
                        commandType: CommandType.StoredProcedure).Single();

                return getPurchaseWorkReportTsrDetailsResult;
            }
        }

        public TechnicalWorkReportTsrDetailsResponse GetTechnicalWorkReportTsrDetails(int fillId, int purchaseItemId)
        {
            var detailsResponse = new TechnicalWorkReportTsrDetailsResponse { FillId = fillId };

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@fillId", fillId.ToString(), DbType.String, ParameterDirection.Input);
                p.Add("@strNTLogin", string.Empty, DbType.String, ParameterDirection.Input);

                detailsResponse.WipHistoryDetailResult = conn.Query<WipHistoryDetailResult>(
                    @"SELECT PURCH_ITEM_ID AS PurchaseItemId, SUP_NAME AS SupplierName, PROC_NAME AS ProcedureName, CUSTOMER_PERSON AS CustomerPerson, FILL_OBJ_DESC AS FillObjectDescription FROM A_V_FILLS_SEARCH with (noLock)  WHERE ID = @fillID",
                    new { fillID = fillId.ToString() })
                    .FirstOrDefault();

                detailsResponse.TasksFindForFillIdResult =
                    conn.Query<TasksFindForFillIdResult>("A_SP_TASKS_FIND_FOR_FILL_ID", p,
                        commandType: CommandType.StoredProcedure).Where(x => x.Print_Order.HasValue).ToList();

                var fillGetMonitorsForNcrResult =
                    conn.Query<FillGetMonitorsForNcrResult>("A_SP_FILL_GET_MONITORS_FOR_NCR", p,
                        commandType: CommandType.StoredProcedure).ToList();

                foreach (var tasksForFill in detailsResponse.TasksFindForFillIdResult)
                {
                    tasksForFill.FillGetMonitorsForNcrResult = fillGetMonitorsForNcrResult.Where(
                            monitor => monitor.Stepper_Id == tasksForFill.Step_Id).ToList();

                }
            }

            return detailsResponse;
        }

        public WipStepDetailsResponse GetWipStepDetails(int stepId, int fillId, string login, int? phStepId)
        {
            var detailsResponse = new WipStepDetailsResponse { StepId = stepId, FillId = fillId, LoginId = login };

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@stepId", stepId, DbType.String, ParameterDirection.Input);
                p.Add("@phStepId", phStepId, DbType.String, ParameterDirection.Input);

                using (var multi = conn.QueryMultiple("Portal_GetStepDetails", p, commandType: CommandType.StoredProcedure))
                {
                    detailsResponse.TaskEditDataResult = multi.Read<TaskEditDataResult>().Single();

                }

                var taskLog = _dbContext.TaskLogs.FirstOrDefault(x => x.TaskId == stepId && x.FillId == fillId);

                detailsResponse.TaskRunningDto = GetTotalTime(taskLog);

                var p1 = new DynamicParameters();

                p1.Add("@fillID", fillId, DbType.String, ParameterDirection.Input);
                p1.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input);

                using (var multi = conn.QueryMultiple("A_SP_TASKS_FIND_FOR_PURCHASE_ITEM_AND_ACT_PART", p1,
                    commandType: CommandType.StoredProcedure))
                {
                    detailsResponse.TaskItemParts = multi.Read<TaskItemPart>().ToList();
                }

                var p2 = new DynamicParameters();
                p2.Add("@procStepID", phStepId, DbType.String, ParameterDirection.Input);
                p2.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input);

                detailsResponse.ReferenceFiles = conn.Query<GetReferenceFiles>("A_SP_PROCEDURE_GET_REFERENCE_FILES", p2, commandType: CommandType.StoredProcedure).ToList();

                detailsResponse.ReferenceTheories = conn.Query<GetReferenceTheories>("SELECT l.THEORY_ID AS TheoryId,t.NAME AS TheoryName,OBJECT_ID AS ObjectId FROM A_PROCEDURE_STEP_THEORY_LINK l, A_V_THEORY_APPROVED_DATA t WHERE l.THEORY_ID = t.ID AND l.PROC_STEP_ID = @stepId", new { stepId = phStepId }, commandType: CommandType.Text).ToList();

                foreach (var referenceTheoriese in detailsResponse.ReferenceTheories.AsQueryable())
                {
                    referenceTheoriese.DocLinks = _documentFilesService.GetDocByObjectId(referenceTheoriese.ObjectId);
                }

                if (detailsResponse.TaskEditDataResult.Status != "FINISHED")
                {
                    foreach (var task in detailsResponse.TaskItemParts)
                    {
                        if ((task.Status == "REQUESTED" || task.Status == "ACCEPTED") && !string.IsNullOrWhiteSpace(task.Print_Order) && task.HAS_MONITOR == "1")
                        {
                            var monitorParams = new DynamicParameters();

                            monitorParams.Add("@RELATED_OBJECT_ID", null, DbType.String, ParameterDirection.Input);
                            monitorParams.Add("@PROCEDURE_STEP_ID", null, DbType.String, ParameterDirection.Input);
                            monitorParams.Add("@TASK_ID", task.STEP_ID, DbType.String, ParameterDirection.Input);
                            monitorParams.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input);

                            using (var multi = conn.QueryMultiple("A_SP_MONITOR_TEMPLATES_GET_DATA_FOR_OBJECT", monitorParams,
                                commandType: CommandType.StoredProcedure))
                            {
                                detailsResponse.MonitorTemplateResult = multi.Read<MonitorTemplateResult>().ToList();
                            }

                            if (detailsResponse.MonitorTemplateResult.Any())
                            {
                                foreach (var monitorTemplate in detailsResponse.MonitorTemplateResult)
                                {
                                    monitorTemplate.FillId = fillId;
                                    monitorTemplate.MonitorTemplateMultiChoices = conn.Query<MonitorTemplateMultiChoiceResult>(
                                        @"SELECT TXT AS Text, IS_ANSWER AS IsAnswer FROM A_MONITOR_TEMPLATES_MULT_CHOICE WHERE MONITOR_ID = @monitorId  ORDER BY ORD",
                                        new { monitorId = monitorTemplate.Id })
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.Id,
                                            Text = x.Text
                                        }).ToList();
                                }
                            }

                            break;
                        }

                        var taskData =
                            conn.Query<GetTaskDetailResult>(
                                "SELECT * FROM A_TASKS WHERE ID = @stepId",
                                new { stepId = task.STEP_ID }, commandType: CommandType.Text).SingleOrDefault();

                        if (taskData != null)
                        {
                            task.IsEditable = false;
                            if (taskData.Status == "ACCEPTED" && taskData.REQUESTEE_ID == login)
                            {
                                task.IsEditable = true;
                            }
                            else if (taskData.Status == "REQUESTED" && taskData.GROUP_REQUESTEE_ID == login)//Need to check this GroupRequesteeId in Roles from session
                            {
                                task.IsEditable = true;
                            }
                        }

                        var taskObjectLink = conn.Query<GetTaskObjectLinkResult>("SELECT * FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @stepId", new { stepId = task.STEP_ID }, commandType: CommandType.Text).SingleOrDefault();

                        if (taskObjectLink != null)
                        {
                            var p3 = new DynamicParameters();
                            p3.Add("@firstTime", null, DbType.String, ParameterDirection.Input);
                            p3.Add("@strAPart", taskObjectLink.Object_Id, DbType.String, ParameterDirection.Input);
                            p3.Add("@strListToexpand", taskObjectLink.Object_Id, DbType.String, ParameterDirection.Input);
                            p3.Add("@strExpandAllList", taskObjectLink.Object_Id, DbType.String,
                                ParameterDirection.Input);
                            p3.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input);

                            task.GetActualPartsShowHierarchys =
                                conn.Query<GetActualPartsShowHierarchy>("A_SP_ACTUAL_PARTS_SHOW_HIERARCHY", p3,
                                    commandType: CommandType.StoredProcedure).ToList();
                        }
                    }
                }
            }

            return detailsResponse;
        }

        public void UpdateStepMonitor(MonitorTemplateResult request)
        {
            var p = new DynamicParameters();

            p.Add("@id", request.Id, DbType.String, ParameterDirection.Input);
            p.Add("@failAction", request.Fail_Action, DbType.String, ParameterDirection.Input);
            if (request.Monitor_Type == "MULTIPLE")
            {
                p.Add("@result", request.Mult_Choice_Answer, DbType.String, ParameterDirection.Input);
            }
            else
            {
                p.Add("@result", request.Print_Result, DbType.String, ParameterDirection.Input);
            }
            p.Add("@comment", request.Comment, DbType.String, ParameterDirection.Input);
            p.Add("@target", request.Target, DbType.String, ParameterDirection.Input);
            p.Add("@tolerance", request.Tolerance, DbType.String, ParameterDirection.Input);
            p.Add("@theSaurusId", request.TheSaurusId, DbType.String, ParameterDirection.Input);
            p.Add("@strNTLogin", request.StrNtLogin, DbType.String, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                int i = conn.Execute("Portal_StepSaveMonitor", p, commandType: CommandType.StoredProcedure);
            }
        }

        public string CloseTask(string taskId, string login)
        {
            var p = new DynamicParameters();

            p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            p.Add("@ID", taskId, DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult = conn.Query<StepStartTaskResult>("A_SP_TASK_QUICK_CLOSE", p, commandType: CommandType.StoredProcedure);

                var retStatus = p.Get<string>("RET_STATUS");
                var msgs = p.Get<string>("MSGS");

                if (!string.IsNullOrWhiteSpace(retStatus))
                {
                    return retStatus;
                }

                var p2 = new DynamicParameters();

                p2.Add("@retVal", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p2.Add("@retMSG", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p2.Add("@TASK_ID", taskId, DbType.String, ParameterDirection.Input, size: 50);
                p2.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
                var newTextMonitorResult = conn.Query<StepStartTaskResult>("A_SP_MONITORS_CHECK_TASK_FOR_NEW_TEXT_MONITOR_RESULTS", p2, commandType: CommandType.StoredProcedure);
                string returnValue = p2.Get<string>("retVal");
                return returnValue;
            }
        }

        public ResultNotification<TaskLogDto> StepStart(int stepId, string login, int fillId)
        {
            var response = new ResultNotification<TaskLogDto>();

            var p = new DynamicParameters();

            p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            p.Add("@ID", stepId.ToString(), DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult = conn.Query<StepStartTaskResult>("A_SP_TASK_ACCEPT", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<string>("RET_STATUS");

                if (!string.IsNullOrWhiteSpace(status))
                {
                    response.AddError(status);
                }

                if ((status != null && !status.Contains("ERROR")) || status == null)
                {
                    var entity = new TaskLog()
                    {
                        TaskId = stepId,
                        StartTime = DateTime.Now,
                        StatusId = TimerStatuseConstants.InProgress,
                        UserId = login,
                        FillId = fillId
                    };

                    _dbContext.TaskLogs.Add(entity);
                    _dbContext.SaveChanges();

                    var taskDto = GetTotalTime(entity);

                    response.Entity = taskDto;

                    _dbContext.SaveChanges();
                }
            }
            return response;
        }
        public ResultNotification<TaskLogDto> StepDone(int stepId, string login, int fillId)
        {
            var response = new ResultNotification<TaskLogDto>();

            var p = new DynamicParameters();

            p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            p.Add("@ID", stepId, DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult = conn.Execute("A_SP_TASK_QUICK_CLOSE", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<string>("RET_STATUS");

                if (!string.IsNullOrWhiteSpace(status))
                {
                    response.AddError(status);
                }

                if ((status != null && !status.Contains("ERROR")) || status == null)
                {
                    var taskLog = _dbContext.TaskLogs.Where(x => x.TaskId == stepId && x.FillId == fillId)
                            .OrderByDescending(x => x.Id)
                            .FirstOrDefault();

                    if (taskLog != null)
                    {
                        if (taskLog.StatusId == TimerStatuseConstants.InProgress)
                        {
                            taskLog.EndTime = DateTime.Now;
                            taskLog.TotalTime = TimeSpan.FromSeconds((taskLog.EndTime.Value - taskLog.StartTime).TotalSeconds);
                        }

                        taskLog.StatusId = TimerStatuseConstants.Stopped;

                        _dbContext.SaveChanges();

                        response.Entity = GetTotalTime(taskLog);
                    }
                }
            }

            return response;
        }

        public ResultNotification<TaskLogDto> StepResume(int? id, int stepId, int fillId, string loggedUserId)
        {
            var result = new ResultNotification<TaskLogDto>();

            var taskLog = _dbContext.TaskLogs.Where(x => x.Id == id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            if (taskLog == null)
            {
                var entity = new TaskLog()
                {
                    TaskId = stepId,
                    StartTime = DateTime.Now,
                    StatusId = TimerStatuseConstants.InProgress,
                    UserId = loggedUserId,
                    FillId = fillId
                };

                _dbContext.TaskLogs.Add(entity);
                _dbContext.SaveChanges();

                taskLog = entity;
            }
            else
            {
                taskLog.StatusId = TimerStatuseConstants.InProgress;
                taskLog.StartTime = DateTime.Now;
                taskLog.EndTime = null;
                _dbContext.SaveChanges();
            }

            result.Entity = GetTotalTime(taskLog);

            return result;
        }

        public ResultNotification<TaskLogDto> StepPause(int taskLogId, int fillId)
        {
            var result = new ResultNotification<TaskLogDto>();

            var taskLog = _dbContext.TaskLogs.Where(x => x.Id == taskLogId && x.FillId == fillId)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();

            taskLog.StatusId = TimerStatuseConstants.Paused;
            taskLog.EndTime = DateTime.Now;

            var pausedTime = TimeSpan.FromSeconds((taskLog.EndTime.Value - taskLog.StartTime).TotalSeconds);

            taskLog.TotalTime = taskLog.TotalTime.Add(pausedTime);

            _dbContext.SaveChanges();

            result.Entity = GetTotalTime(taskLog);

            return result;
        }

        public void AssumeSteps(int fillId, string login)
        {
            var p = new DynamicParameters();

            p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            p.Add("@FILL_ID", fillId.ToString(), DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult = conn.Query<StepStartTaskResult>("A_SP_FILL_CANCEL_UNFINISHED_STEPS", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<string>("RET_STATUS");
            }
        }

        public void CancelUnfinishedSteps(int fillId, string login)
        {
            var p = new DynamicParameters();

            p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            p.Add("@FILL_ID", fillId.ToString(), DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult = conn.Query<StepStartTaskResult>("A_SP_FILL_CANCEL_UNFINISHED_STEPS", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<string>("RET_STATUS");
                var msg = p.Get<string>("MSGS");
            }
        }

        public ResultNotification<bool> AssumeTask(int taskId, string login)
        {
            var ressult = new ResultNotification<bool>();

            var p = new DynamicParameters();

            p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            p.Add("@ID", taskId.ToString(), DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult = conn.Query<StepStartTaskResult>("A_SP_TASK_ASSUME_CONTROL", p, commandType: CommandType.StoredProcedure);

                var retStatus = p.Get<string>("RET_STATUS");
                var msg = p.Get<string>("MSGS");

                if (!string.IsNullOrWhiteSpace(retStatus))
                {
                    ressult.AddError(retStatus);
                }
            }

            return ressult;
        }

        public ReferenceTheoryResponse GetTheoryData(int theoryId, string login)
        {
            var detailsResponse = new ReferenceTheoryResponse { TheoryId = theoryId };

            var p = new DynamicParameters();
            p.Add("@ID", theoryId.ToString(), DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var objectId = conn.Query<string>("A_SP_THEORY_GET_OBJECT_ID_FROM_ID", p, commandType: CommandType.StoredProcedure).SingleOrDefault();

                var p1 = new DynamicParameters();
                p1.Add("@strID", objectId, DbType.String, ParameterDirection.Input, size: 50);
                p1.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
                detailsResponse.TheoryGetInfoResult = conn.Query<TheoryGetInfoResult>("A_SP_THEORY_GET_EDIT_INFORMATION", p1, commandType: CommandType.StoredProcedure).FirstOrDefault();

                detailsResponse.ObjectInfoResult = conn.Query<ObjectInfoResult>("SELECT * FROM A_OBJECTS WHERE ID = @Id", new { Id = objectId }, commandType: CommandType.Text).FirstOrDefault();

                detailsResponse.WorkflowDataResult = conn.Query<WorkflowDataResult>("SELECT * FROM A_WORKFLOWS WHERE ID = (SELECT WF_ID FROM A_WORKFLOWS_STARTED WHERE ID = (SELECT WFS_ID FROM A_OBJECTS WHERE ID = @Id))", new { Id = objectId }, commandType: CommandType.Text).FirstOrDefault();

                var p2 = new DynamicParameters();
                p2.Add("@TOID", objectId, DbType.String, ParameterDirection.Input, size: 50);
                p2.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
                detailsResponse.TheoryAdditionalCommentResults = conn.Query<TheoryAdditionalCommentResult>("A_SP_THEORY_COMMENTS_GET_DATA", p2, commandType: CommandType.StoredProcedure).ToList();

                var p3 = new DynamicParameters();
                p3.Add("@pOBJID", objectId, DbType.String, ParameterDirection.Input, size: 50);
                p3.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
                detailsResponse.GetTheoryReferenceFilesResults = conn.Query<GetTheoryReferenceFilesResult>("A_SP_THEORY_HEADER_GET_REFERENCE_FILES", p3, commandType: CommandType.StoredProcedure).ToList();

                detailsResponse.ReferenceTheoryResults = conn.Query<ReferenceTheoryResult>("SELECT * FROM A_V_THEORY_HEADER_REF_THEORIES WHERE OBJECT_ID = @Id", new { Id = objectId }, commandType: CommandType.Text).ToList();

                var p4 = new DynamicParameters();
                p4.Add("@strID", detailsResponse.TheoryGetInfoResult.Id, DbType.String, ParameterDirection.Input, size: 50);
                p4.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
                detailsResponse.TheoryGetDepartmentResults = conn.Query<TheoryGetDepartmentResult>("A_SP_THEORY_SHOW_MY_DEPARTMENT_DATA", p4, commandType: CommandType.StoredProcedure).ToList();

                detailsResponse.TheoryGetCompanyAddressResults = conn.Query<TheoryGetCompanyAddressResult>("SELECT * FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = (SELECT LOCATION FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = (SELECT CREATING_CO FROM A_OBJECTS WHERE OBJ_ID = @Id))", new { Id = detailsResponse.TheoryGetInfoResult.Id }, commandType: CommandType.Text).ToList();

                var p5 = new DynamicParameters();
                p5.Add("@ID", detailsResponse.TheoryGetInfoResult.Security_Level, DbType.String, ParameterDirection.Input, size: 50);
                p5.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
                detailsResponse.TheoryGetSecurityLevelResults = conn.Query<TheoryGetSecurityLevelResult>("A_SP_SECURITY_LEVEL_GET_DATA_BY_ID", p5, commandType: CommandType.StoredProcedure).ToList();

                var p6 = new DynamicParameters();
                p6.Add("@ID", detailsResponse.TheoryGetInfoResult.Id, DbType.String, ParameterDirection.Input, size: 50);
                p6.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
                detailsResponse.TheoryGetRolesResults = conn.Query<TheoryGetRolesResult>("A_SP_THEORY_GET_ROLES_TO_VIEW", p6, commandType: CommandType.StoredProcedure).ToList();

                detailsResponse.TheoryGetParagraphDataResults = conn.Query<TheoryGetParagraphDataResult>("A_SP_THEORY_PARAGRAPHS_GET_DATA", p2, commandType: CommandType.StoredProcedure).ToList();

                var p7 = new DynamicParameters();
                p7.Add("@strID", objectId, DbType.String, ParameterDirection.Input, size: 50);
                p7.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
                detailsResponse.TheoryGetRevisionDataResults = conn.Query<TheoryGetRevisionDataResult>("A_SP_OBJECT_GET_REV_DATA", p7, commandType: CommandType.StoredProcedure).ToList();
            }

            return detailsResponse;
        }

        public void UpdateRootPart(int partId, string serialNumber, int taskId, string login)
        {
            var p = new DynamicParameters();

            p.Add("@ID", partId, DbType.String, ParameterDirection.Input, size: 500);
            p.Add("@taskID", taskId, DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@SN", serialNumber, DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult = conn.Query<StepStartTaskResult>("A_SP_ACTUAL_PART_UPDATE_SERIAL_FROM_SERIALIZE_TASK", p, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<NcrDataResult> SearchNcrs(int? partId, string login)
        {
            var p = new DynamicParameters();

            p.Add("@fieldList", null, DbType.String, ParameterDirection.Input, size: 4000);
            p.Add("@alias", "S", DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strWHERE", @" PROCEDURE_ID in (SELECT DISTINCT p.ROOT FROM A_O_PROCEDURES p WHERE p.VERB_NAME = 'NCR') AND PROCEDURE_STEP_ID is null  ", DbType.String, ParameterDirection.Input, size: 4000);
            p.Add("@strPurposes", null, DbType.String, ParameterDirection.Input, size: 4000);
            p.Add("@strObjects", null, DbType.String, ParameterDirection.Input, size: 4000);
            p.Add("@strProjects", null, DbType.String, ParameterDirection.Input, size: 4000);
            p.Add("@strSort", " ORDER BY COLOR_CODE,SORT_ID DESC", DbType.String, ParameterDirection.Input, size: 500);
            p.Add("@serialNumber", null, DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@partID", partId, DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var ncrSearch = conn.Query<NcrDataResult>("A_SP_NCR_SEARCH", p, commandType: CommandType.StoredProcedure);
                return ncrSearch;
            }
        }

        public string AddProcedureAsSubTask(string objId, string parentId, string login)
        {

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@newObjID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@messages", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@PARENT_TASK_ID", parentId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@PROC_HIST_ID", objId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

                conn.Query<StepStartTaskResult>("A_SP_TASK_ADD_PROCEDURE_AS_CHILD_TASK", p, commandType: CommandType.StoredProcedure);

                string newObjID = p.Get<string>("newObjID");
                string messages = p.Get<string>("messages");

                return messages;
            }
        }

        public TaskLogDto GetTotalTime(TaskLog taskLog)
        {
            if (taskLog == null)
            {
                return null;
            }

            var taskLogDto = new TaskLogDto(taskLog);

            taskLogDto.Duration = "0s";

            var timer = new TimeSpan();

            if (taskLog.StatusId == TimerStatuseConstants.InProgress)
            {
                timer = taskLog.EndTime?.Subtract(taskLog.StartTime) ?? DateTime.Now.Subtract(taskLog.StartTime);
                timer = timer.Add(taskLog.TotalTime);
            }
            else
            {
                timer = taskLog.TotalTime;
            }

            var timerInSeconds = timer.TotalSeconds;

            if (timerInSeconds <= 60)
            {
                taskLogDto.Duration = timer.Seconds + "s";
            }
            else if (timer.TotalMinutes <= 60)
            {
                taskLogDto.Duration = timer.Minutes + "m" + timer.Seconds + "s";
            }
            else
            {
                taskLogDto.Duration = timer.Hours + "h" + timer.Minutes + "m" + timer.Seconds + "s";
            }

            taskLogDto.TotalSeconds = timer.TotalSeconds;

            return taskLogDto;
        }

        public List<SelectFile> GetMultiChoiceAnswers(string id)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"SELECT ID as Id,TXT AS Name from A_MONITOR_TEMPLATES_MULT_CHOICE WHERE MONITOR_ID = {id} order by ORD").ToList();

            return result;
        }

        public ProcedureView GetProcedureByObjId(string objId)
        {
            var result = _dbContext.Procedures.SingleOrDefault(x => x.ObjectId == objId);

            return result;
        }

    }
}