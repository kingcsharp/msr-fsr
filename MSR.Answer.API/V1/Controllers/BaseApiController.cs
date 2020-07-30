using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSR.Domain.Helpers;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiController]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
        public int UserId => DelegateHandler.GetCurrentUserId();
        
        public string Controller => this.ControllerContext.RouteData.Values["controller"].ToString();
    }
}
