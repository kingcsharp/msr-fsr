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
using Microsoft.AspNetCore.SignalR;
using MSR.Application.Hubs;

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
        private readonly IHubContext<MessageHub> _messageHub;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="messageHub" />
        /// 
        public ProductController(ICommandDispatcher dispatcher, IHubContext<MessageHub> messageHub)
        {
            _dispatcher = dispatcher;
            _messageHub = messageHub;
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
            await SendApprovalNotificationHubMessage(EnumApprovalTables.ProductApproval, _messageHub);
            return ret.ToOkObjectResponse<ProductModel>(await DetermineResponseMessage(ret, "Create"));
        }

        /// <summary>
        /// GetProduct
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet, HasPrivilegeApi(PrivilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<ProductModel>>))]
        public async Task<IActionResult> GetProduct([FromQuery] GetProductRequest filters)
        {
            var getProduct = filters.ToGetProductCommand();
            var ret = await _dispatcher.DispatchAsync(getProduct);
            return ret.ToOkObjectResponse<IEnumerable<ProductModel>>();
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
            var updateProduct = request.ToUpdateProductCommand();
            var ret = await _dispatcher.DispatchAsync(updateProduct);
            await SendApprovalNotificationHubMessage(EnumApprovalTables.ProductApproval, _messageHub);
            return ret.ToOkObjectResponse<ProductModel>(await DetermineResponseMessage(ret, "Update"));
        }

        private async Task<string> DetermineResponseMessage(ICommandResponse commandResponse, string action)
        {
            var product = commandResponse.ToEntity<ProductModel>();
            var response = $"Product {action} Successful";

            if (!string.IsNullOrWhiteSpace(product.ApprovalStatus))
            {
                await SendApprovalNotificationHubMessage(EnumApprovalTables.LocationApproval, _messageHub);
                response = $"Product {action} Pending Approval";
            }

            return response;
        }
    }
}
