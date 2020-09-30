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
    public class DocumentController : BaseApiController
    {
        private const string privilegeApiName = "Documents";
        private ICommandDispatcher _dispatcher;

        /// <summary>
        /// Ctor DocumentController
        /// </summary>
        /// <param name="dispatcher"></param>
        public DocumentController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Gets a list of Documents or a single Document matching the Id.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<ICollection<DocumentView>>))]
        public async Task<IActionResult> GetDocuments([FromQuery] GetDocumentRequest filters)
        {

            var getDocument = filters.ToGetDocumentCommand();
            var ret = await _dispatcher.DispatchAsync(getDocument);
            return ret.ToOkObjectResponse<ICollection<DocumentView>>();
        }

        /// <summary>
        /// Creates a new Document or DocumentApproval.
        /// </summary>
        /// <param name="newDocument"></param>
        /// <returns></returns>
        [HttpPost, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<DocumentView>))]
        public async Task<IActionResult> CreateDocument(CreateDocumentRequest newDocument)
        {
            var command = newDocument.ToCreateDocumentCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<DocumentView>("Document was successfully added.");
        }

        /// <summary>
        /// Updates a Document or DocumentApproval based on the user privilege. Id is required.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<DocumentView>))]
        public async Task<IActionResult> UpdateDocument([FromBody, Required] UpdateDocumentRequest request)
        {
            var updateDocument = request.ToUpdateDocumentCommand();
            var ret = await _dispatcher.DispatchAsync(updateDocument);
            return ret.ToOkObjectResponse<DocumentView>("Document has been successfully updated.");
        }

        /// <summary>
        /// Deletes any Document or DocumentApproval with a matching Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var command = new DeleteDocument() { Id = id };
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse("Document has been sucessfully deleted.");
        }
    }
}
