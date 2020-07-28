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
        ICommandHandler<GetRoles>
    {
        private readonly IRoleService _roleService;

        public RoleAppService(IRoleService roleService)
        {
            _roleService = roleService;
        }


        public async Task<ICommandResponse> HandleAsync(GetRoles command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.GetRolesMapAsync(command);
            return new CommandResponse<ICollection<Domain.Models.Role>>(ret);
        }
    }
}
