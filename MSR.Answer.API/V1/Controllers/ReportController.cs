using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Domain.Models;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using System.ComponentModel.DataAnnotations;
using MSR.Domain.Abstractions.Services;
using System.Threading;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ReportController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;
        private IReportService _reportService;

        public ReportController(ICommandDispatcher dispatcher, IReportService reportService)
        {
            _dispatcher = dispatcher;
            _reportService = reportService;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<ReportModel>>))]
        public async Task<IActionResult> Get([FromQuery, Required] GetReportRequest request)
        {
            var command = request.ToGetReportCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<ReportModel>>();
        }

        [HttpGet("Dashboard/{id}"), SwaggerResponse(typeof(AuditActionResult<ReportDashboardModel>))]
        public async Task<IActionResult> GetDashboard([FromRoute, Required] GetDashboardRequest request)
        {
            var command = request.ToGetDashboardCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ReportDashboardModel>();
        }

    }
}
