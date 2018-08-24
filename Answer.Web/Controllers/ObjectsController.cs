using Msr.Models.Objects;
using Msr.Services.jqGrid;
using Msr.Services.Objects;
using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;

namespace Answer.Web.Controllers
{
    [AuthorizeUser]
    public class ObjectsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetObjects(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

            return PartialView("_Objects");
        }

        public ActionResult ObjectsData(JqGridParam param)
        {
            var objectsService = new ObjectsService();

            var totalRows = objectsService.GetObjectsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ObjectView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ObjectView.ObjectTable))
                    {
                        totalRows = totalRows.Where(x => x.ObjectTable.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ObjectView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(ObjectView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ObjectView.ObjectTable);
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