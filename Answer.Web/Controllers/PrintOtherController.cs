using System.Web.Mvc;
using Msr.Services.Orders;

namespace Answer.Web.Controllers
{
    public class PrintOtherController : Controller
    {
        private readonly OrderService _orderService;

        public PrintOtherController()
        {
            _orderService = new OrderService();
        }

        public ActionResult PrintReport(int id, string reportType)
        {
            if (reportType == "WORK_REPORT")
            {
                return DeliveryTsr(id);
            }

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