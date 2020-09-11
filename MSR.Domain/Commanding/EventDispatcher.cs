using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.SQSEventing.Abstractions;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace MSR.Domain.Commanding
{
    public class EventDispatcher<T> : EventDispatcher where T : class, IEvent
    {
        public EventDispatcher(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        public async override Task Dispatch(IEvent integrationEvent)
        {
            var handler = _provider.GetService<IEventHandler<T>>();

            await handler.HandleAsync((T)integrationEvent, new CancellationToken());
        }
    }

    public abstract class EventDispatcher
    {
        protected readonly IServiceProvider _provider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        public abstract Task Dispatch(IEvent integrationEvent);
    }
}
