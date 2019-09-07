using Answer.Web.Filters;
using Msr.Services.Orders;
using Msr.Services.Orders.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    [AuthorizeUser]
    public class PrintOtherController : BaseController
    {
        private readonly OrderService _orderService;

        public PrintOtherController()
        {
            _orderService = new OrderService();
        }

        public ActionResult PrintReport(int id, string reportType)
        {
            if (reportType == "DELIVERY")
            {
                return DeliveryTsr(id);
            }

            if (reportType == "WIP_HIST")
            {
                return PrintOtherWipHistory(id);
            }

            if (reportType == "HISTORY_LABEL")
            {
                return NcrTsr(id);
            }

            if (reportType == "MONITOR_LABEL")
            {
                return MonitorLabelTsr(id);
            }

            if (reportType == "PART_LABEL")
            {
                return PartLabelTsr(id);
            }

            return Content("Report Type not found");
        }

        public ActionResult WorkReportTsr(int id, int purchaseItemId, string tsrType, string showSteps, string showShipping)
        {
            if (tsrType == "PURCHASE")
            {
                var response = _orderService.GetPurchaseWorkReportTsrDetails(purchaseItemId);
                return PartialView("_ViewWorkReportPurchaseSummaryTsr", response);
            }
            else
            {
                var response = _orderService.GetTechnicalWorkReportTsrDetails(id, purchaseItemId);
                return PartialView("_ViewWorkReportTechnicalSummaryTsr", response);
            }
        }

        public ActionResult DeliveryTsr(int id)
        {
            var response = _orderService.GetDeliveryTsrDetails(id);
            return PartialView("_ViewDeliveryTsr", response);
        }

        public ActionResult NcrTsr(int id)
        {
            var orderService = new OrderService();
            var taskService = new TaskService();
            var ncrDetails = orderService.GetNcrDetails(id.ToString());

            var docs = orderService.GetDocuments(ncrDetails.Details.FillObjId);

            var photos = GetDocViewModel(docs, orderService, 400);
            ncrDetails.Photos = photos;

            var response = taskService.GetTaskWithMonitors(id.ToString());

            ncrDetails.MonitorItem = response.MonitorItem.Where(x => x.Description.Contains("Nonconformity") || x.Description.Contains("NCR")).ToList();

            return PartialView("_ViewNcrTsr", ncrDetails);
        }

        //private List<DocumentView> GetDocViewModel(List<DocumentView> docs, OrderService orderService, int width)
        //{
        //    var photos = new List<DocumentView>();

        //    foreach (var doc in docs)
        //    {
        //        if (doc.ContentType == "image/jpeg" || doc.ContentType == "image/gif" || doc.ContentType == "image/png")
        //        {
        //            var photo = orderService.GetDocumentBase64(doc.ServerPath);

        //            photos.Add(new DocumentView
        //            {
        //                FileArray = photo,
        //                Name = doc.Name
        //            });
        //        }
        //    }

        //    return photos;
        //}

        private List<DocumentView> GetDocViewModel(List<DocumentView> docs, OrderService orderService, int width)
        {
            var photos = new List<DocumentView>();

            foreach (var doc in docs)
            {
                if (string.IsNullOrWhiteSpace(doc.ContentType) == true) { doc.ContentType = string.Empty; } else { doc.ContentType = doc.ContentType.Trim().ToLower(); }

                if (doc.ContentType == "image/jpg" || doc.ContentType == "image/jpeg" || doc.ContentType == "image/gif" || doc.ContentType == "image/png")
                {
                    // var photo = orderService.GetDocumentBase64(doc.ServerPath);

                    photos.Add(new DocumentView
                    {
                        // FileArray = photo,
                        Name = doc.Name,
                        DocumentURL = Uri.EscapeUriString(doc.ServerPath)
                    });
                }
            }

            return photos;
        }

        public ActionResult PartLabelTsr(int id)
        {
            var response = _orderService.GetPartLabelTsrDetails(id);
            return PartialView("_ViewPartLabelTsr", response);
        }

        public ActionResult MonitorLabelTsr(int id)
        {
            var response = _orderService.GetMonitorLabelTsrDetails(id);
            return PartialView("_ViewMonitorLabelTsr", response);
        }

        public ActionResult PrintOtherWipHistory(int id)
        {
            var response = _orderService.GetWipHistoryTsrDetail(id);

            return PartialView("_ViewTsrWipHistory", response);
        }
    }
}