using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NSwag.Annotations;
using MSR.Answer.API.Attributes;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using MSR.Application.Hubs;
using MSR.Domain.Commands;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class PurchaseOrderController : BaseApiController
    {
        private const string PrivilegeApiName = "PurchaseOrder";
        private readonly ICommandDispatcher _dispatcher;
        private readonly IHubContext<MessageHub> _messageHub;

        public PurchaseOrderController(ICommandDispatcher dispatcher, IHubContext<MessageHub> messageHub)
        {
            _dispatcher = dispatcher;
            _messageHub = messageHub;
        }

        [HttpGet, HasPrivilegeApi(PrivilegeApiName, EnumPrivilege.CanRead)]
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
            return ret.ToOkObjectResponse<PurchaseOrderView>(await DetermineResponseMessage(ret, "Create"));
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
            return ret.ToOkObjectResponse<PurchaseOrderView>(await DetermineResponseMessage(ret, "Update"));
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
            return ret.ToOkObjectResponse<PurchaseOrderView>(await DetermineResponseMessage(ret, "Delete"));
        }

        private async Task<string> DetermineResponseMessage(ICommandResponse commandResponse, string action)
        {
            var poView = commandResponse.ToEntity<PurchaseOrderView>();
            var response = $"PurchaseOrder {action} Pending Approval";

            if (string.IsNullOrWhiteSpace(poView.Status))
            {
                response = $"PurchaseOrder {action} Successfull";
                poView.Status = "Approved";
            }
            await SendApprovalNotificationHubMessage(EnumApprovalTables.PurchaseOrderApproval, _messageHub);

            return response;
        }
    }
}
