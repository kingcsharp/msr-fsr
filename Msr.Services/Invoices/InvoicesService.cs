using Msr.Models.Invoices;
using Msr.Repositories;
using Msr.Services.Invoices.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Msr.Models.Tasks;

namespace Msr.Services.Invoices
{
    public class POListItem {
        public string REFERENCEPO { get; set; }
        public int custid { get; set; }
        public string customername { get; set; }
        public string name { get; set; }
        public string OpenDate { get; set; }
        public string CloseDate { get; set; }
        public Decimal InvoicedBalance { get; set; }
        public Decimal UninvoicedBalance { get; set; }
        public Decimal Balance { get; set; }
        public Decimal UnusedAmount { get; set; }
        public Decimal Outstanding;
        public string ToJSON() {
            return JsonConvert.SerializeObject(this);
        }
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

        // return list of items available for invoicing for a given PO
        public IQueryable<InvoicePoWorkItem> InvoiceItemsFinishedByPurchaseId(string referencePo)
        {
            return InvoiceViewList()
                .Where(x => x.Status == "FINISHED" && x.CustPurchNum == referencePo)
                .Distinct();
        }

        // return list of work items contained within a specific invoice and PO
        public List<InvoicePoWorkItem> InvoiceItemsByPurchaseId(string referencePo, List<InvoiceWorkItem> hasValue)
        {
            var invoiceWorkItemIds = hasValue.Select(x => x.ItemId);
            return InvoiceViewList()
                .Where(x => x.CustPurchNum == referencePo && invoiceWorkItemIds.Contains(x.FillItemId))
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
            var items = _dbContext.InvoiceWorkItems.Where(
                    z => z.InvoiceId == invoiceId.Value.ToString()
                )
                .Distinct()
                .Select(z => z.ItemId).ToList();
            var ret = InvoiceViewList().Where(
                x => x.CustPurchNum == po &&
                    items.Contains(x.FillItemId)
                )
                .Distinct()
                .ToList();
            return ret;
        }

        public List<POListItem> InvoicePoList(bool onEdit)
        {
            return GetDistinctPOList();
        }

        public List<POListItem> GetDistinctPOList(int custid = -1, string facilityCode = "03")
        {
            string sql =
                "SELECT DISTINCT a.REFERENCEPO as REFERENCEPO, " +
                    "a.custid as custid, a.customername as customername, " +
                    "isnull(po.name, '') as name, " +
                    "po.OpenDate as OpenDate, " +
                    "po.CloseDate as CloseDate, " +
                    "isnull(po.InvoicedBalance, 0.0) as InvoicedBalance, " +
                    "isnull(po.UninvoicedBalance, 0.0) as UninvoicedBalance, " +
                    "isnull(po.Balance, 0.0) as Balance, " +
                    "isnull(po.UnusedAmount, 0.0) as UnusedAmount, " +
                    "po.TotalPurchaseLimit as TotalPurchaseLimit " +
                "FROM A_POS_WITH_COMPLETED_WOS a " +
                "LEFT JOIN Portal_PurchaseOrders po ON (a.REFERENCEPO = po.ReferencePo) " +
                "LEFT JOIN Portal_workorders wo ON (a.REFERENCEPO = wo.ReferencePo) " +
                "WHERE po.status = 'APPROVED' " +
                "AND wo.FILLITEMID NOT IN (SELECT ITEMID FROM PORTAL_INVOICEWORKITEM) " +
                "AND ((a.custid = @p0) OR (@p0 = -1)) " +
                "ORDER BY a.REFERENCEPO";

            List<POListItem> distinctPOList = _dbContext
                .Database
                .SqlQuery<POListItem>(sql, custid)
                .ToList();

            // Filter by Facility (or NULL)
            distinctPOList = FacilityFilter(distinctPOList, facilityCode);

            distinctPOList.ForEach(e => {
                // Outstanding - $ Totals will reflect the Product Price x QTY for all FINISHED (not
                // yet invoiced) WOs on that PO in the grid.
                // The "amount" field looks like it takes into account quantity and tax, so I'll use that.
                e.Outstanding = InvoiceItemsFinishedByPurchaseId(e.REFERENCEPO)
                    .Select(x => x.Amount)
                    .Sum().GetValueOrDefault();

            });

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
                             Purchaser = p.FullName,
                             LocationName = pwo.LocationName,
                             TaskId = pwo.TaskId
                         };

