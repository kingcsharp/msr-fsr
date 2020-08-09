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

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class QuotesProductsController : BaseApiController
    {
        private readonly ICommandDispatcher _dispatcher;

        public QuotesProductsController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, HasPrivilegeApi("QuotesProducts", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<IEnumerable<QuotesProductsModel>>))]
        public async Task<IActionResult> Get([FromQuery] GetQuotesProductsRequest filters)
        {
            var getQuotesProducts = filters.ToGetQuotesProductsRequestCommand();
            var ret = await _dispatcher.DispatchAsync(getQuotesProducts);
            return ret.ToOkObjectResponse<IEnumerable<QuotesProductsModel>>();
        }
    }
}
