using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Users;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class BuyerController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ActiveClass = "WIP";

            var loggedUser = User.Identity.GetUserId();

            var userService = new UserService();

            var company = userService.GetCompanyId(loggedUser);

            ViewBag.ClientName = company.Name;

            return View();
        }

        public ActionResult BuyerData(JqGridParam param)
        {
            var loggedUser = User.Identity.GetUserId();

            var userService = new UserService();
            var taskService = new TaskService();

            var company = userService.GetCompanyId(loggedUser);

            var orderService = new OrderService();

            var totalRows = orderService.GetBuyerWorkOrderQueryable().Where(x => x.CustId == company.Id);

            if (param.where !=null && param.where.rules.Any())
            {
                foreach (var rule    in param.where.rules)
                {
                    if (rule.field == nameof(BuyerView.PurchaseItemId))
                    {
                        totalRows = totalRows.Where(x => x.PurchaseItemId == rule.data);
                    }
                    else if (rule.field == nameof(BuyerView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(BuyerView.Serial))
                    {
                        totalRows = totalRows.Where(x => x.Serial.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(BuyerView.CustPurchNum))
                    {
                        totalRows = totalRows.Where(x => x.CustPurchNum.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(BuyerView.Qty))
                    {
                        double value;
                        if (double.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Qty == value);
                        }
                    }
                    else if (rule.field == nameof(BuyerView.StartDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                                totalRows = totalRows.Where(q => q.StartDate.HasValue && q.StartDate.Value.Day ==value.Day && q.StartDate.Value.Month == value.Month && q.StartDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(BuyerView.DueDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.DueDate.HasValue && q.DueDate.Value.Day == value.Day && q.DueDate.Value.Month == value.Month && q.DueDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(BuyerView.ProcName))
                    {
                        totalRows = totalRows.Where(x => x.ProcName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(BuyerView.InvoiceId))
                    {
                        totalRows = totalRows.Where(x => x.InvoiceId.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(BuyerView.CurStepText))
                    {
                        totalRows = totalRows.Where(x => x.CurStepText.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(BuyerView.InvoiceAmount))
                    {
                        Decimal value;
                        if (Decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.InvoiceAmount == value);
                        }
                    }
                    else if (rule.field == nameof(BuyerView.InvoiceDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.InvoiceDate.HasValue && q.InvoiceDate.Value.Day == value.Day && q.InvoiceDate.Value.Month == value.Month && q.InvoiceDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(BuyerView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            string orderBy = nameof(BuyerView.SupplierName);
            string orderDirection = "asc";

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
            totalRows = totalRows.Skip(param.pageIndex-1);
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);

            var results = totalRows.ToList();

            foreach (var r in results)
            {
                r.HasMonitor = taskService.CheckHasMonitors(r.FillId);
            }

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