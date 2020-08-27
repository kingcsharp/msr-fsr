using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Role = MSR.Domain.Models.Role;

namespace MSR.Application.ApplicationServices
{
    public class RoleAppService :
        ICommandHandler<GetRoles>,
        ICommandHandler<CreateRole>,
        ICommandHandler<UpdateRole>
    {
        private readonly IRoleService _roleService;

        public RoleAppService(IRoleService roleService)
        {
            _roleService = roleService;
        }


        public async Task<ICommandResponse> HandleAsync(GetRoles command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.GetRolesMapAsync(command);
            return new CommandResponse<ICollection<Role>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateRole command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.CreateRoleAsync(command);
            return new CommandResponse<Role>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(UpdateRole command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.UpdateRoleAsync(command);
            return new CommandResponse<Role>(ret);
        }
    }
}
