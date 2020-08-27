using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Domain.Exceptions;
using MSR.Domain.Commanding.Enums;

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

        public async Task<Domain.Models.Role> CreateRoleAsync(CreateRole command)
        {
            var role = new EntityFramework.Entities.Role()
            {
                IsCertificationRole = command.IsCertificationRole,
                Name = command.Name,
            };

            await _unitOfWork.Roles.AddAsync(role);

            var parentRoles = await _unitOfWork.Roles.Query().Where(i => command.ParentRoleIds.Contains(i.Id)).ToListAsync();

            foreach (var parent in parentRoles)
            {
                var map = new RoleChildRoleMap()
                {
                    ParentRole = parent,
                    ChildRole = role,
                };
                await _unitOfWork.RoleChildRoleMaps.AddAsync(map);
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Domain.Models.Role>(role);
        }

        public async Task<ICollection<Domain.Models.Role>> GetRolesMapAsync(GetRoles command)
        {
            var result = new List<Domain.Models.Role>();
            var roles = await _unitOfWork.Roles.Query().Include(i => i.Menus).ThenInclude(i => i.MenuRolePermission)
                                                       .Include(i => i.Menus).ThenInclude(i => i.MenuItem).ThenInclude(i => i.MenuGroup)
                                                       .ToListAsync();
            var rolesIds = roles.Select(x => x.Id).ToList();
            var allChildRoles = await _unitOfWork.RoleChildRoleMaps.Query().Include(x => x.ChildRole).ThenInclude(i => i.Menus)
            .ThenInclude(i => i.MenuRolePermission).Where(x => rolesIds.Contains(x.ParentRoleId)).ToListAsync();
            var allParentRoles = await _unitOfWork.RoleChildRoleMaps.Query().Include(x => x.ParentRole).Where(x => rolesIds.Contains(x.ChildRoleId.Value)).ToListAsync();
            foreach (var role in roles)
            {
                var domRole = _mapper.Map<Domain.Models.Role>(role);
                
                foreach (var menu in role.Menus)
                {
                    var domMenu = _mapper.Map<Domain.Models.MenuItem>(menu.MenuItem);
                    var childRolesMenuPermissions = allChildRoles.Where(i => i.ParentRoleId == role.Id).Select(i => i.ChildRole).SelectMany(i => i.Menus).Select(i => i.MenuRolePermission);

                    domMenu.InheritedPermissions = new Domain.Models.Permission
                    {
                        CanActivate = childRolesMenuPermissions.Any(i => i.CanActivate),
                        CanApprove = childRolesMenuPermissions.Any(i => i.CanApprove),
                        CanCreate = childRolesMenuPermissions.Any(i => i.CanCreate),
                        CanDelete = childRolesMenuPermissions.Any(i => i.CanDelete),
                        CanEdit = childRolesMenuPermissions.Any(i => i.CanEdit),
                        CanRead = childRolesMenuPermissions.Any(i => i.CanRead)
                    };
                    domMenu.Permissions = _mapper.Map<Domain.Models.Permission>(menu.MenuRolePermission);
                    domRole.Menus.Add(domMenu);
                }
                var domparentRoles = allParentRoles.Where(i => i.ChildRoleId == role.Id).Select(i => _mapper.Map<Domain.Models.Role>(i.ParentRole));
                domRole.ParentRoles.AddRange(domparentRoles);

                domRole.HasAssignedUsers = _unitOfWork.UserRoles.Query().Any(i => i.RoleId == role.Id);

                result.Add(domRole);
            }

            return result;
        }

        public async Task<Domain.Models.Role> UpdateRoleAsync(UpdateRole command)
        {
            var role = await _unitOfWork.Roles.Query().Include(i => i.ParentRoles).FirstOrDefaultAsync(i => i.Id == command.Id);

            if(role is null)
            {
                throw new DomainException($"Role with ID: {command.Id} not found", DomainError.NotFound);
            }

            role.Name = command.Name;
            role.IsCertificationRole = command.IsCertificationRole;

            _unitOfWork.Roles.Update(role);
            var parentRoleIds = role.ParentRoles.Select(i => i.ParentRoleId);

            //Exists in DB but not in list: Remove
            var parentsToRemove = parentRoleIds.Except(command.ParentRoleIds);
            //Does not Exist in DB: Add
            var parentsToAdd = command.ParentRoleIds.Except(parentRoleIds);

            var maps = await _unitOfWork.RoleChildRoleMaps.Query().Where(i => i.ChildRoleId == command.Id && parentsToRemove.Contains(i.ParentRoleId)).ToListAsync();

            foreach (var parent in maps)
            {
                _unitOfWork.RoleChildRoleMaps.Delete(false, parent);
            }

            var parents = await _unitOfWork.Roles.Query().Where(i => parentsToAdd.Contains(i.Id)).ToListAsync();

            foreach (var parent in parents)
            {
                var map = new RoleChildRoleMap()
                {
                    ParentRole = parent,
                    ChildRole = role
                };
                await _unitOfWork.RoleChildRoleMaps.AddAsync(map);
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Domain.Models.Role>(role);
        }
    }
}
