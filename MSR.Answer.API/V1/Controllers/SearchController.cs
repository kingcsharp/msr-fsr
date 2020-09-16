using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Views;
using NSwag.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class SearchController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public SearchController(ILogger<SearchController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet(), SwaggerResponse(typeof(AuditActionResult<IEnumerable<SearchView>>))]
        public async Task<IActionResult> Search([FromQuery]GetSearchRequest request)
        {
            var response = new AuditActionResult<IEnumerable<SearchView>>()
            {
                SuccessMessage = "Search Successfully returned 2 results",
                Object = new List<SearchView>() {
                    new SearchView()
                    {
                        ItemId = 1,
                        ItemName = "WorkOrder 123",
                        Description = "WorkOrder 123",
                        ItemType = "WorkOrder",
                        LastUpdatedBy = "James Bridgeford",
                        LastUpdatedOn = DateTime.UtcNow
                    },
                    new SearchView()
                    {
                        ItemId = 1,
                        ItemName = "Part 123",
                        Description = "Part 123",
                        ItemType = "Part",
                        LastUpdatedBy = "James Bridgeford",
                        LastUpdatedOn = DateTime.UtcNow
                    }
                }.AsEnumerable()
            };

            return new OkObjectResult(response);
        }
    }
}
