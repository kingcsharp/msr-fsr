using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Attributes;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

    }
}