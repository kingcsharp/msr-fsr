using MSR.Domain.Commands;
using MSR.Domain.Commanding.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application
{
    public class MsrAppService : ICommandHandler<SystemLogin>
    {
        public Task<ICommandResponse> HandleAsync(SystemLogin command, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
