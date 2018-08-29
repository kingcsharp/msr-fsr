using Msr.Models.Parts;
using Msr.Services.jqGrid;
using Msr.Services.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Answer.Web.Filters;
using Answer.Web.ViewModel;
using Msr.Commons.Files;
using Msr.Infrastructure.Common.Constansts;
using Msr.Models.Menus;
using Msr.Services.Documents;
using Msr.Services.Parts.ViewModels;
using Msr.Services.PartTypes;
using Msr.Services.Locations;
using Msr.Services.Roles;
using Msr.Services.ProductionPlanning;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Parts)]
    public class PartsController : BaseController
    {
        private readonly LocationService _locationService;
        private readonly PartsService _partsService;
        private readonly RoleService _roleService;
        private readonly PartTypeService _partTypeService;
        private readonly DocumentFilesService _documentFilesService;
        private readonly ProductionPlanningService _productionPlanningService;

        public PartsController()
        {
            _locationService = new LocationService();
            _partsService = new PartsService();
            _roleService = new RoleService();
            _partTypeService = new PartTypeService();
            _documentFilesService = new DocumentFilesService();
            _productionPlanningService = new ProductionPlanningService();
        }

        public ActionResult Index()
        {
            ViewBag.ActiveClass = "Parts";

            return View();
        }

        public ActionResult PartsData(JqGridParam param)
        {
            var totalRows = _partsService.GetPartsQueryable();

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
                    else if (rule.field == nameof(PartsView.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root.ToLower().Contains(rule.data.ToLower()));
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
                    else if (rule.field == nameof(PartsView.PartTypeName))
                    {
                        totalRows = totalRows.Where(x => x.PartTypeName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.Consumable))
                    {
                        totalRows = totalRows.Where(x => x.Consumable.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.UnitShippingWeight))
                    {
                        totalRows = totalRows.Where(x => x.UnitShippingWeight.ToString().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.Rev.ToString().Contains(rule.data.ToLower()));
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

            totalRows = param.sortOrder == "desc" ? totalRows.OrderByDescending(orderBy) : totalRows.OrderBy(orderBy);

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
            var getCurrentUser = GetCurrentUser();

            var model = _partsService.GetByObjectId(id);

            var part = new AddPartViewModel();

            part = part.MapToDto(model);

            part.Setup(_documentFilesService, _partsService, _partTypeService, _productionPlanningService, getCurrentUser.Company, getCurrentUser.Id);

            return View(part);
        }

        public ActionResult ViewFile(string callBackitem)
        {
            var callBackUrl = "http://docs.google.com/gview?url=" + "http://infolab.stanford.edu/pub/papers/google.pdf&embedded=true";
            ViewBag.callBackitem = callBackUrl;

            return PartialView("_ViewFile");
        }

        public ActionResult AddPart()
        {
            var getCurrentUser = GetCurrentUser();

            var part = new AddPartViewModel();

            part.Setup(_documentFilesService, _partsService, _partTypeService, _productionPlanningService, getCurrentUser.Company, getCurrentUser.Id);

            return View(part);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPart(AddPartViewModel model)
        {
            var getCurrentUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                model.NTLogin = getCurrentUser.Id;
                model.SubParts = null;
                model.Company = getCurrentUser.Company;

                var response = _partsService.Create(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Part has been created successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = $"<strong>Something went wrong :</strong> {response.ErrorMessage}";

                model.Setup(_documentFilesService, _partsService, _partTypeService, _productionPlanningService, getCurrentUser.Company, getCurrentUser.Id);

                return View(model);
            }

            model.Setup(_documentFilesService, _partsService, _partTypeService, _productionPlanningService, getCurrentUser.Company, getCurrentUser.Id);

            return View(model);

        }

        public ActionResult Edit(string id)
        {
            var getCurrentUser = GetCurrentUser();

            var model = _partsService.GetByObjectId(id);

            var part = new AddPartViewModel();

            part = part.MapToDto(model);

            var subPartModel = _partsService.GetSubPartByObjId(id, getCurrentUser.Id);

            part.SubPartList = part.SubPartMapToDto(subPartModel);

            part.Setup(_documentFilesService, _partsService, _partTypeService, _productionPlanningService, getCurrentUser.Company, getCurrentUser.Id);

            part.InternalEqualParts = _partsService.GetInternalEqualPartByPartId(part.Id);

            var preview = string.Join(",", part.DocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'", x.SERVER_PATH)));
            ViewBag.Preview = preview;

            var jsonSerialiser = new JavaScriptSerializer();
            var previewConfig = jsonSerialiser.Serialize(part.DocLinks.Select(x => new
            {
                caption = x.NAME,
                type = MimeTypes.GetContentType(x.CONTENTTYPE),
                size = 6666,
                url = Url.Action("DeletesingleReference", "Documents", new { file = x.LINKED_DOC_ID }),
                downloadUrl = x.SERVER_PATH,
                key = x.LINKED_DOC_ID
            }));

            ViewBag.PreviewConfig = previewConfig;

            return View(part);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AddPartViewModel model)
        {
            var getCurrentUser = GetCurrentUser();

            foreach (var item in model.SubPartList)
            {
                if (item.PartId == null)
                {
                    ModelState.AddModelError(nameof(item.PartId), "Part is required");
                }
                if (item.Qty < 0)
                {
                    ModelState.AddModelError(nameof(item.Qty), "Qty is required");
                }
            }

            if (ModelState.IsValid)
            {
                model.NTLogin = getCurrentUser.Id;

                var response = _partsService.Update(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Part has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong";


                model.Setup(_documentFilesService, _partsService, _partTypeService, _productionPlanningService, getCurrentUser.Company, getCurrentUser.Id);

                return View(model);
            }

            model.Setup(_documentFilesService, _partsService, _partTypeService, _productionPlanningService, getCurrentUser.Company, getCurrentUser.Id);

            return View(model);
        }

        public ActionResult PartDelete(string id, string ntlog)
        {

            var response = _partsService.Delete(id: id, ntlogin: ntlog);

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
            var viewModel = new EditSafetyStockViewModel()
            {
                PartObjectId = id
            };

            var getCurrentUser = GetCurrentUser();
            var locations = _locationService.GetLocationsForSafetyStock(getCurrentUser.Root_Company);

            var partSafetyStocks = _partsService.GetPartSafetyStocksByLocation(locations.Select(x => x.Id), id);
            var roles = _roleService.GetActiveRoles();

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
            var getCurrentUser = GetCurrentUser();

            foreach (var safetyStock in partsSafetyStocks)
            {
                _partsService.UpdatePartsSafetyStocks(safetyStock, partObjectId, getCurrentUser.Id);
            }

            TempData["SuccessMessage"] = "Safety Stock updated successfully.";

            return RedirectToAction("EditSafetyStock");
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
                var getCurrentUser = GetCurrentUser();

                var response = _partsService.ImportParts(postedFile, getCurrentUser.Id);

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
            Response.Write(string.Join(",", ImportPartViewModel.GetHeaderColumns()));
            Response.End();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubPartDelete(string id)
        {
            var response = _partsService.SubPartDeleteById(id);

            if (response.HasErrors())
            {
                return Json(response.ErrorMessage, JsonRequestBehavior.AllowGet);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}