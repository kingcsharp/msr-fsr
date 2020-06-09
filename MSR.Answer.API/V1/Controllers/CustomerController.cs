using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MSR.Answer.API.V1.Controllers
{
    public class CustomerController : BaseApiController
    {
        private readonly ICommandDispatcher _dispatcher;

        public CustomerController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var getCustomer = new GetCustomer() { Id = id };
            var ret = await _dispatcher.DispatchAsync(getCustomer);
            return ret.ToOkObjectResponse<Customer>();
        }


        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> GetCustomers([FromUri]GetMultipleCustomersRequest filters)
        {
            var getCustomers = filters.ToGetMultipleCustomersCommand();
            var ret = await _dispatcher.DispatchAsync(getCustomers);
            return ret.ToOkObjectResponse<Customer>();
        }

        [HttpPost]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> CreateCustomer([FromBody, Required]CreateCustomerRequest request)
        {

        }


    }
}
