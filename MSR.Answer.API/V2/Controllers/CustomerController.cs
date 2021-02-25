using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Answer.API.Attributes;
using MSR.Domain.Models;
using MSR.Answer.API.V2.Models;
using MSR.Domain.Models.Paging;
using MSR.Application.Abstractions;

namespace MSR.Answer.API.V2.Controllers
{
    [ApiVersion("2.0")]
    [VersionedRoute("[controller]")]
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerViewService _customerViewService;

        public CustomerController(ICustomerViewService viewService)
        {
            _customerViewService = viewService;
        }

        [HttpGet]
        [SwaggerResponse(typeof(ApiPagingModel<CustomerModel>))]
        public async Task<IActionResult> GetCustomers([FromQuery] GetMultipleCustomersRequest filters, [FromQuery] ApiPagingModel<CustomerModel> paging)
        {
            var customers = await _customerViewService.GetCustomers(paging, filters.Id,filters.Name, filters.Address, filters.Phone, filters.PrimaryContactUserId, filters.SecondaryContactUserId, filters.LocationId, filters.IsActive);

            return GenerateOkViewResponse<ApiPagingModel<CustomerModel>>(customers);
        }
    }
}
