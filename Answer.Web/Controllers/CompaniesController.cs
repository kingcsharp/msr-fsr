using Msr.Models.Companies;
using Msr.Services.Companies;
using Msr.Services.Companies.ViewModels;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Msr.Services.Documents;
using Msr.Infrastructure.Common.Constansts;
using Msr.Services.Roles;

namespace Answer.Web.Controllers
{
    public class CompaniesController : BaseController
    {
        private readonly RoleService _roleService;
        private readonly CompanyService _companyService;
        private readonly DocumentFilesService _documentFilesService;

        public CompaniesController()
        {
            _roleService = new RoleService();
            _companyService = new CompanyService();
            _documentFilesService = new DocumentFilesService();
        }

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
            var totalRows = _companyService.GetCompaniesQueryable();

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

            company.Setup(_documentFilesService, _companyService);

            return View(company);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(AddCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = _companyService.Create(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Company has been created successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";
                    model.Setup(_documentFilesService, _companyService);
                    return View(model);
                }
            }

            return View();
        }

        public ActionResult Edit(string id)
        {
            var model = _companyService.GetCompanyByObjId(id);

            model.Setup(_documentFilesService, _companyService, GetCurrentUser().Id);

            var preview = string.Join(",", model.DocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'", x.SERVER_PATH)));
            ViewBag.Preview = preview;

            var jsonSerialiser = new JavaScriptSerializer();
            var previewConfig = jsonSerialiser.Serialize(model.DocLinks.Select(x => new
            {
                caption = x.NAME,
                type = x.TYPE,
                size = 6666,
                url = Url.Action("DeletesingleReference", "Documents", new {file = x.LINKED_DOC_ID}),
                downloadUrl = x.SERVER_PATH,
                key = x.LINKED_DOC_ID
            }));

            ViewBag.PreviewConfig = previewConfig;

            return View(model);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(EditCompanyViewModel model)
        {
            var companyService = new CompanyService();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = companyService.Update(model: model);

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

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase postedFile)
        {
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                var currentUser = GetCurrentUser();

                var response = _companyService.ImportCompanies(postedFile, currentUser);

                if (response.HasErrors())
                {
                    TempData[NotificationConstants.ErrrorMessage] = response.ErrorMessage;
                }
                else
                {
                    if (response.Entity.Any(x => !x.Processed))
                    {
                        TempData[NotificationConstants.WarningMessage] = "File has been processed with errors";
                    }
                    else
                    {
                        TempData[NotificationConstants.SuccessMessage] = "File has been processed successfully";
                    }
                }

                return View(response.Entity);
            }

            TempData[NotificationConstants.ErrrorMessage] = "Please upload a file";

            return View();
        }

        public void ImportSampleFile()
        {
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=parts-upload.csv");
            Response.Write(string.Join(",", CompanyImportViewModel.GetHeaderColumns()));
            Response.End();
        }
    }
}