using MSR.Domain.SQSEventing.Models;
using System.Threading.Tasks;

namespace MSR.Domain.SQSEventing.Abstractions
{
    public interface ISendSQSMessages
    {
        public Task SendMessage(MessageEnvelope data);
    }
}
