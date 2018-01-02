using EntityFrameworkExtras.EF6;
using Msr.Models.Invoices;
using Msr.Repositories;
using Msr.Services.Invoices.Procedures;
using Msr.Services.Invoices.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Msr.Services.Invoices
{
   public class InvoicesService
    {
        private readonly MsrDbContext _dbContext;

        public InvoicesService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<InvoiceView> GetInvoicesQueryable()
        {
            return _dbContext.InvoicesViews;
        }

        public Decimal? GetInvoicesTotalDebitAmount()
        {
            var result = _dbContext.Database.SqlQuery<Decimal?>("SELECT sum(NEW_ITEMS_AMT) AS MY_TOTAL FROM A_V_INVOICES_WITH_ACCT_INFORMATION i WHERE ( CREATING_CO = '2' OR CUSTOMER_CO = '2')  AND ACCT_TYPE = 'PURCHASING_ACCOUNT' AND  (ACCOUNT_ID LIKE '%%' OR ACCOUNT_ID is NULL ) AND  (INVOICE_ID LIKE '%%' OR INVOICE_ID is NULL ) AND  (INVOICE_DATE LIKE '%%' OR INVOICE_DATE is NULL ) AND  (SUPPLIER_NAME LIKE '%%' OR SUPPLIER_NAME is NULL ) AND  (CUSTOMER_NAME LIKE '%%' OR CUSTOMER_NAME is NULL ) AND  SUPPLIER_CO = '2'").SingleOrDefault();

            return result;
        }

        public Decimal? GetInvoicesTotalPaidAmount()
        {
            var result = _dbContext.Database.SqlQuery<Decimal?>("SELECT sum(AMT_PAID) AS MY_TOTAL FROM A_V_INVOICES_WITH_ACCT_INFORMATION i WHERE ( CREATING_CO = '2' OR CUSTOMER_CO = '2')  AND ACCT_TYPE = 'PURCHASING_ACCOUNT' AND  (ACCOUNT_ID LIKE '%%' OR ACCOUNT_ID is NULL ) AND  (INVOICE_ID LIKE '%%' OR INVOICE_ID is NULL ) AND  (INVOICE_DATE LIKE '%%' OR INVOICE_DATE is NULL ) AND  (SUPPLIER_NAME LIKE '%%' OR SUPPLIER_NAME is NULL ) AND  (CUSTOMER_NAME LIKE '%%' OR CUSTOMER_NAME is NULL ) AND  SUPPLIER_CO = '2'").SingleOrDefault();

            return result;
        }

        public InvoiceDetailViewModel InvoiceDetailById(string id)
        {
            var result = _dbContext.Database.SqlQuery<InvoiceDetailViewModel>($"SELECT * FROM A_V_INVOICES_WITH_ACCT_INFORMATION WHERE INVOICE_ID = '{id}'").SingleOrDefault();
            
            return result;
        }

        public List<InvoiceDetailListViewModel> InvoiceDetailListById(string id)
        {
            var result = _dbContext.Database.SqlQuery<InvoiceDetailListViewModel>($"SELECT * FROM A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA WHERE INVOICE_ID = '{id}'").ToList();

            return result;
        }

        public InvoiceDetailEditViewModel InvoiceEditDetailById(string id)
        {
            var result = _dbContext.Database.SqlQuery<InvoiceDetailEditViewModel>($"SELECT * FROM A_ACCOUNT_INVOICE_ITEMS WHERE ID = '{id}'").SingleOrDefault();

            return result;
        }

        public List<InvoiceViewPurchaseItemViewModels> InvoiceViewItemById(string strPurchaseID, string ntLogin)
        {
            var purchaseHistId = int.Parse(strPurchaseID) - 1;

            var strPurchaseId = new SqlParameter("@strPurchaseID", purchaseHistId);
            var strQuoteId = new SqlParameter("@strQuoteID", DBNull.Value);
            var strShowAll = new SqlParameter("@strShowAll", "true");
            var strListToExpand = new SqlParameter("@strListToexpand", DBNull.Value);
            var strExpandAllList = new SqlParameter("@strExpandAllList", DBNull.Value);
            var showAddCost = new SqlParameter("@showAddCost", "1");
            var strNTLogin = new SqlParameter("@strNTLogin", ntLogin);

            var result = _dbContext.Database.SqlQuery<InvoiceViewPurchaseItemViewModels>
                ("exec A_SP_PURCHASE_SHOW_ITEM_TREE  @strPurchaseID,@strQuoteID,@strShowAll,@strListToexpand,@strExpandAllList,@showAddCost,@strNTLogin", strPurchaseId, strQuoteId, strShowAll, strListToExpand, strExpandAllList, showAddCost, strNTLogin).ToList();

            return result;
        }

        public ResultNotification<string> Create(InvoiceDetailViewModel model, string ntLogin)
        {
            var result = new ResultNotification<string>();
            try
            {

                var editInvoiceAccountProcedure = new InvoiceAccountEditProcedure()
                {
                    AccountId = model.ACCOUNT_ID,
                    InvoiceId = model.INVOICE_ID,
                    Name = model.INVOICE_NAME,
                    Status = model.STATUS,
                    CustPurchNum = model.CUST_PURCH_NUM,
                    SalexTax = model.SALES_TAX,
                    strNTLogin = ntLogin
                };
                _dbContext.Database.ExecuteStoredProcedure(editInvoiceAccountProcedure);
                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }


        public ResultNotification<string> EditDebitCreadit(InvoiceDetailEditViewModel model, string ntLogin)
        {
            var result = new ResultNotification<string>();
            try
            {
                var id = "";
                if (model.ID == null)
                {
                    id = null;
                }
                else
                {
                    id = model.ID;
                }
                var editDebitCreaditProcedure = new InvoiceEditCreaditDebitProcedure()
                {
                    AccountId = model.ACCOUNT_ID,
                    InvoiceId = model.INVOICE_ID,
                    DatePosted = model.DATE_POSTED,
                    Type = model.ITEM_TYPE,
                    UnitPrice =model.UNIT_PRICE,
                    Qty = model.QTY,
                    Id = id,
                    Comment = model.COMMENTS,
                    TaxRate = model.TAX_RATE,
                    Description = model.DESCRIPTION,
                    CustLineNum = model.CUST_LINE_ITEM,
                    CustSinglePo = model.CUST_SINGLE_PO,
                    strNTLogin=ntLogin

                };
                _dbContext.Database.ExecuteStoredProcedure(editDebitCreaditProcedure);
                result.Entity = editDebitCreaditProcedure.NewId;
                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        public bool Delete(string id, string ntlogin)
        {
            try
            {
                var invoiceDetailDeleteProcedure = new InvoiceDetailDeleteProcedure() { PaymentId = id, strNTLogin = ntlogin };

                _dbContext.Database.ExecuteStoredProcedure(invoiceDetailDeleteProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public ResultNotification<string> SendInvoiceToCustomer(string invoiceId, string accId, DateTime? invoiceDate, DateTime? dueDate, string ntLogin)
        {
            var result = new ResultNotification<string>();
            try
            {
            
                var invoiceSendCustomerProcedure = new InvoiceSendCustomerProcedure()
                {
                    AccountId =accId,
                    InvoiceId = invoiceId,
                    DateInvoiced =invoiceDate,
                    DueDate = dueDate,
                    strNTLogin = ntLogin

                };
                _dbContext.Database.ExecuteStoredProcedure(invoiceSendCustomerProcedure);
                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }
    }
}