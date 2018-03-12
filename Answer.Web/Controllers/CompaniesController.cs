using Msr.Models.Companies;
using Msr.Services.Companies;
using Msr.Services.Companies.ViewModels;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Services.Documents;
using Msr.Services.Workflows;

namespace Answer.Web.Controllers
{
    public class CompaniesController : BaseController
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Companies";

            return View(viewModel);
        }

        public ActionResult GetCompanies(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

            return PartialView("_Companies");
        }

        public ActionResult CompaniesData(JqGridParam param)
        {
            var companyService = new CompanyService();

            var totalRows = companyService.GetCompaniesQueryable();

            var defaultStatusList = base.GetDefaultStatus();

            totalRows = totalRows.Where(x => defaultStatusList.Contains(x.Status) && x.Id.Length > 0);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(CompanyView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(CompanyView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CompanyView.ParentName))
                    {
                        totalRows = totalRows.Where(x => x.ParentName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CompanyView.PicRecord))
                    {
                        totalRows = totalRows.Where(x => x.PicRecord.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CompanyView.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CompanyView.CoType))
                    {
                        totalRows = totalRows.Where(x => x.CoType.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CompanyView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CompanyView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(CompanyView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }

                }
            }
            var orderBy = nameof(CompanyView.Name);
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
        public ActionResult Create()
        {
            var company = new AddCompanyViewModel();

            company.Setup(new DocumentFilesService(), new CompanyService());

            return View(company);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(AddCompanyViewModel model)
        {
            var userService = new CompanyService();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = userService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Company has been created successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";
                    model.Setup(new DocumentFilesService(), new CompanyService());
                    return View(model);
                }
            }

            return View();
        }
        public ActionResult Edit(string id)
        {
            var currrentUser = GetCurrentUser();
            var workflowService = new WorkflowService();
            var checkoutEntity = workflowService.CheckOutObject(id, currrentUser.Id);

            var taskService = new CompanyService();

            var model = taskService.GetCompanyByObjId(checkoutEntity.Entity);

            model.Setup(new DocumentFilesService(), new CompanyService(), GetCurrentUser().Id);

            return View(model);            
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(EditCompanyViewModel model)
        {
            var companyService = new CompanyService();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = companyService.Edit(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Company has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            return View(model);
        }
        public ActionResult Delete(string id)
        {
            var taskService = new CompanyService();
           
            string ntLogin = GetCurrentUser().Id;

            var response = taskService.Delete(id, ntLogin);

            if (response)
            {
                TempData["SuccessMessage"] = "Company deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";

            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            var taskService = new CompanyService();

            var model = taskService.GetCompanyByObjId(id);

            model.Setup(new DocumentFilesService(), new CompanyService(), GetCurrentUser().Id);

            return View(model);
        }
    }
}