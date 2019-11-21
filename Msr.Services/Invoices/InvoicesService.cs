using Msr.Models.Invoices;
using Msr.Repositories;
using Msr.Services.Invoices.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Msr.Services.Invoices
{
    public class POListItem {
        public string REFERENCEPO;
        public int custid;
    }
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

        public List<InvoicePoWorkItem> InvoiceExportByPo(string po, int? invoiceId)
        {
            var ret = InvoiceViewList().Where(x => x.Status == "FINISHED" && x.CustPurchNum == po
             && _dbContext.InvoiceWorkItems.Where(z => z.InvoiceId == invoiceId.Value.ToString()).Distinct()
             .Select(z => z.ItemId)
             .Contains(x.FillItemId))
                .Distinct().ToList();
            return ret;
        }

        public List<POListItem> InvoicePoList(bool onEdit)
        {
            return GetDistinctPOList();
        }

        public List<POListItem> GetDistinctPOList()
        {
            string sql = $"SELECT REFERENCEPO, custid FROM A_POS_WITH_COMPLETED_WOS ORDER BY REFERENCEPO";

            List<POListItem> distinctPOList =
                _dbContext.Database.SqlQuery<POListItem>(sql).Distinct().OrderBy(x => x.REFERENCEPO).ToList();

            return distinctPOList;
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

        public MemoryStream GetInvoicesZippedArchive(ref List<InvoiceQuickbooksFileModel> invoiceQuickbooksFileModels)
        {
            var invoiceArchiveMemoryStream = new MemoryStream();

            using (var invoiceArchive = new ZipArchive(invoiceArchiveMemoryStream, ZipArchiveMode.Create, true))
            {

                foreach (InvoiceQuickbooksFileModel invoiceQuickbooksFileModel in invoiceQuickbooksFileModels)
                {
                    ZipArchiveEntry invoiceFile = invoiceArchive.CreateEntry(invoiceQuickbooksFileModel.InvoiceFileName);

                    using (var invoiceZipArchiveEntryStream = invoiceFile.Open())

                    using (var invoiceZipArchiveEntryStreamWriter = new StreamWriter(invoiceZipArchiveEntryStream))
                    {
                        invoiceZipArchiveEntryStreamWriter.Write(invoiceQuickbooksFileModel.InvoiceQuickbooksFileText);
                    }

                }

            }

            invoiceArchiveMemoryStream.Seek(0, SeekOrigin.Begin);

            return invoiceArchiveMemoryStream;
        }

        public List<InvoiceQuickbooksFileModel> GetAllInvoicesInQuickbooksFormat(List<InvoiceView> invoiceList = null)
        {
            List<InvoiceQuickbooksFileModel> invoiceQuickbooksFileModels = new List<InvoiceQuickbooksFileModel>();

            if (invoiceList == null )
            {
                invoiceList = GetInvoiceViewQueryable().ToList();
            }

            foreach (InvoiceView invoiceView in invoiceList)
            {
                InvoiceQuickbooksFileModel invoiceQuickbooksFileModel = new InvoiceQuickbooksFileModel();

                invoiceQuickbooksFileModel.InvoiceView = invoiceView;
                invoiceQuickbooksFileModel.InvoiceQuickbooksFileText = GetInvoiceQuickbooksFormat(invoiceView.Id, invoiceView.CustPo);

                invoiceQuickbooksFileModels.Add(invoiceQuickbooksFileModel);
            }

            return invoiceQuickbooksFileModels;
        }

        public string GetInvoiceQuickbooksFormat(int? id, string po)
        {
            string invoiceQuickbooksFormat = string.Empty;

            StringBuilder invoiceFileText = new StringBuilder();

            var invoiceDelimiter = "\t";

            var invoiceDetails = GetInvoiceViewQueryable().Where(x => x.Id == id).SingleOrDefault();
            var invoiceItems = InvoiceExportByPo(po, id);

            invoiceFileText.Append(string.Format("!TRNS{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("TRNSID{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("TRNSTYPE{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("DATE{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("ACCNT{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("NAME{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("CLASS{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("AMOUNT{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("DOCNUM{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("MEMO{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("CLEAR{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("TOPRINT{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("ADDR1{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("ADDR2{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("ADDR3{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("ADDR4{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("ADDR5{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("DUEDATE{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("TERMS{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("PAID{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("SHIPDATE{0}", invoiceDelimiter));
            invoiceFileText.Append(Environment.NewLine);

            invoiceFileText.Append(string.Format("!SPL{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("SPLID{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("TRNSTYPE{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("DATE{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("ACCNT{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("NAME{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("CLASS{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("AMOUNT{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("DOCNUM{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("MEMO{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("CLEAR{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("QNTY{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("PRICE{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("INVITEM{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("PAYMETH{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("TAXABLE{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("REIMBEXP{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("EXTRA{0}", invoiceDelimiter));
            invoiceFileText.Append(Environment.NewLine);
            invoiceFileText.Append(string.Format("!ENDTRNS"));
            invoiceFileText.Append(Environment.NewLine);

            invoiceFileText.Append(string.Format("TRNS{0}", invoiceDelimiter)); //TRNS
            invoiceFileText.Append(string.Format("{0}{1}", invoiceDetails.InvoiceNumber, invoiceDelimiter)); //TRNSID
            invoiceFileText.Append(string.Format("{0}{1}", "INVOICE", invoiceDelimiter)); //TRNSTYPE
            invoiceFileText.Append(string.Format("{0:d}{1}", invoiceDetails.InvoiceDate, invoiceDelimiter)); //DATE
            invoiceFileText.Append(string.Format("{0}{1}", "1100", invoiceDelimiter)); //ACCNT
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //NAME
            invoiceFileText.Append(string.Format("{0}{1}", invoiceDetails.InvoiceClass, invoiceDelimiter)); //CLASS
            invoiceFileText.Append(string.Format("{0}{1}", invoiceDetails.SubTotal, invoiceDelimiter)); //AMOUNT
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //DOCNUM
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //MEMO
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //CLEAR
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //TOPRINT
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //ADDR1
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //ADDR2
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //ADDR3
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //ADDR4
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //ADDR5
            invoiceFileText.Append(string.Format("{0:d}{1}", "", invoiceDelimiter)); //DUEDATE
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //TERMS
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //PAID
            invoiceFileText.Append(string.Format("{0:d}{1}", "", invoiceDelimiter)); //SHIPDATE

            invoiceFileText.Append(Environment.NewLine);

            foreach (var item in invoiceItems)
            {
                if (!string.IsNullOrWhiteSpace(item.ToString()))
                {
                    invoiceFileText.Append(string.Format("SPL{0}", invoiceDelimiter)); //SPL
                    invoiceFileText.Append(string.Format("{0}{1}", invoiceDetails.InvoiceNumber, invoiceDelimiter)); //SPLID
                    invoiceFileText.Append(string.Format("{0}", "INVOICE")); //TRNSTYPE
                    invoiceFileText.Append(string.Format("{0:d}{1}", invoiceDetails.InvoiceDate, invoiceDelimiter)); //DATE
                    invoiceFileText.Append(string.Format("{0}{1}", "1100", invoiceDelimiter)); //ACCNT
                    invoiceFileText.Append(string.Format("{0}{1}", item.Purchaser, invoiceDelimiter)); //NAME
                    invoiceFileText.Append(string.Format("{0}{1}", invoiceDetails.InvoiceClass, invoiceDelimiter)); //CLASS
                    invoiceFileText.Append(string.Format("{0}{1}", item.Amount, invoiceDelimiter)); //AMOUNT
                    invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //DOCNUM
                    invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //MEMO
                    invoiceFileText.Append(string.Format("{0}{1}", item.FillQty, invoiceDelimiter)); //MEMO
                    invoiceFileText.Append(string.Format("{0}{1}", item.TotalSalePrice, invoiceDelimiter)); //QNTY
                    invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //PRICE
                    invoiceFileText.Append(string.Format("{0}{1}", "Y", invoiceDelimiter)); //INVITEM
                    invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //PAYMETH
                    invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //TAXABLE
                    invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //REIMBEXP
                    invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //EXTRA
                    invoiceFileText.Append(Environment.NewLine);
                }
            }

            invoiceFileText.Append(string.Format("SPL{0}", invoiceDelimiter));
            invoiceFileText.Append(string.Format("{0}{1}", invoiceDetails.InvoiceNumber, invoiceDelimiter));
            invoiceFileText.Append(string.Format("{0:d}{1}", "INVOICE", invoiceDelimiter));
            invoiceFileText.Append(string.Format("{0:d}{1}", invoiceDetails.InvoiceDate, invoiceDelimiter));
            invoiceFileText.Append(string.Format("{0}{1}", "1100", invoiceDelimiter)); //ACCNT
            invoiceFileText.Append(string.Format("{0}{1}", "Sales Tax Payable", invoiceDelimiter)); //NAME
            invoiceFileText.Append(string.Format("{0}{1}", invoiceDetails.InvoiceClass, invoiceDelimiter)); //CLASS
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //AMOUNT
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //DOCNUM
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //MEMO
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //MEMO
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //Qty
            invoiceFileText.Append(string.Format("{0}%", invoiceDetails.Tax)); //PRICE
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter));//INVITEM
            invoiceFileText.Append(string.Format("{0}{1}", "N", invoiceDelimiter)); //PAYMETH
            invoiceFileText.Append(string.Format("{0}{1}", "Y", invoiceDelimiter));//TAXABLE
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //REIMBEXP
            invoiceFileText.Append(string.Format("{0}{1}", "", invoiceDelimiter)); //EXTRA

            invoiceFileText.Append(Environment.NewLine);

            invoiceFileText.Append(string.Format("ENDTRNS"));

            invoiceQuickbooksFormat = invoiceFileText.ToString();

            return invoiceQuickbooksFormat;
        }

    }
}
