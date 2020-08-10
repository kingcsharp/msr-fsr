using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Answer.Domain.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Models.Config;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class FileController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public FileController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<FileModel>>))]
        public async Task<IActionResult> GetFiles([FromQuery] GetFileRequest req)
        {
            if (!CurrentUser.HasPrivilege(EnumUtils.ParseMenuType(req.EntityName), EnumPrivilege.CanRead)) {
                throw new DomainException("Permission Denied", DomainError.BadRequest);
            }
            var ret = await _dispatcher.DispatchAsync(new GetFiles() {
                entityName = req.EntityName,
                entityId = req.EntityId,
                fileId = req.FileId
            });
            return ret.ToOkObjectResponse<ICollection<FileModel>>();
        }

        [HttpPost]
        [SwaggerResponse(typeof(AuditActionResult<FileModel>))]
        public async Task<IActionResult> AddFile(CreateFileRequest newfile)
        {
            if (!CurrentUser.HasPrivilege(EnumUtils.ParseMenuType(newfile.EntityName), EnumPrivilege.CanCreate)) {
                throw new DomainException("Permission Denied", DomainError.BadRequest);
            }
            var command = newfile.ToCreateFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<FileModel>("File was successfully added.");
        }

        [HttpPost("Help")]
        [SwaggerResponse(typeof(AuditActionResult<UploadResponse>))]
        public async Task<IActionResult> UploadFile([FromForm]UploadFileRequest request)
        {
            var command = request.ToUploadFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<UploadResponse>("File was successfully Uploaded.");
        }

        [HttpPost("Import")]
        [SwaggerResponse(typeof(ImportAuditActionResult<IEnumerable<object>>))]
        public async Task<IActionResult> ImportFile([FromBody, Required] ImportRequest request)
        {
            var command = request.ToImportFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToImportOkObjectResponse<IEnumerable<object>>("Data Imported Successfully");
        }

        [HttpDelete]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DetachFile([FromQuery] DetachFileRequest req)
        {
            if (!CurrentUser.HasPrivilege(EnumUtils.ParseMenuType(req.EntityName), EnumPrivilege.CanDelete)) {
                throw new DomainException("Permission Denied", DomainError.BadRequest);
            }
            var command = new DetachFile() {
                entityId = req.EntityId,
                entityName = req.EntityName,
                fileId = req.FileId
            };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<int>("File was successfully removed.");
        }
    }
}
