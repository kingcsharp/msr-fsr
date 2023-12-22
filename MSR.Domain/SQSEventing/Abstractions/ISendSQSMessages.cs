using MSR.Domain.SQSEventing.Models;
using System.Threading.Tasks;

namespace MSR.Domain.SQSEventing.Abstractions
{
    public interface ISendSQSMessages
    {
        public Task<string> SendMessage(MessageEnvelope data);
        public Task<string> SendXMLMessage(MessageEnvelope data);
    }
}
