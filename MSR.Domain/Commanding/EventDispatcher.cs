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
            try
            {
                var handler = _provider.GetService<IEventHandler<T>>();

                await handler.HandleAsync((T)@event, new CancellationToken());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
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
