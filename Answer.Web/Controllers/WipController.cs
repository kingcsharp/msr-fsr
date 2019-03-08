using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Answer.Web.ViewModel.Wip;
using Msr.Infrastructure.Common.Constansts;
using Msr.Models.Orders;
using Msr.Services;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.EquipmentMaintenances;
using Msr.Services.EquipmentMaintenances.ViewModels;
using Msr.Services.Helpers;
using Msr.Services.jqGrid;
using Msr.Services.Locations;
using Msr.Services.Notes;
using Msr.Services.Objects;
using Msr.Services.Orders;
using Msr.Services.Orders.Messaging;
using Msr.Services.Orders.Procedures;
using Msr.Services.Orders.ViewModels;
using Msr.Services.Procedures;
using Msr.Services.Procedures.Messages;
using Msr.Services.Roles;
using Msr.Web.ViewModel.Engineering;

namespace Answer.Web.Controllers
{
    [AuthorizeUser]
    public class WipController : BaseController
    {
        private OrderService _orderService;
        private TaskService _taskService;
        private ProceduresService _proceduresService;
        private RoleService _roleService;
        private ObjectsService _objectsService;
        private EquipmentMaintenanceService _equipmentMaintenanceService;
        private readonly DocumentFilesService _documentFilesService;
        private LocationService _locationService;

        public WipController()
        {
            _locationService = new LocationService();
            _equipmentMaintenanceService = new EquipmentMaintenanceService();
            _orderService = new OrderService();
            _taskService = new TaskService();
            _proceduresService = new ProceduresService();
            _roleService = new RoleService();
            _documentFilesService = new DocumentFilesService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();
            var currentUser = GetCurrentUser();

            ViewBag.ActiveClass = "WIP";
            ViewBag.Locations = _locationService.GetParentLocations().Select(x => x.Name).OrderBy(o => o).ToList();

            return View(viewModel);
        }

        private static List<string> GetMultiStatusList(JqGridParam param)
        {
            var statusList = new List<string>();

            statusList.AddRange(from itemRule in param.@where.rules.Where(x => x.field == "Status")
                                where !string.IsNullOrWhiteSpace(itemRule.data)
                                select itemRule.data.ToLower());

            return statusList;
        }

