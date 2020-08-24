using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Net;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class QuoteController : BaseApiController
    {
        private const string privilegeApiName = "QuotesProducts";
        private readonly ICommandDispatcher _dispatcher;

        public QuoteController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("Product"), HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<QuotesProductsView>>))]
        public async Task<IActionResult> Get([FromQuery] GetQuotesProductsRequest filters)
        {
            var getQuotesProducts = filters.ToGetQuotesProductsRequestCommand();
            var ret = await _dispatcher.DispatchAsync(getQuotesProducts);
            return ret.ToOkObjectResponse<IEnumerable<QuotesProductsView>>();
        }

        /// <summary>
        /// Creates a Quote based on the <paramref name="quote"/> request.
        /// </summary>
        /// <param name="quote"></param>
        /// <permission>CanCreate Privilege required</permission>
        /// <returns>Quote DTO</returns>
        [HttpPost]
        [HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<QuoteModel>))]
        public async Task<IActionResult> CreateQuote([FromBody, Required] CreateQuoteRequest quote)
        {
            var command = quote.ToCreateQuoteCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<QuoteModel>("Quote was successfully added.");
        }

        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<QuoteModel>>))]
        public async Task<IActionResult> GetQuote([FromQuery] GetQuoteRequest filters)
        {
            var getQuote = filters.ToGetQuotesCommand();
            var ret = await _dispatcher.DispatchAsync(getQuote);
            return ret.ToOkObjectResponse<IEnumerable<QuoteModel>>();
        }

        /// <summary>
        /// Deletes a Quote based on the <paramref name="id"/> parameter
        /// </summary>
        /// <param name="id"></param>
        /// <permission>CanDelete Privilege required</permission>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanDelete)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult))]
        public async Task<IActionResult> DeleteQuote([FromRoute] int id)
        {
            var command = new DeleteQuote()
            {
                Id = id
            };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<QuoteModel>("Quote was successfully deleted.");
        }
    }
}
