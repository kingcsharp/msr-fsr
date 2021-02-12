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
using MSR.Domain.Models;
using System.Collections.Concurrent;

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

            foreach (var userRole in command.UserRoles)
            {
                _unitOfWork.UserRoles.AttachAndInsert(new UserRole()
                {
                    Role = role,
                    UserId = userRole.UserId,
                    CertificationFromDate = command.IsCertificationRole ? userRole.CertificationFromDate : null,
                    CertificationToDate = command.IsCertificationRole ? userRole.CertificationToDate : null
                });
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Domain.Models.Role>(role);
        }

        public async Task DeleteRoleAsync(DeleteRole command)
        {
            var inUse = _unitOfWork.UserRoles.Query().Any(i => i.RoleId == command.Id);
            inUse |= _unitOfWork.WorkflowGroupRoleMaps.Query().Any(i => i.RoleId == command.Id);
            inUse |= _unitOfWork.HelpPageRoles.Query().Any(i => i.RoleId == command.Id);

            if (inUse)
            {
                throw new DomainException($"Role in Use.  Cannot be deleted", DomainError.Conflict);
            }

            var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (role is null)
            {
                throw new DomainException($"Role with ID: {command.Id} not found", DomainError.NotFound);
            }

            var maps = await _unitOfWork.RoleChildRoleMaps.Query().Where(i => i.ChildRoleId == command.Id || i.ParentRoleId == command.Id).ToListAsync();

            foreach (var map in maps)
            {
                _unitOfWork.RoleChildRoleMaps.Delete(false, map);
            }

            _unitOfWork.Roles.Delete(false, role);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<ICollection<Domain.Models.Role>> GetRolesMapAsync(GetRoles command)
        {
            var result = new List<Domain.Models.Role>();
            
            var roles = await _unitOfWork.Roles.Query().ToListAsync();

            _ = await _unitOfWork.MenuRoles.Query().Include(i => i.MenuItem).ThenInclude(i => i.MenuGroup)
                                                       .ToListAsync();

            _ = await _unitOfWork.MenuRolePermissions.Query().ToListAsync();

            var rolesIds = roles.Select(x => x.Id).ToList();
            
            var allChildRoles = await _unitOfWork.RoleChildRoleMaps.Query().Include(x => x.ChildRole).ThenInclude(i => i.Menus)
            .ThenInclude(i => i.MenuRolePermission).Where(x => rolesIds.Contains(x.ParentRoleId)).ToListAsync();
            
            var allParentRoles = await _unitOfWork.RoleChildRoleMaps.Query().Include(x => x.ParentRole).Where(x => rolesIds.Contains(x.ChildRoleId.Value)).ToListAsync();
            
            var userRoleEntities = await _unitOfWork.UserRoles.Query().Where(s => rolesIds.Contains(s.RoleId)).ToListAsync();
            
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

                domRole.HasAssignedUsers = userRoleEntities.Any(i => i.RoleId == role.Id);

                result.Add(domRole);
            }

            return result;
        }

        public async Task<ICollection<UserModel>> GetRoleAssignedUsers(GetRoleUsers command)
        {
            var users = await _unitOfWork.UserRoles.Query().Include(x => x.User)
                .Where(x => x.RoleId == command.RoleId)
                .Select(x => _mapper.Map<UserModel>(x)).ToListAsync();

            return users;
        }

        public async Task<ICollection<RolesUsersView>> GetRolesAssignedUsers(GetRolesUsers command)
        {
            var roleUsersModelList = new List<RolesUsersView>();

            var iquerableRoleUsersView = _unitOfWork.UserRoles.Query()
                .Select(x => new RolesUsersView
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    RoleId = x.RoleId,
                    CertificationFromDate = x.CertificationFromDate,
                    CertificationToDate = x.CertificationToDate,
                    User = new UserModel() { FirstName = x.User.FirstName, LastName = x.User.LastName, Id = x.UserId }
                });

            if (command.RoleId.HasValue)
            {
                iquerableRoleUsersView = iquerableRoleUsersView.Where(x => x.RoleId == command.RoleId);
            }

            var roleUsersView = await iquerableRoleUsersView.ToListAsync();

            return roleUsersView;
        }

        public async Task<Domain.Models.Role> UpdateRoleAsync(UpdateRole command)
        {
            var role = await _unitOfWork.Roles.Query().Include(i => i.ParentRoles).FirstOrDefaultAsync(i => i.Id == command.Id);

            if (role is null)
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

            var userRoles = await _unitOfWork.UserRoles.Query().Where(x => x.RoleId == command.Id).ToListAsync();
            var commandUserIds = command.UserRoles.Select(x => x.UserId).ToList();

            foreach (var userRole in userRoles)
            {
                if (!commandUserIds.Contains(userRole.UserId))
                {
                    _unitOfWork.UserRoles.Delete(false, userRole, true);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            foreach (var userRole in command.UserRoles)
            {
                var foundUserRole = userRoles.FirstOrDefault(x => x.UserId == userRole.UserId);
                if (foundUserRole == null)
                {
                    _unitOfWork.UserRoles.AttachAndInsert(new UserRole()
                    {
                        Role = role,
                        UserId = userRole.UserId,
                        CertificationFromDate = command.IsCertificationRole ? userRole.CertificationFromDate : null,
                        CertificationToDate = command.IsCertificationRole ? userRole.CertificationToDate : null
                    });
                }
                else
                {
                    foundUserRole.CertificationFromDate = command.IsCertificationRole ? userRole.CertificationFromDate : null;
                    foundUserRole.CertificationToDate = command.IsCertificationRole ? userRole.CertificationToDate : null;
                    _unitOfWork.UserRoles.Update(foundUserRole);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Domain.Models.Role>(role);
        }
    }
}
