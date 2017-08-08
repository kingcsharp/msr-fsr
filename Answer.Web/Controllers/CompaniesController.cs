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
using Msr.Infrastructure.Common;

namespace Answer.Web.Controllers
{
    public class CompaniesController : Controller
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
            company.Setup();
            return View(company);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(AddCompanyViewModel model)
        {
            var userService = new CompanyService();

            if (ModelState.IsValid)
            {
                model.NTLogin = "1618";

                var response = userService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Company has been created successfully.";
                    return RedirectToAction("Companies");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";
                    model.Setup();
                    return View(model);
                }
            }

            return View();
        }
        public ActionResult Edit(string id)
        {
            var taskService = new CompanyService();

            var model = taskService.GetCompanyByObjId(id);

            model.Setup(new DocumentFilesService(), new CompanyService());

            return View(model);            
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(EditCompanyViewModel model)
        {
            var companyService = new CompanyService();

            if (ModelState.IsValid)
            {
                model.NTLogin = "1618";

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
    }
}