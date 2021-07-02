using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NSwag.Annotations;
using MSR.Answer.API.Attributes;
using MSR.Domain.Views;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using MSR.Domain.Commands;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class PurchaseOrderController : BaseApiController
    {
        private const string PrivilegeApiName = "PurchaseOrder";
        private readonly ICommandDispatcher _dispatcher;

        public PurchaseOrderController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<PurchaseOrderView>>))]
        public async Task<IActionResult> GetPurchaseOrders([FromQuery] GetPurchaseOrderRequest filters)
        {
            var getPurchaseOrder = filters.ToGetPurchaseOrderCommand();
            var ret = await _dispatcher.DispatchAsync(getPurchaseOrder);
            return ret.ToOkObjectResponse<IEnumerable<PurchaseOrderView>>();
        }

        [HttpPost, HasPrivilegeApi(PrivilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<PurchaseOrderView>))]
        public async Task<IActionResult> CreatePurchaseOrder([FromBody]CreatePurchaseOrderRequest request)
        {
            var command = request.ToCreatePurchaseOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<PurchaseOrderView>(
                DetermineResponseMessage<PurchaseOrderView>(ret, "Create", "Purchase Order")
            );
        }

        /// <summary>
        /// UpdatePurchaseOrder
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch, HasPrivilegeApi(PrivilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<PurchaseOrderView>))]
        public async Task<IActionResult> UpdatePurchaseOrder([FromBody] UpdatePurchaseOrderRequest request)
        {
            var command = request.ToUpdatePurchaseOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<PurchaseOrderView>(
                DetermineResponseMessage<PurchaseOrderView>(ret, "Update", "Purchase Order")
            );
        }
        
        /// <summary>
        /// DeletePurchaseOrder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), HasPrivilegeApi(PrivilegeApiName, EnumPrivilege.CanDelete)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult))]
        public async Task<IActionResult> DeletePurchaseOrder([FromRoute, Required]int id)
        {
            var command = new DeletePurchaseOrder() { Id = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<PurchaseOrderView>(
                DetermineResponseMessage<PurchaseOrderView>(ret, "Delete", "Purchase Order")
            );
        }

        [HttpGet("View")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<PurchaseOrderDBView>>))]
        public async Task<IActionResult> GetPurchaseOrdersDBView([FromQuery] GetPurchaseOrderDBRequest request)
        {
            var getPurchaseOrder = request.ToGetPurchaseOrderDBViewCommand();
            var ret = await _dispatcher.DispatchAsync(getPurchaseOrder);
            return ret.ToOkObjectResponse<IEnumerable<PurchaseOrderDBView>>();
        }
    }
}
