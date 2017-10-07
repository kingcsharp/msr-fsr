using Msr.Models.PurchesOrder;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Msr.Services.PurchesOrder.ViewModels;
using Msr.Services.PurchesOrder.Procedures;
using EntityFrameworkExtras.EF6;
using Msr.Models.Comman;

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

        public PurchesOrderView GetById(string id)
        {
            return GetPurchesOrderQueryable().SingleOrDefault(x => x.ObjectId == id);
        }

        public List<SelectFile> GetCompaniesList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT TOP 500 NAME,ID FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = ID OR ROOT_CO_ID = 2 )").ToList();

            return result;
        }

        public List<ProductsCanPurchase> GetCompinesProducts(string clientCo, string supplierCo)
        {
            if (!string.IsNullOrWhiteSpace(clientCo)  && !string.IsNullOrWhiteSpace(supplierCo))
            {
                var sql =
                    $"SELECT DISTINCT TOP 500 * FROM A_V_ORDERS_LOOK_UP_FOR_ACCOUNT WHERE ORDER_ID IS NOT NULL AND (( CUSTOMER_CO LIKE '%{clientCo}%' ) ) AND (( SUPPLIER_ID LIKE '%{supplierCo}%' ) ) ORDER BY NAME";
                var result = _dbContext.Database.SqlQuery<ProductsCanPurchase>(sql).ToList();

                return result;
            }
            return new List<ProductsCanPurchase>();
        }

        public List<SelectFile> PurchasedOrderProducts(string id)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"SELECT ORDER_ID AS ID,NAME FROM A_V_ACCOUNTS_PURCHASABLE_ORDERS WHERE ACCOUNT_OBJECT_ID = '{id}' ORDER BY NAME").ToList();

            return result;
        }

        public ResultNotification<AddPurchaseResponse> Create(NewPurchaseOrderViewModel model)
        {
            var responsePurchase = new ResultNotification<AddPurchaseResponse> { Entity = new AddPurchaseResponse() };

            try
            {
                var addPurchaseOrderProcedure = new AddPurchaseOrderProcedure
                {
                    ObjId = model.ObjId,
                    Name = model.POName,
                    AcctType = model.AccountType,
                    ReferencePO = model.RefCustPO,
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
                ////log

                responsePurchase.AddError("There is an error with the request");

                return responsePurchase;
            }
        }
    }
}
