using Msr.Models.ActualParts;
using Msr.Services.ActualParts;
using Msr.Services.ActualParts.ViewModels;
using Msr.Services.jqGrid;
using Msr.Services.Locations;
using Msr.Services.Parts;
using Msr.Services.Products;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Infrastructure.Common.Constansts;
using Msr.Models.Menus;
using Msr.Services.Companies;
using Msr.Services.Orders;
using Msr.Services.Roles;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Parts)]
    public class ActualPartsController : BaseController
    {
        private readonly LocationService _locationService;
        private readonly ActualPartsService _actualPartsService;
        private readonly PartsService _partsService;
        private readonly PeopleService _peopleService;
        private readonly ProductService _productService;
        private readonly CompanyService _companyService;

        public ActualPartsController()
        {
            _locationService = new LocationService();
            _actualPartsService = new ActualPartsService();
            _partsService = new PartsService();
            _peopleService = new PeopleService();
            _productService = new ProductService();
            _companyService = new CompanyService();
        }
        public ActionResult Index(string serial)
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Actual Parts";
            ViewBag.Serial = serial;
            return View(viewModel);
        }

        public ActionResult ActualPartsData(JqGridParam param)
        {

            var totalRows = _actualPartsService.GetActualPartsQueryable();

            var defaultStatusList = GetDefaultStatus();

            totalRows = totalRows.Where(x => defaultStatusList.Contains(x.Status));

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ActualPartsView.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ActualPartsView.Serial))
                    {
                        if (!string.IsNullOrWhiteSpace(rule.data))
                        {
                            totalRows = totalRows.Where(x => x.Serial.ToLower().Contains(rule.data.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(ActualPartsView.PartDesc))
                    {
                        totalRows = totalRows.Where(x => x.PartDesc.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartsView.CompanyPartNumber))
                    {
                        totalRows = totalRows.Where(x => x.CompanyPartNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartsView.Qty))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Qty == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.Qty.ToString().Contains(rule.data.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(ActualPartsView.LocationName))
                    {
                        totalRows = totalRows.Where(x => x.LocationName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartsView.NickName))
                    {
                        totalRows = totalRows.Where(x => x.NickName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartsView.CurrentOwnerName))
                    {
                        totalRows = totalRows.Where(x => x.CurrentOwnerName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartsView.ApStatus))
                    {
                        totalRows = totalRows.Where(x => x.ApStatus.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartsView.Rev))
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
                    else if (rule.field == nameof(ActualPartsView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(ActualPartsView.CreatingCoName))
                    {
                        totalRows = totalRows.Where(x => x.CreatingCoName.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }
            var orderBy = nameof(ActualPartsView.PartDesc);
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
            var approvalGroup = new SaveActualPartsViewModel();

            approvalGroup.SetUp(_actualPartsService, _partsService, _locationService, _peopleService, _productService,
                _companyService, GetCurrentUser());

            return View(approvalGroup);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(SaveActualPartsViewModel model)
        {
            var actualPartsService = new ActualPartsService();

            if (ModelState.IsValid)
            {
                var user = GetCurrentUser();

                model.NTLogin = user.Id;

                var response = actualPartsService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Actual Part has been created successfully.";
                    return RedirectToAction("Index");
                }
            }
            TempData["ErrorMessage"] = "Something went wrong.";

            model.SetUp(_actualPartsService, _partsService, _locationService, _peopleService, _productService,
                _companyService, GetCurrentUser());
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var actualPartsService = new ActualPartsService();

            var model = actualPartsService.GetActualPartById(id);

            var actualPart = new SaveActualPartsViewModel();

            actualPart = actualPart.MapToDto(model);

            actualPart.SetUp(_actualPartsService, _partsService, _locationService, _peopleService, _productService,
                _companyService, GetCurrentUser());

            return View(actualPart);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(SaveActualPartsViewModel model)
        {
            var actualPartsService = new ActualPartsService();

            if (ModelState.IsValid)
            {
                var user = GetCurrentUser();

                model.NTLogin = user.Id;

                var response = actualPartsService.Edit(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Actual Part has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            return View(model);
        }

        public ActionResult ActualPartDelete(string id, string ntlogin)
        {
            var taskService = new PartsService();

            var response = taskService.Delete(id: id, ntlogin: ntlogin);

            if (response)
            {
                TempData["SuccessMessage"] = "Part deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }

        public ActionResult ViewHistory(string id)
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Actual Parts";
            ViewBag.ActualPartId = id;

            return View(viewModel);
        }

        public ActionResult ActualPartsViewHistoryData(JqGridParam param, string id)
        {
            var totalRows = _actualPartsService.GetActualPartViewHistoryQueryable().Where(x => x.ActualPartId == id & x.TaskStetTitle != "PENDING_PARENT_ACCEPTANCE").OrderBy(x => x.ColourCode).ThenBy(x => x.ActualStopDate).ThenBy(x => x.ActualStartDate).ThenBy(x => x.CurPlannedStopDate).ThenBy(x => x.CurPlannerStartDate).AsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ActualPartViewHistoryView.RequesteeId))
                    {
                        totalRows = totalRows.Where(x => x.RequesteeId == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ActualPartViewHistoryView.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartViewHistoryView.OriginalRequestor))
                    {
                        totalRows = totalRows.Where(x => x.OriginalRequestor.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartViewHistoryView.LatestRequestee))
                    {
                        totalRows = totalRows.Where(x => x.LatestRequestee.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartViewHistoryView.CompanyName))
                    {
                        totalRows = totalRows.Where(x => x.CompanyName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ActualPartViewHistoryView.TaskType))
                    {
                        totalRows = totalRows.Where(x => x.TaskType.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ActualPartViewHistoryView.Description);

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

        public ActionResult ViewHistoryClose(string id, string ntlog)
        {

            var response = _actualPartsService.Close(id: id, ntlogin: ntlog);

            if (response)
            {
                TempData["SuccessMessage"] = "Successfully Closed Task.";
                return RedirectToAction("ViewHistory");
            }

            TempData["ErrorMessage"] = "Something went wrong.";

            return RedirectToAction("ViewHistory");
        }

        public ActionResult ReassignTask(string id)
        {
            var actualPart = new ReassignTaskViewModel();

            actualPart.SetUp(new PeopleService(), new RoleService());

            actualPart.Id = id;

            return View(actualPart);

        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult ReassignTask(ReassignTaskViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = _actualPartsService.ReAssignTask(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Task has been reassigned successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            return View(model);
        }

        public ActionResult DeleteHistory(string id)
        {

            string actualPartId = TempData["actualPartId"].ToString();


            if (ModelState.IsValid)
            {
                var response = _actualPartsService.DeleteViewHistory(id, GetCurrentUser().Id);

                if (response)
                {
                    TempData["SuccessMessage"] = "History has been deleted successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return RedirectToAction(actionName: "ViewHistory", routeValues: new { id = actualPartId });
            }

            return RedirectToAction(actionName: "ViewHistory", routeValues: new { id = actualPartId });
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

                var response = _actualPartsService.ImportActualParts(postedFile, currentUser);

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
            Response.AddHeader("Content-Disposition", "attachment;filename=actual-parts-upload.csv");
            Response.Write(string.Join(",", ActualPartImportViewModel.GetHeaderColumns()));
            Response.End();
        }
    }
}