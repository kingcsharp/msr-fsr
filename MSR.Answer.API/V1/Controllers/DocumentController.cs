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
using MSR.Domain.Views;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
#if DEBUG
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
#endif
    public class DocumentController : BaseApiController
    {
        private const string privilegeApiName = "Documents";
        private ICommandDispatcher _dispatcher;

        public DocumentController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<ICollection<DocumentView>>))]
        public async Task<IActionResult> GetDocuments([FromQuery] GetDocumentRequest filters)
        {

            var getDocument = filters.ToGetDocumentCommand();
            var ret = await _dispatcher.DispatchAsync(getDocument);
            return ret.ToOkObjectResponse<ICollection<DocumentView>>();
        }

        [HttpPost, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<DocumentView>))]
        public async Task<IActionResult> CreateDocument(CreateDocumentRequest newDocument)
        {
            var command = newDocument.ToCreateDocumentCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<DocumentView>("Document was successfully added.");
        }

        [HttpPatch, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<DocumentView>))]
        public async Task<IActionResult> UpdateDocument([FromBody, Required] UpdateDocumentRequest request)
        {
            var updateDocument = request.ToUpdateDocumentCommand();
            var ret = await _dispatcher.DispatchAsync(updateDocument);
            return ret.ToOkObjectResponse<DocumentView>("Document has been successfully updated.");
        }
    }
}
