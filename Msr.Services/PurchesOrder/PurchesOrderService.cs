using Msr.Models.PurchesOrder;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Msr.Services.PurchesOrder.ViewModels;
using Msr.Services.PurchesOrder.Procedures;
using EntityFrameworkExtras.EF6;
using Msr.Models.Comman;
using System.Data.SqlClient;
using System.Data;
using Msr.Models.Orders;
using Msr.Models.Tasks;
using Msr.Models.Workflows;
using Msr.Services.Users.Messages;
using Msr.Models.Products;

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

        public IQueryable<PurchaseView> GetPurchaseViewQueryable()
        {
            return _dbContext.PurchaseViews;
        }

        public PurchesOrderView GetById(string id)
        {
            return GetPurchesOrderQueryable().SingleOrDefault(x => x.ObjectId == id);
        }

        public List<SelectFile> GetCompaniesList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT TOP 500 NAME,ID FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = ID OR ROOT_CO_ID = 2 )").ToList();

            return result;
        }

        public List<ProductsSearchDataView> GetCompanyProducts(string customerId, string supplierId)
        {
            if (!string.IsNullOrWhiteSpace(customerId) && !string.IsNullOrWhiteSpace(supplierId))
            {
                var products = _dbContext.ProductsSearchDataView
                    .Where(x => x.CustomerId == customerId && x.SupplierId == supplierId)
                    .Where(x => x.Status == WorkflowStatusConstants.Approved ||
                                x.Status == WorkflowStatusConstants.ApprovedButRevisiing).ToList();

                return products;
            }
            return new List<ProductsSearchDataView>();
        }

        public List<string> GetProductsById(string accountObjId)
        {
            var result = _dbContext.Database.SqlQuery<string>($"SELECT ORDER_ID FROM A_V_ACCOUNTS_PURCHASABLE_ORDERS WHERE ACCOUNT_OBJECT_ID = '{accountObjId}' ORDER BY NAME").ToList();

            return result;
        }


        public List<SelectFile> PurchasedOrderProducts(string id)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"SELECT ORDER_ID AS ID,NAME FROM A_V_ACCOUNTS_PURCHASABLE_ORDERS WHERE ACCOUNT_OBJECT_ID = '{id}' ORDER BY NAME").ToList();

            return result;
        }

        public PurchaseFormAccountViewModel AccountPurchaseOrderById(string id)
        {
            var result = _dbContext.Database.SqlQuery<PurchaseFormAccountViewModel>($"SELECT * FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = '{id}'").FirstOrDefault();

            return result;
        }

        public List<PurchasePoViewModel> PurchasedOrderProductsPoList(string id)
        {
            var result = _dbContext.Database.SqlQuery<PurchasePoViewModel>($"SELECT * FROM A_V_ACCOUNTS_PURCHASABLE_ORDERS WHERE ACCT_ID = '{id}' ORDER BY NAME").ToList();

            return result;
        }

        public PurchasePoModel PurchasedOrderById(string id)
        {
            var result = _dbContext.Database.SqlQuery<PurchasePoModel>($"SELECT * FROM A_O_PURCHASES WHERE OBJECT_ID = '{id}'").SingleOrDefault();

            return result;
        }
        public List<FillTasks> PurchasedOrderFillTasksById(string id)
        {
            var result = _dbContext.Database.SqlQuery<FillTasks>($"SELECT * FROM A_V_FILL_TASKS WHERE PURCH_ITEM_ID IN (SELECT ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = '{id}')").ToList();

            return result;
        }
        public List<SelectFile> PurchasedOrderOwnerList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"SELECT DISTINCT TOP 500 NAME AS Name,ID AS Id FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = '2' OR ROOT_CO_ID = ID ) AND (( NAME LIKE '%%' AND NAME LIKE '%%' ) )").ToList();

            return result;
        }
        public List<SelectFile> PurchasedOrderLocationList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT ID, [NAME] FROM A_V_LOCATIONS_APPROVED_DATA_QUICK WHERE PARENT_LOCATION IS NULL ORDER BY NAME").ToList();

            return result;
        }
        public PurchaseApprovedData PurchasedApprovedDataById(string id)
        {
            var sql = $"SELECT* FROM A_V_PURCHASES_APPROVED_DATA WHERE HISTORY_REF_ID = '{id}'";

            var result = _dbContext.Database.SqlQuery<PurchaseApprovedData>(sql).SingleOrDefault();

            return result;
        }

        public PurchaseApprovedData PurchasedApprovedDataByObjId(string objectId)
        {
            var sql = $"SELECT * FROM A_V_PURCHASES_APPROVED_DATA WHERE HISTORY_REF_ID = " +
                      $"(SELECT TOP 1 PURCHASE_HIST_ID FROM A_TASK_ORDER_INFORMATION " +
                      $"WHERE TASK_ID IN(SELECT  TOP 1 TASK_ID FROM A_V_FILL_TASKS " +
                      $"WHERE PURCH_ITEM_ID IN(SELECT ID FROM A_ORDER_ITEMS " +
                      $"WHERE PURCHASE_HIST_ID IN(SELECT OBJ_ID FROM A_OBJECTS WHERE ID = '{objectId}'))))";

            var result = _dbContext.Database.SqlQuery<PurchaseApprovedData>(sql).SingleOrDefault();

            return result;
        }

        public List<PurchaseOrderFillViewModel> PurchasedOrderSearchTasks(string id, string ntLogin)
        {
            var result = _dbContext.Database.SqlQuery<Task>($"EXEC A_SP_TASKS_SEARCH NULL,'S','(ID IN (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = ''{id}'') OR ID IN (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ID IN (SELECT ID FROM A_FILLS WHERE PURCH_ITEM_ID IN (SELECT ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = ''{id}''))))',NULL,NULL,NULL,' ORDER BY COLOR_CODE,SORT_ID DESC','{ntLogin }'").ToList();

            var ids = result.Where(x => x.FILL_ID != null).Select(x => x.FILL_ID).ToArray();
            var str = string.Join(",", ids);

            if (string.IsNullOrEmpty(str))
            {
                return new List<PurchaseOrderFillViewModel>();
            }

            var sql = $"SELECT * FROM A_V_FILLS_SEARCH WHERE ID IN(" + str + ") AND FILL_OBJ_ID IS NULL and QTY_NEEDS_FILLING > 0";

            var fills = _dbContext.Database.SqlQuery<PurchaseOrderFill>(sql).Select(x => new PurchaseOrderFillViewModel()
            {
                CUST_NAME = x.CUST_NAME,
                SUP_NAME = x.SUP_NAME,
                OBJ_DESC = x.OBJ_DESC,
                FULL_NAME = x.FULL_NAME,
                PRODUCT_ID = x.PRODUCT_ID,
                PURCHASER_ID = x.PURCHASER_ID,
                PROC_NAME = x.PROC_NAME,
                PROC_ID = x.PROC_ID,
                SYS_PROC_ID = x.SYS_PROC_ID,
                APP_OBJ_DESC = x.APP_OBJ_DESC,
                PRICING_TABLE_ID = x.PRICING_TABLE_ID,
                CUSTOMER = x.CUSTOMER,
                SUPPLIER = x.SUPPLIER,
                PURCHASE_QTY = x.PURCHASE_QTY,
                UNIT_PRICE = x.UNIT_PRICE,
                TOTAL_PRICE = x.TOTAL_PRICE,
                DEST = x.DEST,
                FROM_LOC = x.FROM_LOC,
                TO_LOC = x.TO_LOC,
                PURCHASE_HIST_ID = x.PURCHASE_HIST_ID,
                ACCOUNT_ID = x.ACCOUNT_ID,
                TOT_QTY = x.TOT_QTY,
                PARENT_QTY = x.PARENT_QTY,
                WEIGHT = x.WEIGHT,
                WEIGHT_UNIT = x.WEIGHT_UNIT,
                OBJ_PROD_APPLIES_TO = x.OBJ_PROD_APPLIES_TO,
                PROCEDURE_HIST_ID = x.PROCEDURE_HIST_ID,
                STEPS_IN_AP = x.STEPS_IN_AP,
                PROD_HIST_ID = x.PROD_HIST_ID,
                CUSTOMER_PERSON = x.CUSTOMER_PERSON,
                PURCHASE_ID = x.PURCHASE_ID,
                ID = x.ID,
                PURCH_ITEM_ID = x.PURCH_ITEM_ID,
                FILL_BY = x.FILL_BY,
                FILL_OBJ_ID = x.FILL_OBJ_ID,
                FILL_QTY = x.FILL_QTY,
                FILLER = x.FILLER,
                TASK_ID = x.TASK_ID,
                QTY_FILLED = x.QTY_FILLED,
                QTY_NEEDS_FILLING = x.QTY_NEEDS_FILLING,
                SUB_FILL_FOR = x.SUB_FILL_FOR,
                FILL_DATE = x.FILL_DATE,
                PURCHASE_ITEM_PARENT_ID = x.PURCHASE_ITEM_PARENT_ID,
                PROD_PRICE_LIST = x.PROD_PRICE_LIST,
                CUST_LINE_ITEM = x.CUST_LINE_ITEM,
                BATCH_FILL = x.BATCH_FILL,
                BATCH_PARENT = x.BATCH_PARENT,
                GroupWO = x.GroupWO
            }).ToList();

            return fills;
        }


        public List<PurchasePoModel> PurchasedOrderOrderItems(string id)
        {
            var accName = "";
            var result = _dbContext.Database.SqlQuery<PurchasePoModel>(
                "SELECT v.*, aoi.MATERIAL_TRANSFER_TICKET_NUMBER as MATERIAL_TRANSFER_TICKET_NUMBER " +
                "FROM A_V_ORDER_ITEMS_ALL_DATA v " +
                "LEFT JOIN A_ORDER_ITEMS aoi ON (v.id = aoi.id) " +
                $"WHERE v.PURCHASE_HIST_ID = '{id}' AND v.PARENT IS NULL "
            ).ToList();

            foreach (var item in result)
            {
                item.UNIT_PRICE = Math.Round((decimal)item.TOTAL_PRICE / Convert.ToDecimal(item.QTY), 2);
                item.TOTAL_PRICE = Math.Round((decimal)item.TOTAL_PRICE, 2);

                if (item.CycleTime.HasValue)
                {
                    item.DUE_DATE = DateTime.Now.AddDays(item.CycleTime.Value);
                }

                if (item.ACCT_NAME != null)
                {
                    accName = item.ACCT_NAME;
                }
            }
            foreach (var item in result)
            {
                item.ACCT_NAME = accName;
            }
            return result;
        }


        public List<SelectFile> PurchasedOrderPoDropDownList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"SELECT DISTINCT TOP 500 NAME as Name,ORDER_ID as Id FROM A_V_ORDERS_LOOK_UP_FOR_ACCOUNT WHERE ( CUSTOMER_CO = '2' OR CUSTOMER_CO = '2' ) AND (( NAME LIKE '%%' AND NAME LIKE '%%' ) ) ORDER BY NAME").ToList();

            return result;
        }
        public List<SelectFile> PurchasedOrderPoAcctForAllList(LoggedUserIdResult currentUser)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"SELECT DISTINCT TOP 500 NAME as Name,ID as Id FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ACCT_TYPE IN ('PURCHASING_ACCOUNT','WARRANTY_ACCOUNT') AND (( NAME LIKE '%%' AND NAME LIKE '%%' ) ) ORDER BY NAME").ToList();

            return result;
        }

        public ResultNotification<string> PurchaseOrderWorkFlow(string id, string ntLogin)
        {
            var responsePurchase = new ResultNotification<string>();
            try
            {
                var returnVal = new SqlParameter("@returnVal", ParameterDirection.Output);
                var messages = new SqlParameter("@messages", ParameterDirection.Output);
                var NewId = new SqlParameter("@objID", id);
                var ntLog = new SqlParameter("@strNTLogin", ntLogin);
                _dbContext.Database.ExecuteSqlCommand("exec A_SP_OBJECT_CHECK_FOR_VALIDITY @returnVal,@messages, @objID,@strNTLogin", returnVal, messages, NewId, ntLog);
                return responsePurchase;
            }
            catch (Exception ex)
            {

                responsePurchase.AddError(ex.Message);

                return responsePurchase;
            }

        }


        public ResultNotification<string> CreatePurchaseSaveUpdate(PurchasePoModel model, string ntLogin)
        {
            var responsePurchase = new ResultNotification<string>();
            try
            {
                _dbContext.Database.ExecuteSqlCommand("UPDATE A_PURCHASES_HISTORY SET  CUST_PURCH_NUM = '" + model.CUST_PURCH_NUM + "', SUP_PURCH_NUM = '" + model.SUP_PURCH_NUM + "', ACCT_FOR_ALL='" + model.ROOT + "' WHERE OBJECT_ID = '" + model.newId + "'");

                var id = new SqlParameter("@pHistID", model.newId);
                var ntLog = new SqlParameter("@strNTLogin", ntLogin);
                _dbContext.Database.ExecuteSqlCommand("exec A_SP_PURCHASES_UPDATE_ALL_ACCOUNTS_ON_PURCHASE_ITEMS @pHistID,@strNTLogin", id, ntLog);

                var nullValue = DBNull.Value;

                for (int i = 0; i < model.OrderItems.Count; i++)
                {
                    var dueDate = new SqlParameter();
                    var qty = new SqlParameter();
                    var custLintItem = new SqlParameter();
                    var materialTransferTicketNumber = new SqlParameter();

                    if (model.OrderItems[i].DUE_DATE == null)
                    {
                        dueDate = new SqlParameter("@DUE_DATE", DBNull.Value);
                    }
                    else
                    {
                        dueDate = new SqlParameter("@DUE_DATE", model.OrderItems[i].DUE_DATE);
                    }

                    if (model.OrderItems[i].QTY == null)
                    {
                        qty = new SqlParameter("@QTY", DBNull.Value);
                    }
                    else
                    {
                        qty = new SqlParameter("@QTY", model.OrderItems[i].QTY);
                    }

                    if (model.OrderItems[i].CUST_LINE_ITEM == null)
                    {
                        custLintItem = new SqlParameter("@CUST_LINE_ITEM", DBNull.Value);
                    }
                    else
                    {
                        custLintItem = new SqlParameter("@CUST_LINE_ITEM", model.OrderItems[i].CUST_LINE_ITEM);
                    }

                    if (string.IsNullOrWhiteSpace(model.OrderItems[i].MATERIAL_TRANSFER_TICKET_NUMBER) == true)
                    {
                        materialTransferTicketNumber = new SqlParameter("@MATERIAL_TRANSFER_TICKET_NUMBER", DBNull.Value);
                    }
                    else
                    {
                        materialTransferTicketNumber = new SqlParameter("@MATERIAL_TRANSFER_TICKET_NUMBER", model.OrderItems[i].MATERIAL_TRANSFER_TICKET_NUMBER);
                    }

                    var orderId = new SqlParameter("@ID", model.OrderItems[i].ID);

                    _dbContext.Database.ExecuteSqlCommand("exec Portal_UpdateOrderItem @DUE_DATE,@QTY,@CUST_LINE_ITEM, @MATERIAL_TRANSFER_TICKET_NUMBER,@ID", dueDate, qty, custLintItem, materialTransferTicketNumber, orderId);
                }

                var checkForValidityProcedure = new PoCheckForValidityProcedure
                {
                    objId = model.ID,
                    strNTLogin = ntLogin
                };
                _dbContext.Database.ExecuteStoredProcedure(checkForValidityProcedure);
            }
            catch (Exception ex)
            {

                responsePurchase.AddError(ex.Message);

                return responsePurchase;
            }
            return responsePurchase;
        }

        public ResultNotification<string> AddPurchasedOrderItem(string id, string orderId, string ntLogin)
        {
            var responsePurchase = new ResultNotification<string>();
            try
            {
                var purchasePoDetailProcedure = new PurchasePoDetailProcedure();
                purchasePoDetailProcedure.strPurchaseObjID = id;
                purchasePoDetailProcedure.orderID = orderId;
                purchasePoDetailProcedure.Strntlogin = ntLogin;
                _dbContext.Database.ExecuteStoredProcedure(purchasePoDetailProcedure);

                return responsePurchase;
            }
            catch (Exception ex)
            {
                responsePurchase.AddError(ex.Message);

                return responsePurchase;
            }

        }

        public ResultNotification<string> PurchasedOrderUpdateAndShowOrderItemList(PurchaseFormAccountViewModel model, string ntLogin)
        {
            var responsePurchase = new ResultNotification<string>();
            int count = 0;
            try
            {

                var orderid = model
                    .ProductPo
                    .Select(x => x.ORDER_ID)
                    .FirstOrDefault();

                var purchasePoProcedure = new PurchasePoProcedure();
                purchasePoProcedure.Orderid = orderid;
                purchasePoProcedure.Strntlogin = ntLogin;

                _dbContext.Database.ExecuteStoredProcedure(purchasePoProcedure);
                responsePurchase.Entity = purchasePoProcedure.NewID;

                _dbContext.Database.ExecuteSqlCommand(
                    "UPDATE A_PURCHASES_HISTORY " +
                    "SET  CUST_PURCH_NUM = '" + model.REFERENCE_PO +
                    "', ACCT_FOR_ALL = '" + model.Root +
                    "' WHERE OBJECT_ID = '" + responsePurchase.Entity + "'"
                );

                var purchasePoViewItemProcedure = new PurchasePoViewItemProcedure();
                var purchasePoDetailProcedure = new PurchasePoDetailProcedure();

                for (int i = 0; i < model.ProductPo.Count; i++) {
                    var poCreations = model.ProductPo[i].GroupWO ? 1 : model.ProductPo[i].Qty;
                    for (var j = 0; j < poCreations; j++) {
                        purchasePoViewItemProcedure.purchObjID = purchasePoProcedure.NewID;

                        if (purchasePoDetailProcedure.NewID == null) {
                            purchasePoViewItemProcedure.orderItemID = null;
                        } else {
                            purchasePoViewItemProcedure.orderItemID = purchasePoDetailProcedure.NewID;
                        }
                        purchasePoViewItemProcedure.qty = model.ProductPo[i].GroupWO ? model.ProductPo[i].Qty.ToString() : "1";
                        purchasePoViewItemProcedure.acctID = model.Root;
                        purchasePoViewItemProcedure.strNTLogin = ntLogin;
                        purchasePoViewItemProcedure.GroupWO = model.ProductPo[i].GroupWO;

                        _dbContext.Database.ExecuteStoredProcedure(purchasePoViewItemProcedure);

                        // The root item (0) is already created, the rest need to be created here.
                        if (count != 0) {
                            purchasePoDetailProcedure.strPurchaseObjID = purchasePoProcedure.NewID;
                            purchasePoDetailProcedure.orderID = model.ProductPo[i].ORDER_ID;
                            purchasePoDetailProcedure.Strntlogin = ntLogin;
                            _dbContext.Database.ExecuteStoredProcedure(purchasePoDetailProcedure);
                        }

                        count++;
                    }
                }

                _dbContext.Database.ExecuteSqlCommand(
                    "UPDATE A_PURCHASES_HISTORY " +
                    "SET  CUST_PURCH_NUM = '" + model.REFERENCE_PO +
                    "', ACCT_FOR_ALL = '" + model.Root +
                    "' WHERE OBJECT_ID = '" + responsePurchase.Entity + "'"
                );

                var purchase = PurchasedOrderById(responsePurchase.Entity);

                var id = new SqlParameter("@pHistID", purchase.ID);
                var ntlog = new SqlParameter("@strNTLogin", ntLogin);
                _dbContext.Database.ExecuteSqlCommand(
                    "exec A_SP_PURCHASES_UPDATE_ALL_ACCOUNTS_ON_PURCHASE_ITEMS " +
                    "@pHistID,@strNTLogin", id, ntlog
                );

            }
            catch (Exception ex)
            {

                responsePurchase.AddError(ex.Message);

                return responsePurchase;
            }
            return responsePurchase;
        }

        public ResultNotification<AddPurchaseResponse> Create(NewPurchaseOrderViewModel model)
        {
            var responsePurchase = new ResultNotification<AddPurchaseResponse> { Entity = new AddPurchaseResponse() };

            try
            {
                var addPurchaseOrderProcedure = new AddPurchaseOrderProcedure
                {
                    ObjId = model.ObjId,
                    Id = model.Id,
                    Name = model.POName,
                    AcctType = model.AccountType,
                    ReferencePO = model.RefCustPO,
                    ReferenceName = model.RefCustPO,
                    SupplierCo = model.SupplierDepartment,
                    CustomerCo = model.Client,
                    CustomerBillCo = model.CustRefNum,
                    OpenDate = model.OpenDate,
                    CloseDate = model.CloseDate,
                    TotalPurchaseLimit = model.TotalPurchaseLimit.ToString(),
                    TaxRate = model.Tax,
                    InvoiceTrigger = model.InvoiceTrigger,
                    InvoicePeriodNumber = model.InvoicePeriod,
                    InvoicePeriodType = model.InvoicePeriodType,
                    FirstInvoiceDate = model.FirstInvoiceDate.ToString(),
                    PaymentGracePeriod = model.GracePeriod.ToString(),
                    LateFeePercentage = model.LatePaymentFee.ToString(),
                    ReapplyLateFee = model.ReApplyFrequency.ToString(),
                    Strntlogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(addPurchaseOrderProcedure);

                _dbContext.Database.ExecuteSqlCommand($"DELETE FROM A_ACCOUNT_PURCHASABLE_ORDERS_LINK WHERE ACCOUNT_ID = '{addPurchaseOrderProcedure.NewID}'");

                if (model.Products != null)
                {
                    foreach (var product in model.Products)
                    {
                        _dbContext.Database.ExecuteSqlCommand($"INSERT INTO A_ACCOUNT_PURCHASABLE_ORDERS_LINK (ID,ACCOUNT_ID,ORDER_ID,DRCM,MODBY) VALUES (newID(),'{addPurchaseOrderProcedure.NewID}','{product}',getDate(),'{model.NTLogin}')");
                    }
                }

                responsePurchase.Entity.NewId = addPurchaseOrderProcedure.NewID;


                var checkForValidityProcedure = new PoCheckForValidityProcedure
                {
                    objId = addPurchaseOrderProcedure.NewID,
                    strNTLogin = model.NTLogin
                };
                _dbContext.Database.ExecuteStoredProcedure(checkForValidityProcedure);

                if (string.IsNullOrWhiteSpace(model.ObjId))
                {
                    responsePurchase.SuccessMessage = "Purchase order has been created successfully.";
                }
                else
                {
                    responsePurchase.SuccessMessage = "Purchase order has been updated successfully.";
                }



                return responsePurchase;

            }
            catch (Exception ex)
            {
                responsePurchase.AddError(ex.Message);

                return responsePurchase;
            }

        }
        public ResultNotification<string> SavePurchaseMultiFill(PurchaseOrderMultiFillViewModel model, string ntLogin)
        {
            var responsePurchase = new ResultNotification<string>();
            try
            {
                foreach (var item in model.FillList)
                {
                    if (string.IsNullOrWhiteSpace(item.LOCATION_ID) == true)
                    {
                        throw new ArgumentNullException("The Site (LOCATION_ID) is required.");
                    }

                    var fillWithPartAndSerialNumberProcedure = new FillWithPartAndSerialNumberProcedure
                    {
                        FillId = item.ID,
                        StrPartId = item.OBJ_PROD_APPLIES_TO,
                        StrSN = item.SERIAL_NUMBER,
                        StrQty = item.PURCHASE_QTY,
                        StrLoc = item.LOCATION_ID,
                        StrOwner = "2",
                        StrNtLogin = ntLogin

                    };
                    _dbContext.Database.ExecuteStoredProcedure(fillWithPartAndSerialNumberProcedure);


                }
                responsePurchase.SuccessMessage = "Success";
                return responsePurchase;
            }
            catch (Exception ex)
            {
                responsePurchase.AddError(ex.Message);

                return responsePurchase;
            }
        }

        public string GetHistId(string loginId, string objectId)
        {
            var sql =
                $"SELECT PURCHASE_HIST_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID IN(SELECT  TOP 1 TASK_ID FROM A_V_FILL_TASKS WHERE PURCH_ITEM_ID IN(SELECT ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID IN(SELECT OBJ_ID FROM A_OBJECTS WHERE ID = '{objectId}')))";
            var result = _dbContext.Database.SqlQuery<string>(sql).FirstOrDefault();

            return result;
        }

        public string CloseAccount(string id, string ntlogin)
        {
            _dbContext.Database.ExecuteSqlCommand($"UPDATE A_ACCOUNTS_HISTORY SET CLOSE_DATE = getDate(), DRCM = getDate(), modby= " + ntlogin + " WHERE OBJECT_ID = " + id + "");

            return "";
        }

    }
}
