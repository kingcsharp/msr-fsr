using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Models.Config;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ErrorController : BaseApiController
    {
        private readonly GeneralInformation _generalInfo;
        private readonly ILogger _logger;

        public ErrorController(GeneralInformation generalInfo, ILogger<ErrorController> logger)
        {
            _generalInfo = generalInfo;
            _logger = logger;
        }


        [HttpPost]
        private void LogError(FrontEndError ex)
        {
            var exception = new WebException(ex.Error);
            var m = Regex.Match(_generalInfo.WebsiteURL, @"(http(s?):\/\/)(?<servername>[^\s]*)\/?");
            if (m.Success)
            {
                exception.Data.Add("RootURL", m.Groups["servername"].Value);
                exception.Data.Add("Environment", _generalInfo.Environment);
            }

            _logger.LogError("[Front End Error] {@error}", exception);
        }
    }
}
