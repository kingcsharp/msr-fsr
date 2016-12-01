using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Notes;
using Msr.Services.Orders;
using Msr.Services.Users;
using Msr.Web.ViewModel;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class WipController : BaseController
    {
        public ActionResult Engineering()
        {
            ViewBag.ActiveClass = "WIP";

            var loggedUser = User.Identity.GetUserId();

            var userService = new UserService();

            var company = userService.GetCompanyId(loggedUser);

            ViewBag.ClientName = company.Name;

            return View();
        }

        public ActionResult EngineeringData(JqGridParam param)
        {
            var loggedUser = User.Identity.GetUserId();
            var userService = new UserService();
           var company = userService.GetCompanyId(loggedUser);
            var orderService = new OrderService();

            var totalRows = orderService.GetWorkOrderQueryable().Where(x => x.CustId == company.Id);


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
                    else if (rule.field == nameof(WorkOrderView.StartDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows =
                                totalRows.Where(
                                    q =>
                                        q.StartDate.HasValue && q.StartDate.Value.Day == value.Day &&
                                        q.StartDate.Value.Month == value.Month && q.StartDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.DueDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows =
                                totalRows.Where(
                                    q =>
                                        q.DueDate.HasValue && q.DueDate.Value.Day == value.Day &&
                                        q.DueDate.Value.Month == value.Month && q.DueDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(WorkOrderView.ProcName))
                    {
                        totalRows = totalRows.Where(x => x.ProcName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(WorkOrderView.CurStepText))
                    {
                        totalRows = totalRows.Where(x => x.CurStepText.ToLower().Contains(rule.data.ToLower()));
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
            totalRows = totalRows.Skip(param.pageIndex - 1);
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

            var ncrDetails = orderService.GetNcrDetails(id);

            return PartialView("_NcrModel", ncrDetails);
        }

        public ActionResult GetPhotsModel(string id)
        {
            var orderService = new OrderService();

            var docs = orderService.GetDocuments(id);

            var photos = new List<DocViewModel>();

            foreach (var doc in docs)
            {
                if (doc.ContentType == "image/jpeg" || doc.ContentType == "image/gif" || doc.ContentType == "image/png")
                {
                    var photo = orderService.GetDocumentBase64(doc.ServerPath, 400);

                    photos.Add(new DocViewModel
                    {
                        FileArray = photo,
                        FileName = doc.Name
                    });
                }
            }
                   
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
    }
}