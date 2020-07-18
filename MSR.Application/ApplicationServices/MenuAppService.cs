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
    public class MenuAppService :
        ICommandHandler<GetMenu>, 
        ICommandHandler<CreateMenuRoleMap>,
        ICommandHandler<UpdateMenuRoleMap>,
        ICommandHandler<RemoveMenuRoleMap>
    {

        private readonly IRoleService _roleService;
        private readonly IMenuService _menuService;

        public MenuAppService(IRoleService roleService, IMenuService menuService)
        {
            _roleService = roleService;
            _menuService = menuService;
        }

        public async Task<ICommandResponse> HandleAsync(GetMenu command, CancellationToken cancellationToken = default)
        {
            var ret = await _menuService.GetMenuAsync(command);
            return new CommandResponse<IEnumerable<MenuItem>>(ret);
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
    }
}