using Msr.Models.ApprovalStages;
using Msr.Services.ApprovalGroups;
using Msr.Services.ApprovalStages;
using Msr.Services.ApprovalStages.VIewModels;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Microsoft.Ajax.Utilities;
using Msr.Models.Menus;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Approvals)]
    public class ApprovalStagesController : BaseController
    {
        private readonly ApprovalStagesService _approvalStagesService;
        private readonly ApprovalGroupsService _approvalGroupsService;

        public ApprovalStagesController()
        {
            _approvalStagesService = new ApprovalStagesService();
            _approvalGroupsService = new ApprovalGroupsService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "ApprovalStages";

            return View(viewModel);
        }

        public ActionResult ApprovalStagesData(JqGridParam param)
        {
            var user = GetCurrentUser();

            var totalStages = _approvalStagesService.GetApprovalStagesQueryable()
                .Where(x => (x.Hide != true && x.Hide == null) && x.CreatingCo == user.Company);

            var totalRows = totalStages.DistinctBy(x => new { x.Id, x.StageName }).ToList().AsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ApprovalStagesView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ApprovalStagesView.StageName))
                    {
                        totalRows = totalRows.Where(x => x.StageName.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ApprovalStagesView.StageName);

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
            var approvalStage = new EditApprovalStagesViewModel();
            var currentUser = GetCurrentUser();

            approvalStage.Setup(_approvalGroupsService, _approvalStagesService, currentUser.Id, currentUser.Company);

            return View(approvalStage);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(EditApprovalStagesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = GetCurrentUser();
                model.NtLogin = currentUser.Id;

                var response = _approvalStagesService.Create(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Stage has been created successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";
                    model.Setup(_approvalGroupsService, _approvalStagesService, currentUser.Id, currentUser.Company);
                    return View(model);
                }
            }
            return View();
        }

        public ActionResult Edit(string id)
        {
            var currentUser = GetCurrentUser();
            var model = _approvalStagesService.GetApprovalStageById(id);

            var approvalStage = new EditApprovalStagesViewModel();

            approvalStage = approvalStage.MapToDto(model);

            approvalStage.Setup(_approvalGroupsService, _approvalStagesService, currentUser.Id, currentUser.Company);

            return View(approvalStage);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(EditApprovalStagesViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NtLogin = GetCurrentUser().Id;


                var response = _approvalStagesService.Update(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Stage has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";
                return View(model);
            }

            return View(model);
        }

        public ActionResult Hide(string id)
        {
            var response = _approvalStagesService.HideStageWorkFlow(id);

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