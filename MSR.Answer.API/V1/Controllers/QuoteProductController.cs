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

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class QuoteProductController : BaseApiController
    {
        private const string privilegeApiName = "QuotesProducts";
        private readonly ICommandDispatcher _dispatcher;

        public QuoteProductController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<IEnumerable<QuotesProductsView>>))]
        public async Task<IActionResult> Get([FromQuery] GetQuotesProductsGridViewRequest filters)
        {
            var getQuotesProductsGridView = filters.ToGetQuotesProductsRequestCommand();
            var ret = await _dispatcher.DispatchAsync(getQuotesProductsGridView);
            return ret.ToOkObjectResponse<IEnumerable<QuotesProductsView>>();
        }


        /// <summary>
        /// Creates a Quote based on the <paramref name="newQuote"/> request.
        /// </summary>
        /// <param name="newQuote"></param>
        /// <permission>CanCreate Privilege required</permission>
        /// <returns>Quote DTO</returns>
        [HttpPost]
        [HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
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
        [HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
