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

        public async Task<XmlDownloadFileModel> DownloadFile(int Id)
        {
            var XmlTransmissionLog = await _unitOfWork.XmlTransmissionLogs.Query()
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (XmlTransmissionLog == null)
            {
                return null;
            }
            else if (XmlTransmissionLog.XmlLink == "")
            {
                return null;
            }
            var fileName = $"{XmlTransmissionLog.XmlLink.Split("/").Last().Split(".xml").First()}.xml";

            var sftpInfo = _config.GetSection(nameof(TransmissionInformation)).Get<TransmissionInformation>();
            Stream fileStream = await _fileDownloader.DownloadFile(fileName, sftpInfo.S3Bucket);
            XmlDownloadFileModel xmlDownloadFile = new XmlDownloadFileModel()
            {
                Name = fileName,
                FileContent = fileStream
            };


            return xmlDownloadFile;
        }
    }
}

