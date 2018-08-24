using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Menus;
using Msr.Services.Orders;

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
            var response = _orderService.GetNcrTsrDetails(id);
            return PartialView("_ViewNcrTsr", response);
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