using Dapper;
using EntityFrameworkExtras.EF6;
using Msr.Infrastructure.Files;
using Msr.Models.Comman;
using Msr.Models.EquipmentMaintenances;
using Msr.Models.Orders;
using Msr.Models.Procedures;
using Msr.Models.Sensor;
using Msr.Models.Tasks;
using Msr.Repositories;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.Helpers;
using Msr.Services.Orders.Messaging;
using Msr.Services.Orders.Procedures;
using Msr.Services.Orders.ViewModels;
using Msr.Services.Roles.Procedures;
using Msr.Services.Users.Messages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;

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

            response.Details = _dbContext.Database.SqlQuery<NcrDetails>("Portal_GetNcrReports @FileId", fileIdParm).Single();

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

        public string GetDocumentBase64(string url)
        {
            var sb = new StringBuilder();

            var extension = Path.GetExtension(url);

            if (extension.StartsWith(".") == true)
            {
                extension = extension.Substring(1);
            }

            var data = "";
            var fileName = MimeTypes.GetTypes(extension.Replace(".", ""));
            if (fileName == "application/pdf")
            {
                data = "data:application/pdf;base64,";
            }
            else if (fileName == "vnd.ms-word")
            {
                data = "data:vnd.ms-word;base64,";
            }
            else if (fileName == "application/zip")
            {
                data = "data:application/zip;base64,";
            }
            else if (fileName == "application/vnd.ms-powerpoint")
            {
                data = url;
                return data;
            }
            else if (fileName == "jpg" || fileName == "png" || fileName == "jpeg" || fileName == "gif")
            {
                if (extension.ToLower() == "jpg")
                {
                    extension = "jpeg";
                }

                data = $"data:image/{extension};base64,";
            }

            byte[] _byte = GetImage(url);

            sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return data + sb;

        }

        private byte[] GetImage(string url)
        {
            byte[] buf;

            try
            {
                var webProxy = new WebProxy();
                var req = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)req.GetResponse();
                var stream = response.GetResponseStream();

                using (var br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
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

                    detailsResponse.TaskStepResults = multi.Read<TaskStepResult>().Where(x => x.PRINT_ORDER != null).ToList();
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
                    detailsResponse.PurchaseItemInfoResultList = multi.Read<PurchaseItemInfoResult>().ToList();
                    detailsResponse.PurchaseItemInfoResult = detailsResponse.PurchaseItemInfoResultList.FirstOrDefault();
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
                        @"SELECT CUST_NAME AS CustomerName, PROD_NAME AS ProductName, FILL_OBJ_DESC AS PartInfo, PROC_NAME AS ProcedureName, CUST_LINE_ITEM AS CustomerPo,PROC_ID AS ProcId FROM A_V_FILLS_SEARCH with (noLock)  WHERE ID = @Id",
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
                        commandType: CommandType.StoredProcedure).ToList();

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
                    conn.Query<GetPartsAndKitsLabelsResult>("Portal_GetPartsAndKitsLabels", p,
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

        public WipHistoryTsrResponse GetWipHistoryTsrDetail(int fillId, LoggedUserIdResult getCurrentUser)
        {
            var detailsResponse = new WipHistoryTsrResponse
            {
                FillId = fillId
            };

            List<StepDocument> docFiles = new List<StepDocument>();

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

                foreach (var item in detailsResponse.WipTaskResult)
                {
                    var p2 = new DynamicParameters();
                    p2.Add("@procStepID", item.ProcedureStepId, DbType.String, ParameterDirection.Input);
                    p2.Add("@strNTLogin", getCurrentUser.Id, DbType.String, ParameterDirection.Input);

                    // DONE
                    var files = conn.Query<GetReferenceFiles>("A_SP_PROCEDURE_GET_REFERENCE_FILES", p2, commandType: CommandType.StoredProcedure).ToList();

                    foreach (var file in files)
                    {
                        docFiles.Add(new StepDocument() { DocId = file.Doc_Id, Name = file.Name,ProcedureStepId=item.ProcedureStepId });
                    }
                }
            }
            detailsResponse.ListDocument = docFiles;

            //var actualPartId = GetWorkOrderQueryable().Where(x => x.FillId == fillId.ToString()).Select(a => a.ActualPartId).FirstOrDefault();

            //var docs = this.GetDocuments(actualPartId);
            //foreach (var doc in docs)
            //{
            //    foreach (var docFile in docFiles)
            //    {
            //        if(doc.Id == docFile.DocId)
            //        {
            //            docFile.ServerPath = doc.ServerPath;
            //            docFile.DocumentURL = doc.DocumentURL;
            //            docFile.Name = doc.Name;
            //        }
            //    }
            //}

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
                var saveOrderItemPurchaseDueDateProcedure = new SaveOrderItemPurchaseDueDateProcedure { purchItemID = model.PurchaseItemId, DueDate = DateTime.Parse(model.Value), NTLogin = model.NTLogin };

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
                var ids = model.TaskId.Split(',');

                model.TaskId = ids[0];

                var saveWorkItemImagesProcedure = new SaveWorkItemImagesProcedure(model);

                _dbContext.Database.ExecuteStoredProcedure(saveWorkItemImagesProcedure);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    var p = new DynamicParameters();
                    p.Add("@ActualPartId", ids[1], DbType.String, ParameterDirection.Input);
                    p.Add("@FileId", saveWorkItemImagesProcedure.NewId, DbType.String, ParameterDirection.Input);
                    p.Add("@ModBy", model.NTLogin, DbType.String, ParameterDirection.Input);

                    var resultAddPartFile = conn.Execute("Portal_Actual_Part_Related_Files", p, commandType: CommandType.StoredProcedure);
                }

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

        public List<GetPurchaseWorkReportTsrDetailsResult> GetPurchaseWorkReportTsrDetails(int purchaseItemId)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("@purchaseItemId", purchaseItemId, DbType.String, ParameterDirection.Input);

                var getPurchaseWorkReportTsrDetailsResult =
                    conn.Query<GetPurchaseWorkReportTsrDetailsResult>("Portal_GetTsrPurchaseWorkReport", p,
                        commandType: CommandType.StoredProcedure).ToList();

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

        public WipStepDetailsResponse GetWipStepDetails(ref LoggedUserIdResult loggedUserIdResult, ref List<GetMyRolesResult> userRoles, int stepId, int fillId, string login, int? phStepId)
        {
            WipStepDetailsResponse wipStepDetailsResponse = new WipStepDetailsResponse();

            GetTaskDetailResult taskData = null;
            TaskItemPart taskItemPart = null;
            TaskLog taskLog = null;

            wipStepDetailsResponse.LoginId = login;
            wipStepDetailsResponse.StepId = stepId;
            wipStepDetailsResponse.FillId = fillId;
            wipStepDetailsResponse.WorkOrderDetailsResponse.FillId = fillId;

            using (IDbConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                DynamicParameters refactorGetStepDetailsParameters = new DynamicParameters();

                refactorGetStepDetailsParameters.Add("@STRNTLOGIN", login, DbType.String, ParameterDirection.Input);
                refactorGetStepDetailsParameters.Add("@STEPID", stepId, DbType.String, ParameterDirection.Input);
                refactorGetStepDetailsParameters.Add("@PHSTEPID", phStepId, DbType.String, ParameterDirection.Input);
                refactorGetStepDetailsParameters.Add("@FILLID", fillId, DbType.String, ParameterDirection.Input);
                refactorGetStepDetailsParameters.Add("@PROCSTEPID", phStepId, DbType.String, ParameterDirection.Input);

                using (var gridReader = sqlConnection.QueryMultiple("WIP_GET_STEP_DETAILS", refactorGetStepDetailsParameters, commandType: CommandType.StoredProcedure))
                {

                    // TASK DESCRIPTION TAB DETAILS

                    // PORTAL_GETSTEPDETAILS SPROC - 1ST RESULT SET

                    wipStepDetailsResponse.TaskEditDataResult = gridReader.Read<TaskEditDataResult>().Single();

                    gridReader.Read();

                    gridReader.Read();

                    // A_SP_TASKS_FIND_FOR_PURCHASE_ITEM_AND_ACT_PART - 4TH RESULT SET

                    wipStepDetailsResponse.TaskItemParts = gridReader.Read<TaskItemPart>().ToList();

                    wipStepDetailsResponse.ParentId = wipStepDetailsResponse.TaskItemParts.Select(x => x.PARENT_ID.ToString()).FirstOrDefault();

                    // A_SP_PROCEDURE_GET_REFERENCE_FILES - 5TH RESULT SET

                    wipStepDetailsResponse.ReferenceFiles = gridReader.Read<GetReferenceFiles>().ToList();

                    // A_V_THEORY_APPROVED_DATA QUERY

                    wipStepDetailsResponse.ReferenceTheories = gridReader.Read<GetReferenceTheories>().ToList();

                    // MONITOR TAB DETAILS
                    // A_SP_MONITOR_TEMPLATES_GET_DATA_FOR_OBJECT

                    taskItemPart = wipStepDetailsResponse.TaskItemParts.FirstOrDefault();

                    // if ((task.Status == "REQUESTED" || task.Status == "ACCEPTED" || task.Status == "PENDING_PARENT_ACCEPTANCE" || task.Status == "CLOSED") && task.Print_Order.HasValue && task.HAS_MONITOR.HasValue && task.HAS_MONITOR == 1)
                    // {

                    wipStepDetailsResponse.MonitorTemplateResult = gridReader.Read<MonitorTemplateResult>().ToList();

                    // }

                    // TASK DATA

                    taskData = gridReader.Read<GetTaskDetailResult>().SingleOrDefault();

                    // A_SP_ACTUAL_PARTS_SHOW_HIERARCHY

                    taskItemPart.GetActualPartsShowHierarchys = gridReader.Read<GetActualPartsShowHierarchy>().ToList();

                    // EXEC DBO.PORTAL_GETPURCHASEITEMDETAILS - 1ST RESULT SET

                    wipStepDetailsResponse.WorkOrderDetailsResponse.FileSearchResult = gridReader.Read<FileSearchResult>().SingleOrDefault();

                    // EXEC DBO.PORTAL_GETPURCHASEITEMDETAILS - 2ND RESULT SET

                    wipStepDetailsResponse.WorkOrderDetailsResponse.Parts = gridReader.Read<string>().ToList();

                    // EXEC DBO.PORTAL_GETPURCHASEITEMDETAILS - 3RD RESULT SET

                    wipStepDetailsResponse.WorkOrderDetailsResponse.TaskStepResults = gridReader.Read<TaskStepResult>().Where(x => x.PRINT_ORDER != null).ToList();

                    // PORTAL_WORKITEMIMAGESBYID - REFACTOR _orderService.GetOrderItemImagesById(stepId.ToString());

                    wipStepDetailsResponse.DocLinkImages = gridReader.Read<WorkOrderImageView>().ToList();

                    // CURRENT SENSOR VALUES

                    wipStepDetailsResponse.SensorDataModels = gridReader.Read<SensorDataModel>().ToList();

                    // SELECT * FROM DBO.PORTAL_EQUIPMENTMAINTENANCEVIEW

                    wipStepDetailsResponse.EquipmentMaintenanceView = gridReader.Read<EquipmentMaintenanceView>().ToList();

                    // TASK LOG FOR TIMES

                    taskLog = gridReader.Read<TaskLog>().SingleOrDefault();

                }

                TaskStepResult currentStep = wipStepDetailsResponse.WorkOrderDetailsResponse.TaskStepResults.SingleOrDefault(x => x.StepId == stepId.ToString());

                wipStepDetailsResponse.ParentPartId = wipStepDetailsResponse.WorkOrderDetailsResponse.FileSearchResult.FillObjectId.ToString();

                wipStepDetailsResponse.LoggedUserIdResult = loggedUserIdResult;
                wipStepDetailsResponse.TaskEditDataResult.StepTitle = currentStep.Title;
                wipStepDetailsResponse.TaskEditDataResult.Description = currentStep.Description;

                // USER PERMISSIONS PROCESSING

                var currentStepRequiredRoles = currentStep.Roles?.Split(',');

                foreach (var personRole in userRoles)
                {
                    if (currentStepRequiredRoles.Any(x => x == personRole.Role_Id))
                    {
                        if (personRole.StartDate.HasValue && personRole.EndDate.HasValue)
                        {
                            if (DateTime.Today.Date >= personRole.StartDate.Value.Date && DateTime.Today.Date < personRole.EndDate.Value.Date.AddDays(1))
                            {
                                wipStepDetailsResponse.HasStepRoles = true;
                                break;
                            }
                        }
                        else
                        {
                            wipStepDetailsResponse.HasStepRoles = true;
                            break;
                        }
                    }
                }

                // SETUP THE MONITOR TEMPLATES

                // PERF IMPROVEMENT - ADDED IF .ANY CHECK - DON'T GET THE SENSOR VALUES IF THERE AREN'T ANY MONITORS

                if (wipStepDetailsResponse.MonitorTemplateResult != null && wipStepDetailsResponse.MonitorTemplateResult.Any() == true)
                {
                    foreach (var monitorTemplate in wipStepDetailsResponse.MonitorTemplateResult)
                    {
                        monitorTemplate.FillId = fillId;
                        monitorTemplate.Step_Id = stepId.ToString();
                        monitorTemplate.Ph_Step_Id = phStepId.ToString();
                        monitorTemplate.SensorDataModels = wipStepDetailsResponse.SensorDataModels;

                        monitorTemplate.Setup(wipStepDetailsResponse.EquipmentMaintenanceView);

                        for (int i = 0; i < monitorTemplate.FailActionList.Count; i++)
                        {
                            if (monitorTemplate.FailActionList[i].Value == monitorTemplate.Fail_Action)
                            {
                                monitorTemplate.FailActionList[i].Selected = true;
                            }
                        }
                    }
                }

                // ADDITIONAL MONITOR DATA PROCESSING
                // TO DO: CONFIRM WITH MIKE WE DON'T NEED THIS - HE MENTIONED BEFORE THAT THE MULTI CHOICE MONITORS ARE NOT BEING USED

                //if (wipStepDetailsResponse.MonitorTemplateResult.Any())
                //{
                //    foreach (var monitorTemplate in wipStepDetailsResponse.MonitorTemplateResult)
                //    {
                //        monitorTemplate.FillId = fillId;

                //        //monitorTemplate.MonitorTemplateMultiChoices = sqlConnection
                //        //    .Query<MonitorTemplateMultiChoiceResult>(
                //        //        @"SELECT TXT AS Text, IS_ANSWER AS IsAnswer FROM A_MONITOR_TEMPLATES_MULT_CHOICE WHERE MONITOR_ID = @monitorId  ORDER BY ORD",
                //        //        new { monitorId = monitorTemplate.Id })
                //        //    .Select(x => new SelectListItem
                //        //    {
                //        //        Value = x.Id,
                //        //        Text = x.Text
                //        //    }).ToList();

                //    }
                //}

            }

            // ADDITIONAL TASK DATA PROCESSING

            foreach (var stepTaskItemPart in wipStepDetailsResponse.TaskItemParts)
            {

                stepTaskItemPart.GetActualPartsShowHierarchys = taskItemPart.GetActualPartsShowHierarchys;

            }

            if (taskData != null)
            {
                taskItemPart.IsEditable = false;

                if (taskData.Status == "ACCEPTED" && taskData.REQUESTEE_ID == login)
                {
                    taskItemPart.IsEditable = true;
                }
                else if (taskData.Status == "REQUESTED" && taskData.GROUP_REQUESTEE_ID == login) // Need to check this GroupRequesteeId in Roles from session
                {
                    taskItemPart.IsEditable = true;
                }
            }

            // ADDITIONAL REFERENCE DOCUMENT PROCESSING

            // TODO: THIS IS ANOTHER DB QUERY, CALL OUTSIDE OF THE CURRENT USING
            // TODO: BETTER YET INCLUDE IN THE ONE DATABASE HIT

            foreach (var referenceTheories in wipStepDetailsResponse.ReferenceTheories.AsQueryable())
            {
                referenceTheories.DocLinks = _documentFilesService.GetDocByObjectId(referenceTheories.ObjectId);
            }

            // IMAGE PROCESSING

            wipStepDetailsResponse.Images = new ImageViewModel
            {
                FillId = fillId,
                TaskId = stepId,
                ActualPartId = wipStepDetailsResponse.ParentPartId
            };

            var dockLinks = wipStepDetailsResponse.DocLinkImages.Select(itemImage => new DocLink
            {
                SERVER_PATH = itemImage.Path,
                CONTENTTYPE = itemImage.ContentType,
                LINKED_DOC_ID = itemImage.Id,
                NAME = itemImage.FILE_NAME
            }).ToList();

            wipStepDetailsResponse.Images.PreviewConfig = FileInputConfigHelper.GetPreviewConfigValue(dockLinks, "/Doc/DeleteImageById", "/Doc/Download");

            wipStepDetailsResponse.Images.Preview = FileInputConfigHelper.GetPreviewValue(dockLinks, _documentFilesService);

            wipStepDetailsResponse.HasPreviousStepCompleted = HasPreviousStepCompleted(wipStepDetailsResponse.TaskItemParts, stepId);

            // TO DO!!!!

            // var taskLog = _dbContext.TaskLogs.FirstOrDefault(x => x.TaskId == stepId && x.FillId == fillId);

            wipStepDetailsResponse.TaskRunningDto = GetTotalTime(taskLog);

            return wipStepDetailsResponse;
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
                    // DONE
                    detailsResponse.TaskEditDataResult = multi.Read<TaskEditDataResult>().Single();
                }

                var taskLog = _dbContext.TaskLogs.FirstOrDefault(x => x.TaskId == stepId && x.FillId == fillId);

                // TO DO
                detailsResponse.TaskRunningDto = GetTotalTime(taskLog);

                var p1 = new DynamicParameters();

                p1.Add("@fillID", fillId, DbType.String, ParameterDirection.Input);
                p1.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input);

                using (var multi = conn.QueryMultiple("A_SP_TASKS_FIND_FOR_PURCHASE_ITEM_AND_ACT_PART", p1, commandType: CommandType.StoredProcedure))
                {
                    // DONE
                    detailsResponse.TaskItemParts = multi.Read<TaskItemPart>().ToList();
                }

                // DONE
                detailsResponse.ParentId = detailsResponse.TaskItemParts.Select(x => x.PARENT_ID.ToString()).FirstOrDefault();

                var p2 = new DynamicParameters();

                p2.Add("@procStepID", phStepId, DbType.String, ParameterDirection.Input);
                p2.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input);

                // DONE
                detailsResponse.ReferenceFiles = conn.Query<GetReferenceFiles>("A_SP_PROCEDURE_GET_REFERENCE_FILES", p2, commandType: CommandType.StoredProcedure).ToList();

                // DONE
                detailsResponse.ReferenceTheories = conn.Query<GetReferenceTheories>("SELECT l.THEORY_ID AS TheoryId,t.NAME AS TheoryName,OBJECT_ID AS ObjectId FROM A_PROCEDURE_STEP_THEORY_LINK l, A_V_THEORY_APPROVED_DATA t WHERE l.THEORY_ID = t.ID AND l.PROC_STEP_ID = @stepId", new { stepId = phStepId }, commandType: CommandType.Text).ToList();

                // TODO: CONFIRM WE DON'T NEED THIS
                // WE JUST GOT THE REF THEORIES IN THE STEP ABOVE -- CONFIRM STEP ID'S ARE THE SAME
                // LOOKS LIKE A DOUBLE QUERY SAME THING AS ABOVE BASICALLY

                foreach (var item in detailsResponse.TaskItemParts)
                {
                    if (item.Print_Order.HasValue && item.STEP_ID == stepId.ToString())
                    {
                        detailsResponse.ReferenceTheories = conn.Query<GetReferenceTheories>($"SELECT l.THEORY_ID AS TheoryId,t.NAME AS TheoryName,OBJECT_ID AS ObjectId FROM A_PROCEDURE_STEP_THEORY_LINK l, A_V_THEORY_APPROVED_DATA t WHERE l.THEORY_ID = t.ID AND l.PROC_STEP_ID ='{item.OldStepId}'").ToList();
                    }
                }

                // DONE

                foreach (var referenceTheories in detailsResponse.ReferenceTheories.AsQueryable())
                {
                    referenceTheories.DocLinks = _documentFilesService.GetDocByObjectId(referenceTheories.ObjectId);
                }

                if (detailsResponse.TaskEditDataResult.Status != "FINISHED")
                {

                    // PERF IMPROVEMENT - ADDED x.STEP_ID == stepId.ToString() - FUNC IS TO GET THIS SPECIFIC STEP DETAILS
                    // WHY ARE WE GETTING ALL OF THEM STEPS AND PROCESSING ALL OF THE STEPS?  DIDN'T SEEM TO BREAK ANYTHING

                    foreach (var task in detailsResponse.TaskItemParts.Where(x => x.STEP_ID == stepId.ToString() && x.Print_Order != null))
                    {
                        if ((task.Status == "REQUESTED" || task.Status == "ACCEPTED" ||
                             task.Status == "PENDING_PARENT_ACCEPTANCE" || task.Status == "CLOSED")
                            && task.Print_Order.HasValue && task.HAS_MONITOR.HasValue && task.HAS_MONITOR == 1)
                        {
                            var monitorParams = new DynamicParameters();

                            monitorParams.Add("@RELATED_OBJECT_ID", null, DbType.String, ParameterDirection.Input);
                            monitorParams.Add("@PROCEDURE_STEP_ID", null, DbType.String, ParameterDirection.Input);
                            monitorParams.Add("@TASK_ID", task.STEP_ID, DbType.String, ParameterDirection.Input);
                            monitorParams.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input);

                            if (task.STEP_ID == detailsResponse.StepId.ToString())
                            {

                                using (var multi = conn.QueryMultiple("A_SP_MONITOR_TEMPLATES_GET_DATA_FOR_OBJECT", monitorParams, commandType: CommandType.StoredProcedure))
                                {
                                    detailsResponse.MonitorTemplateResult = multi.Read<MonitorTemplateResult>().ToList();
                                }

                                if (detailsResponse.MonitorTemplateResult.Any())
                                {
                                    foreach (var monitorTemplate in detailsResponse.MonitorTemplateResult)
                                    {
                                        monitorTemplate.FillId = fillId;
                                        monitorTemplate.MonitorTemplateMultiChoices = conn
                                            .Query<MonitorTemplateMultiChoiceResult>(
                                                @"SELECT TXT AS Text, IS_ANSWER AS IsAnswer FROM A_MONITOR_TEMPLATES_MULT_CHOICE WHERE MONITOR_ID = @monitorId  ORDER BY ORD",
                                                new { monitorId = monitorTemplate.Id })
                                            .Select(x => new SelectListItem
                                            {
                                                Value = x.Id,
                                                Text = x.Text
                                            }).ToList();
                                    }
                                }
                            }
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

                detailsResponse.HasPreviousStepCompleted = HasPreviousStepCompleted(detailsResponse.TaskItemParts, stepId);

            }

            return detailsResponse;
        }

        public ResultNotification<string> UpdateStepMonitor(MonitorTemplateResult request)
        {
            var response = new ResultNotification<string>();

            try
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
                p.Add("@SENSOR_MAPPING_ID", request.SensorMappingID, DbType.Int32, ParameterDirection.Input);
                p.Add("@strNTLogin", request.StrNtLogin, DbType.String, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    int i = conn.Execute("Portal_StepSaveMonitor", p, commandType: CommandType.StoredProcedure);

                    if (i == 0)
                    {
                        response.AddError("There is an error updating monitor");
                    }
                }
            }
            catch (Exception exception)
            {
                response.AddError(exception.Message);
            }

            return response;
        }

        public string CloseTask(string taskId, string login, int fillId)
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

                if (retStatus != null && !retStatus.Contains("ERROR") || retStatus == null)
                {
                    StopTimer(taskId, fillId);
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

        public ResultNotification<TaskLogDto> StepStart(int stepId, string login, int fillId, string parentId)
        {
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                var fill_Id = new SqlParameter("@fillID", fillId.ToString());
                var ntLogin = new SqlParameter("@strNTLogin", login);

                var result = _dbContext.Database.SqlQuery<TaskItemPart>("EXEC A_SP_TASKS_FIND_FOR_PURCHASE_ITEM_AND_ACT_PART @fillID, @strNTLogin", fill_Id, ntLogin).FirstOrDefault();

                if (result.Status == "REQUESTED" && result.HAS_CHILD != null && !result.Print_Order.HasValue)
                {
                    ////Start parent procedure step
                    var response = StepStart(Convert.ToInt32(result.STEP_ID), login, fillId);

                    if (response.HasErrors())
                    {
                        return response;
                    }
                }
            }

            return StepStart(stepId, login, fillId);
        }

        private ResultNotification<TaskLogDto> StepStart(int stepId, string login, int fillId)
        {
            var response = new ResultNotification<TaskLogDto>();

            var p = new DynamicParameters();

            p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            p.Add("@ID", stepId.ToString(), DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult =
                    conn.Query<StepStartTaskResult>("A_SP_TASK_ACCEPT", p, commandType: CommandType.StoredProcedure);

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

        public ResultNotification<TaskLogDto> StepDone(int stepId, string login, int fillId, int parentPartId, bool isSerilizeStep)
        {
            var response = new ResultNotification<TaskLogDto>();

            var p3 = new DynamicParameters();
            p3.Add("@firstTime", null, DbType.String, ParameterDirection.Input);
            p3.Add("@strAPart", parentPartId, DbType.String, ParameterDirection.Input);
            p3.Add("@strListToexpand", parentPartId, DbType.String, ParameterDirection.Input);
            p3.Add("@strExpandAllList", parentPartId, DbType.String, ParameterDirection.Input);
            p3.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var result = conn.Query<GetActualPartsShowHierarchy>("A_SP_ACTUAL_PARTS_SHOW_HIERARCHY", p3,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (result != null)
                {
                    UpdateRootPart(result.Id, result.Serial, stepId, login);
                }
            }

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

                if (status != null && !status.Contains("ERROR") || status == null)
                {
                    response.Entity = StopTimer(stepId.ToString(), fillId);
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

        public void CancelUnfinishedSteps(int fillId, string login, int invoice)
        {
            var p = new DynamicParameters();

            p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            p.Add("@FILL_ID", fillId.ToString(), DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);
            p.Add("@InvoiceForWO", invoice, DbType.Int32, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var stepStartDoneTaskResult = conn.Query<StepStartTaskResult>("A_SP_FILL_CANCEL_UNFINISHED_STEPS", p, commandType: CommandType.StoredProcedure);
                var status = p.Get<string>("RET_STATUS");
                var msg = p.Get<string>("MSGS");
            }
        }

        public ResultNotification<bool> TakeOverPO(int fillId, string login)
        {
            var ressult = new ResultNotification<bool>();

            var fill_Id = new SqlParameter("@fillID", fillId);
            var ntLogin = new SqlParameter("@strNTLogin", login);
            var result = _dbContext.Database.SqlQuery<TaskItemPart>("exec A_SP_TASKS_FIND_FOR_PURCHASE_ITEM_AND_ACT_PART @fillID,@strNTLogin", fill_Id, ntLogin).ToList();
            var parentTask = result.Where(x => x.HAS_CHILD.HasValue && x.HAS_CHILD == 1).FirstOrDefault();
            var p = new DynamicParameters();

            foreach (var item in result.Where(x => x.Status != "CLOSED"))
            {
                p.Add("@RET_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@MSGS", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                p.Add("@ID", item.STEP_ID, DbType.String, ParameterDirection.Input, 50);
                p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, 50);
                p.Add("@ParentTaskId", parentTask.STEP_ID, DbType.String, ParameterDirection.Input, 50);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    conn.Query<StepStartTaskResult>("Portal_TakeOverPO", p, commandType: CommandType.StoredProcedure);
                    var retStatus = p.Get<string>("RET_STATUS");
                    var msg = p.Get<string>("MSGS");

                    if (!string.IsNullOrWhiteSpace(retStatus))
                    {
                        ressult.AddError(retStatus);
                        break;
                    }
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

        public ResultNotification<StepStartTaskResult> UpdateRootPart(int partId, string serial, int taskId, string login)
        {
            var result = new ResultNotification<StepStartTaskResult>();
            try
            {
                var p = new DynamicParameters();

                p.Add("@ID", partId, DbType.String, ParameterDirection.Input, size: 500);
                p.Add("@taskID", taskId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@SN", serial, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@strNTLogin", login, DbType.String, ParameterDirection.Input, size: 50);

                using (IDbConnection conn =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    var stepStartDoneTaskResult =
                        conn.Query<StepStartTaskResult>("A_SP_ACTUAL_PART_UPDATE_SERIAL_FROM_SERIALIZE_TASK", p,
                            commandType: CommandType.StoredProcedure);
                }
                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
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

        private TaskLogDto StopTimer(string taskId, int fillId)
        {
            var taskLog = _dbContext.TaskLogs
            .Where(x => x.TaskId.ToString() == taskId && x.FillId == fillId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefault();

            if (taskLog == null) return null;

            if (taskLog.StatusId == TimerStatuseConstants.InProgress)
            {
                taskLog.EndTime = DateTime.Now;
                taskLog.TotalTime = TimeSpan.FromSeconds((taskLog.EndTime.Value - taskLog.StartTime).TotalSeconds);

                if (taskLog.TotalTime.Days > 0)
                {
                    taskLog.TotalTime = new TimeSpan(0, 23, 59, 59, 0);
                }
            }

            taskLog.StatusId = TimerStatuseConstants.Stopped;

            _dbContext.SaveChanges();

            return GetTotalTime(taskLog);
        }

        private bool HasPreviousStepCompleted(List<TaskItemPart> taskItemParts, int currentStepId)
        {
            const int firstStep = 1;

            var currentStep = taskItemParts.SingleOrDefault(x => x.STEP_ID == currentStepId.ToString());

            if (currentStep.Print_Order == firstStep)
            {
                return !IsDone(currentStep.Status);
            }

            var previousStep = taskItemParts
                .Where(x => x.Print_Order < currentStep.Print_Order)
                .OrderByDescending(x => x.Print_Order).FirstOrDefault();

            return IsDone(previousStep.Status);
        }

        private bool IsDone(string status)
        {
            return status == WorkItemStatusConstants.Finished || status == WorkItemStatusConstants.Closed;
        }
    }
}
