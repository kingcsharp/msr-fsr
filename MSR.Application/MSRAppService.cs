using MSR.Domain.Commands;
using MSR.Domain.Commanding.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application
{
    public class MsrAppService :
        ICommandHandler<SystemLogin>
    {
        public Task<ICommandResponse> HandleAsync(SystemLogin command, CancellationToken cancellationToken = default)
        {
            /* call into Account Service in Infrastructure 
             * Get back a response - In this instance is the JWT Token
             * Respond with a CommandResponse
             */
        }
    }
}
