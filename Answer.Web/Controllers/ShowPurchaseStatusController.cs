using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.ProductsActualPart;
using Msr.Models.ShowPurchaseStatus;
using Msr.Services.jqGrid;
using Msr.Services.ProductsActualParts;
using Msr.Services.ShowPurchaseStatus;

namespace Answer.Web.Controllers
{
    public class ShowPurchaseStatusController : Controller
    {
        // GET: ShowPurchaseStatus
        public ActionResult Index(string id)
        {
            var showPurchaseStatusService = new ShowPurchaseStatusService();

            var viewModel = showPurchaseStatusService.GetPurchaseItem(id: id);

            ViewBag.TaskId = id;
            ViewBag.ActiveClass = "Purchase Status Page";

            return View(viewModel);
        }
        public ActionResult ShowPurchaseStatusData(JqGridParam param, string id)
        {
            var showPurchaseStatusService = new ShowPurchaseStatusService();

            var totalRows = showPurchaseStatusService.GetPurchaseStatusQueryable(id: id).AsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ShowPurchaseStatusView.DESCRIPTION))
                    {
                        totalRows = totalRows.Where(x => x.DESCRIPTION == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ShowPurchaseStatusView.PARENT_ID))
                    {
                        totalRows = totalRows.Where(x => x.PARENT_ID.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ShowPurchaseStatusView.LAST_COMMENT))
                    {
                        totalRows = totalRows.Where(x => x.LAST_COMMENT.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ProductsActualPartView.ProductName);

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
