using EntityFrameworkExtras.EF6;
using Msr.Models.Comman;
using Msr.Models.Invoices;
using Msr.Models.Invoices;
using Msr.Repositories;
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

        public IQueryable<InvoiceView> GetInvoiceViewQueryable()
        {
            return _dbContext.InvoicesViews;
        }

        public IQueryable<Invoice> GetInvoicesQueryable()
        {
            return _dbContext.Invoices;
        }

        public List<InvoiceDetailListViewModel> InvoiceItemsDetailListById(string poNumber)
        {
            var result = _dbContext.Database.SqlQuery<InvoiceDetailListViewModel>($"SELECT item.ID,item.DATE_POSTED,item.ITEM_TYPE,item.CUST_PURCH_NUM, item.PURCHASE_ID,item.PURCH_ITEM_ID,item.DESCRIPTION, item.COMMENTS,item.QTY,item.UNIT_PRICE,item.AMOUNT,item.TAX, item.TOTAL,PW.TotalSalePrice FROM A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA as item left join A_V_INVOICES_WITH_ACCT_INFORMATION as acct on acct.INVOICE_ID=item.INVOICE_ID inner join Portal_WorkOrders as PW on PW.FillId=item.FILL_ID where acct.PO_NUMBER = '{poNumber}'").ToList();

            return result;
        }
        public InvoiceDetailListViewModel InvoiceItemDetailById(string id)
        {
            var result = _dbContext.Database.SqlQuery<InvoiceDetailListViewModel>($"SELECT * FROM A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA WHERE ID = '{id}'").SingleOrDefault();

            return result;
        }

        public List<SelectFile> InvoicePoList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"select DISTINCT PO_NUMBER as Id,PO_NUMBER as Name from dbo.A_ACCOUNT_INVOICES where PO_NUMBER IS NOT NULL").ToList();

            return result;
        }

        public ResultNotification<string> Create(InvoiceViewModel model)
        {
            var result = new ResultNotification<string>();
            try
            {
                var invoice = new Invoice();
                invoice.Client = model.Client;
                invoice.Items = model.Items;
                invoice.Description = model.InvoiceDescription;
                invoice.Status = model.Status;
                invoice.CustPo = model.CustPo;
                invoice.InvoiceDate = model.InvoiceDate.Value;
                invoice.Total = model.Total;
                invoice.SubTotal = model.SubTotal;
                invoice.Tax = model.Tax;
                invoice.Supplier = model.Supplier;
                invoice.InvoiceClass = model.InvoiceClass;
                _dbContext.Invoices.Add(invoice);
                _dbContext.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        public ResultNotification<string> Edit(InvoiceViewModel model)
        {
            var result = new ResultNotification<string>();
            try
            {
                var invoice = _dbContext.Invoices.Where(x => x.Id == model.Id).Single();
                invoice.Client = model.Client;
                invoice.Items = model.Items;
                invoice.Description = model.InvoiceDescription;
                invoice.Status = model.Status;
                invoice.CustPo = model.CustPo;
                invoice.InvoiceDate = model.InvoiceDate.Value;
                invoice.Total = model.Total;
                invoice.SubTotal = model.SubTotal;
                invoice.Tax = model.Tax;
                invoice.InvoiceClass = model.InvoiceClass;
                invoice.Supplier = model.Supplier;
                _dbContext.SaveChanges();

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