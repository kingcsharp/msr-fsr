
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class PartAppService :
        ICommandHandler<GetParts>
    {
        private readonly IPartService _partService;

        public PartAppService(IPartService partService)
        {
            _partService = partService;
        }

        public Task<ICommandResponse> HandleAsync(GetParts command, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
