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

        [HttpGet,Route("AdHocReport/{title}")]
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
                } 
            };

            return Json(dict[title]);
        }
    }
}