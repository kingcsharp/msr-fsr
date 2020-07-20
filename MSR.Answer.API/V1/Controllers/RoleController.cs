using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MSR.Domain.Models;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Extentions;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class RoleController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public RoleController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        
        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<Role>>))]
        public async Task<IActionResult> GetRoles()
        {
            var ret = await _dispatcher.DispatchAsync(new GetRoles());
            return ret.ToOkObjectResponse<ICollection<Role>>();
        }


        

    }
}