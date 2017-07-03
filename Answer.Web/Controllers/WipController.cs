using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Notes;
using Msr.Services.Orders;
using Msr.Services.Orders.Messaging;
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
                foreach (var rule    in param.where.rules)
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
                        if (rule.data!="ALL")
                        {
                            totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.Notes))
                    {
                        totalRows = totalRows.Where(x => x.Notes.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            string orderBy = "SupplierName";
            string orderDirection = "asc";

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

            var totalPages = (int) Math.Ceiling((float) totalRecords/(float) param.pageSize);

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
            var orderService = new OrderService();
            var taskService = new TaskService();
            var ncrDetails = orderService.GetNcrDetails(id);

            var docs = orderService.GetDocuments(ncrDetails.Details.FillObjId);

            var photos = GetDocViewModel(docs, orderService,400);
            ncrDetails.Photos = photos;

            var response = taskService.GetTaskWithMonitors(id);

            ncrDetails.MonitorItem = response.MonitorItem.Where(x=> x.Description.Contains("Nonconformity") || x.Description.Contains("NCR")).ToList();

            return PartialView("_NcrModel", ncrDetails);
        }

        public ActionResult GetPhotsModel(string id)
        {
            var orderService = new OrderService();

            var docs = orderService.GetDocuments(id);

            var photos = GetDocViewModel(docs, orderService,400);
                   
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

        public ActionResult Details(int id)
        {
            WorkOrderDetailsResponse response = _orderService.GetPurchaseItemDetails(id);

            return View(response);
        }

        public ActionResult PrintTraveler(int id)
        {
            var response = _orderService.GetTsrDetails(id);

            return PartialView("_ViewTsr", response);
        }

        public ActionResult PrintOtherWipHistory(int id)
        {
            var response = _orderService.GetWipHistoryTsrDetail(id);

            return PartialView("_ViewTsrWipHistory", response);
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