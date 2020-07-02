using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class RoleAppService :
        ICommandHandler<CreateMenuRoleMap>,
        ICommandHandler<UpdateMenuRoleMap>,
        ICommandHandler<RemoveMenuRoleMap>,
        ICommandHandler<GetRoles>
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

        public async Task<ICommandResponse> HandleAsync(UpdateMenuRoleMap command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.UpdateMenuRoleMapAsync(command);
            return new CommandResponse<bool>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(RemoveMenuRoleMap command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.RemoveMenuRoleMap(command.Id);
            return new CommandResponse<bool>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetRoles command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.GetRolesMapAsync(command);
            return new CommandResponse<ICollection<Domain.Models.Role>>(ret);
        }
    }
}
