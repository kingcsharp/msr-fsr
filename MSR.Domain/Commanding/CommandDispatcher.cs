using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MSR.Domain.Commanding.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Commanding
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public const string EXECUTION_TIMEOUT_EXPIRED = "EXECUTION TIMEOUT EXPIRED";
        public const string TIMEOUT_EXPIRED = "TIMEOUT EXPIRED";
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public CommandDispatcher(IServiceProvider serviceProvider, ILogger<CommandDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<ICommandResponse> DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));

                if (handler == null)
                {
                    throw new Exception($"No service for {command}");
                }

                return await (handler as ICommandHandler<TCommand>).HandleAsync(command, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().StartsWith(EXECUTION_TIMEOUT_EXPIRED) ||
                    ex.Message.ToUpper().StartsWith(TIMEOUT_EXPIRED) ||
                    (ex.InnerException != null &&
                     ex.InnerException.Message
                       .ToUpper().StartsWith(EXECUTION_TIMEOUT_EXPIRED)))
                {
                    return CommandResponse.Retry(ex);
                }
                else
                {
                    return CommandResponse.Error(ex);
                }
            }
        }

        public Task<ICommandResponse<TResponse>> DispatchAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
