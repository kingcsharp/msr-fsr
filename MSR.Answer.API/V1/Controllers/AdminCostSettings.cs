using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Models;
using MSR.Domain.Commands;
using MSR.Domain.Commanding.Abstractions;
using NSwag.Annotations;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    /// AdminCostSettings
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class AdminCostSettings : BaseApiController
    {
        private readonly ICommandDispatcher _dispatcher;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dispatcher"></param>
        public AdminCostSettings(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get all admin cost settings
        /// </summary>
        /// <description>
        /// Get all admin cost settings.  Note that this is
        /// normally a table with one single row.
        /// </description>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<AdminCostSettingsModel>))]
        public async Task<IActionResult> GetAdminCostSettings()
        {
            var ret = await _dispatcher.DispatchAsync(new GetAdminCostSettingsCommand());
            return ret.ToOkObjectResponse<AdminCostSettingsModel>();
        }

    }
}
