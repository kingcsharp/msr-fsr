using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using MSR.Domain.Models;
using System.Net;
using Microsoft.AspNetCore.SignalR;
using MSR.Domain.Commands;
using MSR.Domain.Abstractions.Services;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ProductController : BaseApiController
    {
        private const string PrivilegeApiName = "QuotesProducts";
        private readonly ICommandDispatcher _dispatcher;

        public ProductController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Creates a Product based on the <paramref name="product"/> request.
        /// </summary>
        /// <param name="product"></param>
        /// <permission>CanCreate Privilege required</permission>
        /// <returns>Product DTO</returns>
        [HttpPost]
        [HasPrivilegeApi(PrivilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<ProductModel>))]
        public async Task<IActionResult> CreateProduct([FromBody, Required] CreateProductRequest product)
        {
            var command = product.ToCreateProductCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProductModel>(
                DetermineResponseMessage<ProductModel>(ret, "Create", "Product")
            );
        }

        /// <summary>
        /// GetProduct
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<ProductModel>>))]
        public async Task<IActionResult> GetProduct([FromQuery] GetProductRequest filters)
        {
            var getProduct = filters.ToGetProductCommand();
            var ret = await _dispatcher.DispatchAsync(getProduct);
            return ret.ToOkObjectResponse<IEnumerable<ProductModel>>();
        }

        /// <summary>
        /// GetPurchaseOrderProduct
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("GetPurchaseOrderProduct")]
        [HasPrivilegeApi(PrivilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<PurchaseOrderProductModel>>))]
        public async Task<IActionResult> GetPurchaseOrderProduct([FromQuery] GetProductRequest filters)
        {
            var getPurchaseOrderProduct = filters.ToGetPurchaseOrderProductCommand();
            var ret = await _dispatcher.DispatchAsync(getPurchaseOrderProduct);
            return ret.ToOkObjectResponse<IEnumerable<PurchaseOrderProductModel>>();
        }

        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch, HasPrivilegeApi(PrivilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<ProductModel>))]
        public async Task<IActionResult> UpdateProduct([FromBody, Required] UpdateProductRequest request)
        {
            UpdateProduct updateProduct = request.ToUpdateProductCommand();
            var ret = await _dispatcher.DispatchAsync(updateProduct);
            return ret.ToOkObjectResponse<ProductModel>(
                DetermineResponseMessage<ProductModel>(ret, "Update", "Product")
            );
        }
    }
}
