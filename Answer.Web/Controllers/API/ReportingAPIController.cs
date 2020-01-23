using Microsoft.AspNet.OData;
using Msr.Models.Reporting;
using Msr.Services.Reporting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace Answer.Web.Controllers.API
{
    [RoutePrefix("api/Reporting")]
    public class ReportingAPIController : ApiController
    {
        private ReportingService _reportingService;

        public ReportingAPIController()
        {
            _reportingService = new ReportingService();
        }

        [HttpGet, EnableQuery, Route("CombinedFinancialData")]
        public IQueryable<CombinedFinancialData> GetCombinedFinancialData()
        {
            var ret = _reportingService.GetCombinedFinancialData();
            return ret;
        }

        [HttpGet, EnableQuery, Route("WorkOrdersWithoutInvoices")]
        public IQueryable<WorkOrdersWithoutInvoices> GetWorkOrdersWithoutInvoices()
        {
            var ret = _reportingService.GetWorkOrdersWithoutInvoices();
            return ret;
        }

        [HttpGet, EnableQuery, Route("SerialNumberHistory")]
        public IQueryable<SerialNumberHistory> GetSerialNumberHistory()
        {
            var ret = _reportingService.GetSerialNumberHistory();
            return ret;
        }

        [HttpGet, EnableQuery, Route("WorkInProcess")]
        public IQueryable<WorkInProcess> GetWorkInProcess()
        {
            var ret = _reportingService.GetWorkInProcess();
            return ret;
        }

        [HttpGet, EnableQuery, Route("ActualPartsHistorybyPartNumber")]
        public IQueryable<ActualPartsHistory> GetActualPartsHistorybyPartNumber()
        {
            var ret = _reportingService.GetActualPartsHistory().OrderBy(i => i.PN);
            return ret;
        }

        [HttpGet, EnableQuery, Route("ActualPartsHistorybySerialNumber")]
        public IQueryable<ActualPartsHistory> GetActualPartsHistorybySerialNumber()
        {
            var ret = _reportingService.GetActualPartsHistory().OrderBy(i => i.SN);
            return ret;
        }

        [HttpGet, EnableQuery, Route("CustomerRevenueData")]
        public IQueryable<OperationsCustomerRevenueData> GetRevenueByCustomerData()
        {
            var ret = _reportingService.GetRevenueByCustomerData();
            return ret;
        }

        [HttpGet, EnableQuery, Route("KitRevenueData")]
        public IQueryable<OperationsKitRevenueData> GetRevenueByKitsData()
        {
            var ret = _reportingService.GetRevenueByKitsData();
            return ret;
        }
        [HttpGet, EnableQuery, Route("KitCountsData")]
        public IQueryable<OperationsKitCountData> GetCountsByKitsData()
        {
            var ret = _reportingService.GetCountsByKitsData();
            return ret;
        }

        [HttpGet, Route("AdHocReport/{title}")]
        public JsonResult<AdHocReportItem> AdHocReport(string title)
        {
            var dict = new Dictionary<string, AdHocReportItem>() {
                {
                    "ActualPartsHistorybyPartNumber",
                    new AdHocReportItem()
                    {
                        Columns = new List<object>(){
                            new { field = "pn", headerText = "PN", width = 75 },
                            new { field = "sn", headerText = "SN", width = 75 },
                            new { field = "workOrderNumber", headerText = "WO#", width = 75 },
                            new { field = "dateCompleted", headerText = "Date Completed", width = 75, format = "{0:MM/dd/yyyy}" },
                            new { field = "cycleCount", headerText = "Cycle Count", width = 25 },
                            new { field = "ncDisposition", headerText = "NC Disposition", width = 125 }
                        },
                        DataURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/ActualPartsHistorybyPartNumber",
                        Title = "Actual Parts History by Part Number"
                    }
                },
                {
                    "ActualPartsHistorybySerialNumber",
                    new AdHocReportItem()
                    {
                        Columns = new List<object>(){
                        new { field = "sn", headerText = "SN", width = 75 },
                        new { field = "pn", headerText = "PN", width = 75 },
                        new { field = "workOrderNumber", headerText = "WO#", width = 75 },
                        new { field = "dateCompleted", headerText = "Date Completed", width = 75, format = "{0:MM/dd/yyyy}" },
                        new { field = "cycleCount", headerText = "Cycle Count", width = 25 },
                        new { field = "ncDisposition", headerText = "NC Disposition", width = 125 }
                        },
                        DataURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/ActualPartsHistorybySerialNumber",
                        Title = "Actual Parts History by Serial Number"
                    }
                },
                {
                    "SerialNumberHistory",
                    new AdHocReportItem()
                    {
                        Columns = new List<object>(){
                        new { field = "serialNumber", headerText = "Serial #", width = 75 },
                        new { field = "woNumber", headerText = "WO #", width = 75 },
                        new { field = "woCreationDate", headerText = "Created", width = 70, format = "{0:MM/dd/yyyy}" },
                        new { field = "dueDate", headerText = "Due Date", width = 70, format = "{0:MM/dd/yyyy}" },
                        new { field = "shipDate", headerText = "Ship Date", width = 75, format = "{0:MM/dd/yyyy}" },
                        new { field = "msrfsrFacility", headerText = "Facility", width = 60 },
                        new { field = "customerName", headerText = "Customer", width = 100 },
                        new { field = "specno", headerText = "Spec #", width = 100 },
                        new { field = "kitName", headerText = "Kit Name", width = 100 },
                        new { field = "ncrNumber", headerText = "NCR #", width = 75 },
                        new { field = "pono", headerText = "PO #", width = 75 },
                        new { field = "mttn", headerText = "MTTN", width = 75 },
                        new { field = "cycleCount", headerText = "Cycle Count", width = 75 }
                        },
                        DataURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/SerialNumberHistory",
                        Title = "Serial Number History"
                    }
                },
                {
                    "WorkInProcess",
                    new AdHocReportItem()
                    {
                        Columns = new List<object>(){
                        new { field = "woItem", headerText = "WO #", width = 100 },
                        new { field = "dueDate", headerText = "Due Date", width = 75, format = "{0:MM/dd/yyyy}" },
                        new { field = "details", headerText = "Details", width = 200, format = "{0:MM/dd/yyyy}" }
                        },
                        DataURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/WorkInProcess",
                        Title = "Work in Process"
                    }
                }
            };

            return Json(dict[title]);
        }
    }
}