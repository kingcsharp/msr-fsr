using Microsoft.Extensions.Logging;
using MSR.Domain.Commanding.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Commanding
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public CommandDispatcher(IServiceProvider serviceProvider, ILogger<CommandDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task<ICommandResponse> DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class,ICommand
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) ;

            return (handler as ICommandHandler<TCommand>).HandleAsync(command, cancellationToken);
        }
    }
}
