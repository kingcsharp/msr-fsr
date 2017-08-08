using Msr.Models.ApprovalStages;
using Msr.Services.ApprovalGroups;
using Msr.Services.ApprovalStages;
using Msr.Services.ApprovalStages.VIewModels;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class ApprovalStagesController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "ApprovalStages";

            return View(viewModel);
        }

        public ActionResult ApprovalStagesData(JqGridParam param)
        {
            var approvalStagesService = new ApprovalStagesService();

            var totalRows = approvalStagesService.GetApprovalStagesQueryable();

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

            approvalStage.Setup(new ApprovalGroupsService(), new ApprovalStagesService());

            return View(approvalStage);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(EditApprovalStagesViewModel model)
        {
            var approvalStagesService = new ApprovalStagesService();
            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";
                var response = approvalStagesService.Create(model: model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Stage has been created successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";
                    model.Setup(new ApprovalGroupsService(), new ApprovalStagesService());
                    return View(model);
                }
            }
            return View();
        }

        public ActionResult Edit(string id)
        {
            var approvalStagesService = new ApprovalStagesService();

            var model = approvalStagesService.GetApprovalStageById(id);

            var approvalStage = new EditApprovalStagesViewModel();

            approvalStage = approvalStage.MapToDto(model);

            approvalStage.Setup(new ApprovalGroupsService(), new ApprovalStagesService());

            return View(approvalStage);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(EditApprovalStagesViewModel model)
        {
            var approvalStagesService = new ApprovalStagesService();
            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";

                var response = approvalStagesService.Edit(model: model);
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
    }
}