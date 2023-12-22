using System.Threading.Tasks;

namespace MSR.Answer.Processor.SQSServices.Abstractions
{
    public interface ISqsXMLConsumerService
    {
        Task StartConsuming();
        void StopConsuming();
    }
}