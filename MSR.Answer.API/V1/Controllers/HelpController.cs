using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class HelpController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public HelpController(ILogger<HelpController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpPost("/Support"), AllowAnonymous]
        public async Task<IActionResult> SubmitSupportRequest([FromBody, Required]CreateSupportRequest request)
        {
            var command = request.ToSubmitSupportCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

        [HttpGet, HasPrivilegeApi("HelpPages",EnumPrivilege.CanRead)]
        public async Task<IActionResult> GetHelpPages([FromQuery] GetHelpPageRequest request)
        {
            var command = request.ToGetHelpPageCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<IEnumerable<HelpPage>>();
        }

        [HttpPost, HasPrivilegeApi("HelpPages", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> CreateHelpPage([FromBody, Required]CreateHelpPageRequest request)
        {
            var command = request.ToCreateHelpPageCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

        [HttpPost("/Role"), HasPrivilegeApi("HelpPages", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> CreateHelpPageRole([FromBody, Required]CreateHelpPageRoleRequest request)
        {
            var command = request.ToCreateHelpPageRoleCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

        [HttpPatch, HasPrivilegeApi("HelpPages", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> UpdateHelpPage([FromBody, Required]UpdateHelpPageRequest request)
        {
            var command = request.ToUpdateHelpPageCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

        [HttpDelete("{id}"), HasPrivilegeApi("HelpPages", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> DeleteHelpPage(int id)
        {
            var command = new DeleteHelpPage() { HelpPageId = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

        [HttpDelete("/Role/{id}"), HasPrivilegeApi("HelpPages", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> DeleteHelpPageRole(int id)
        {
            var command = new DeleteHelpPageRole() { HelpPageRoleId = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }
        
    }
}
