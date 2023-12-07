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
        [SwaggerResponse(typeof(AuditActionResult<XmlTransmissionLogModel>))]
        public async Task<IActionResult> AddMessage([FromBody, Required] XmlTransmissionRequest request)
        {
            var command = request.ToTransmitXmlFileCommand();
            var ret = await _dispatcher.DispatchAsync(command) as CommandResponse<XmlTransmissionLogModel>;
            if (ret == null || ret.Data == null)
            {
                return StatusCode(500, "Xml file doesn't exist in AWS s3");
            }
            else
            {
                return ret.ToOkObjectResponse<XmlTransmissionLogModel>("File transmitted successfully");
            }
            
        }

        [HttpGet("DownloadFile/{Id}"), SwaggerResponse(typeof(FileStreamResult))]
        public async Task<FileStreamResult> DownlodFile([FromRoute, Required] DownloadXmlRequest request)
        {
            var command = request.ToDownloadXmlFileCommand();
            var ret = await _dispatcher.DispatchAsync(command) as CommandResponse<XmlDownloadFileModel>;
            return File(ret.Data.FileContent, "application/octet-stream", ret.Data.Name);
        }
    }
}

