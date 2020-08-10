using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Intigration.Abstractions
{
    public interface IIntegrationEventHandler
    { }

    public interface IIntegrationEventHandler<in TIntegrationEvent>: IIntegrationEventHandler where TIntegrationEvent: IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
    }
}
