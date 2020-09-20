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

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ReportController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;


        public ReportController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<ReportModel>>))]
        [HasPrivilegeApi("Reports", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get()
        {
            var ret = await _dispatcher.DispatchAsync(new GetReport());
            return ret.ToOkObjectResponse<ICollection<ReportModel>>();
        }

        [HttpGet("Dashboard/{id}"), SwaggerResponse(typeof(AuditActionResult<ReportDashboardModel>))]
        [HasPrivilegeApi("Reports", EnumPrivilege.CanRead)]
        public async Task<IActionResult> GetDashboard([FromRoute, Required]GetDashboardRequest request)
        {
            var command = request.ToGetDashboardCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ReportDashboardModel>();
        }
    }
}
