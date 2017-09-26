using Msr.Models.PurchesOrder;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Services.PurchesOrder.ViewModels;
using Msr.Services.PurchesOrder.Procedures;
using EntityFrameworkExtras.EF6;
using Msr.Models.Comman;
using Msr.Models.Parts;

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

        public PurchesOrderView GetById(string Id)
        {
            return GetPurchesOrderQueryable().SingleOrDefault(x => x.ObjectId == Id);
        }

        public List<SelectFile> GetCompaniesList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT TOP 500 NAME,ID FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = ID OR ROOT_CO_ID = 2 )").ToList();

            return result;
        }

        public List<ProductsCanPurchase> GetCompinesProducts(string coid, string clientCo)
        {
            var sql =
                $"SELECT DISTINCT TOP 500 * FROM A_V_ORDERS_LOOK_UP_FOR_ACCOUNT WHERE ORDER_ID IS NOT NULL AND (( CUSTOMER_CO LIKE '%{coid}%' ) ) AND (( SUPPLIER_ID LIKE '%{clientCo}%' ) ) ORDER BY NAME";
            var result = _dbContext.Database.SqlQuery<ProductsCanPurchase>(sql).ToList();

            return result;
        }
        public bool Save(NewPurchaseOrderViewModel model)
        {
            try
            {
                var addPurchaseOrderProcedure = new AddPurchaseOrderProcedure
                {
                    ////Client = model.Client,
                    ////ReferencePO = model.POName,
                    AcctType = model.AccountType,
                    ReferencePO = model.RefCustPO,
                    SupplierCo = model.SupplierDepartment.ToString(),
                    CustomerBillCo = model.CustRefNum.ToString(),
                    OpenDate = model.OpenDate.ToString(),
                    CloseDate = model.CloseDate.ToString(),
                    TotalPurchaseLimit = model.TotalPurchaseLimit.ToString(CultureInfo.InvariantCulture),
                    TaxRate = model.Tax.ToString(CultureInfo.InvariantCulture),
                    InvoiceTrigger = model.InvoiceTrigger,
                    InvoicePeriodNumber = model.InvoicePeriod.ToString(),
                    InvoicePeriodType = model.InvoicePeriodType,
                    FirstInvoiceDate = model.FirstInvoiceDate.ToString(),
                    PaymentGracePeriod = model.GracePeriod.ToString(),
                    LateFeePercentage = model.LatePaymentFee.ToString(),
                    ReapplyLateFee = model.ReApplyFrequency.ToString(),
                    //// = model.FromDate,
                    //// = model.ToDate,
                    //// = model.ClientName,
                    Strntlogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(addPurchaseOrderProcedure);

                _dbContext.Database.ExecuteSqlCommand($"DELETE FROM A_ACCOUNT_PURCHASABLE_ORDERS_LINK WHERE ACCOUNT_ID = '{addPurchaseOrderProcedure.NewID}'");
                if (model.Products != null)
                {
                    var productList = model.Products.Split(',');

                    foreach (var product in productList)
                    {
                        _dbContext.Database.ExecuteSqlCommand($"INSERT INTO A_ACCOUNT_PURCHASABLE_ORDERS_LINK (ID,ACCOUNT_ID,ORDER_ID,DRCM,MODBY) VALUES (newID(),'{addPurchaseOrderProcedure.NewID}','{product}',getDate(),'{model.NTLogin}')");

                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
    }
}
