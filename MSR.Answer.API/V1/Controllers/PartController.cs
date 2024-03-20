using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Domain.Abstractions.Services;
using MSR.Application.Abstractions;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    /// Part Controller
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class PartController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;
        private IPartViewService _partViewService;

        /// <summary>
        /// Part Controller
        /// </summary>
        public PartController(ICommandDispatcher dispatcher, IPartViewService partViewService)
        {
            _dispatcher = dispatcher;
            _partViewService = partViewService;
        }

        /// <summary>
        /// Get part by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<PartModel>>))]
        public async Task<IActionResult> GetPart([FromQuery] GetPartRequest request)
        {
            var command = request.ToGetPartsCommand();
            var ret = await _dispatcher.DispatchAsync(command);
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
            PartModel data = (ret as ICommandResponse<PartModel>).Data;
            if (data == null || !data.IsPending)
            {
                message = "Part was successfully updated.";
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

        [HttpGet("partExport")]
        [SwaggerResponse(typeof(FileContentResult))]
        public async Task<ActionResult> ExportFile([FromQuery] PartExportRequest filters)
        {
            var ret = await _partViewService.ExportParts(filters.ToPartExportQueryFilters());
            return File(ret.data, "application/octet-stream", ret.FileName);
        }
    }
}
