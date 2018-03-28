using Msr.Models.ApprovalGroups;
using Msr.Services.ApprovalGroups;
using Msr.Services.ApprovalGroups.ViewModels;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class ApprovalGroupsController : BaseController
    {
        private readonly ApprovalGroupsService _approvalGroupsService;

        public ApprovalGroupsController()
        {
            _approvalGroupsService = new ApprovalGroupsService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "ApprovalGroups";

            return View(viewModel);
        }

        public ActionResult ApprovalGroupsData(JqGridParam param)
        {
            var currentUser = GetCurrentUser();

            var totalRows = _approvalGroupsService.GetApprovalGroupsQueryable().Where(x => x.Hide == null && x.CreatingCo == currentUser.Company);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ApprovalGroupsView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ApprovalGroupsView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ApprovalGroupsView.Name);

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

        public ActionResult Create()
        {
            var approvalGroup = new EditApprovalGroupsViewModel();

            var currentUser = GetCurrentUser();

            approvalGroup.Setup(_approvalGroupsService, currentUser.Id, currentUser.Company);

            return View(approvalGroup);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(EditApprovalGroupsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = GetCurrentUser();

                model.NTLogin = currentUser.Id;

                var response = _approvalGroupsService.Create(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Group has been created successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(_approvalGroupsService, currentUser.Id, currentUser.Company);
                    return View(model);
                }
            }

            return View();
        }

        public ActionResult Edit(string id)
        {
            var currentUser = GetCurrentUser();

            var model = _approvalGroupsService.GetApprovalGroupById(id);

            var approvalStage = new EditApprovalGroupsViewModel();

            approvalStage = approvalStage.MapToDto(model);

            approvalStage.Setup(_approvalGroupsService, currentUser.Id, currentUser.Company);

            return View(approvalStage);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(EditApprovalGroupsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = GetCurrentUser();

                model.NTLogin = currentUser.Id;

                var response = _approvalGroupsService.Edit(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Group has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            return View(model);
        }

        public ActionResult Hide(string id)
        {
            var response = _approvalGroupsService.HideGroupWorkFlow(id);

            if (response != null)
            {
                TempData["SuccessMessage"] = "Workflow successfully hidden.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong.";
                return RedirectToAction("Index");
            }

        }
    }
}