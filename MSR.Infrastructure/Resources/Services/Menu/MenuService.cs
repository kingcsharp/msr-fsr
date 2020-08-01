using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Menu
{
    public class MenuService : IMenuService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Domain.Models.MenuItem>> GetMenuAsync(GetMenu command)
        {
            var menuItems = _unitOfWork.MenuItems.Query().Include(i => i.Roles).ThenInclude(i => i.MenuRolePermission)
                                                         .Include(i => i.MenuGroup)
                                                         .Include(i => i.Roles).ThenInclude(i => i.Role).ThenInclude(i => i.ChildRoles)
                                                                               .ThenInclude(i => i.ChildRole).ThenInclude(i => i.Menus)
                                                                               .ThenInclude(i => i.MenuRolePermission);

            var retMenuItems = new List<Domain.Models.MenuItem>();

            foreach (var efMenuItem in menuItems)
            {
                var domainMenuItem = new Domain.Models.MenuItem()
                {
                    Id = efMenuItem.Id,
                    Icon = efMenuItem.Icon,
                    Info = efMenuItem.Info,
                    Name = efMenuItem.Name,
                    OrderNumber = efMenuItem.OrderNumber,
                    URL = efMenuItem.URL,
                    EnumMenuItem = EnumUtils.ParseMenuType(efMenuItem.Name)
                };

                if (efMenuItem.MenuGroup != null)
                {
                    domainMenuItem.MenuGroup = new Domain.Models.MenuGroup()
                    {
                        Icon = efMenuItem.MenuGroup.Icon,
                        Info = efMenuItem.MenuGroup.Info,
                        Name = efMenuItem.MenuGroup.Name,
                        OrderNumber = efMenuItem.MenuGroup.OrderNumber,
                        URL = efMenuItem.MenuGroup.URL
                    };
                }

                foreach(var menuRole in efMenuItem.Roles)
                {
                    var role = menuRole.Role;
                    var domainRole = new Domain.Models.Role()
                    {
                        Id = role.Id,
                        IsCertificationRole = role.IsCertificationRole,
                        Menus = null,
                        Name = role.Name
                    };

                    var listEnumPrivilege = new List<int>();
                    if (menuRole.MenuRolePermission != null)
                    {
                        domainRole.Permissions = _mapper.Map<Permission>(menuRole.MenuRolePermission);
                    }

                    var childRoles = role.ChildRoles.SelectMany(i => i.ChildRole.Menus).Select(i => i.MenuRolePermission);

                    domainRole.InheritedPermissions = new Permission
                    {
                        CanActivate = childRoles.Any(i => i.CanActivate),
                        CanApprove = childRoles.Any(i => i.CanApprove),
                        CanCreate = childRoles.Any(i => i.CanCreate),
                        CanDelete = childRoles.Any(i => i.CanDelete),
                        CanEdit = childRoles.Any(i => i.CanEdit),
                        CanRead = childRoles.Any(i => i.CanRead)
                    };
                    domainMenuItem.Roles.Add(domainRole);
                }
                retMenuItems.Add(domainMenuItem);
            }

            return retMenuItems;
        }

        public async Task<bool> RemoveMenuRoleMap(RemoveMenuRoleMap command)
        {
            var menuRole = await _unitOfWork.MenuRoles.Query().Include(i => i.MenuItem)
                                                              .Include(i => i.Role)
                                                              .Include(i => i.MenuRolePermission)
                                                              .FirstOrDefaultAsync(i => i.MenuItemId == command.MenuId && i.RoleId == command.RoleId);

            if(menuRole is null)
            {
                throw new DomainException($"{nameof(MenuRole)} not found", DomainError.NotFound);
            }

            if (menuRole.MenuRolePermission != null)
            {
                _unitOfWork.MenuRolePermissions.Delete(false, menuRole.MenuRolePermission.Id);
            }

            _unitOfWork.MenuRoles.Delete(false, menuRole.Id);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateMenuRoleMapAsync(UpdateMenuRoleMap command)
        {
            var roleMenu = await _unitOfWork.MenuRoles.Query().Include(i => i.MenuRolePermission)
                                                      .FirstOrDefaultAsync(i => i.RoleId == command.RoleId && i.MenuItemId == command.MenuId);

            if (roleMenu == null) { throw new DomainException("MenuRole does not exist", DomainError.BadRequest); }

            if(roleMenu.MenuRolePermission is null)
            {
                var menuRolePermission = _mapper.Map<MenuRolePermission>(command);

                menuRolePermission.MenuRole = roleMenu;
                menuRolePermission.MenuRoleId = roleMenu.Id;

                await _unitOfWork.MenuRolePermissions.AddAsync(menuRolePermission);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }

            roleMenu.MenuRolePermission = _mapper.Map<MenuRolePermission>(command);
            _unitOfWork.MenuRoles.Update(roleMenu);
            await _unitOfWork.SaveChangesAsync();
            return true;
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
    }
}
