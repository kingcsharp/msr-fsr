using MSR.Domain.Commanding.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.SQSEventing.Abstractions
{
    public interface IEventHandler
    { }

    public interface IEventHandler<in TEvent>: IEventHandler where TEvent: IEvent
    {
        Task HandleAsync(TEvent handledEvent, CancellationToken cancellationToken = default);
    }
}
