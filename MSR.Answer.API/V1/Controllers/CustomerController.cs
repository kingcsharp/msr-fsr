using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Net;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class CustomerController : BaseApiController
    {
        private readonly ICommandDispatcher _dispatcher;

        public CustomerController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        /*
        [HttpGet("{id}"), HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.NoContent,typeof(AuditActionResult))]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var getCustomer = new GetCustomer() { Id = id };
            var ret = await _dispatcher.DispatchAsync(getCustomer);
            return ret.ToOkObjectResponse<Customer>();
        }
        */

        [HttpGet, HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> GetCustomers([FromQuery]GetMultipleCustomersRequest filters)
        {
            var getCustomers = filters.ToGetMultipleCustomersCommand();
            var ret = await _dispatcher.DispatchAsync(getCustomers);
            return ret.ToOkObjectResponse<IEnumerable<Customer>>();
        }

        [HttpPost, HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> CreateCustomer([FromBody, Required]CreateCustomerRequest request)
        {
            var createCustomer = request.ToCreateCustomerCommand();

            var ret = await _dispatcher.DispatchAsync(createCustomer);
            return ret.ToCreatedResponse<Customer>();
        }

        [HttpPatch, HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> UpdateCustomer([FromBody, Required]UpdateCustomerRequest request)
        {
            var updateCustomer = request.ToUpdateCustomerCommand();
            var ret = await _dispatcher.DispatchAsync(updateCustomer);
            return ret.ToNoContentResponse();
        }

        [HttpDelete("{id}"), HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DeactivateCustomer([FromRoute] int id)
        {
            var disableCustomer = new DeactivateCustomer() { CustomerId = id };
            var ret = await _dispatcher.DispatchAsync(disableCustomer);
            return ret.ToNoContentResponse();
        }
    }
}
