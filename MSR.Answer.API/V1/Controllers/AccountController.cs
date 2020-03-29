using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.V1.Attributes;
using MSR.Answer.API.V1.Models;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("v1")]
    [VersionedRoute("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Login([FromBody, Required]SystemLoginRequest request)
        {

        }

    }
}