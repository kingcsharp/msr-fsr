using System.Threading.Tasks;

namespace MSR.Answer.Processor.SQSServices.Abstractions
{
    public interface ISqsConsumerService
    {
        Task StartConsuming();
        void StopConsuming();
    }
}