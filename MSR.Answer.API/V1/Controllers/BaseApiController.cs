using Microsoft.AspNetCore.Mvc;
using MSR.Domain.Helpers;

namespace MSR.Answer.API.V1.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        public int UserId => DelegateHandler.GetCurrentUserId();
    }
}
