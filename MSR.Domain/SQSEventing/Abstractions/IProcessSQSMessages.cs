using System.Threading.Tasks;

namespace MSR.Domain.SQSEventing.Abstractions
{
    public interface IProcessSQSMessages
    {
        public Task ProcessMessage(string messageData);
    }
}
