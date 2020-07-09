using Microsoft.Extensions.Logging;
using MSR.Domain.Commanding.Abstractions;
using System;
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

        public async Task<ICommandResponse> DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class,ICommand
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            try
            {
                var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>));

                if (handler == null) {
                    throw new Exception($"No service for {command}");
                }

                return await (handler as ICommandHandler<TCommand>).HandleAsync(command, cancellationToken);
            }
            catch(Exception ex)
            {
                return CommandResponse.Error(ex);
            }
        }

        public Task<ICommandResponse<TResponse>> DispatchAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
