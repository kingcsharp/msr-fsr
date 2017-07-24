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

namespace Answer.Web.Controllers
{
    public class CompaniesController : Controller
    {
        // GET: Companies
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Companies";

            return View(viewModel);
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
                    else if (rule.field == nameof(CompanyView.Rev))
                    {
                        var rev = Convert.ToInt32(rule.data);
                        totalRows = totalRows.Where(x => x.Rev == rev);
                    }
                    else if (rule.field == nameof(CompanyView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
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
                //Need to dynamic 
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
    }
}