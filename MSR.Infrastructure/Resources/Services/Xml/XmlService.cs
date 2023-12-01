using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Xml
{
    public class XmlService : IXmlService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public XmlService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<XmlService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<XmlTransmissionLogModel> GetConfigItem(int id, CancellationToken cancellationToken = default)
        {
            var xmlTransmissionLog = await _unitOfWork.XmlTransmissionLogs.Query()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (xmlTransmissionLog == null)
            {
                return null;
            }

            var domReport = _mapper.Map<XmlTransmissionLogModel>(xmlTransmissionLog);

            return domReport;
        }
    }
}

