using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IXmlService
    {
        Task<XmlTransmissionLogModel> GetConfigItem(int id, CancellationToken cancellationToken = default);
    }
}
