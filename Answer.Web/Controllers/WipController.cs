using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.ViewModel.Wip;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Notes;
using Msr.Services.Orders;
using Msr.Services.Orders.Messaging;
using Msr.Services.Orders.Procedures;
using Msr.Services.Orders.ViewModels;
using Msr.Web.Controllers;
using Msr.Web.ViewModel.Engineering;

namespace Answer.Web.Controllers
{
    [Authorize]
    public class WipController : BaseController
    {
        private OrderService _orderService;

        public WipController()
        {
            _orderService = new OrderService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "WIP";

            return View(viewModel);
        }

        public ActionResult EngineeringData(JqGridParam param)
        {
            var orderService = new OrderService();

            var totalRows = orderService.GetWorkOrderQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(WorkOrderView.PurchaseItemId))
                    {
                        totalRows = totalRows.Where(x => x.PurchaseItemId == rule.data);
                    }
                    else if (rule.field == nameof(WorkOrderView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.Serial))
                    {
                        totalRows = totalRows.Where(x => x.Serial.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.CustPurchNum))
                    {
                        totalRows = totalRows.Where(x => x.CustPurchNum.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.ProductName))
                    {
                        totalRows = totalRows.Where(x => x.ProductName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.ProductName))
                    {
                        totalRows = totalRows.Where(x => x.ProductName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.Qty))
                    {
                        double value;
                        if (double.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Qty == value);
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.StDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.StDate.HasValue && q.StDate.Value.Day == value.Day &&
                                        q.StDate.Value.Month == value.Month && q.StDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.DueDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.DueDate.HasValue && q.DueDate.Value.Day == value.Day &&
                                        q.DueDate.Value.Month == value.Month && q.DueDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.ProcName))
                    {
                        totalRows = totalRows.Where(x => x.ProcName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.CurStepText))
                    {
                        if (rule.data != "ALL")
                        {
                            var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                            if (statusList.Any())
                            {
                                totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));

                            }
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.Notes))
                    {
                        totalRows = totalRows.Where(x => x.Notes.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            string orderDirection = "asc";

            if (param.sortOrder == "desc")
            {
                totalRows = totalRows.OrderByDescending(param.sortColumn);
            }
            else
            {
                totalRows = totalRows.OrderBy(param.sortColumn);
            }

            var totalRecords = totalRows.Count();
            totalRows = totalRows.Skip(param.pageSize * (param.pageIndex - 1));
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);

            var taskService = new TaskService();

            var results = totalRows.ToList();

            foreach (var r in results)
            {
                r.HasMonitor = taskService.CheckHasMonitors(r.FillId);
                r.HasNcr = taskService.CheckHasNcr(r.ActualPartId);
            }

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            Session["WIP_Nav"] = string.Join(",", results.Select(x => x.FillId).ToList());

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNcrModel(string id)
        {
            var orderService = new OrderService();
            var taskService = new TaskService();
            var ncrDetails = orderService.GetNcrDetails(id);

            var docs = orderService.GetDocuments(ncrDetails.Details.FillObjId);

            var photos = GetDocViewModel(docs, orderService, 400);
            ncrDetails.Photos = photos;

            var response = taskService.GetTaskWithMonitors(id);

            ncrDetails.MonitorItem = response.MonitorItem.Where(x => x.Description.Contains("Nonconformity") || x.Description.Contains("NCR")).ToList();

            return PartialView("_NcrModel", ncrDetails);
        }

        public ActionResult GetPhotsModel(string id)
        {
            var orderService = new OrderService();

            var docs = orderService.GetDocuments(id);

            var photos = GetDocViewModel(docs, orderService, 400);

            return PartialView("_Photos", photos);
        }

        public ActionResult GetMonitorsModel(string id)
        {
            var taskService = new TaskService();

            var response = taskService.GetTaskWithMonitors(id);

            return PartialView("_Monitors", response);
        }

        [HttpPost]
        public JsonResult AddInstruction(string id, string message)
        {
            var noteService = new NoteService();

            var loggedUserId = User.Identity.GetUserId();

            noteService.AddNote(id, message, 1, loggedUserId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult WipListModel()
        {
            var currentUser = GetCurrentUser();

            var viewModel = new WipListViewModel();
            var orderService = new OrderService();

            viewModel.WoItemsInprogress  = orderService.GetWorkOrderQueryable()
                    .Where(x => x.Status == WorkItemStatusConstants.Accepted && x.SupplierId == currentUser.Root_Company) 
                    .OrderByDescending(o => o.DueDate)
                    .ToList();

            var procs = viewModel.WoItemsInprogress.Select(p => p.ProcName).ToList();

            viewModel.WoItemsByProcedures = orderService.GetWorkOrderQueryable().Where(x=> procs.Contains(x.ProcName)).ToList();

            
            return PartialView("_WipListModal", viewModel);
        }

        public ActionResult Details(int? id,string ntlogin)
        {
            var currentUser = GetCurrentUser();
            var orderService = new OrderService();

            var workItems =  orderService.GetWorkOrderQueryable()
                               .Where(x => x.RequesteeId == currentUser.Id)
                               .OrderByDescending(o => o.DueDate)
                               .ToList();

            if (!id.HasValue)
            {
                id = int.Parse(workItems.First().FillId);
            }

            var response = _orderService.GetPurchaseItemDetails(id.Value,ntlogin);
            response.WoItems = workItems;

            return View(response);
        }

        public ActionResult PrintTraveler(int id,string ntlogin)
        {
            var response = _orderService.GetTsrDetails(id,ntlogin);

            return PartialView("_ViewTsr", response);
        }

        public ActionResult PrintOther(int id)
        {
            var vm = new PrintOtherViewModel { FillId = id };
            var purchaseItemId = _orderService.GetPurchaseItemIdByFillId(id);
            vm.Setup(purchaseItemId);

            return PartialView("_PrintOther", vm);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult EditOrderItemInline(SaveWorkOrderViewModel model)
        {
            var orderService = new OrderService();
            if (ModelState.IsValid)
            {
                var loggedUserId = User.Identity.GetUserId();
                model.NTLogin = loggedUserId;

                var response = false;

                if (model.ColumnName == "CustPurchNum")
                {
                    response = orderService.SaveOrderItemPunchNum(model: model);
                }
                else if (model.ColumnName == "FillQty")
                {
                    response = orderService.SaveOrderItemQty(model: model);
                }
                else if (model.ColumnName == "DueDate")
                {
                    response = orderService.SaveOrderItemDueDate(model: model);
                }

                if (response)
                {
                    TempData["SuccessMessage"] = "Part has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";


                    return Json(new { success = false, responseText = "Something went wrong." }, JsonRequestBehavior.AllowGet);
                }

            }


            return Json(new { success = false, responseText = "Something went wrong." }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetWipStepDetails(int stepId, int fillId, int? phStepId)
        {
            ViewBag.FillId = fillId;

            var loggedUserId = User.Identity.GetUserId();
            var response = _orderService.GetWipStepDetails(stepId, fillId, loggedUserId, phStepId);

            foreach (var monitorTemplate in response.MonitorTemplateResult)
            {
                monitorTemplate.Setup();
            }

            response.Images = new ImageViewModel
            {
                FillId = fillId,
                TaskId = stepId
            };

            return PartialView("_InitialInspection", response);
        }


        [HttpPost]
        public ActionResult UpdateStepMonitor(MonitorTemplateResult monitorTemplate)
        {
            if (ModelState.IsValid)
            {
                _orderService.UpdateStepMonitor(monitorTemplate);
            }

            return RedirectToAction("Details", new { id = monitorTemplate.FillId });
        }

        [HttpPost]
        public ActionResult UpdateStepMonitorAndClose(MonitorTemplateResult monitorTemplate)
        {
            if (ModelState.IsValid)
            {
                var loggedUserId = User.Identity.GetUserId();
                _orderService.UpdateStepMonitor(monitorTemplate);
                var returnValue = _orderService.CloseTask(monitorTemplate.TaskId, loggedUserId);
                if(returnValue  == "NEW_TEXT")
                    return RedirectToAction("Details", new { id = monitorTemplate.FillId });//"../monitors/addNewTextResults.asp?TASK_ID="
            }

            return RedirectToAction("Details", new { id = monitorTemplate.FillId });//"asp/workerScreen/refreshProgress.asp"
        }

        [HttpPost]
        public ActionResult StepDoneClick(int stepId)
        {
            var loggedUserId = User.Identity.GetUserId();
            _orderService.StepDone(stepId, loggedUserId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StepStartClick(int stepId)
        {
            var loggedUserId = User.Identity.GetUserId();
            _orderService.StepStart(stepId, loggedUserId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AssumeTaskClick(int taskId)
        {
            var loggedUserId = User.Identity.GetUserId();
            _orderService.AssumeTask(taskId, loggedUserId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TaskAssumeClick(int taskId)
        {

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CancelUnfinishedSteps(int fillId)
        {
            var loggedUserId = User.Identity.GetUserId();
            _orderService.CancelUnfinishedSteps(fillId, loggedUserId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetReferenceTheory(int theoryId)
        {
            var loggedUserId = User.Identity.GetUserId();
            var response = _orderService.GetTheoryData(theoryId, loggedUserId);

            return PartialView("_ViewReferenceTheory", response);
        }

        public ActionResult GetReferenceTheoryFile(int fileId)
        {
            var loggedUserId = User.Identity.GetUserId();
            //var response = _orderService.GetTheoryData(fileId, loggedUserId);

            //return PartialView("_ViewReferenceTheory", response);
            return File(Stream.Null, "xml");
        }

        [HttpPost]
        public ActionResult TakeTaskOwnersShip(int taskId)
        {
            var loggedUserId = User.Identity.GetUserId();
            var statusMessage = _orderService.AssumeTask(taskId, loggedUserId);
            // need to check response and then redirect from JQuery
            var response = statusMessage.Contains("ERROR")
                ? new {Code = "Error", Message = statusMessage}
                : new {Code = "OK", Message = statusMessage};
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateRootPart(int partId, string serialNumber, int taskId)
        {
            var loggedUserId = GetCurrentUser().Id;
            _orderService.UpdateRootPart(partId, serialNumber, taskId, loggedUserId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateRootParts(List<ActualPart> parts, int taskId)
        {
            var loggedUserId = GetCurrentUser().Id;//to be removed
            foreach (ActualPart part in parts.Where(x => x.TreeLevel > 0))
            {
                _orderService.UpdateRootPart(part.Id, part.Serial, taskId, loggedUserId);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchNcrs(int? partId, int? ncrId, string partSerialNumber, string taskStatus, string assignee)
        {
            var viewModel = _orderService.SearchNcrs(partId, GetCurrentUser().Id);
            return View(viewModel);
        }

        private List<DocumentView> GetDocViewModel(List<DocumentView> docs, OrderService orderService, int width)
        {
            var photos = new List<DocumentView>();

            foreach (var doc in docs)
            {
                if (doc.ContentType == "image/jpeg" || doc.ContentType == "image/gif" || doc.ContentType == "image/png")
                {
                    var photo = orderService.GetDocumentBase64(doc.ServerPath, width);

                    photos.Add(new DocumentView
                    {
                        FileArray = photo,
                        Name = doc.Name
                    });
                }
            }

            return photos;
        }
    }
}