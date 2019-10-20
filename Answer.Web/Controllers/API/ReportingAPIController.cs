using Microsoft.AspNet.OData;
using Msr.Models.Reporting;
using Msr.Services.Reporting;
using System.Linq;
using System.Web.Http;

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