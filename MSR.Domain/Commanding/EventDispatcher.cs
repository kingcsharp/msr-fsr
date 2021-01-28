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

        public async override Task Dispatch(IEvent @event)
        {
            //We are no longer going to catch errors here so that it bubbles up to the caller.  This wil make sure that messages that mess up will go to the DL queue

            using var scope = _provider.CreateScope();
            var handler = scope.ServiceProvider.GetService<IEventHandler<T>>();

            await handler.HandleAsync((T)@event, new CancellationToken());
        }
    }

    public abstract class EventDispatcher
    {
        protected ILogger _logger;
        protected readonly IServiceProvider _provider;

        public EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger)
        {
            _provider = serviceProvider;
            _logger = logger;
        }

        public abstract Task Dispatch(IEvent @event);
    }
}
