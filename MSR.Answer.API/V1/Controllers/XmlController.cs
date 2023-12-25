using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Models;
using MSR.Domain.Abstractions.AWS;
using MSR.Infrastructure.Resources.AWS;
using NSwag.Annotations;
using System.Threading;
using MSR.Infrastructure.Resources.Services.Xml;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MSR.Infrastructure.Resources.Services.Aws3Service;
using System.Linq;
using MSR.Domain.Models.Config;
using AutoMapper.Configuration;
using Rollbar.DTOs;
using System.IO;
using ClosedXML;
using MSR.Domain.Commands;
using MSR.Domain.Commanding;
using Microsoft.AspNetCore.DataProtection;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class XmlController : BaseApiController
    {
        private readonly IMapper _mapper;
        private ICommandDispatcher _dispatcher;

        public XmlController(IUnitOfWork unitOfWork, IMapper mapper, ICommandDispatcher dispatcher)
        {
            _mapper = mapper;
            _dispatcher = dispatcher;
        }
        [HttpPatch("Transmit")]
        [SwaggerResponse(typeof(AuditActionResult<string>))]
        public async Task<IActionResult> AddMessage([FromBody, Required] XmlTransmissionRequest request)
        {
            var command = request.ToTransmitXmlFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<string>("The file has been sent successfully.");
            
        }

        [HttpGet("DownloadFile/{Id}")]
        [SwaggerResponse(typeof(AuditActionResult<FileModel>))]
        public async Task<IActionResult> DownloadAsync([FromRoute, Required] DownloadXmlRequest request)
        {
            var command = request.ToDownloadXmlFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<FileModel>();
        }
    }
}

