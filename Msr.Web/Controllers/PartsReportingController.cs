using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Models.Tasks;
using Msr.Services.jqGrid;
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
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(PartsReportsViewModel vm)
        {
            vm.SetDate();

            var taskService = new TaskService();

            var monitors = taskService.GetReports(vm.Number, vm.FromDate, vm.ToDate, vm.ReportById, vm.MonitorId);

            vm.MonitorsWithTaskAndResults = monitors;

            if (vm.MonitorId == MonitorTypeConstants.Densitometer || vm.MonitorId == MonitorTypeConstants.Voltage)
            {
                GetGraphLineData(vm);
                vm.DataSetsJson = JsonConvert.SerializeObject(vm.DataSets, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }

            return View(vm);
        }


        [HttpPost]
        public JsonResult GetMonitors(string input, int type)
        {
            var taskService = new TaskService();

            var monitors = taskService.GetMonitors(input, type);

            return Json(monitors, JsonRequestBehavior.AllowGet);
        }

        private void GetGraphLineData(PartsReportsViewModel reportsViewModel)
        {
            var uniqueDateCount = reportsViewModel.MonitorsWithTaskAndResults.Select(x => x.TaskStopDate).Distinct();

            reportsViewModel.Categories = uniqueDateCount.OrderBy(x => x).Select(x => x.ToString()).ToList();

            reportsViewModel.DataSets.Add(new ReportItemData
                    {
                        name = "1 Densitometer reading",
                        data = GetData(reportsViewModel.MonitorsWithTaskAndResults,"#1", uniqueDateCount)
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

        private List<decimal> GetData(List<MonitorsWithTaskAndResult> monitorsWithTaskAndResults, string s, IEnumerable<DateTime?> uniqueDate)
        {
            var slotData = new List<decimal>();

            foreach (var item in uniqueDate.OrderBy(x=>x))
            {
                var printValue = monitorsWithTaskAndResults.Single(x => x.Description.Contains(s) && x.TaskStopDate == item.GetValueOrDefault());
                slotData.Add(string.IsNullOrWhiteSpace(printValue.PrintResult)? decimal.Zero: decimal.Parse(printValue.PrintResult));
            }
            
            return slotData;
        }
    }

}