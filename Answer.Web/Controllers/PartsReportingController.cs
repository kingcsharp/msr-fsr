using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Tasks;
using Msr.Services.Orders;
using Msr.Services.Users;
using Msr.Web.ViewModel;
using Msr.Web.ViewModel.Reports;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class PartsReportingController : BaseController
    {
        public ActionResult Index()
        {
            var vm = new PartsReportsViewModel();

            var loggedUser = User.Identity.GetUserId();
            var userService = new UserService();
            var company = userService.GetCompanyId(loggedUser);
            var orderService = new OrderService();

            var workOrders = orderService.GetWorkOrderQueryable()
                .Where(x => x.CustId == company.Id)
                .Select(x => new {x.Serial, x.ActualPartId})
                .ToList();

            vm.Serials = workOrders.Select(x => new SelectListItem {Text = x.Serial , Value = x.Serial}).ToList();
            vm.PartNumbers = workOrders.Select(x => new SelectListItem { Text = x.ActualPartId, Value = x.ActualPartId }).ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(PartsReportsViewModel vm)
        {
            vm.SetDate();

            var taskService = new TaskService();

            var monitors = taskService.GetReports(vm.Number, vm.FromDate, vm.ToDate, vm.ReportTypeId, vm.MonitorId);

            if (monitors.Any())
            {
                vm.MonitorsWithTaskAndResults = monitors;
                var monitor = monitors.FirstOrDefault();

                if (vm.ReportTypeId == ReportTypeConstants.Graph)
                {
                    GetGraphLineData(vm, monitor);
                    vm.DataSetsJson = JsonConvert.SerializeObject(vm.DataSets, Formatting.Indented, new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});

                    if (monitor.Description.Contains("(mA)"))
                    {
                        vm.Ytext = "Volts";
                    }
                    if (monitor.Description.Contains("Densitometer"))
                    {
                        vm.Ytext = "Degrees (C)";
                    }
                }
            }

            return PartialView("_Report", vm);
        }

        [HttpPost]
        public JsonResult GetMonitors(string input, int type, int reportTypeId)
        {
            var taskService = new TaskService();

            var monitors = taskService.GetMonitors(input, type, reportTypeId);

            var monitorList = new List<SelectListItem>();

            monitorList.Add(new SelectListItem {Text = "--Select--" , Value = ""});
            monitorList.AddRange(monitors);

            var data = monitorList.Select(x => new 
            {
                id = x.Value,
                text = x.Text
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private void GetGraphLineData(PartsReportsViewModel reportsViewModel, MonitorsWithTaskAndResult monitor)
        {
            var uniqueDateCount = reportsViewModel.MonitorsWithTaskAndResults.Select(x => x.TaskStopDate).Distinct();

            reportsViewModel.Categories = uniqueDateCount.OrderBy(x => x).Select(x => x.Value.ToString("MM/dd/yyyy")).ToList();

            if (monitor.Description.Contains("Densitometer"))
            {
                reportsViewModel.DataSets.Add(new ReportItemData
                {
                    name = "1 Densitometer reading",
                    data = GetData(reportsViewModel.MonitorsWithTaskAndResults, "#1", uniqueDateCount)
                });

                reportsViewModel.DataSets.Add(new ReportItemData
                {
                    name = "2 Densitometer reading",
                    data = GetData(reportsViewModel.MonitorsWithTaskAndResults, "#2", uniqueDateCount)
                });

                reportsViewModel.DataSets.Add(new ReportItemData
                {
                    name = "3 Densitometer reading",
                    data = GetData(reportsViewModel.MonitorsWithTaskAndResults, "#3", uniqueDateCount)
                });
            }
            else
            {
                reportsViewModel.DataSets.Add(new ReportItemData
                {
                    name = "Input Current Value (mA)",
                    data = GetData(reportsViewModel.MonitorsWithTaskAndResults, "Input Current Value", uniqueDateCount)
                });
            }
        }

        private List<decimal> GetData(List<MonitorsWithTaskAndResult> monitorsWithTaskAndResults, string s, IEnumerable<DateTime?> uniqueDate)
        {
            var slotData = new List<decimal>();

            foreach (var item in uniqueDate.OrderBy(x=>x))
            {
                if (monitorsWithTaskAndResults != null && monitorsWithTaskAndResults.Any())
                {
                  var  printValue = monitorsWithTaskAndResults.FirstOrDefault(x => x.Description.Contains(s) && x.TaskStopDate == item.GetValueOrDefault());
                    slotData.Add(string.IsNullOrWhiteSpace(printValue.PrintResult) ? decimal.Zero : decimal.Parse(printValue.PrintResult));
                }
                else
                {
                    slotData.Add(0);
                }
            }
            
            return slotData;
        }
    }

}