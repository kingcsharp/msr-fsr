using Msr.Models.Documents;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.jqGrid;
using Msr.Services.Parts;
using Msr.Services.Roles;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Answer.Web.Controllers
{
    public class DocumentsController : BaseController
    {
        private readonly DocumentService _documentService;

        public DocumentsController()
        {
            _documentService = new DocumentService();
        }
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Documents";

            return View(viewModel);
        }

        public ActionResult DocumentsData(JqGridParam param)
        {
            var defaultStatusList = GetDefaultStatus();

            var totalRows = _documentService.GetDocumentsQueryable().Where(x => defaultStatusList.Contains(x.Status));

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
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.ApprovalDate.HasValue && q.ApprovalDate.Value.Day == value.Day &&
                                                             q.ApprovalDate.Value.Month == value.Month && q.ApprovalDate.Value.Year == value.Year);
                        }
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

            location.Setup(new RoleService(), new PartsService(), new DocumentFilesService(), new DocumentService(), GetCurrentUser().Id);

            location.Id = "NEW";
            location.Rev = 1;

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveDocumentViewModel model)
        {
            var currentUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                model.NTLogin = currentUser.Id;

                model.Company = currentUser.Company;

                var response = _documentService.Create(model);

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
            var model = _documentService.GetById(id);

            var location = new SaveDocumentViewModel();

            location = location.MapToDto(model);

            location.Setup(new RoleService(), new PartsService(), new DocumentFilesService(), _documentService, GetCurrentUser().Id);

            var preview = string.Join(",", location.DocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'", x.SERVER_PATH)));

            ViewBag.Preview = preview;

            var jsonSerialiser = new JavaScriptSerializer();

            var previewConfig = jsonSerialiser.Serialize(location.DocLinks
                .Select(x => new
                {
                    caption = x.NAME,
                    type = x.TYPE,
                    size = 6666,
                    url = Url.Action("DeletesingleReference", "Documents", new {linkDocId = x.LINKED_DOC_ID}),
                    downloadUrl = x.SERVER_PATH,
                    key = x.LINKED_DOC_ID
                }));

            ViewBag.PreviewConfig = previewConfig;

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveDocumentViewModel model)
        {
            var documentService = new DocumentService();

            if (ModelState.IsValid)
            {
                var currentUser = GetCurrentUser();

                model.NTLogin = currentUser.Id;

                model.Company = currentUser.Company;

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

        public ActionResult DeletesingleReference(string file)
        {
            var documentService = new DocumentService();

            var result = documentService.RemoveSingleFileReference(file);

            return Json(result ? "Ok" : "error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddsingleReference(string linkDocId, string files)
        {
            var result = false;

            foreach (var file in files.Split(','))
            {
                result = _documentService.SaveSingleFileReference(linkDocId, file, GetCurrentUser().Id);
            }

            return Json(result ? "Ok" : "error", JsonRequestBehavior.AllowGet);
        }
    }
}