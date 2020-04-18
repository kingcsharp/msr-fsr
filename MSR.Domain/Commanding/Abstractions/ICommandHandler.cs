using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandHandler { }

    public interface ICommandHandler<in TCommand>: ICommandHandler where TCommand: ICommand
    {
        Task<ICommandResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }

}
