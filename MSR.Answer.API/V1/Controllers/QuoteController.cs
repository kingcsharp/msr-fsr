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
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class QuoteController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public QuoteController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Creates a Quote based on the <paramref name="newQuote"/> request.
        /// </summary>
        /// <param name="newQuote"></param>
        /// <permission>CanCreate Privilege required</permission>
        /// <returns>Quote DTO</returns>
        [HttpPost]
        [HasPrivilegeApi("QuotesProducts", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<QuoteModel>))]
        public async Task<IActionResult> Post([FromBody, Required] CreateQuoteRequest newQuote)
        {
            var command = newQuote.ToCreateQuoteCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<QuoteModel>("Quote was successfully added.");
        }

        /// <summary>
        /// Deletes a Quote based on the <paramref name="id"/> parameter
        /// </summary>
        /// <param name="id"></param>
        /// <permission>CanDelete Privilege required</permission>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [HasPrivilegeApi("QuotesProducts", EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var command = new DeleteQuote() {
                Id = id
            };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<QuoteModel>("Quote was successfully deleted.");
        }
    }
}
