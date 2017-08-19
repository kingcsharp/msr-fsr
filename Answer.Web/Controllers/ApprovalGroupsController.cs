using Msr.Models.ApprovalGroups;
using Msr.Services.ApprovalGroups;
using Msr.Services.ApprovalGroups.ViewModels;
using Msr.Services.jqGrid;
using Msr.Services.Roles;
using Msr.Services.Users;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class ApprovalGroupsController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "ApprovalGroups";

            return View(viewModel); 
        }

        public ActionResult ApprovalGroupsData(JqGridParam param)
        {
            var approvalGroupsService = new ApprovalGroupsService();

            var totalRows = approvalGroupsService.GetApprovalGroupsQueryable().Where(x=>x.Hide!=true);

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

            approvalGroup.Setup(new UserService(), new RoleService(), new ApprovalGroupsService());

            return View(approvalGroup);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(EditApprovalGroupsViewModel model)
        {
            var approvalGroupsService = new ApprovalGroupsService();

            if (ModelState.IsValid)
            {
                model.NTLogin = "1618";

                var response = approvalGroupsService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Group has been created successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new UserService(), new RoleService(), new ApprovalGroupsService());
                    return View(model);
                }
            }

            return View();
        }

        public ActionResult Edit(string id)
        {
            var approvalGroupsService = new ApprovalGroupsService();

            var model = approvalGroupsService.GetApprovalGroupById(id);

            var approvalStage = new EditApprovalGroupsViewModel();

            approvalStage = approvalStage.MapToDto(model);

            approvalStage.Setup(new UserService(), new RoleService(), new ApprovalGroupsService());

            return View(approvalStage);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(EditApprovalGroupsViewModel model)
        {

            var approvalGroupsService = new ApprovalGroupsService();
            if (ModelState.IsValid)
            {
                model.NTLogin = "1618";

                var response = approvalGroupsService.Edit(model: model);

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
            var taskService = new ApprovalGroupsService();

            var response = taskService.HideGroupWorkFlow(id);

            if (response != null)
            {
                TempData["SuccessMessage"] = "Workflow Hide successfully.";
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