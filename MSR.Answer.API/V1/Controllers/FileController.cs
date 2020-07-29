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
using System.Collections.Generic;
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
        public async Task<IActionResult> GetFiles(string entityName, int entityId, int? fileId)
        {
            var ret = await _dispatcher.DispatchAsync(new GetFiles() {
                entityName = entityName,
                entityId = entityId,
                fileId = fileId
            });
            return ret.ToOkObjectResponse<ICollection<FileModel>>();
        }

        [HttpPost]
        [SwaggerResponse(typeof(AuditActionResult<FileModel>))]
        public async Task<IActionResult> AddFile(CreateFileRequest newfile)
        {
            var command = newfile.ToCreateFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<FileModel>("File was successfully added.");
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DetachFile(string entityName, int entityId, int? fileId)
        {
            var command = new DetachFile() {
                entityId = entityId,
                entityName = entityName,
                fileId = fileId
            };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<FileModel>("File was successfully removed.");
        }
    }
}
