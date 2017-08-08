using Msr.Models.Roles;
using Msr.Services.jqGrid;
using Msr.Services.Roles;
using Msr.Services.Roles.ViewModels;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class RolesController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Roles";

            return View(viewModel);
        }

        public ActionResult UserRolesData(JqGridParam param)
        {
            var roleService = new RoleService();

            var defaultStatusList = new [] { "CREATING", "DENIED", "APPROVED", "APPROVED_BUT_REVISING", "APPROVED_BUT_DELETING" };

            var totalRows = roleService.GetUserRolesQueryable().Where(x=> defaultStatusList.Contains(x.Status));

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(RolesView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(RolesView.RoleName))
                    {
                        totalRows = totalRows.Where(x => x.RoleName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(RolesView.SecurityLevelName))
                    {
                        totalRows = totalRows.Where(x => x.SecurityLevelName.ToLower() == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(RolesView.SecurityLevel))
                    {
                        totalRows = totalRows.Where(x => x.SecurityLevel.ToLower() == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(RolesView.Revision))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Revision == value);
                        }
                    }
                    else if (rule.field == nameof(RolesView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }
            var orderBy = param.sortColumn;

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
            var saveRoleViewModel = new SaveRoleViewModel();
            saveRoleViewModel.Setup();

            return View(saveRoleViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveRoleViewModel model)
        {
            var roleService = new RoleService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";

                var response = roleService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "User Role has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup();

                    return View(model);
                }
            }

            model.Setup();

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var roleService = new RoleService();

            var model = roleService.GetRoleByid(Id: id);

            var saveRoleViewModel = new SaveRoleViewModel();

            saveRoleViewModel = saveRoleViewModel.MapToDto(model);
            saveRoleViewModel.Setup();

            return View(saveRoleViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveRoleViewModel model)
        {
            var roleService = new RoleService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";

                var response = roleService.Edit(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "User Role has been Updated successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup();

                    return View(model);
                }
            }

            model.Setup();

            return View(model);
        }
    }
}