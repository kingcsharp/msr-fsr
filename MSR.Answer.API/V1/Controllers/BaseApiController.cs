using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSR.Domain.Helpers;

namespace MSR.Answer.API.V1.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        public int UserId => DelegateHandler.GetCurrentUserId();
    }
}
