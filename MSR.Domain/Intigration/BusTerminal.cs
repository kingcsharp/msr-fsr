using MSR.Domain.Intigration.Abstractions;
using System.Threading.Tasks;

namespace MSR.Domain.Intigration
{
    public class BusTerminal : IProcessSQSMessages
    {
        public Task ProcessMessage(string messageData)
        {
            return Task.CompletedTask;
        }
    }
}
