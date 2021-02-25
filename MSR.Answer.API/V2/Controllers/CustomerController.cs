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
        public async Task<IActionResult> GetCustomers(int? id, string name, string address, string phone, int? primaryContactUserId, int? secondaryContactUserId, int? locationId, bool? isActive, [FromQuery] ApiPagingModel<CustomerModel> paging)
        {
            var customers = await _customerViewService.GetCustomers(paging, id,name, address, phone, primaryContactUserId, secondaryContactUserId, locationId, isActive);

            return GenerateOkViewResponse<ApiPagingModel<CustomerModel>>(customers);
        }
    }
}
