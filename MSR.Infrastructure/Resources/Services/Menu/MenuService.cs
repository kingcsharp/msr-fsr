using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
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
        public async Task<IEnumerable<MenuItem>> GetMenuAsync(GetMenu command)
        {
            var menuItems = _unitOfWork.MenuItems.Query().Include(i => i.Roles).ThenInclude(i => i.MenuRolePermission);
            var retMenuItems = new List<MenuItem>();

            foreach (var efMenuItem in menuItems)
            {
                var domainMenuItem = new MenuItem()
                {
                    Icon = efMenuItem.Icon,
                    Info = efMenuItem.Info,
                    Name = efMenuItem.Name,
                    OrderNumber = efMenuItem.OrderNumber,
                    URL = efMenuItem.URL,
                    EnumMenuItem = EnumUtils.ParseMenuType(efMenuItem.Name)
                };

                if (efMenuItem.MenuGroup != null)
                {
                    domainMenuItem.MenuGroup = new MenuGroup()
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
                        if (menuRole.MenuRolePermission.CanActivate)
                        {
                            listEnumPrivilege.Add((int)EnumPrivilege.CanActivate);
                        }
                        if (menuRole.MenuRolePermission.CanApprove)
                        {
                            listEnumPrivilege.Add((int)EnumPrivilege.CanApprove);
                        }
                        if (menuRole.MenuRolePermission.CanCreate)
                        {
                            listEnumPrivilege.Add((int)EnumPrivilege.CanCreate);
                        }
                        if (menuRole.MenuRolePermission.CanDelete)
                        {
                            listEnumPrivilege.Add((int)EnumPrivilege.CanDelete);
                        }
                        if (menuRole.MenuRolePermission.CanEdit)
                        {
                            listEnumPrivilege.Add((int)EnumPrivilege.CanEdit);
                        }
                        if (menuRole.MenuRolePermission.CanRead)
                        {
                            listEnumPrivilege.Add((int)EnumPrivilege.CanRead);
                        }
                    }
                    domainRole.Permissions = listEnumPrivilege.ToArray();
                    domainMenuItem.Roles.Add(domainRole);
                }
                retMenuItems.Add(domainMenuItem);
            }

            return retMenuItems;
        }
    }
}
