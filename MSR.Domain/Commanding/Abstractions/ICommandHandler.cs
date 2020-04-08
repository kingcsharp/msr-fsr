using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandHandler<in TCommand>: ICommandHandler where TCommand: ICommand
    {
        Task<ICommandResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler { }
}
