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
using MSR.Domain.Models;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models.Paging;
using MSR.Application.Abstractions;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class CustomerController : BaseApiController
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly ICustomerViewService _customerViewService;

        public CustomerController(ICommandDispatcher dispatcher, ICustomerViewService viewService)
        {
            _dispatcher = dispatcher;
            _customerViewService = viewService;
        }

        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<IEnumerable<CustomerModel>>))]
        public async Task<IActionResult> GetCustomers([FromQuery] GetMultipleCustomersRequest filters)
        {
            //return Ok();
            var customers = await _customerViewService.GetCustomers(filters.Skip, filters.Take,filters.Id,filters.Name,filters.Address, filters.Phone, filters.PrimaryContactUserId, filters.SecondaryContactUserId, filters.LocationId, filters.IsActive);
            return GenerateOkViewResponse<ICollection<CustomerModel>>(customers.data, customers.totalRows);
            
        }

        [HttpPost, HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<CustomerModel>))]
        public async Task<IActionResult> CreateCustomer([FromBody, Required] CreateCustomerRequest request)
        {
            var createCustomer = request.ToCreateCustomerCommand();

            var ret = await _dispatcher.DispatchAsync(createCustomer);
            return ret.ToOkObjectResponse<CustomerModel>(DetermineResponseMessage(ret, "Creation"));
        }

        [HttpPatch, HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> UpdateCustomer([FromBody, Required] UpdateCustomerRequest request)
        {
            var updateCustomer = request.ToUpdateCustomerCommand();
            var ret = await _dispatcher.DispatchAsync(updateCustomer);
            return ret.ToOkObjectResponse(DetermineResponseMessage(ret, "Update"));
        }

        [HttpDelete("{id}"), HasPrivilegeApi("CustomersDepartments", EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DeactivateCustomer([FromRoute] int id)
        {
            var disableCustomer = new DeactivateCustomer() { CustomerId = id };
            var ret = await _dispatcher.DispatchAsync(disableCustomer);
            return ret.ToOkObjectResponse(DetermineResponseMessage(ret, "Deactivate"));
        }


        //TODO: Refactor to Generic
        private string DetermineResponseMessage(ICommandResponse commandResponse, string action)
        {
            var customer = commandResponse.ToEntity<CustomerModel>();
            var response = $"Customer {action} Successful";

            if (!string.IsNullOrWhiteSpace(customer.Status))
            {

                response = $"Customer {action} Pending Approval";
            }

            return response;
        }
    }
}
