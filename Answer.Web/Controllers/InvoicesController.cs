using Msr.Models.Invoices;
using Msr.Services.Invoices;
using Msr.Services.Invoices.ViewModel;
using Msr.Services.jqGrid;
using Msr.Services.PurchesOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Answer.Web.Controllers
{
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

            var totalRows = _invoicesService.GetInvoicesQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {

                    if (rule.field == nameof(InvoiceView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
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
        public ActionResult InvoiceModelDetail(string id)
        {
            var invoiceViewModel = new InvoiceViewModel();

            invoiceViewModel.invoiceDetailList = _invoicesService.InvoiceItemsDetailListById(id).ToList();

            return PartialView("_InvoiceItemsList", invoiceViewModel);
        }

        [HttpGet]
        public ActionResult LoadInvoice()
        {
            var invoiceViewModel = new InvoiceViewModel();

            invoiceViewModel.SetUp(new PurchesOrderService(), new InvoicesService());

            return PartialView("_InvoiceAddEditModel", invoiceViewModel);
        }

        [HttpGet]
        public ActionResult LoadInvoiceById(int? id)
        {
            var invoiceViewModel = new InvoiceViewModel();

            var model = _invoicesService.GetInvoicesQueryable().Where(x => x.Id == id).SingleOrDefault();

            invoiceViewModel.invoiceDetailList = _invoicesService.InvoiceItemsDetailListById(model.CustPo).ToList();

            invoiceViewModel = invoiceViewModel.MapToDto(model);

            invoiceViewModel.SetUp(new PurchesOrderService(), new InvoicesService());

            return PartialView("_InvoiceAddEditModel", invoiceViewModel);

        }

        [HttpPost]
        public ActionResult Create(InvoiceViewModel model)
        {

            var currentUser = GetCurrentUser();
            model.Supplier = currentUser.Root_Company;
            if (model.Id == null)
            {
                var response = _invoicesService.Create(model: model);
                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Invoice Created Successfully";
                }
                else
                {
                    TempData["SuccessMessage"] = "Something Went Wrong";
                }
            }
            else
            {
                var response = _invoicesService.Edit(model: model);
                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Invoice Updated Successfully";
                }
                else
                {
                    TempData["SuccessMessage"] = "Something Went Wrong";
                }
            }
            return RedirectToAction("index");
        }
        public ActionResult ExportFile(int? id, string items)
        {
            var delimiter = "\t";
            var invoice = _invoicesService.GetInvoicesQueryable().Where(x => x.Id == id).SingleOrDefault();

            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("Customer Number{0}", delimiter));
            sb.Append(string.Format("Account{0}", delimiter));
            sb.Append(string.Format("Invoice Date{0}", delimiter));
            sb.Append(string.Format("Invoice Number{0}", delimiter));
            sb.Append(string.Format("Po Number{0}", delimiter));
            sb.Append(string.Format("Item Code{0}", delimiter));
            sb.Append(string.Format("Description{0}", delimiter));
            sb.Append(string.Format("Qty{0}", delimiter));
            sb.Append(string.Format("Price{0}", delimiter));
            sb.Append(Environment.NewLine);

            var itemsArray = items.Split(',');

            foreach (var item in itemsArray)
            {
                var invoiceItem = _invoicesService.InvoiceItemDetailById(item);

                sb.Append(string.Format("{0}{1}", invoice.Client, delimiter));
                sb.Append(string.Format("{0}{1}", "1100", delimiter));
                sb.Append(string.Format("{0}{1}", invoice.InvoiceDate.ToString("d"), delimiter));
                sb.Append(string.Format("{0}{1}", invoice.InvoiceNumber, delimiter));
                sb.Append(string.Format("{0}{1}", invoice.CustPo, delimiter));
                sb.Append(string.Format("{0}{1}", invoiceItem.CUST_PURCH_NUM, delimiter));
                sb.Append(string.Format("{0}{1}", invoiceItem.DESCRIPTION, delimiter));
                sb.Append(string.Format("{0}{1}", invoiceItem.QTY, delimiter));
                sb.Append(string.Format("{0}{1}", invoiceItem.UNIT_PRICE, delimiter));
                sb.Append(Environment.NewLine);
            }

            var byteArray = Encoding.ASCII.GetBytes(sb.ToString());
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/plain", string.Format("{0}.iif", invoice.InvoiceNumber));
        }

    }
}