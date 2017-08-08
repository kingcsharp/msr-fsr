using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.ApprovalWorkflows;
using Msr.Services.ApprovalWorkflows;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;

namespace Answer.Web.Controllers
{
    public class ApprovalWorkflowsController : Controller
    {
        
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Approval Workflows";

            return View(viewModel);
        }

        public ActionResult ApprovalWorkflowsData(JqGridParam param)
        {
            var taskService = new ApprovalWorkflowsService();

            var totalRows = taskService.GetApprovalWorkflowsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ApprovalWorkflowsView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ApprovalWorkflowsView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    
                }
            }

            var orderBy = nameof(ApprovalWorkflowsView.Id);

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

        public ActionResult Edit(string id)
        {
            return null;
        }
    }
}