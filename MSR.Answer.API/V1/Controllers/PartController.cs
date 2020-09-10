using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Application.Hubs;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class PartController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;
        private readonly IHubContext<MessageHub> _messageHub;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dispatcher"></param>
        public PartController(ICommandDispatcher dispatcher, IHubContext<MessageHub> messageHub)
        {
            _dispatcher = dispatcher;
            _messageHub = messageHub;
        }

        /// <summary>
        /// Get part by Id
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        [HasPrivilegeApi("Parts", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<PartModel>>))]
        public async Task<IActionResult> GetPart([FromQuery] GetPartRequest req)
        {
            var ret = await _dispatcher.DispatchAsync(new GetParts()
            {
                partID = req.Id,
            });
            return ret.ToOkObjectResponse<ICollection<PartModel>>();
        }

        /// <summary>
        /// Create part
        /// </summary>
        /// <param name="newpart"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPrivilegeApi("Parts", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<PartModel>))]
        public async Task<IActionResult> AddPart(CreatePartRequest newpart)
        {
            var command = newpart.ToCreatePartCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            var message = "Part was successfully submitted to workflow for approval.";
            if (!(ret as ICommandResponse<PartModel>).Data.IsPending)
            {
                message = "Part was successfully added.";
            }
            else
            {
                await SendApprovalNotificationHubMessage(EnumApprovalTables.PartApproval, _messageHub);

            }
            return ret.ToOkObjectResponse<PartModel>(message);
        }

        /// <summary>
        /// Update part
        /// </summary>
        /// <param name="newpart"></param>
        /// <returns></returns>
        [HttpPatch]
        [HasPrivilegeApi("Parts", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<PartModel>))]
        public async Task<IActionResult> UpdatePart(UpdatePartRequest newpart)
        {
            var command = newpart.ToUpdatePartCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            var message = "Part update was successfully submitted to workflow for approval.";
            if (!(ret as ICommandResponse<PartModel>).Data.IsPending)
            {
                message = "Part was successfully updated.";
            }
            else
            {
                await SendApprovalNotificationHubMessage(EnumApprovalTables.PartApproval, _messageHub);
            }
            return ret.ToOkObjectResponse<PartModel>(message);
        }

        /// <summary>
        /// Delete part by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [HasPrivilegeApi("Parts", EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DeletePart(int id)
        {
            var command = new DeletePart()
            {
                Id = id
            };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<PartModel>("Part was successfully removed.");
        }
    }
}
