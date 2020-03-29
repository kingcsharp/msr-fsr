using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandDispatcher
    {
        Task<ICommandResponse<T>> DispatchAsync<T>(ICommand<T> command, CancellationToken cancellationToken = default);
        Task<ICommandResponse> DispatchAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    }
}
