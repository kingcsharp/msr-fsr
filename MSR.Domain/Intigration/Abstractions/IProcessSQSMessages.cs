using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Intigration.Abstractions
{
    public interface IProcessSQSMessages
    {
        public Task ProcessMessage(string messageData);
    }
}
