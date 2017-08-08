using Msr.Models.Documents;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.jqGrid;
using Msr.Services.Parts;
using Msr.Services.Roles;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class DocumentsController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Documents";

            return View(viewModel);
        }

        public ActionResult DocumentsData(JqGridParam param)
        {
            var documentService = new DocumentService();

            var totalRows = documentService.GetDocumentsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(DocumentView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(DocumentView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DocumentView.CreatingCoName))
                    {
                        totalRows = totalRows.Where(x => x.CreatingCoName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DocumentView.DeptName))
                    {
                        totalRows = totalRows.Where(x => x.DeptName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DocumentView.SecurityLevel))
                    {
                        totalRows = totalRows.Where(x => x.SecurityLevel.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DocumentView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(DocumentView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(DocumentView.ApprovalDate))
                    {
                        totalRows = totalRows.Where(x => x.ApprovalDate.ToString().Contains(rule.data));
                    }
                    else if (rule.field == nameof(DocumentView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            string orderBy = param.sortColumn;

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
            var location = new SaveDocumentViewModel();

            location.Setup(new RoleService(), new PartsService(),new DocumentService());

            location.Id = "NEW";
            location.Rev = 1;
            
            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveDocumentViewModel model)
        {
            var documentService = new DocumentService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";
                //need to be from logedin user company id
                model.Company = "2";

                var response = documentService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Document has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    return View(model);
                }
            }

            return View(model);

        }

        public ActionResult Edit(string id)
        {
            var documentService = new DocumentService();

            var model = documentService.GetById(id);

            var location = new SaveDocumentViewModel();

            location = location.MapToDto(model: model);

            location.Setup(new RoleService(),new PartsService(), new DocumentService());

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveDocumentViewModel model)
        {
            var documentService = new DocumentService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";
                //need to be from logedin user company id
                model.Company = "2";

                var response = documentService.Save(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Document has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            return View(model);
        }
    }
}