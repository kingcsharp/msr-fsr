using System;
using System.Collections.Generic;
using System.Text;
using MSR.Domain.Models;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IXmlTransmissionRetryService
    {
        Task<ToastMessage> RetryXmlSubmission(int transmissionId);
    }
}
