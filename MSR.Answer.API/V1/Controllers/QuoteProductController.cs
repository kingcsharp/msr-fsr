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
    public class QuoteProductController : BaseApiController
    {
        private const string privilegeApiName = "QuotesProducts";
        private readonly ICommandDispatcher _dispatcher;

        public QuoteProductController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<QuotesProductsView>>))]
        public async Task<IActionResult> Get([FromQuery] GetQuotesProductsGridViewRequest filters)
        {
            var getQuotesProductsGridView = filters.ToGetQuotesProductsRequestCommand();
            var ret = await _dispatcher.DispatchAsync(getQuotesProductsGridView);
            return ret.ToOkObjectResponse<IEnumerable<QuotesProductsView>>();
        }

        /// <summary>
        /// Creates a Quote based on the <paramref name="quote"/> request.
        /// </summary>
        /// <param name="quote"></param>
        /// <permission>CanCreate Privilege required</permission>
        /// <returns>Quote DTO</returns>
        [HttpPost("Quote")]
        [HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<QuoteModel>))]
        public async Task<IActionResult> CreateQuote([FromBody, Required] CreateQuoteRequest quote)
        {
            var command = quote.ToCreateQuoteCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<QuoteModel>("Quote was successfully added.");
        }

        [HttpGet("Quote"), HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
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
        [HttpDelete("Quote/{id}")]
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

        /// <summary>
        /// Creates a Product based on the <paramref name="product"/> request.
        /// </summary>
        /// <param name="product"></param>
        /// <permission>CanCreate Privilege required</permission>
        /// <returns>Product DTO</returns>
        [HttpPost("Product")]
        [HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<ProductModel>))]
        public async Task<IActionResult> CreateProduct([FromBody, Required] CreateProductRequest product)
        {
            var command = product.ToCreateProductCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProductModel>("Product was successfully added.");
        }

        [HttpGet("Product"), HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<ProductModel>>))]
        public async Task<IActionResult> GetProduct([FromQuery] GetProductRequest filters)
        {
            var getProduct = filters.ToGetProductCommand();
            var ret = await _dispatcher.DispatchAsync(getProduct);
            return ret.ToOkObjectResponse<IEnumerable<ProductModel>>();
        }

        [HttpPatch("Product"), HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<ProductModel>))]
        public async Task<IActionResult> UpdateProduct([FromBody, Required] UpdateProductRequest request)
        {
            var updateProduct = request.ToUpdateProductCommand();
            var ret = await _dispatcher.DispatchAsync(updateProduct);
            return ret.ToOkObjectResponse<ProductModel>("Product has been successfully updated.");
        }
    }
}
