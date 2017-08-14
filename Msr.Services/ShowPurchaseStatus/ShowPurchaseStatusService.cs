using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.ShowPurchaseStatus;
using Msr.Repositories;

namespace Msr.Services.ShowPurchaseStatus
{
    public class ShowPurchaseStatusService
    {
        private readonly MsrDbContext _dbContext;

        public ShowPurchaseStatusService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ShowPurchaseStatusView> GetPurchaseStatusQueryable(string id)
        {
            var fieldList = new SqlParameter("@fieldList", DBNull.Value);
            var alias = new SqlParameter("@alias", "S");
            var strWhere = new SqlParameter("@strWHERE", $"(PURCHASE_HIST_ID = {id ?? "0"})");
            var strPurposes = new SqlParameter("@strPurposes", DBNull.Value);
            var strObjects = new SqlParameter("@strObjects", DBNull.Value);
            var strProjects = new SqlParameter("@strProjects", DBNull.Value);
            var strSort = new SqlParameter("@strSort", "ORDER BY SORT_ID");
            //need to be dynamic
            var strNTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<ShowPurchaseStatusView>("EXEC A_SP_TASKS_SEARCH @fieldList,@alias,@strWHERE,@strPurposes,@strObjects,@strProjects,@strSort,@strNTLogin", fieldList, alias, strWhere, strPurposes, strObjects, strProjects, strSort, strNTLogin).ToList();

            return result.AsQueryable();
        }
        public PurchaseItem GetPurchaseItem(string id)
        {
            var refId = new SqlParameter("@HISTORY_REF_ID", id ?? "0");

            var result = _dbContext.Database.SqlQuery<PurchaseItem>("SELECT CUST_PURCH_NUM AS PurchaseNumber, DESCRIPTION AS Description,PURCHASE_STATUS AS Status FROM A_V_PURCHASES_APPROVED_DATA WHERE HISTORY_REF_ID = @HISTORY_REF_ID", refId).SingleOrDefault() ??
                         new PurchaseItem();
            return result;
        }
    }
}
