using Answer.Web.Filters;
using Msr.Models.Invoices;
using Msr.Models.Menus;
using Msr.Services.Invoices;
using Msr.Services.Invoices.ViewModel;
using Msr.Services.jqGrid;
using Msr.Services.PurchesOrder;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Forecast)]
    public class InvoicesController : BaseController
    {
        private readonly InvoicesService _invoicesService;

        public InvoicesController()
        {
            _invoicesService = new InvoicesService();
        }

        public ActionResult Index()
        {
            var viewModel = new InvoiceViewModel();

            ViewBag.ActiveClass = "Invoice";

            return View(viewModel);
        }

        public ActionResult InvoicesData(JqGridParam param)
        {

            var totalRows = _invoicesService.GetInvoiceViewQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {

                    if (rule.field == nameof(InvoiceView.Status) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(InvoiceView.CustPo))
                    {
                        totalRows = totalRows.Where(x => x.CustPo.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.Client))
                    {
                        totalRows = totalRows.Where(x => x.Client.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.InvoiceNumber))
                    {
                        totalRows = totalRows.Where(x => x.InvoiceNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.Total))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Total == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.Total == -1);
                        }
                    }
                    else if (rule.field == nameof(InvoiceView.InvoiceDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.InvoiceDate.Day == value.Day &&
                                                             x.InvoiceDate.Month == value.Month &&
                                                             x.InvoiceDate.Year == value.Year);
                        }
                    }

                }
            }

            var orderBy = nameof(InvoiceView.Id);

            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }

            if (param.sortOrder == "desc")
            {
                totalRows = totalRows.OrderByDescending(orderBy);
            }
            else
            {
                totalRows = totalRows.OrderBy(orderBy);
            }

            var totalRecords = totalRows.Count();
            totalRows = totalRows.Skip(param.pageSize * (param.pageIndex - 1));
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);

            var results = totalRows.ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InvoiceModelGetPoList(int id)
        {
            var invoiceViewModel = new InvoiceViewModel();

            invoiceViewModel.setPoList(_invoicesService.GetDistinctPOList(id));

            return PartialView("_InvoiceItemsList", invoiceViewModel);
        }

        [HttpPost]
        public ActionResult InvoiceModelEditGetPoItems(string id, bool hasValue)
        {
            var invoiceViewModel = new InvoiceViewModel();

            invoiceViewModel.InvoiceItemList = _invoicesService.InvoiceItemsByPurchaseId(id, hasValue).ToList();

            return PartialView("_InvoiceEditItemsList", invoiceViewModel);
        }

        [HttpGet]
        public ActionResult LoadInvoice()
        {
            var invoiceAddViewModel = new InvoiceViewModel();

            invoiceAddViewModel.SetUp(new PurchesOrderService(), new InvoicesService(), false);

            return PartialView("_InvoiceAddModel", invoiceAddViewModel);
        }

        [HttpGet]
        public ActionResult LoadInvoiceById(int? id, bool hasValue)
        {

            var invoiceViewModel = new InvoiceViewModel();

            var model = _invoicesService.GetInvoicesQueryable().SingleOrDefault(x => x.Id == id);

            invoiceViewModel = invoiceViewModel.MapToDto(model);

            invoiceViewModel.SetUp(new PurchesOrderService(), new InvoicesService(), true);

            invoiceViewModel.InvoiceItemList = _invoicesService.InvoiceItemsByPurchaseId(model.CustPo, hasValue).ToList();

            invoiceViewModel.InvoiceWorkItem = _invoicesService.InvoiceItemsById(model.Id.ToString()).ToList();

            invoiceViewModel.Items = string.Join(",", invoiceViewModel.InvoiceWorkItem.Select(x => x.ItemId));

            return PartialView("_InvoiceEditModel ", invoiceViewModel);
        }

        [HttpPost]
        public ActionResult Create(InvoiceViewModel model)
        {
            var currentUser = GetCurrentUser();
            model.Supplier = currentUser.Root_Company;

            var response = _invoicesService.Create(model: model);
            if (!response.HasErrors())
            {
                TempData["SuccessMessage"] = "Invoice Created Successfully";
            }
            else
            {
                TempData["SuccessMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Save(InvoiceViewModel model)
        {
            var currentUser = GetCurrentUser();
            model.Supplier = currentUser.Root_Company;

            var response = _invoicesService.Edit(model: model);
            if (!response.HasErrors())
            {
                TempData["SuccessMessage"] = "Invoice Updated Successfully";
            }
            else
            {
                TempData["SuccessMessage"] = "Something Went Wrong";
            }

            return RedirectToAction("index");
        }

        public ActionResult ExportFile(int? id, string po)
        {
            var invoiceDetails = _invoicesService.GetInvoiceViewQueryable().Where(x => x.Id == id).FirstOrDefault();

            var invoiceFileText = _invoicesService.GetInvoiceQuickbooksFormat(id, po);

            var invoiceFileBytes = Encoding.ASCII.GetBytes(invoiceFileText);

            var invoiceMemoryStream = new MemoryStream(invoiceFileBytes);

            return File(invoiceMemoryStream, "text/plain", string.Format("{0}.iif", invoiceDetails.InvoiceNumber));
        }

        //public ActionResult ExportFile(int? id, string po)
        //{
        //    var items = _invoicesService.InvoiceExportByPo(po, id);


        //    var delimiter = "\t";
        //    var invoice = _invoicesService.GetInvoiceViewQueryable().Where(x => x.Id == id).SingleOrDefault();

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(string.Format("!TRNS{0}", delimiter));
        //    sb.Append(string.Format("TRNSID{0}", delimiter));
        //    sb.Append(string.Format("TRNSTYPE{0}", delimiter));
        //    sb.Append(string.Format("DATE{0}", delimiter));
        //    sb.Append(string.Format("ACCNT{0}", delimiter));
        //    sb.Append(string.Format("NAME{0}", delimiter));
        //    sb.Append(string.Format("CLASS{0}", delimiter));
        //    sb.Append(string.Format("AMOUNT{0}", delimiter));
        //    sb.Append(string.Format("DOCNUM{0}", delimiter));
        //    sb.Append(string.Format("MEMO{0}", delimiter));
        //    sb.Append(string.Format("CLEAR{0}", delimiter));
        //    sb.Append(string.Format("TOPRINT{0}", delimiter));
        //    sb.Append(string.Format("ADDR1{0}", delimiter));
        //    sb.Append(string.Format("ADDR2{0}", delimiter));
        //    sb.Append(string.Format("ADDR3{0}", delimiter));
        //    sb.Append(string.Format("ADDR4{0}", delimiter));
        //    sb.Append(string.Format("ADDR5{0}", delimiter));
        //    sb.Append(string.Format("DUEDATE{0}", delimiter));
        //    sb.Append(string.Format("TERMS{0}", delimiter));
        //    sb.Append(string.Format("PAID{0}", delimiter));
        //    sb.Append(string.Format("SHIPDATE{0}", delimiter));
        //    sb.Append(Environment.NewLine);

        //    sb.Append(string.Format("!SPL{0}", delimiter));
        //    sb.Append(string.Format("SPLID{0}", delimiter));
        //    sb.Append(string.Format("TRNSTYPE{0}", delimiter));
        //    sb.Append(string.Format("DATE{0}", delimiter));
        //    sb.Append(string.Format("ACCNT{0}", delimiter));
        //    sb.Append(string.Format("NAME{0}", delimiter));
        //    sb.Append(string.Format("CLASS{0}", delimiter));
        //    sb.Append(string.Format("AMOUNT{0}", delimiter));
        //    sb.Append(string.Format("DOCNUM{0}", delimiter));
        //    sb.Append(string.Format("MEMO{0}", delimiter));
        //    sb.Append(string.Format("CLEAR{0}", delimiter));
        //    sb.Append(string.Format("QNTY{0}", delimiter));
        //    sb.Append(string.Format("PRICE{0}", delimiter));
        //    sb.Append(string.Format("INVITEM{0}", delimiter));
        //    sb.Append(string.Format("PAYMETH{0}", delimiter));
        //    sb.Append(string.Format("TAXABLE{0}", delimiter));
        //    sb.Append(string.Format("REIMBEXP{0}", delimiter));
        //    sb.Append(string.Format("EXTRA{0}", delimiter));
        //    sb.Append(Environment.NewLine);
        //    sb.Append(string.Format("!ENDTRNS"));
        //    sb.Append(Environment.NewLine);

        //    sb.Append(string.Format("TRNS{0}", delimiter)); //TRNS
        //    sb.Append(string.Format("{0}{1}", invoice.InvoiceNumber, delimiter)); //TRNSID
        //    sb.Append(string.Format("{0}{1}", "INVOICE", delimiter)); //TRNSTYPE
        //    sb.Append(string.Format("{0:d}{1}", invoice.InvoiceDate, delimiter)); //DATE
        //    sb.Append(string.Format("{0}{1}", "1100", delimiter)); //ACCNT
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //NAME
        //    sb.Append(string.Format("{0}{1}", invoice.InvoiceClass, delimiter)); //CLASS
        //    sb.Append(string.Format("{0}{1}", invoice.SubTotal, delimiter)); //AMOUNT
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //DOCNUM
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //MEMO
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //CLEAR
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //TOPRINT
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //ADDR1
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //ADDR2
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //ADDR3
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //ADDR4
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //ADDR5
        //    sb.Append(string.Format("{0:d}{1}", "", delimiter)); //DUEDATE
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //TERMS
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //PAID
        //    sb.Append(string.Format("{0:d}{1}", "", delimiter)); //SHIPDATE

        //    sb.Append(Environment.NewLine);

        //    foreach (var item in items)
        //    {
        //        if (!string.IsNullOrWhiteSpace(item.ToString()))
        //        {
        //            sb.Append(string.Format("SPL{0}", delimiter)); //SPL
        //            sb.Append(string.Format("{0}{1}", invoice.InvoiceNumber, delimiter)); //SPLID
        //            sb.Append(string.Format("{0}", "INVOICE")); //TRNSTYPE
        //            sb.Append(string.Format("{0:d}{1}", invoice.InvoiceDate, delimiter)); //DATE
        //            sb.Append(string.Format("{0}{1}", "1100", delimiter)); //ACCNT
        //            sb.Append(string.Format("{0}{1}", item.Purchaser, delimiter)); //NAME
        //            sb.Append(string.Format("{0}{1}", invoice.InvoiceClass, delimiter)); //CLASS
        //            sb.Append(string.Format("{0}{1}", item.Amount, delimiter)); //AMOUNT
        //            sb.Append(string.Format("{0}{1}", "", delimiter)); //DOCNUM
        //            sb.Append(string.Format("{0}{1}", "", delimiter)); //MEMO
        //            sb.Append(string.Format("{0}{1}", item.FillQty, delimiter)); //MEMO
        //            sb.Append(string.Format("{0}{1}", item.TotalSalePrice, delimiter)); //QNTY
        //            sb.Append(string.Format("{0}{1}", "", delimiter)); //PRICE
        //            sb.Append(string.Format("{0}{1}", "Y", delimiter)); //INVITEM
        //            sb.Append(string.Format("{0}{1}", "", delimiter)); //PAYMETH
        //            sb.Append(string.Format("{0}{1}", "", delimiter)); //TAXABLE
        //            sb.Append(string.Format("{0}{1}", "", delimiter)); //REIMBEXP
        //            sb.Append(string.Format("{0}{1}", "", delimiter)); //EXTRA
        //            sb.Append(Environment.NewLine);
        //        }
        //    }

        //    sb.Append(string.Format("SPL{0}", delimiter));
        //    sb.Append(string.Format("{0}{1}", invoice.InvoiceNumber, delimiter));
        //    sb.Append(string.Format("{0:d}{1}", "INVOICE", delimiter));
        //    sb.Append(string.Format("{0:d}{1}", invoice.InvoiceDate, delimiter));
        //    sb.Append(string.Format("{0}{1}", "1100", delimiter)); //ACCNT
        //    sb.Append(string.Format("{0}{1}", "Sales Tax Payable", delimiter)); //NAME
        //    sb.Append(string.Format("{0}{1}", invoice.InvoiceClass, delimiter)); //CLASS
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //AMOUNT
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //DOCNUM
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //MEMO
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //MEMO
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //Qty
        //    sb.Append(string.Format("{0}%", invoice.Tax)); //PRICE
        //    sb.Append(string.Format("{0}{1}", "", delimiter));//INVITEM
        //    sb.Append(string.Format("{0}{1}", "N", delimiter)); //PAYMETH
        //    sb.Append(string.Format("{0}{1}", "Y", delimiter));//TAXABLE
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //REIMBEXP
        //    sb.Append(string.Format("{0}{1}", "", delimiter)); //EXTRA

        //    sb.Append(Environment.NewLine);


        //    sb.Append(string.Format("ENDTRNS"));
        //    var byteArray = Encoding.ASCII.GetBytes(sb.ToString());
        //    var stream = new MemoryStream(byteArray);

        //    return File(stream, "text/plain", string.Format("{0}.iif", invoice.InvoiceNumber));
        //}

        [HttpGet]
        public ActionResult DownloadAllInvoices()
        {
            List<InvoiceQuickbooksFileModel> invoiceQuickbooksFileModels = _invoicesService.GetAllInvoicesInQuickbooksFormat();

            MemoryStream compressedInvoices = _invoicesService.GetInvoicesZippedArchive(ref invoiceQuickbooksFileModels);

            return File(compressedInvoices, "text/plain", string.Format("All Invoices - {0}.zip", DateTime.Now.ToShortDateString()));
        }

        [HttpGet]
        public ActionResult DownloadFilteredInvoices(JqGridParam jqGridParam)
        {
            List<InvoiceView> invoiceList = GetFilteredInvoices(jqGridParam);

            List<InvoiceQuickbooksFileModel> invoiceQuickbooksFileModels = _invoicesService.GetAllInvoicesInQuickbooksFormat(invoiceList);

            MemoryStream compressedInvoices = _invoicesService.GetInvoicesZippedArchive(ref invoiceQuickbooksFileModels);

            return File(compressedInvoices, "text/plain", string.Format("Invoices - {0}.zip", DateTime.Now.ToShortDateString()));
        }

        public List<InvoiceView> GetFilteredInvoices(JqGridParam jqGridParam)
        {

            var invoicesQueryable = _invoicesService.GetInvoiceViewQueryable();

            if (jqGridParam.where != null && jqGridParam.where.rules.Any())
            {
                foreach (var rule in jqGridParam.where.rules)
                {

                    if (rule.field == nameof(InvoiceView.Status) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            invoicesQueryable = invoicesQueryable.Where(x => list.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(InvoiceView.CustPo))
                    {
                        invoicesQueryable = invoicesQueryable.Where(x => x.CustPo.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.Client))
                    {
                        invoicesQueryable = invoicesQueryable.Where(x => x.Client.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.Description))
                    {
                        invoicesQueryable = invoicesQueryable.Where(x => x.Description.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.InvoiceNumber))
                    {
                        invoicesQueryable = invoicesQueryable.Where(x => x.InvoiceNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.Total))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            invoicesQueryable = invoicesQueryable.Where(x => x.Total == value);
                        }
                        else
                        {
                            invoicesQueryable = invoicesQueryable.Where(x => x.Total == -1);
                        }
                    }
                    else if (rule.field == nameof(InvoiceView.InvoiceDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            invoicesQueryable = invoicesQueryable.Where(x => x.InvoiceDate.Day == value.Day &&
                                                             x.InvoiceDate.Month == value.Month &&
                                                             x.InvoiceDate.Year == value.Year);
                        }
                    }

                }
            }

            var orderBy = nameof(InvoiceView.Id);

            if (!string.IsNullOrWhiteSpace(jqGridParam.sortColumn))
            {
                orderBy = jqGridParam.sortColumn;
            }

            if (jqGridParam.sortOrder == "desc")
            {
                invoicesQueryable = invoicesQueryable.OrderByDescending(orderBy);
            }
            else
            {
                invoicesQueryable = invoicesQueryable.OrderBy(orderBy);
            }

            var invoiceList = invoicesQueryable.ToList();

            return invoiceList;
        }

    }
}
