using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ICommandResponse> DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
        Task<ICommandResponse<TResponse>> DispatchAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    }
}
