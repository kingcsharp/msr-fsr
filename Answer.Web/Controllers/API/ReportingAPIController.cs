using Microsoft.AspNet.OData;
using Msr.Models.Reporting;
using Msr.Services.Reporting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
    }
}