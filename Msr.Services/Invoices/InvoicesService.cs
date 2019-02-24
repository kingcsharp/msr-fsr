using Msr.Models.Invoices;
using Msr.Repositories;
using Msr.Services.Invoices.ViewModel;
using System;
using System.Collections.Generic;
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

        public List<InvoicePoWorkItem> InvoiceItemsByPurchaseId(string purchaseId, bool hasValue)
        {
            if (hasValue)
            {
                return InvoiceViewList().Where(x => x.Status == "FINISHED" && x.CustPurchNum == purchaseId).Distinct().ToList();
            }

            return InvoiceViewList()
                .Where(x => x.Status == "FINISHED" && x.CustPurchNum == purchaseId && !_dbContext.InvoiceWorkItems.Select(y => y.ItemId).Contains(x.FillItemId))
                .Distinct()
                .ToList();
        }

        public List<InvoiceWorkItem> InvoiceItemsById(string invoiceId)
        {
            var result = _dbContext.InvoiceWorkItems.Where(x => x.InvoiceId == invoiceId).ToList();

            return result;
        }

        public List<InvoicePoWorkItem> InvoiceExportByPo(string po)
        {
            var result = InvoiceViewList().Where(x => x.CustPurchNum == po && x.Status == "FINISHED" && _dbContext.InvoiceWorkItems.Select(y => y.ItemId).Contains(x.FillItemId)).ToList();

            return result;
        }

        public List<string> InvoicePoList(bool onEdit)
        {
            var flag = false;

            var woItem = _dbContext.InvoiceWorkItems.ToList();

            foreach (var item in woItem)
            {
                var invoicePoWorkItems = InvoiceViewList().Where(x => x.Status == "FINISHED" && x.CustPurchNum == item.RefPo).Distinct().ToList();

                foreach (var refPo in invoicePoWorkItems)
                {
                    var woItemCount = _dbContext.InvoiceWorkItems.Count(x => x.RefPo == refPo.CustPurchNum);

                    if (invoicePoWorkItems.Count == woItemCount)
                    {
                        flag = true;
                    }
                }
            }

            if (flag && !onEdit)
            {
                return InvoiceViewList().Where(x => x.Status == "FINISHED" && x.CustPurchNum != null &&
                !_dbContext.InvoiceWorkItems.Where(y => y.RefPo == x.CustPurchNum && y.ItemId == x.FillItemId).Select(y => y.RefPo).Contains(x.CustPurchNum))
                .Select(x => x.CustPurchNum).Distinct().ToList();
            }

            return InvoiceViewList().Where(x => x.Status == "FINISHED" && x.CustPurchNum != null).Select(x => x.CustPurchNum).Distinct().ToList();
        }

        public IQueryable<InvoicePoWorkItem> InvoiceViewList()
        {
            var result = from pwo in _dbContext.WorkOrders
                         join po in _dbContext.PurchesOrderViews on pwo.ReferencePo equals
                         po.ReferencePo
                         join p in _dbContext.Peoples on pwo.Purchaser equals p.ObjectId
                         select new InvoicePoWorkItem
                         {
                             FillItemId = pwo.FillItemId,
                             CustPurchNum = pwo.ReferencePo,
                             PurchaseId = pwo.PurchaseId,
                             OpenDate = po.OpenDate,
                             PurchaseItemId = pwo.PurchaseItemId,
                             Description = pwo.ProductName + "(" + pwo.ProcObjId + ")",
                             FillQty = pwo.FillQty,
                             TotalSalePrice = pwo.TotalSalePrice,
                             Amount = pwo.Amount,
                             Status = pwo.Status,
                             Purchaser = p.FullName
                         };

            return result;
        }
        public ResultNotification<string> Create(InvoiceViewModel model)
        {
            var result = new ResultNotification<string>();

            try
            {
                var invoice = new Invoice();
                invoice.Client = model.Client;
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

                var invoiceItem = model.Items.Split(',');

                foreach (var item in invoiceItem)
                {
                    var invoiceWorkItem = new InvoiceWorkItem
                    {
                        ItemId = item,
                        InvoiceId = invoice.Id.ToString(),
                        RefPo = invoice.CustPo,
                    };

                    _dbContext.InvoiceWorkItems.Add(invoiceWorkItem);
                }

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
                var invoice = _dbContext.Invoices.Single(x => x.Id == model.Id);
                invoice.Client = model.Client;
                invoice.Description = model.InvoiceDescription;
                invoice.Status = model.Status;
                invoice.CustPo = model.CustPo;
                invoice.InvoiceDate = model.InvoiceDate.Value;
                invoice.Total = model.Total;
                invoice.SubTotal = model.SubTotal;
                invoice.Tax = model.Tax;
                invoice.InvoiceClass = model.InvoiceClass;
                invoice.Supplier = model.Supplier;

                var invoiceItem = model.Items?.Split(',');

                var workItem = _dbContext.InvoiceWorkItems.Where(x => x.InvoiceId == model.Id.ToString()).ToList();
                _dbContext.InvoiceWorkItems.RemoveRange(workItem);

                if (invoiceItem != null)
                    foreach (var item in invoiceItem)
                    {
                        var invoiceWorkItem = new InvoiceWorkItem();

                        invoiceWorkItem.ItemId = item;
                        invoiceWorkItem.InvoiceId = invoice.Id.ToString();
                        invoiceWorkItem.RefPo = invoice.CustPo;
                        _dbContext.InvoiceWorkItems.Add(invoiceWorkItem);
                    }
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