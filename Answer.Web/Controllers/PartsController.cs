using Msr.Models.Parts;
using Msr.Services.jqGrid;
using Msr.Services.Parts;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Answer.Web.ViewModel;
using Msr.Services.Documents;
using Msr.Services.Parts.ViewModels;
using Msr.Services.PartTypes;
using Msr.Services.Locations;
using Msr.Services.Roles;
using Msr.Services.Roles.Messages;
using Msr.Services.Workflows;

namespace Answer.Web.Controllers
{
    public class PartsController : BaseController
    {
        private readonly LocationService _locationService;
        private readonly PartsService _partsService;
        private readonly RoleService _roleService;
        private readonly PartTypeService _partTypeService;
        private readonly DocumentFilesService _documentFilesService;

        public PartsController()
        {
            _locationService = new LocationService();
            _partsService = new PartsService();
            _roleService = new RoleService();
            _partTypeService = new PartTypeService();
            _documentFilesService = new DocumentFilesService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Parts";

            return View(viewModel);
        }

        public ActionResult PartsData(JqGridParam param)
        {
            var taskService = new PartsService();

            var totalRows = taskService.GetPartsQueryable();

            var defaultStatusList = new[] { "CREATING", "DENIED", "APPROVED", "APPROVED_BUT_REVISING", "APPROVED_BUT_DELETING" };

            totalRows = totalRows.Where(x => defaultStatusList.Contains(x.Status) && x.CompanyPartNumber != null);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PartsView.ObjectId))
                    {
                        totalRows = totalRows.Where(x => x.ObjectId == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PartsView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.Spare))
                    {
                        totalRows = totalRows.Where(x => x.Spare.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.Unit))
                    {
                        totalRows = totalRows.Where(x => x.Unit.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.CompanyPartNumber))
                    {
                        totalRows = totalRows.Where(x => x.CompanyPartNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.CompanyName))
                    {
                        totalRows = totalRows.Where(x => x.CompanyName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.Consumable))
                    {
                        totalRows = totalRows.Where(x => x.Consumable.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(PartsView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }

            var orderBy = nameof(PartsView.CompanyPartNumber);

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

        public ActionResult Details(string id)
        {
            var taskService = new PartsService();

            var model = taskService.GetById(id);

            var part = new AddPartViewModel();

            part = part.MapToDto(model);

            part.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService(), GetCurrentUser().Company, GetCurrentUser().Id);

            return View(part);
        }

        public ActionResult ViewFile(string callBackitem)
        {
            var callBackUrl = "http://docs.google.com/gview?url=" + "http://infolab.stanford.edu/pub/papers/google.pdf&embedded=true";//callBackitem url need to be dynamic
            ViewBag.callBackitem = callBackUrl;

            return PartialView("_ViewFile");
        }
        public ActionResult AddPart()
        {
            var part = new AddPartViewModel();

            part.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService(), GetCurrentUser().Company, GetCurrentUser().Id);

            return View(part);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPart(AddPartViewModel model)
        {
            var partsService = new PartsService();

            var curerntUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                model.NTLogin = curerntUser.Id;
                model.SubParts = null;
                model.Company = curerntUser.Company;

                var response = partsService.Create(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Part has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService(), curerntUser.Company,curerntUser.Id);

                    return View(model);
                }

            }

            model.Setup(new DocumentFilesService(), _partsService,_partTypeService, curerntUser.Company, curerntUser.Id);;

            return View(model);

        }
        public ActionResult Edit(string id)
        {
            var currrentUser = GetCurrentUser();
            var workflowService = new WorkflowService();
            var checkoutEntity = workflowService.CheckOutObject(id, currrentUser.Id);

            var model = _partsService.GetById(checkoutEntity.Entity);

            var part = new AddPartViewModel();

            part = part.MapToDto(model);

            part.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService(), currrentUser.Company, currrentUser.Id);

            part.InternalEqualParts = _partsService.GetInternalEqualPartByPartId(part.Id);

            part.InternalEqualParts = _partsService.GetInternalEqualPartByPartId(part.Id);

            var preview = string.Join(",", part.DocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'", x.SERVER_PATH)));
            ViewBag.Preview = preview;

            var jsonSerialiser = new JavaScriptSerializer();
            var previewConfig = jsonSerialiser.Serialize(part.DocLinks.Select(x => new
            {
                caption = x.NAME,
                type = x.TYPE,
                size = 6666,
                url = Url.Action("DeletesingleReference", "Documents", new {file = x.LINKED_DOC_ID}),
                downloadUrl = x.SERVER_PATH,
                key = x.LINKED_DOC_ID
            }));

            ViewBag.PreviewConfig = previewConfig;

            return View(part);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AddPartViewModel model)
        {
            var currentUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;
                model.SubParts = null;

                var response = _partsService.Edit(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Part has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_documentFilesService, _partsService, _partTypeService, currentUser.Company, currentUser.Id);

                return View(model);
            }

            model.Setup(_documentFilesService, _partsService, _partTypeService, currentUser.Company, currentUser.Id);

            return View(model);
        }
        public ActionResult PartDelete(string id,string ntlog)
        {
            var taskService = new PartsService();

            var response = taskService.Delete(id: id,ntlogin:ntlog);

            if (response)
            {
                TempData["SuccessMessage"] = "Part deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }

        public ActionResult EditSafetyStock(string id)
        {
            EditSafetyStockViewModel viewModel = new EditSafetyStockViewModel()
            {
                PartObjectId = id
            };

            var currentUser = GetCurrentUser();
            var locations = _locationService.GetLocationsForSafetyStock(currentUser.Root_Company);

            List<PartsSafetyStock> partSafetyStocks = _partsService.GetPartSafetyStocksByLocation(locations.Select(x => x.Id), id);
            List<RoleResult> roles = _roleService.GetActiveRoles();

            viewModel.Roles.Add(new SelectListItem { Value = "", Text = "--Select Role--" });

            viewModel.Roles.AddRange(roles.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Name
            }).OrderBy(o => o.Text));

            foreach (var location in locations)
            {
                var part = partSafetyStocks.SingleOrDefault(x => x.LocationId == location.Id) ?? new PartsSafetyStock();

                if (!string.IsNullOrEmpty(part.Id))
                {
                    part.RolesAssignedToWarn = _partsService.GetSafetyStockRoles(part.Id, "WARN");
                    part.RolesAssignedToFail = _partsService.GetSafetyStockRoles(part.Id, "FAIL");
                }

                part.LocationName = location.CompleteName;
                part.LocationId = location.Id;
                viewModel.PartsSafetyStocks.Add(part);
            }

            return View(viewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSafetyStock(string partObjectId, List<PartsSafetyStock> partsSafetyStocks)
        {
            var user = GetCurrentUser();

            foreach (var safetyStock in partsSafetyStocks)
            {
                _partsService.UpdatePartsSafetyStocks(safetyStock, partObjectId, user.Id);
            }

            TempData["SuccessMessage"] = "Update Safety Stock updated successfully.";

            return RedirectToAction("EditSafetyStock");
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {

                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    var response = _partsService.ImportParts(postedFile, GetCurrentUser().Id);

                    if (!response.HasErrors())
                    {
                        TempData["SuccessMessage"] = response.SuccessMessage;
                        return RedirectToAction("Index");
                    }

                    TempData["ErrorMessage"] = response.ErrorMessage;
                    return View();
                }

                ModelState.AddModelError("File", "Please Upload Your file");
                return View();
            }
            ModelState.AddModelError("File", "Please Upload Your file");
            return View();
        }
    }
}