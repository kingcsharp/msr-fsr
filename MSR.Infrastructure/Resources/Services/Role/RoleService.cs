using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateMenuRoleMapAsync(CreateMenuRoleMap command)
        {
            var roleMenu = await _unitOfWork.MenuRoles.FirstOrDefaultAsync(false, i => i.MenuItemId == command.MenuId && i.RoleId == command.RoleId);

            if (roleMenu != null) return roleMenu.Id;

            var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == command.RoleId, null);
            var menu = await _unitOfWork.MenuItems.FirstOrDefaultAsync(false, i => i.Id == command.MenuId, null);

            if (role is null || menu is null)
            {
                throw new DomainException("Role or Menu not found", DomainError.BadRequest);
            }

            var menuRole = new MenuRole()
            {
                MenuItem = menu,
                Role = role
            };

            await _unitOfWork.MenuRoles.AddAsync(menuRole);

            await _unitOfWork.SaveChangesAsync();

            return menuRole.Id;
        }

        public async Task<ICollection<Domain.Models.Role>> GetRolesMapAsync(GetRoles command)
        {
            var roles = await _unitOfWork.Roles.Query().ToListAsync();
            var result = roles.Select(x => _mapper.Map<Domain.Models.Role>(x)).ToList();
            return result;
        }

        public async Task<bool> RemoveMenuRoleMap(int id)
        {
            var roleMenuPermission = await _unitOfWork.MenuRolePermissions.FirstOrDefaultAsync(false, i => i.MenuRole.Id == id);

            if (roleMenuPermission != null)
            {
                _unitOfWork.MenuRolePermissions.Delete(false, roleMenuPermission.Id);
            }

            _unitOfWork.MenuRoles.Delete(false, id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMenuRoleMapAsync(UpdateMenuRoleMap command)
        {
            var roleMenu = await _unitOfWork.MenuRoles.FirstOrDefaultAsync(false, i => i.Id == command.MenuRoleId);

            if (roleMenu == null) { throw new DomainException("MenuRole does not exist", DomainError.BadRequest); }

            var menuRolePermission = _mapper.Map<MenuRolePermission>(command);

            menuRolePermission.MenuRole = roleMenu;
            menuRolePermission.MenuRoleId = roleMenu.Id;

            await _unitOfWork.MenuRolePermissions.AddAsync(menuRolePermission);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
