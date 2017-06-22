using Msr.Models.Parts;
using Msr.Models.Tasks;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Parts;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class PartsController : Controller
    {
        // GET: Parts
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Parts";

            return View(viewModel);
        }
        public ActionResult PartsData(JqGridParam param)
        {
            var taskService = new PartsService();

            var totalRows = taskService.GetPartsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PartsView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PartsView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.CompanyPartNumber))
                    {
                        totalRows = totalRows.Where(x => x.CompanyPartNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.CompanyName))
                    {
                        totalRows = totalRows.Where(x => x.CompanyName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.Revision))
                    {
                        var rev = Convert.ToInt32(rule.data);
                        totalRows = totalRows.Where(x => x.Revision == rev);
                    }
                    else if (rule.field == nameof(PartsView.ToString))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(PartsView.CompanyPartNumber);
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