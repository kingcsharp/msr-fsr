using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.SQSEventing.Abstractions;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace MSR.Domain.Commanding
{
    public class EventDispatcher<T> : EventDispatcher where T : class, IEvent
    {
        public EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger) : base(serviceProvider, logger)
        { }

        public override async Task Dispatch(IEvent @event, CancellationToken cancellationToken)
        {
            using var scope = _provider.CreateScope();
            var handler = scope.ServiceProvider.GetService<IEventHandler<T>>();

            await handler.HandleAsync((T)@event, cancellationToken);
        }
    }

    public abstract class EventDispatcher
    {
        protected readonly ILogger _logger;
        protected readonly IServiceProvider _provider;

        protected EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger)
        {
            _provider = serviceProvider;
            _logger = logger;
        }

        public abstract Task Dispatch(IEvent @event, CancellationToken cancellationToken);
    }
}
