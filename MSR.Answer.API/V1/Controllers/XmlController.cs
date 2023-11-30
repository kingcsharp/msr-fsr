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

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class XmlController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
       // private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private ICommandDispatcher _dispatcher;
        private IDownloadFiles _fileDownloader;
        //private object _config;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly XmlService _xmlService;

        public XmlController(IUnitOfWork unitOfWork, IMapper mapper, ICommandDispatcher dispatcher, IDownloadFiles fileDownloader, XmlService xmlService, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dispatcher = dispatcher;
            _fileDownloader = fileDownloader;
            _xmlService = xmlService;
            _config = configuration;
        }

        [HttpPatch("Transmit")]
        [SwaggerResponse(typeof(AuditActionResult<XmlTransmissionLogModel>))]
        public async Task<IActionResult> AddMessage([FromBody, Required] XmlTransmissionRequest request)
        {
            var command = request.ToTransmitXmlFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<XmlTransmissionLogModel>("File transmitted successfully");
        }

        [HttpGet("GetConfigItem/{id}"), SwaggerResponse(typeof(AuditActionResult<XmlTransmissionLogModel>))]
        public async Task<IActionResult> GetConfigItem([FromRoute, Required] int id, CancellationToken cancellationToken = default)
        {
            var xmlTransmissionLog = await _unitOfWork.XmlTransmissionLogs.Query()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (xmlTransmissionLog == null)
            {
                // Return 404 Not Found if the XML transmission log with the given id is not found
                return NotFound();
            }

            var xmlTransmissionModel = _mapper.Map<XmlTransmissionLogModel>(xmlTransmissionLog);

            if (xmlTransmissionModel == null)
            {
                // There was an issue mapping the XML transmission log to the model
                return StatusCode(500, "Internal Server Error");
                // You might want to handle this case differently based on your application's requirements
            }

            try
            {
                if (string.IsNullOrEmpty(xmlTransmissionLog.XmlLink))
                {
                    return BadRequest("XmlLink is null or empty.");
                }
                string XMLLink = xmlTransmissionLog.XmlLink;

                var fileName = $"{XMLLink.Split("/").Last().Split(".xml").First()}.xml";

                var sftpInfo = _config.GetSection(nameof(TransmissionInformation))
                    .Get<TransmissionInformation>();

                var fileStream = await _fileDownloader.DownloadFile(fileName, sftpInfo.S3Bucket);

                string contentType = "application/octet-stream";
                string fileDownloadName = "download_file.xml";
                return File(fileStream, contentType, fileDownloadName);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("DownloadFile/{id}"), SwaggerResponse(typeof(AuditActionResult<XmlTransmissionLogModel>))]
        public async Task<IActionResult> DownloadFile([FromRoute] int id)
        {
            // Retrieve the XML transmission log using the XmlService
            var xmlTransmissionLog = await _xmlService.GetConfigItem(id);

            if (xmlTransmissionLog == null)
            {
                // Return 404 Not Found if the XML transmission log with the given id is not found
                return NotFound();
            }

            try
            {
                if (string.IsNullOrEmpty(xmlTransmissionLog.XmlLink))
                {
                    return BadRequest("XmlLink is null or empty.");
                }
                // Download the file using the file downloader
                System.IO.Stream fileStream = await _fileDownloader.DownloadFile(xmlTransmissionLog.XmlLink);

                // Set the response headers for the file download
                string contentType = "application/octet-stream"; // You may need to adjust the content type based on your file type
                string fileDownloadName = "downloaded_file.xml";
                return File(fileStream, contentType, fileDownloadName);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the file download
                // You may want to log the exception or return an appropriate error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}

