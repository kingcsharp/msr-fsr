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

        public override Task Dispatch(IEvent integrationEvent)
        {
            var handler = _provider.GetService<IEventHandler<T>>();

            handler.HandleAsync((T)integrationEvent, new CancellationToken());

            return Task.CompletedTask;
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
