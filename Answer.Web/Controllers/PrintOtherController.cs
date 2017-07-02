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
    }
}