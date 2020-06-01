using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var roleMenu = await _unitOfWork.MenuRoles.FirstOrDefaultAsync(false,i => i.MenuItemId == command.MenuId && i.RoleId == command.RoleId);

            if (roleMenu != null) return roleMenu.Id;

            var role = _unitOfWork.Roles.FirstOrDefault(false, i => i.Id == command.RoleId);
            var menu = _unitOfWork.MenuItems.FirstOrDefault(false, i => i.Id == command.MenuId);
            
            if(role is null || menu is null)
            {
                throw new DomainException("Role or Menu not found", DomainError.BadRequest);
            }

            var entity = _unitOfWork.MenuRoles.AddAndSaveChanges(new EntityFramework.Entities.MenuRole()
            {
                MenuItem = menu,
                Role = role
            });

            return entity.Entity.Id;
        }

        public async Task<bool> RemoveMenuRoleMap(int id)
        {
            var roleMenuPermission = _unitOfWork.MenuRolePermissions.FirstOrDefault(false, i => i.MenuRole.Id == id);

            if(roleMenuPermission != null)
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

            if(roleMenu == null) { throw new DomainException("MenuRole does not exist", DomainError.BadRequest); }

            var menuRolePermission = _mapper.Map<MenuRolePermission>(command);

            menuRolePermission.MenuRole = roleMenu;
            menuRolePermission.MenuRoleId = roleMenu.Id;

            _unitOfWork.MenuRolePermissions.Add(menuRolePermission);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
