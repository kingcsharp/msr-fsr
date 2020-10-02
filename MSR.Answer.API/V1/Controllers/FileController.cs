using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Answer.Domain.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    /// FileController: generic file import, save, and export.
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class FileController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public FileController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// GetFiles
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<FileModel>>))]
        public async Task<IActionResult> GetFiles([FromQuery] GetFileRequest req)
        {
            var ret = await _dispatcher.DispatchAsync(new GetFiles() {
                entityName = req.EntityName,
                entityId = req.EntityId,
                fileId = req.FileId
            });
            return ret.ToOkObjectResponse<ICollection<FileModel>>();
        }

        /// <summary>
        /// AddFile
        /// </summary>
        /// <param name="newfile"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(typeof(AuditActionResult<FileModel>))]
        public async Task<IActionResult> AddFile(CreateFileRequest newfile)
        {
            var command = newfile.ToCreateFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<FileModel>("File was successfully added.");
        }

        /// <summary>
        /// Upload Help File
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Help")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerResponse(typeof(UploadResponse))]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest request)
        {
            if (!request.Upload.Any())
            {
                return BadRequest();
            }
            var command = request.ToUploadFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return new OkObjectResult(((ICommandResponse<UploadResponse>)ret).Data);
        }

        /// <summary>
        /// Import data file
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Import")]
        [SwaggerResponse(typeof(ImportAuditActionResult<IEnumerable<ImportError>>))]
        public async Task<IActionResult> ImportFile([FromBody, Required] ImportRequest request)
        {
            var command = request.ToImportFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToImportOkObjectResponse<IEnumerable<ImportError>>("Data Validated and awaiting import.  System will notify you when complete.");
        }

        /// <summary>
        /// Detach a file from an entity.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
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
