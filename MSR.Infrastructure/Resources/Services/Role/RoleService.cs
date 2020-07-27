using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Infrastructure.Resources.EntityFramework.Application;
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

        public async Task<ICollection<Domain.Models.Role>> GetRolesMapAsync(GetRoles command)
        {
            var result = new List<Domain.Models.Role>();
            var roles = await _unitOfWork.Roles.Query().Include(i => i.Menus).ThenInclude(i => i.MenuRolePermission)
                                                       .Include(i => i.Menus).ThenInclude(i => i.MenuItem).ThenInclude(i => i.MenuGroup)
                                                       .Include(i => i.ChildRoles).ThenInclude(i => i.ChildRole).ThenInclude(i => i.Menus).ThenInclude(i => i.MenuRolePermission).ToListAsync();
            foreach (var role in roles)
            {
                var domRole = _mapper.Map<Domain.Models.Role>(role);

                foreach (var menu in role.Menus)
                {
                    var domMenu = _mapper.Map<Domain.Models.MenuItem>(menu.MenuItem);
                    var childRoles = role.ChildRoles.SelectMany(i => i.ChildRole.Menus).Select(i => i.MenuRolePermission);

                    domMenu.InheritedPermissions = new Domain.Models.Permission
                    {
                        CanActivate = childRoles.Any(i => i.CanActivate),
                        CanApprove = childRoles.Any(i => i.CanApprove),
                        CanCreate = childRoles.Any(i => i.CanCreate),
                        CanDelete = childRoles.Any(i => i.CanDelete),
                        CanEdit = childRoles.Any(i => i.CanEdit),
                        CanRead = childRoles.Any(i => i.CanRead)
                    };
                    domMenu.Permissions = _mapper.Map<Domain.Models.Permission>(menu.MenuRolePermission);
                    domRole.Menus.Add(domMenu);
                }
                result.Add(domRole);
            }
            
            return result;
        }

    }
}
