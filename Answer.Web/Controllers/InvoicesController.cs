using Msr.Models.Invoices;
using Msr.Services.Invoices;
using Msr.Services.Invoices.ViewModel;
using Msr.Services.jqGrid;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class InvoicesController :  BaseController
    {
        private readonly InvoicesService _invoicesService;

        public InvoicesController()
        {
            _invoicesService = new InvoicesService();
        }

        public ActionResult Index()
        {
            var viewModel = new InvoiceViewModel();
            viewModel.NewItemsAmt = _invoicesService.GetInvoicesTotalDebitAmount();
            viewModel.AmtPaid = _invoicesService.GetInvoicesTotalPaidAmount();

           if(viewModel.AmtPaid == null)
            {
                viewModel.AmtPaid =Convert.ToDecimal("0.00");
            }
            ViewBag.ActiveClass = "Invoices";

            return View(viewModel);
        }
        
       public ActionResult InvoicesData(JqGridParam param)
        {

            var totalRows = _invoicesService.GetInvoicesQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(InvoiceView.InvoiceId))
                    {
                        totalRows = totalRows.Where(x => x.InvoiceId == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(InvoiceView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.PoNumber))
                    {
                        totalRows = totalRows.Where(x => x.PoNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.ReferencePo))
                    {
                        totalRows = totalRows.Where(x => x.ReferencePo.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.CustPurchNum))
                    {
                        totalRows = totalRows.Where(x => x.CustPurchNum.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(InvoiceView.AcctName))
                    {
                        totalRows = totalRows.Where(x => x.AcctName.ToLower().Contains(rule.data.ToLower()));
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

            var orderBy = nameof(InvoiceView.InvoiceId);

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

       [HttpGet]
       public ActionResult Detail(string id)

        {
            var invoiceDetail = new InvoiceDetailViewModel();

            invoiceDetail = _invoicesService.InvoiceDetailById(id);

            invoiceDetail.SetUp(new InvoicesService(), GetCurrentUser().Id);

            return View(invoiceDetail);
        }

        [HttpPost]
        public ActionResult Detail(InvoiceDetailViewModel model,string ButtonType)
        {
            var invoiceService = new InvoicesService();

            if (ModelState.IsValid)
            {
                var response = invoiceService.Create(model,GetCurrentUser().Id);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Invoice has been created successfully.";

                    return RedirectToAction("Detail", "Invoices", model.INVOICE_ID);
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.SetUp(new InvoicesService(), GetCurrentUser().Id);

                    return View(model);
                }

            }

            model.SetUp(new InvoicesService(), GetCurrentUser().Id);

            return View(model);
        }


        [HttpPost]
        public ActionResult InvoiceToCustomer(string invoiceId,string accId,DateTime? invoiceDate,DateTime? dueDate)
        {
            var invoiceDetail = new InvoiceDetailViewModel();
            var responce = _invoicesService.SendInvoiceToCustomer(invoiceId, accId, invoiceDate, dueDate,GetCurrentUser().Id);

            return Json(responce, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult InvoiceListDetail(string purchaseId)
        {
            var invoiceDetail = new InvoiceViewPurchaseItemViewModels();

            invoiceDetail.SetUp(_invoicesService, purchaseId,GetCurrentUser().Id);

            return View(invoiceDetail);
        }

        [HttpGet]
        public ActionResult EditCreditDebit(string invoiceId,string accId,string id)
        {
            var model = new InvoiceDetailEditViewModel();

            var invoiceService = new InvoicesService();
            if(id!=null)
            {
                model = invoiceService.InvoiceEditDetailById(id);
                invoiceId = model.INVOICE_ID;
                accId = model.ACCOUNT_ID;
            }
            model.SetUp(new InvoicesService(), invoiceId, accId, GetCurrentUser().Id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditCreditDebit(InvoiceDetailEditViewModel model,string ButtonType)
       {
            var invoiceService = new InvoicesService();
           
            if (ModelState.IsValid)
            {
                if(ButtonType== "saveAndClose")
                {
                    var response = invoiceService.EditDebitCreadit(model, GetCurrentUser().Id);
                    if (!response.HasErrors())
                    {
                        TempData["SuccessMessage"] = "Invoice has been created successfully.";

                        return RedirectToAction("Detail", "Invoices", new { id=model.INVOICE_ID});
                    }
                }
                else
                {
                   var response = invoiceService.EditDebitCreadit(model, GetCurrentUser().Id);

                    if (!response.HasErrors())
                    {
                        TempData["SuccessMessage"] = "Invoice has been created successfully.";

                        return RedirectToAction("EditCreditDebit", "Invoices", new { model.INVOICE_ID, model.ACCOUNT_ID });
                    }
                }
            }
                TempData["ErrorMessage"] = "Something went wrong.";
            
            model.SetUp(new InvoicesService(), model.INVOICE_ID, model.ACCOUNT_ID, GetCurrentUser().Id);

            return View(model);
        }

        [HttpGet]
        public ActionResult PrintableInvoice(string id)
        {
            var invoiceDetail = new InvoiceDetailViewModel();

            invoiceDetail = _invoicesService.InvoiceDetailById(id);

            invoiceDetail.SetUp(new InvoicesService(), GetCurrentUser().Id);

            return View(invoiceDetail);
        }

        public ActionResult InvoiceDetailDelete(string id,string invoiceId)
        {
            var response = _invoicesService.Delete(id: id, ntlogin: GetCurrentUser().Id);

            if (response)
            {
                TempData["SuccessMessage"] = "Invoice deleted successfully.";

                return RedirectToAction("Detail", "Invoices",new { id = invoiceId });
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Detail", "Invoices", new { id = invoiceId });
        }

    }
}