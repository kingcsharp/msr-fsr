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
        public IQueryable<CombinedFinancialData> Get()
        {
            try
            {
                var ret = _reportingService.GetCombinedFinancialData();
                return ret;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
    }
}