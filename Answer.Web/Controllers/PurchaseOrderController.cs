using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.jqGrid;
using Msr.Services.PurchesOrder;
using Msr.Models.PurchesOrder;

namespace Answer.Web.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ActiveClass = "PurchaseOrder";

            return View();
        }

        public ActionResult PurchaseOrderData(JqGridParam param)
        {
            var purchaseOrderService = new PurchesOrderService();

            var totalRows = purchaseOrderService.GetPurchesOrderQueryable();
       
            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                  
                    if (rule.field == nameof(PurchesOrderView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PurchesOrderView.ReferencePo))
                    {
                        totalRows = totalRows.Where(x => x.ReferencePo.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.ReferenceName))
                    {
                        totalRows = totalRows.Where(x => x.ReferenceName.ToLower().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchesOrderView.InvoicedBalance))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.InvoicedBalance == value);
                        }
                    }
                    else if (rule.field == nameof(PurchesOrderView.UninvoicedBalance))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.UninvoicedBalance == value);
                        }
                    }
                 
                    else if (rule.field == nameof(PurchesOrderView.Balance))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Balance == value);
                        }
                    }

                    else if (rule.field == nameof(PurchesOrderView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName.ToLower().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchesOrderView.CustomerCo))
                    {
                        totalRows = totalRows.Where(x => x.CustomerCo.ToLower().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchesOrderView.OpenDate))
                    {
                        totalRows = totalRows.Where(x => x.OpenDate.ToString().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.CloseDate))
                    {
                        totalRows = totalRows.Where(x => x.CloseDate.ToString().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchesOrderView.TotalPurchaseLimit))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.TotalPurchaseLimit == value);
                        }
                    }
                    else if (rule.field == nameof(PurchesOrderView.AccType))
                    {
                        totalRows = totalRows.Where(x => x.AccType.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.InvoiceTrigger))
                    {
                        totalRows = totalRows.Where(x => x.InvoiceTrigger.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.UnusedAmmount))
                    {
                        decimal value;

                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.UnusedAmmount == value);
                        }
                    }
                    else if (rule.field == nameof(PurchesOrderView.Rev))
                    {
                        int value;
                        if (int.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }

                    else if (rule.field == nameof(PurchesOrderView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }

                }
            }
            var orderBy = nameof(PurchesOrderView.Name);

            var orderDirection = "asc";

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
    }
}