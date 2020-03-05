using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Msr.Repositories;
using Msr.Services.Workflows.Messages;
using Msr.Services.Workflows.ViewModels;

namespace Msr.Services.Workflows
{
    public class WorkflowService
    {
        private readonly MsrDbContext _dbContext;

        public WorkflowService()
        {
            _dbContext = new MsrDbContext();
        }

        public void SpRunAdminSql()
        {
            _dbContext.Database.ExecuteSqlCommand("exec A_SP_ADMIN_SQL_TO_RUN_EXECUTE");
        }

        public ObjectDataResult GetSpObjectGetData(string id, string loginId)
        {
            var sql = string.Format("exec A_SP_OBJECT_GET_DATA '{0}','{1}'", id, loginId);

            var result = _dbContext.Database.SqlQuery<ObjectDataResult>(sql).Single();

            return result;
        }

        public List<ShowApplicableWorkflowsResult> GetSpObjectShowApplicableWorkflows(string id, string loginId)
        {
            var sql = $"exec A_SP_OBJECT_SHOW_APPLICABLE_WORKFLOWS '{id}','{loginId}'";

            var result = _dbContext.Database.SqlQuery<ShowApplicableWorkflowsResult>(sql).ToList();

            return result;
        }

        public BaseNotification SubmitWorkflow(SubmitWorkflowViewModel vm)
        {
            var result = new BaseNotification();

            try
            {
                SpObjectCheckForValidity(vm.ObjectId, vm.LoginId);

                GetSpObjectShowApplicableWorkflow(vm.ObjectId, vm.LoginId);

                var p = new DynamicParameters();

                p.Add("@msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@msg2", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@objID", vm.ObjectId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@wfID", vm.ApprovalWorflowId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@revComment", vm.Comment, DbType.String, ParameterDirection.Input, size: 2000);
                p.Add("@statOnCompletion", vm.CompletionStart, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@allRevs", vm.AllRevs, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@strNTLogin", vm.LoginId, DbType.String, ParameterDirection.Input, size: 50);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    int i = conn.Execute("A_SP_OBJECT_START_WF", p, commandType: CommandType.StoredProcedure);

                    var msg = p.Get<string>("msg");
                    var msg2 = p.Get<string>("msg2");

                    result.SuccessMessage = msg;
                }

                SpRunAdminSql();
            }
            catch (Exception)
            {
            }

            return result;
        }

        public BaseNotification UnLockObjAndDelete(string objectId, string loginId)
        {
            var result = new BaseNotification();

            try
            {
                var p = new DynamicParameters();

                p.Add("@objID", objectId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@strNTLogin", loginId, DbType.String, ParameterDirection.Input, size: 50);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    conn.Execute("A_SP_OBJECT_UNLOCK_AND_DELETE", p, commandType: CommandType.StoredProcedure);
                }

                SpRunAdminSql();
            }
            catch (Exception)
            {
                ////log
            }

            return result;
        }

        public ResultNotification<string> CheckOutObject(string objectId, string loginId)
        {
            var result = new ResultNotification<string>();

            try
            {
                var objectIdParam = new SqlParameter("@objectId", objectId);

                var checkoutView = _dbContext.Database.SqlQuery<CheckoutView>("select status, root from A_OBJECTS where id =@objectId", objectIdParam).FirstOrDefault();

                if (checkoutView.Status != null && checkoutView.Status == "CREATING")
                {
                    result.Entity = objectId;
                    return result;
                }

                if (checkoutView.Status != null && checkoutView.Status == "APPROVED_BUT_REVISING")
                {
                    var rootParam = new SqlParameter("@root", objectId);

                    var id = _dbContext.Database.SqlQuery<string>($"select Id from A_OBJECTS where root =@root AND STATUS='CREATING'", rootParam).FirstOrDefault();
                    result.Entity = id;
                    return result;
                }

                var p = new DynamicParameters();

                p.Add("@newObjID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@objID", objectId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@strNTLogin", loginId, DbType.String, ParameterDirection.Input, 50);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    var i = conn.Execute("A_SP_OBJECT_CHECKOUT", p, commandType: CommandType.StoredProcedure);

                    var newObjId = p.Get<string>("newObjID");

                    result.Entity = newObjId;

                    var sqlMainData = string.Format("exec A_SP_OBJECT_GET_MAIN_DATA '{0}','{1}'", newObjId, loginId);
                    _dbContext.Database.SqlQuery<ObjectDataResult>(sqlMainData).Single();

                    var sqlCheckoutData = string.Format("exec A_SP_OBJECT_CHECKED_TO_ME '{0}','{1}'", newObjId, loginId);
                    _dbContext.Database.SqlQuery<ObjectDataResult>(sqlCheckoutData).Single();

                }

                SpRunAdminSql();
            }
            catch (Exception)
            {
            }

            return result;
        }

        public BaseNotification DeniedWorkflow(DeniedWorkflowViewModel vm)
        {
            var result = new BaseNotification();

            try
            {
                var p = new DynamicParameters();
                p.Add("@newID", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 8000);
                p.Add("@WFS_ID", vm.Wfsid, DbType.String, ParameterDirection.Input, 50);
                p.Add("@WF_GROUP_ID", vm.WfGroupId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@WF_STAGE_ID", vm.WfStageId, DbType.String, ParameterDirection.Input, 2000);
                p.Add("@reason", vm.Reason, DbType.String, ParameterDirection.Input, 50);
                p.Add("@strNTLogin", vm.NtLogin, DbType.String, ParameterDirection.Input, 50);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    int i = conn.Execute("A_SP_APPROVALS_DENY_ONE", p, commandType: CommandType.StoredProcedure);

                    var newID = p.Get<string>("msg");
                    var msg = p.Get<string>("msg2");

                    result.SuccessMessage = msg;
                }

                SpRunAdminSql();
            }
            catch (Exception)
            {
                /// Log;
            }

            return result;
        }

        public ResultNotification<string> GetObjectTable(string objectId)
        {
            var result = new ResultNotification<string>();

            try
            {
                var strId = new SqlParameter("@strID ", objectId ?? "0");

                var objectTable = _dbContext.Database.SqlQuery<string>("EXEC A_SP_OBJECT_GET_TABLE  @strID", strId).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(objectTable))
                {
                    result.AddError("Unable to find object.");
                    return result;
                }

                result.Entity = objectTable;
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }

            return result;
        }

        public void SpObjectCheckForValidity(string id, string ntLogin)
        {
            var returnVal = new SqlParameter("@returnVal", ParameterDirection.Output);
            var messages = new SqlParameter("@messages", ParameterDirection.Output);
            var newId = new SqlParameter("@objID", id);
            var ntLog = new SqlParameter("@strNTLogin", ntLogin);

            _dbContext.Database.ExecuteSqlCommand("exec A_SP_OBJECT_CHECK_FOR_VALIDITY @returnVal,@messages, @objID,@strNTLogin", returnVal, messages, newId, ntLog);
        }

        public void GetSpObjectShowApplicableWorkflow(string id, string ntLogin)
        {
            var newId = new SqlParameter("@objID", id);
            var ntLog = new SqlParameter("@strNTLogin", ntLogin);

            _dbContext.Database.ExecuteSqlCommand("exec A_SP_OBJECT_SHOW_APPLICABLE_WORKFLOWS @objID,@strNTLogin", newId, ntLog);
        }
    }
}
