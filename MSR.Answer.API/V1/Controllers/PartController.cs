using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class PartController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public PartController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [HasPrivilegeApi("Parts", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<PartModel>>))]
        public async Task<IActionResult> GetPart([FromQuery] GetPartRequest req)
        {
            var ret = await _dispatcher.DispatchAsync(new GetParts() {
                partID = req.Id,
            });
            return ret.ToOkObjectResponse<ICollection<PartModel>>();
        }

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
            return ret.ToOkObjectResponse<PartModel>(message);
        }

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
            return ret.ToOkObjectResponse<PartModel>(message);
        }

        [HttpDelete("{id}")]
        [HasPrivilegeApi("Parts", EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DeletePart(int id)
        {
            var command = new DeletePart() {
                Id = id
            };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<PartModel>("Part was successfully removed.");
        }

        [HttpPost("import")]
        [HasPrivilegeApi("Parts", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<PartModel>>))]
        public async Task<IActionResult> ImportParts(ImportPartsRequest req)
        {
            var command = new ImportParts() {
                base64Data = req.base64Data
            };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<int>("Parts successfully imported");
        }
    }
}
