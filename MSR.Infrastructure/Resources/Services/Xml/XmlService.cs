using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;

namespace MSR.Infrastructure.Resources.Services.Xml
{
    public class XmlService : IXmlService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private IDownloadFiles _fileDownloader;

        public XmlService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<XmlService> logger, Microsoft.Extensions.Configuration.IConfiguration configuration, IDownloadFiles fileDownloader)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = configuration;
            _fileDownloader = fileDownloader;
        }

        public async Task<FileModel> DownloadFile(int Id)
        {
            var XmlTransmissionLog = await _unitOfWork.XmlTransmissionLogs.Query()
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (XmlTransmissionLog == null)
            {
                throw new DomainException("XML trasmission log doesn't exist, you need to finish WorkOrder", DomainError.InternalServerError);
            }
            else if (XmlTransmissionLog.XmlLink == "")
            {
                throw new DomainException("The XML file is not in S3", DomainError.InternalServerError);
            }
            var fileName = $"{XmlTransmissionLog.XmlLink.Split("/").Last().Split(".xml").First()}.xml";

            var XMLS3Bucket = _config.GetSection("XMLS3Bucket").Get<string>();
            Stream fileStream = await _fileDownloader.DownloadFile(fileName, XMLS3Bucket);

            var XmlMemoryStream = new MemoryStream();
            fileStream.CopyTo(XmlMemoryStream);

            var file = new FileModel()
            {
                Name = fileName,
                ContentType = "application/xml",
                FileContents = XmlMemoryStream.ToArray()
            };
            return file;
        }
    }
}

