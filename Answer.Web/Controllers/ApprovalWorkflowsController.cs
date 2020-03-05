using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.ApprovalWorkflows;
using Msr.Models.Menus;
using Msr.Services.ApprovalWorkflows;
using Msr.Services.ApprovalWorkflows.ViewModels;
using Msr.Services.Documents;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Approvals)]
    public class ApprovalWorkflowsController : BaseController
    {
        private readonly ApprovalWorkflowsService _approvalWorkflowsService;

        public ApprovalWorkflowsController()
        {
            _approvalWorkflowsService = new ApprovalWorkflowsService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Approval Workflows";

            return View(viewModel);
        }

        public ActionResult ApprovalWorkflowsData(JqGridParam param)
        {
            var myCo = GetCurrentUser();

            var totalRows = _approvalWorkflowsService.GetApprovalWorkflowsQueryable().Where(x => x.Creating_Co == myCo.Root_Company);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ApprovalWorkflowsView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(ApprovalWorkflowsView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }

                }
            }

            var orderBy = nameof(ApprovalWorkflowsView.Id);

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
            var model = _approvalWorkflowsService.GetApprovalWorkflowsById(id);

            var vm = new ApprovalWorkflowsViewModel();
            vm = vm.MapToDto(model);
            vm.Setup(new DocumentFilesService(), _approvalWorkflowsService);
            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ApprovalWorkflowsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;
                var response = _approvalWorkflowsService.Edit(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Workflows has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";
                return View(model);
            }

            return View(model);
        }

        public ActionResult Add()
        {
            var vm = new ApprovalWorkflowsViewModel();
            vm.Setup(new DocumentFilesService(), _approvalWorkflowsService);

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(ApprovalWorkflowsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;
                var response = _approvalWorkflowsService.Add(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Workflow has been added successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";
                return View(model);
            }

            return View(model);
        }

        public ActionResult Hide(string id)
        {
            var response = _approvalWorkflowsService.HideApplrovalWorkflow(id);

            if (response)
            {
                TempData["SuccessMessage"] = "Workflow successfully hidden.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }
    }
}