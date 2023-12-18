using System.Threading;
using System.Threading.Tasks;
using MSR.Domain.Commands;

namespace MSR.Domain.Abstractions
{
    public interface IAnswerRestClient
    {
        Task<string> PostWorkOrderAsync(CreateWorkOrder command, CancellationToken cancellationToken = default);
        Task<string> PostWorkOrderTransmitXmlAsync(int WorkOrderId, CancellationToken cancellationToken = default);
    }
}