            return result;
        }

        public ResultNotification<string> Create(InvoiceViewModel model)
        {
            var result = new ResultNotification<string>();

            try
            {
                // Iterate through the selected purchase orders and invoice
                // all items for each one.
                var poItems = JsonConvert.DeserializeObject<POListItem[]>(model.Items);
                bool combinePO = model.multiPOInvoice;
                Invoice combinedInvoice = null;

                foreach (var item in poItems)
                {
                    var workItems = InvoiceItemsFinishedByPurchaseId(item.REFERENCEPO).ToList();
                    decimal totalPrice = 0;
                    Invoice invoice;

                    if (!combinePO || combinedInvoice == null) {
                        invoice = new Invoice {
                            Client = model.Client,
                            Description = model.InvoiceDescription,
                            Status = "INVOICED",
                            CustPo = item.REFERENCEPO,
                            InvoiceDate = model.InvoiceDate.Value,
                            Supplier = model.Supplier,
                            InvoiceClass = model.InvoiceClass
                        };
                        _dbContext.Invoices.Add(invoice);
                        _dbContext.SaveChanges();
                        combinedInvoice = invoice;
                    } else {
                        invoice = combinedInvoice;
                        totalPrice = invoice.SubTotal.GetValueOrDefault();
                    }

                    List<string> facility = FacilityCodeToString(model.InvoiceClass);
                    foreach (var wi in workItems) {
                        // Skip work items not for the selected location
                        if (wi.LocationName != null &&
                            !facility.Contains(wi.LocationName.ToUpper()))
                        {
                            continue;
                        }

                        var invoiceWorkItem = new InvoiceWorkItem
                        {
                            ItemId = wi.FillItemId,
                            InvoiceId = invoice.Id.ToString(),
                            RefPo = invoice.CustPo,
                        };
                        totalPrice += wi.Amount.GetValueOrDefault();

                        _dbContext.InvoiceWorkItems.Add(invoiceWorkItem);

                        // mark the work item as having been invoiced
                        TaskObject tobj = _dbContext.Tasks.Find(wi.TaskId);
                        tobj.STATUS = "CLOSED";
                        tobj.CLOSED = 1;
                    }

                    invoice.Total = (
                        totalPrice * ((model.Tax.GetValueOrDefault() / 100) + 1)
                    );
                    invoice.SubTotal = totalPrice;
                    invoice.Tax = model.Tax;

                    _dbContext.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

            }
            return result;
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
                invoice.InvoiceDate = model.InvoiceDate.Value;
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
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
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

        private List<string> FacilityCodeToString(string facilityCode) {
            var facilities = new List<string>();
            // See InvoiceViewModel.InvoiceIdList for these definitions
            // TODO: this should probably be data driven and not hard-coded
            switch (facilityCode) {
                case "04": // PHX
                    facilities.Add("CHANDLER");
                break;
                case "05": // IRE
                    facilities.Add("NAAS");
                break;
                case "06": // ISL
                    facilities.Add("KIRYAT GAT");
                break;
                default: // 03, PDX
                    facilities.Add("HILLSBORO");
                break;
            }

            return facilities;
        }

        private List<POListItem> FacilityFilter(List<POListItem> list, string facilityCode) {
            List<POListItem> poList = list;

            // Filter by Facility (or NULL)
            if (facilityCode != null) {
                var facilities = FacilityCodeToString(facilityCode);

                poList = list.Where(x => {
                    var locs = _dbContext
                        .WorkOrders
                        .Where(y => y.ReferencePo == x.REFERENCEPO && y.Status == "FINISHED")
                        .Select(y => y.LocationName.ToUpper())
                        .ToList();

                    return locs.Contains(null) || (locs.Intersect(facilities).Count() > 0);
                }).ToList();
            }
            return poList;
        }

    }
}
