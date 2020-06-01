using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class RoleAppService :
        ICommandHandler<CreateMenuRoleMap>,
        ICommandHandler<UpdateMenuRoleMap>
    {
        private readonly IRoleService _roleService;

        public RoleAppService(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<ICommandResponse> HandleAsync(CreateMenuRoleMap command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.CreateMenuRoleMapAsync(command);

            return new CommandResponse<int>(ret);
        }

        public Task<ICommandResponse> HandleAsync(UpdateMenuRoleMap command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.
        }
    }
}
