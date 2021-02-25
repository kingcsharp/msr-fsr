using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Helpers;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiController]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
        public int UserId => CurrentUser.GetId();

        public string Controller => this.ControllerContext.RouteData.Values["controller"].ToString();

        protected IActionResult GenerateOkViewResponse<T>(T responseData, int totalRows = 0)
        {
            return Ok(new AuditActionResult<T>()
            {
                TotalNumberOfRecords = totalRows,
                Object = responseData
            });
        }
    }
}
