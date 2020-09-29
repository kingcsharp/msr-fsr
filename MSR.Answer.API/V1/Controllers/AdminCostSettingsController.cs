using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Models;
using MSR.Domain.Commands;
using MSR.Domain.Commanding.Abstractions;
using NSwag.Annotations;
using System.Threading.Tasks;
using MSR.Answer.API.Filters;
using MSR.Domain.Commanding.Enums;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    /// AdminCostSettingsController
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class AdminCostSettingsController : BaseApiController
    {
        private const string privilegeApiName = "AdminCostSettings";
        private readonly ICommandDispatcher _dispatcher;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dispatcher"></param>
        public AdminCostSettingsController(ICommandDispatcher dispatcher)
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
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<AdminCostSettingsModel>))]
        public async Task<IActionResult> GetAdminCostSettings()
        {
            var ret = await _dispatcher.DispatchAsync(new GetAdminCostSettings());
            return ret.ToOkObjectResponse<AdminCostSettingsModel>();
        }

        /// <summary>
        /// Update admin cost setting
        /// </summary>
        /// <description>
        /// Update admin cost setting.  Note that this is
        /// normally a table with one single row.
        /// </description>
        /// <returns></returns>
        [HttpPatch, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<AdminCostSettingsModel>))]
        public async Task<IActionResult> UpdateAdminCostSettings([FromBody, Required] UpdateAdminCostSettingRequest request)
        {
            var updateAdminCostSettings = request.ToUpdateAdminCostSettingsCommand();
            var ret = await _dispatcher.DispatchAsync(updateAdminCostSettings);
            return ret.ToOkObjectResponse<AdminCostSettingsModel>("AdminCostSetting has been successfully updated.");
        }
    }
}