        public ActionResult EngineeringData(JqGridParam param, string step)
        {
            var totalRows = _orderService.GetWorkOrderQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(WorkOrderView.WoItem))
                    {
                        totalRows = totalRows.Where(x => x.WoItem.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.LocationName) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.LocationName.ToLower()));
                        }
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
                    else if (rule.field == nameof(WorkOrderView.CustomerName))
                    {
                        totalRows = totalRows.Where(x => x.CustomerName.ToLower().Contains(rule.data.ToLower()));
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
                    else if (rule.field == nameof(WorkOrderView.Notes))
                    {
                        totalRows = totalRows.Where(x => x.Notes.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.CurStepText))
                    {
                        if (rule.data != "All")
                        {
                            var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                            if (statusList.Any())
                            {
                                totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                            }
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(step))
                {
                    _objectsService = new ObjectsService();
                    var fillIds = _objectsService.GetObjectSearchQueryable()
                        .Where(x => (x.Description != null && x.Description.ToLower().Contains(step)) || (x.Name != null && x.Name.ToLower().Contains(step)))
                        .Select(x => x.ObjectId)
                        .ToList();

                    totalRows = totalRows.Where(x => fillIds.Contains(x.FillItemId));
                }
                else
                {
                    totalRows = totalRows.Where(x => x.Status.ToLower() == WorkItemStatusConstants.Requested
                                                     || x.Status.ToLower() == WorkItemStatusConstants.Accepted);
                }
            }

            var orderDirection = "asc";

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

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNcrModel(string id)
        {
            var ncrDetails = _orderService.GetNcrDetails(id);

            var docs = _orderService.GetDocuments(ncrDetails.Details.FillObjId);

            var photos = GetDocViewModel(docs, _orderService, 400);
            ncrDetails.Photos = photos;

            var response = _taskService.GetTaskWithMonitors(id);

            ncrDetails.MonitorItem = response.MonitorItem.Where(x => x.Description.Contains("Nonconformity") || x.Description.Contains("NCR")).ToList();

            return PartialView("_NcrModel", ncrDetails);
        }

        public ActionResult AddNcrModel(string parentId, string fillId)
        {
            var vm = new AddNcrViewModel();
            vm.ParentId = parentId;
            vm.FillId = fillId;

            return PartialView("_AddNcrModel", vm);
        }

        public ActionResult AddNcrModelData(JqGridParam param)
        {
            var loggedUser = GetCurrentUser();

            var totalRows = _proceduresService.GetProcedureSelect(loggedUser.Id, "", loggedUser.Company, "");

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureSelectResult.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProcedureSelectResult.Creating_Co_Name))
                    {
                        totalRows = totalRows.Where(x => x.Creating_Co_Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureSelectResult.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureSelectResult.Verb_Name))
                    {
                        totalRows = totalRows.Where(x => x.Verb_Name.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }
            else
            {
                totalRows = totalRows.Where(x => x.Verb_Name.ToLower() == "ncr");
            }

            var orderBy = nameof(ProcedureSelectResult.Name);

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

        public ActionResult GetPhotsModel(string id)
        {
            var docs = _orderService.GetDocuments(id);

            var photos = GetDocViewModel(docs, _orderService, 400);

            return PartialView("_Photos", photos);
        }

        public ActionResult GetMonitorsModel(string id)
        {
            var response = _taskService.GetTaskWithMonitors(id);

            return PartialView("_Monitors", response);
        }

        [HttpPost]
        public JsonResult AddInstruction(string id, string message)
        {
            var noteService = new NoteService();

            var loggedUserId = GetCurrentUser().Id;

            noteService.AddNote(id, message, 1, loggedUserId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var vm = new WorkOrderDetailsResponse();

            var currentUser = GetCurrentUser();

            var workItems = _orderService.GetWorkOrderQueryable()
                .Where(x => x.RequesteeId == currentUser.Id)
                .OrderByDescending(o => o.DueDate)
                .ToList();

            vm = _orderService.GetPurchaseItemDetails(id, currentUser.Id);

            var objId = _orderService.GetProcedureByObjId(vm.FileSearchResult.ProcObjId);

            var procedureRoles = _proceduresService.GetSelectedRoles(objId.Id, currentUser.Id)?.Select(x => x.Role_Id).ToList();

            var roles = _roleService.GetAssignedRoles(currentUser.Id);

            var hasRoles = roles.Select(x => x.Role_Id).ToList().Intersect(procedureRoles).Any();

            vm.HasProcedureRoles = hasRoles;
            vm.MyWoItems = workItems;

            var stringFillId = id.ToString();

            var actualPartId = _orderService.GetWorkOrderQueryable().Where(x => x.FillId == stringFillId).Select(a => a.ActualPartId).FirstOrDefault();

            vm.ActualPartId = actualPartId;

            return View(vm);
        }

        public ActionResult StatusView(string locationName)
        {
            var currentUser = GetCurrentUser();

            var viewModel = new WipStatusViewModel
            {
                Location = locationName,
                LocationList = _locationService.GetParentLocations().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                }).OrderBy(o => o.Text).ToList()
            };

            viewModel.LocationList.Insert(0, new SelectListItem { Text = "--Please Select--", Value = "" });

            var workOrdersQueryable = _orderService.GetWorkOrderQueryable();

            if (!string.IsNullOrWhiteSpace(locationName))
            {
                workOrdersQueryable = workOrdersQueryable.Where(x => x.LocationName.ToLower() == locationName.ToLower());
            }

            viewModel.CurrentUser = currentUser;
            viewModel.WipStatusViewItems = workOrdersQueryable
                .Where(x => x.Status == WorkItemStatusConstants.Accepted ||
                            x.Status == WorkItemStatusConstants.WaitingToStart ||
                            x.Status == WorkItemStatusConstants.Requested)
            .Select(x => new WipStatusViewItem
            {
                ProductName = x.ProductName,
                ProcedureName = x.ProcName,
                CompanyPartNumber = x.CompanyPartNumber,
                DueDate = x.DueDate,
                LocationName = x.LocationName
            }).Distinct().ToList();

            var procs = viewModel.WipStatusViewItems.Select(p => p.ProcedureName).ToList();

            viewModel.WoItemsByProcedures = _orderService
                .GetWorkOrderQueryable()
                .Where(x => procs.Contains(x.ProcName))
                .Where(x => x.Status == WorkItemStatusConstants.Accepted ||
                            x.Status == WorkItemStatusConstants.WaitingToStart ||
                            x.Status == WorkItemStatusConstants.Requested).ToList();


            if (Request.IsAjaxRequest())
            {
                return PartialView("_statusItems", viewModel);
            }


            return View(viewModel);
        }

        public ActionResult WipListModel()
        {
            var currentUser = GetCurrentUser();

            var viewModel = new WipListViewModel();
            viewModel.CurrentUser = currentUser;

            viewModel.WoItemsInprogress = _orderService.GetWorkOrderQueryable()
               .Where(x => (x.Status == WorkItemStatusConstants.Accepted || x.Status == WorkItemStatusConstants.WaitingToStart || x.Status == WorkItemStatusConstants.Requested))
               .OrderByDescending(x => x.DueDate)
               .Select(x => new ProcedureInProgressViewModel
               {
                   ProductName = x.ProductName,
                   ProcedureName = x.ProcName,
                   DueDate = x.DueDate
               }).ToList();

            var procs = viewModel.WoItemsInprogress.Select(p => p.ProcedureName).ToList();

            viewModel.WoItemsByProcedures = _orderService
                .GetWorkOrderQueryable()
                .Distinct()
                .Where(x => procs.Contains(x.ProcName))
                .Where(x => x.Status == WorkItemStatusConstants.Accepted ||
                            x.Status == WorkItemStatusConstants.WaitingToStart ||
                            x.Status == WorkItemStatusConstants.Requested).ToList();

            return PartialView("_WipListModal", viewModel);
        }

        public ActionResult PrintTraveler(int id)
        {
            var currentUser = GetCurrentUser();

            var model = _orderService.GetTsrDetails(id, currentUser.Id);

            model.WorkOrderDetailsResponse = _orderService.GetPurchaseItemDetails(id, currentUser.Id);

            model.FillUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Url.Action("Details", "Wip", new { id = model.FillId });

            return PartialView("_ViewTsr", model);
        }

        public ActionResult PrintOther(int id)
        {
            var vm = new PrintOtherViewModel { FillId = id };
            var purchaseItemId = _orderService.GetPurchaseItemIdByFillId(id);
            vm.Setup(purchaseItemId);

            return PartialView("_PrintOther", vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditOrderItemInline(SaveWorkOrderViewModel model)
        {
            var orderService = new OrderService();
            if (ModelState.IsValid)
            {
                var loggedUserId = GetCurrentUser().Id;

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
                    return Json(new { success = true, responseText = "Part has been created successfully." }, JsonRequestBehavior.AllowGet);
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

            var loggedUserId = GetCurrentUser();

            var response = _orderService.GetWipStepDetails(stepId, fillId, loggedUserId.Id, phStepId);

            ViewBag.IsStepStatusClosed = response.TaskEditDataResult.Status == "CLOSED";

            var myRoles = _roleService.GetAssignedRoles(loggedUserId.Id);

            var vm = _orderService.GetPurchaseItemDetails(fillId, loggedUserId.Id);

            response.ParentPartId = vm.FileSearchResult.FillObjectId.ToString();

            var currentStep = vm.TaskStepResults.SingleOrDefault(x => x.StepId == stepId.ToString());

            var objId = _orderService.GetProcedureByObjId(vm.FileSearchResult.ProcObjId);

            var procedureRoles = _proceduresService.GetSelectedRoles(objId.Id, loggedUserId.Id)?.Select(x => x.Role_Id).ToList();
            response.HasStepRoles = myRoles.Select(x => x.Role_Id).ToList().Intersect(procedureRoles).Any();

            if (!response.HasStepRoles)
            {

                var steptRoles = currentStep.Roles?.Split(',');

                foreach (var item in myRoles)
                {
                    if (steptRoles.Any(x => x == item.Role_Id))
                    {

                        if (item.StartDate.HasValue && item.EndDate.HasValue)
                        {
                            if (DateTime.Today.Date >= item.StartDate.Value.Date && DateTime.Today.Date < item.EndDate.Value.Date.AddDays(1))
                            {
                                response.HasStepRoles = true;
                                break;
                            }
                        }
                        else
                        {
                            response.HasStepRoles = true;
                            break;
                        }
                    }
                }
            }

            response.LoggedUserIdResult = loggedUserId;
            response.TaskEditDataResult.StepTitle = currentStep.Title;
            response.TaskEditDataResult.Description = currentStep.Description;

            foreach (var monitorTemplate in response.MonitorTemplateResult)
            {
                monitorTemplate.Setup(_equipmentMaintenanceService, _orderService);

                for (int i = 0; i < monitorTemplate.FailActionList.Count; i++)
                {
                    if (monitorTemplate.FailActionList[i].Value == monitorTemplate.Fail_Action)
                    {
                        monitorTemplate.FailActionList[i].Selected = true;
                    }
                }
            }

            response.Images = new ImageViewModel
            {
                FillId = fillId,
                TaskId = stepId,
                ActualPartId = response.ParentPartId
            };

            var images = _orderService.GetOrderItemImagesById(stepId.ToString());

            var dockLinks = images.Select(itemImage => new DocLink
            {
                SERVER_PATH = itemImage.Path,
                CONTENTTYPE = itemImage.ContentType,
                LINKED_DOC_ID = itemImage.Id,
                NAME = itemImage.FILE_NAME
            })
                .ToList();

            response.Images.PreviewConfig = FileInputConfigHelper.GetPreviewConfigValue(dockLinks, Url.Action("DeleteImageById", "Doc"), Url.Action("Download", "Doc"));

            response.Images.Preview = FileInputConfigHelper.GetPreviewValue(dockLinks, _documentFilesService);

            return PartialView("_InitialInspection", response);
        }

        [HttpPost]
        public ActionResult UpdateStepMonitor(List<MonitorTemplateResult> monitorTemplates)
        {
            var currentUser = GetCurrentUser();

            var result = new ResultNotification<string>();

            foreach (var monitorTemplate in monitorTemplates)
            {
                monitorTemplate.StrNtLogin = currentUser.Id;
                result = _orderService.UpdateStepMonitor(monitorTemplate);

                if (result.HasErrors())
                {
                    break;
                }
            }

            return Json(new { result.ErrorMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateStepMonitorAndClose(List<MonitorTemplateResult> monitorTemplates)
        {
            if (ModelState.IsValid)
            {
                var loggedUserId = GetCurrentUser().Id;

                foreach (var monitorTemplate in monitorTemplates)
                {
                    monitorTemplate.StrNtLogin = loggedUserId;
                    _orderService.UpdateStepMonitor(monitorTemplate);
                }

                var returnValue = _orderService.CloseTask(monitorTemplates.FirstOrDefault().TASK_ID, loggedUserId, monitorTemplates.FirstOrDefault().FillId);

                if (!string.IsNullOrWhiteSpace(returnValue))
                {
                    return Json(returnValue, JsonRequestBehavior.AllowGet);
                }

                if (returnValue == "NEW_TEXT")
                    return Json("OK", JsonRequestBehavior.AllowGet);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StepDoneClick(int stepId, int fillId, int parentPartId, bool isSerilizeStep)
        {
            var loggedUserId = GetCurrentUser().Id;

            var result = _orderService.StepDone(stepId, loggedUserId, fillId, parentPartId, isSerilizeStep);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StepStartClick(int stepId, int fillId, string parentId)
        {
            var loggedUserId = GetCurrentUser().Id;

            var result = _orderService.StepStart(stepId, loggedUserId, fillId, parentId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StepPause(int taskLogId, int fillId)
        {
            var result = _orderService.StepPause(taskLogId, fillId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StepResume(int? taskLogId, int stepId, int fillId)
        {
            var loggedUser = GetCurrentUser();

            var result = _orderService.StepResume(taskLogId, stepId, fillId, loggedUser.Id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AssumeTaskClick(int fillId)
        {
            var loggedUserId = GetCurrentUser().Id;

            var result = _orderService.TakeOverPO(fillId, loggedUserId);

            if (result.HasErrors())
            {
                return Json(result.ErrorMessage, JsonRequestBehavior.AllowGet);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CancelUnfinishedSteps(int fillId)
        {
            var loggedUserId = GetCurrentUser().Id;

            _orderService.CancelUnfinishedSteps(fillId, loggedUserId);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetReferenceTheory(int theoryId)
        {
            var loggedUserId = GetCurrentUser().Id;

            var response = _orderService.GetTheoryData(theoryId, loggedUserId);

            return PartialView("_ViewReferenceTheory", response);
        }

        public ActionResult GetReferenceTheoryFile(int fileId)
        {
            //var response = _orderService.GetTheoryData(fileId, loggedUserId);

            //return PartialView("_ViewReferenceTheory", response);
            return File(Stream.Null, "xml");
        }

        [HttpPost]
        public ActionResult TakeTaskOwnersShip(int fillId)
        {
            var loggedUserId = GetCurrentUser();

            var statusMessage = _orderService.TakeOverPO(fillId, loggedUserId.Id);

            if (statusMessage.HasErrors())
            {
                return Json(new { Code = "Error", Message = statusMessage.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Code = "OK", Message = statusMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateRootPart(int partId, string serialNumber, int taskId)
        {
            var loggedUserId = GetCurrentUser().Id;
            var responce = _orderService.UpdateRootPart(partId, serialNumber, taskId, loggedUserId);

            if (responce.HasErrors())
            {
                return Json(responce.ErrorMessage, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Code = "OK", Message = responce }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateRootParts(List<ActualPart> parts, int taskId, string type, int fillId)
        {
            var loggedUserId = GetCurrentUser();//to be removed

            foreach (ActualPart part in parts.Where(x => x.TreeLevel > 0))
            {
                if (part.Serial != null)
                {
                    var result = _orderService.UpdateRootPart(part.Id, part.Serial, taskId, loggedUserId.Id);

                    if (result.HasErrors())
                    {
                        return Json(result.ErrorMessage, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            if (type == "completeTask")
            {
                var responce = _orderService.CloseTask(taskId.ToString(), loggedUserId.Id, fillId);

                if (!string.IsNullOrWhiteSpace(responce))
                {
                    return Json(responce, JsonRequestBehavior.AllowGet);
                }

                return Json("OK", JsonRequestBehavior.AllowGet);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchNcrs(int? partId, int? ncrId, string partSerialNumber, string taskStatus, string assignee)
        {
            var viewModel = _orderService.SearchNcrs(partId, GetCurrentUser().Id);
            return View(viewModel);
        }

        public ActionResult AddProcedureToTask(string objId, string parentId, string fillId)
        {
            _orderService.AddProcedureAsSubTask(objId, parentId, GetCurrentUser().Id);

            return RedirectToAction("Details", new { id = fillId });
        }

        public ActionResult AddEquipmentMaintenance(string id)
        {
            var loggedUser = GetCurrentUser();

            ViewBag.FillId = id;

            var model = new CreateEquipmentMaintenanceViewModel { NTLogin = loggedUser.Id };

            model.Setup(new EquipmentMaintenanceService(), _roleService);

            return PartialView("_AddEquipment", model);
        }

        [HttpPost]
        public ActionResult AddEquipmentMaintenance(FormCollection form, int id)
        {
            var model = new CreateEquipmentMaintenanceViewModel();
            var loggedUser = GetCurrentUser();
            model.NTLogin = loggedUser.Id;

            TryUpdateModel(model, form);

            var myRoles = _roleService.GetAssignedRoles(loggedUser.Id);

            var hasProductionManagerRole = myRoles.Any(x => x.Role_Name.Contains(RoleConstants.ProductionManager));

            if (hasProductionManagerRole)
            {
                if (!model.PemLastCompletedDate.HasValue)
                {
                    ModelState.AddModelError(nameof(CreateEquipmentMaintenanceViewModel.PemLastCompletedDate), "PemLastCompletedDate is required");
                }
                if (!model.FrequencyField.HasValue)
                {
                    ModelState.AddModelError(nameof(CreateEquipmentMaintenanceViewModel.FrequencyField), "FrequencyField is required");
                }
            }

            if (ModelState.IsValid)
            {
                model.Id = 0;

                var response = _equipmentMaintenanceService.Create(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Equipment maintenance has been created successfully.";

                    return RedirectToAction("Details", "Wip", new { id = id });
                }

                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            model.NTLogin = loggedUser.Id;
            model.Setup(_equipmentMaintenanceService, _roleService);

            return RedirectToAction("Details", "Wip", new { id = id });
        }
        [HttpPost]
        public JsonResult CheckEquipmentStatusById(string id)
        {
            var equipmentMaintenance = _equipmentMaintenanceService.GetEquipmentsQueryable().Where(x => x.RoomEquipmentId == id).OrderBy(x => x.DateTime).FirstOrDefault();

            var canUsed = false;
            var errorMessage = string.Empty;

            if (equipmentMaintenance == null)
            {
                errorMessage = $"Invalid equipment Id '{id}'";
            }
            else
            {
                if (equipmentMaintenance.PemLastCompletedDate.HasValue && equipmentMaintenance.MaintenanceTask == EquipmentMaintenanceTypeConstants.RoutineMaintenance && equipmentMaintenance.TroubleState == false)
                {
                    if (equipmentMaintenance.Status == EquipmentMaintenanceConstants.Assigned || equipmentMaintenance.Status == EquipmentMaintenanceConstants.Requested && equipmentMaintenance.TroubleState == false)
                    {

                        if (DateTime.Now >= equipmentMaintenance.PemLastCompletedDate.Value.AddDays(equipmentMaintenance.FrequencyField.Value))
                        {
                            errorMessage = "This equipment can not be used due to overdue for preventative maintenance.";
                        }
                    }
                }

                if (equipmentMaintenance.MaintenanceTask == EquipmentMaintenanceTypeConstants.Repair && equipmentMaintenance.TroubleState)
                {
                    if (equipmentMaintenance.Status == EquipmentMaintenanceConstants.Assigned || equipmentMaintenance.Status == EquipmentMaintenanceConstants.Requested && equipmentMaintenance.TroubleState == false)
                    {
                        errorMessage = "This equipment can not be used due to trouble state reported";
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                canUsed = true;
            }

            return Json(new { CanUsed = canUsed, ErrorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetLocations()
        {
            var activeLocations = new LocationService()
                .GetLocationsQueryable()
                .Where(x => x.Status.Contains("APPROVED") && x.Name != null && x.Name != "")
                .Select(x => x.Name)
                .ToList();

            var locationslist = string.Join(";", activeLocations.Select(x => x + ":" + x).ToList());

            return Json(new { locationslist }, JsonRequestBehavior.AllowGet);
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