using Msr.Models.Reporting;
using Msr.Services.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace Answer.Web.Controllers.API
{
    [RoutePrefix("api/Reporting")]
    public class ReportingAPIController : ODataController
    {
        private ReportingService _reportingService;

        public ReportingAPIController()
        {
            _reportingService = new ReportingService();
        }

        [HttpGet, Route("CombinedFinancialData")]
        [EnableQuery()]
        public IQueryable<CombinedFinancialData> GetCombinedFinancialData()
        {
            return _reportingService.GetCombinedFinancialData();
        }
    }
}