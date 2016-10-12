using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Orders;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class WipController : Controller
    {
        public ActionResult Engineering()
        {
            return View();
        }

        public ActionResult EngineeringData(JqGridParam param)
        {

            var orderService = new OrderService();

            var totalRows = orderService.GetWorkOrderQueryable();


            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule    in param.where.rules)
                {
                    if (rule.field == nameof(WorkOrderView.PurchaseItemId))
                    {
                        totalRows = totalRows.Where(x => x.PurchaseItemId == rule.data);
                    }
                    else if (rule.field == nameof(WorkOrderView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.Serial))
                    {
                        totalRows = totalRows.Where(x => x.Serial.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.CustPurchNum))
                    {
                        totalRows = totalRows.Where(x => x.CustPurchNum.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.Qty))
                    {
                        double value;
                        if (double.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Qty == value);
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.StartDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows =
                                totalRows.Where(
                                    q =>
                                        q.StartDate.HasValue && q.StartDate.Value.Day == value.Day &&
                                        q.StartDate.Value.Month == value.Month && q.StartDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.DueDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows =
                                totalRows.Where(
                                    q =>
                                        q.DueDate.HasValue && q.DueDate.Value.Day == value.Day &&
                                        q.DueDate.Value.Month == value.Month && q.DueDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.ProcName))
                    {
                        totalRows = totalRows.Where(x => x.ProcName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.CurStepText))
                    {
                        totalRows = totalRows.Where(x => x.CurStepText.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            string orderBy = "SupplierName";
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
            totalRows = totalRows.Skip(param.pageIndex - 1);
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int) Math.Ceiling((float) totalRecords/(float) param.pageSize);


            var results = totalRows.Select(x => new
            {
                x.PurchaseItemId,
                x.SupplierName,
                x.Serial,
                x.CustPurchNum,
                x.Qty,
                x.StDate,
                x.ActualStartDate,
                x.ProductName,
                x.ProcName,
                x.CurStepText,
                x.PurchaseId,
                x.ActualPartId,
                x.TimeComplete,
                x.PercComplete,
                x.HasFile,
                x.HasMonitor,
                x.HasNcr,
                x.FillId
            }).ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetNcrModel(string id)
        {
            var orderService = new OrderService();

            var ncrDetails = orderService.GetNcrDetails(id);

            return PartialView("_NcrModel", ncrDetails);
        }

        public ActionResult GetPhotsModel()
        {
            return PartialView("_Photos");
        }

        public ActionResult GetMonitorsModel()
        {

            return PartialView("_Monitors");
        }
    }
}