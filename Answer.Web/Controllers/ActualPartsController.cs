using Msr.Models.ActualParts;
using Msr.Services.ActualParts;
using Msr.Services.ActualParts.ViewModels;
using Msr.Services.jqGrid;
using Msr.Services.Locations;
using Msr.Services.Parts;
using Msr.Services.Products;
using Msr.Services.Users;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class ActualPartsController : BaseController
    {
        // GET: ActualParts
        public ActionResult Index(string serial)
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Actual Parts";
            ViewBag.Serial = serial;
            return View(viewModel);
        }
        public ActionResult ActualPartsData(JqGridParam param)
        {
            var actualPartsService = new ActualPartsService();
            var totalRows = actualPartsService.GetActualPartsQueryable();
            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ActualPartsView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ActualPartsView.Serial))
                    {
                        totalRows = totalRows.Where(x => x.Serial.ToLower().Contains(rule.data.ToLower()));
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
                    }
                    else if (rule.field == nameof(ActualPartsView.LocationName))
                    {
                        totalRows = totalRows.Where(x => x.LocationName.ToLower().Contains(rule.data.ToLower()));
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

            approvalGroup.SetUp(new ActualPartsService(), new PartsService(), new LocationService(), new UserService(), new ProductService());

            return View(approvalGroup);
        }
        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(SaveActualPartsViewModel model)
        {
            var actualPartsService = new ActualPartsService();
            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";
                var response = actualPartsService.Create(model: model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Group has been created successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.SetUp(new ActualPartsService(), new PartsService(), new LocationService(), new UserService(), new ProductService());
                    return View(model);
                }
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            var actualPartsService = new ActualPartsService();

            var model = actualPartsService.GetActualPartById(id);

            var actualPart = new SaveActualPartsViewModel();

            actualPart = actualPart.MapToDto(model);

            actualPart.SetUp(new ActualPartsService(), new PartsService(), new LocationService(), new UserService(), new ProductService());

            return View(actualPart);
        }
        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(SaveActualPartsViewModel model)
        {

            var actualPartsService = new ActualPartsService();
            if (ModelState.IsValid)
            {
                //Need to dynamic//
                model.NTLogin = "1618";
                //this is from dropdown autoselect current user Company from sesion//
                model.CurOwner = "2";
                var response = actualPartsService.Edit(model: model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Approval Group has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";
                return View(model);
            }

            return View(model);
        }
        public ActionResult ActualPartDelete(string id)
        {
            var taskService = new PartsService();

            var response = taskService.Delete(id: id);

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
            var actualPartsService = new ActualPartsService();

            var totalRows = actualPartsService.GetActualPartViewHistoryQueryable().Where(x => x.ActualPartId == id & x.TaskStetTitle != "PENDING_PARENT_ACCEPTANCE").OrderBy(x => x.ColourCode).ThenBy(x => x.ActualStopDate).ThenBy(x => x.ActualStartDate).ThenBy(x => x.CurPlannedStopDate).ThenBy(x => x.CurPlannerStartDate).AsQueryable();

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

        public ActionResult ViewHistoryClose(string id)
        {
            var taskService = new ActualPartsService();

            var response = taskService.Close(id: id);

            if (response)
            {
                TempData["SuccessMessage"] = "Successfully Closed Task.";
                return RedirectToAction("ViewHistory");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("ViewHistory");
        }
    }
}