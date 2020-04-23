using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class MenuAppService :
        ICommandHandler<GetMenu>
    {
        public async Task<ICommandResponse> HandleAsync(GetMenu command, CancellationToken cancellationToken = default)
        {
            return new CommandResponse();
        }
    }
}
