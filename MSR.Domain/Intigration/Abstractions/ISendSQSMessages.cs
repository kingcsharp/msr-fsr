using MSR.Domain.Intigration.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Intigration.Abstractions
{
    public interface ISendSQSMessages
    {
        public Task SendMessage(MessageEnvelope data);
    }
}
