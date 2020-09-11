using System;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using MSR.Application.Hubs;
using MSR.Domain.Models;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class CustomerController : BaseApiController
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly IHubContext<MessageHub> _messageHub;

        public CustomerController(ICommandDispatcher dispatcher, IHubContext<MessageHub> messageHub)
        {
            _dispatcher = dispatcher;
            _messageHub = messageHub;
        }

        [HttpGet, HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<IEnumerable<Customer>>))]
        public async Task<IActionResult> GetCustomers([FromQuery] GetMultipleCustomersRequest filters)
        {
            var getCustomers = filters.ToGetMultipleCustomersCommand();
            var ret = await _dispatcher.DispatchAsync(getCustomers);
            return ret.ToOkObjectResponse<IEnumerable<Customer>>();
        }

        [HttpPost, HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<Customer>))]
        public async Task<IActionResult> CreateCustomer([FromBody, Required] CreateCustomerRequest request)
        {
            var createCustomer = request.ToCreateCustomerCommand();

            var ret = await _dispatcher.DispatchAsync(createCustomer);
            return ret.ToOkObjectResponse<Customer>(await DetermineResponseMessage(ret, "Creation"));
        }

        [HttpPatch, HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> UpdateCustomer([FromBody, Required] UpdateCustomerRequest request)
        {
            var updateCustomer = request.ToUpdateCustomerCommand();
            var ret = await _dispatcher.DispatchAsync(updateCustomer);
            return ret.ToOkObjectResponse(await DetermineResponseMessage(ret, "Update"));
        }

        [HttpDelete("{id}"), HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DeactivateCustomer([FromRoute] int id)
        {
            var disableCustomer = new DeactivateCustomer() { CustomerId = id };
            var ret = await _dispatcher.DispatchAsync(disableCustomer);
            return ret.ToOkObjectResponse(await DetermineResponseMessage(ret, "Deactivate"));
        }


        //TODO: Refactor to Generic
        private async Task<string> DetermineResponseMessage(ICommandResponse commandResponse, string action)
        {
            var customer = commandResponse.ToEntity<Customer>();
            var response = $"Customer {action} Successful";

            if (!string.IsNullOrWhiteSpace(customer.Status))
            {
                await SendApprovalNotificationHubMessage(EnumApprovalTables.CustomerApproval, _messageHub);

                response = $"Customer {action} Pending Approval";
            }

            return response;
        }
    }
}
