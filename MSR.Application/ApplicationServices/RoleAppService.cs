using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class RoleAppService :
        ICommandHandler<GetRoles>,
        ICommandHandler<GetRolesUsers>,
        ICommandHandler<CreateRole>,
        ICommandHandler<UpdateRole>,
        ICommandHandler<DeleteRole>
    {
        private readonly IRoleService _roleService;

        public RoleAppService(IRoleService roleService)
        {
            _roleService = roleService;
        }


        public async Task<ICommandResponse> HandleAsync(GetRoles command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.GetRolesMapAsync(command);
            var totalRows = await _roleService.GetTotalRoleRows(command);
            return new PagingCommandResponse<ICollection<Role>>(ret, totalRows, command.Term, command.PageNumber, command.PageSize, command.SortAscending);
        }

        public async Task<ICommandResponse> HandleAsync(GetRolesUsers command, CancellationToken cancellationToken = default)
        {
            var ret = await _roleService.GetRolesAssignedUsers(command);
            return new CommandResponse<ICollection<RolesUsersView>>(ret);
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

        public async Task<ICommandResponse> HandleAsync(DeleteRole command, CancellationToken cancellationToken = default)
        {
            await _roleService.DeleteRoleAsync(command);
            return CommandResponse.SuccessCommand;
        }
    }
}
