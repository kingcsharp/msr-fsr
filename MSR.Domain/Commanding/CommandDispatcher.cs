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

        public Task<ICommandResponse<T>> DispatchAsync<T>(ICommand<T> command, CancellationToken cancellationToken = default)
        {
            if(command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _serviceProvider.GetService<ICommandHandler<>
        }

        Task<ICommandResponse> ICommandDispatcher.DispatchAsync<T>(T command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
