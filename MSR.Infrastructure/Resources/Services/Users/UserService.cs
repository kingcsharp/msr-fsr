using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Helpers.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace MSR.Infrastructure.Resources.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticationHelper _authenticationHelper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IAuthenticationHelper authenticationHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticationHelper = authenticationHelper;
        }

        public async Task<Domain.Models.User> CreateUserAsync(CreateUser command)
        {


            var rolesToAdd = new List<UserRole>();
            List<EntityFramework.Entities.Role> getRolesFromDb;
            if (command.Roles != null && command.Roles.Count > 0) {
                getRolesFromDb = _unitOfWork.Roles.Query().Where(x => command.Roles.Select(y => y.Id).Contains(x.Id)).ToList();
            } else {
                getRolesFromDb = new List<EntityFramework.Entities.Role>();
            }
            command.Roles = null;

            var efUser = _mapper.Map<User>(command);

            if (efUser.EmailAlreadyExists(_unitOfWork))
            {
                throw new DomainException($"{nameof(command.Email)} already Exists", DomainError.Conflict);
            }

            if (efUser.UserNameAlreadyExists(_unitOfWork))
            {
                throw new DomainException($"{nameof(command.UserName)} already Exists", DomainError.Conflict);
            }

            //supervisor location
            efUser.Supervisor = _unitOfWork.Users.Query().FirstOrDefault(x => x.Id == command.SupervisorId);
            efUser.Location = _unitOfWork.Locations.Query().FirstOrDefault(x => x.Id == command.LocationId);

            var password = _authenticationHelper.CreateRandomPassword();
            _authenticationHelper.CreatePasswordHash(password, out var hash, out var salt);
            efUser.PasswordHash = hash;
            efUser.PasswordSalt = salt;

            efUser.CreatedBy = command.CurrentUser;
            efUser.CreatedOn = DateTime.UtcNow;

            _unitOfWork.Users.Add(efUser);

            foreach (var role in getRolesFromDb)
            {
                var userRoleAdd = new UserRole() { Role = role, UserId = efUser.Id };
                _unitOfWork.UserRoles.AttachAndInsert(userRoleAdd);
                rolesToAdd.Add(userRoleAdd);
            }

            efUser.Roles = rolesToAdd;

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var data = ex.Message;
            }

            var domainUser = _mapper.Map<Domain.Models.User>(efUser);
            SetRolesToUser(efUser, domainUser);
            domainUser.SupervisorName = efUser.Supervisor.GetFullName();
            domainUser.LocationName = efUser.Location.Name;
            return domainUser;
        }

        public async Task DeactivateUserAsync(DeactivateUser command)
        {
            var user = _unitOfWork.Users.FirstOrDefault(false, i => i.Id == command.AccountId);

            if (user == null) { return; }

            user.IsActive = !user.IsActive;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Domain.Models.User> UpdateUserAsync(UpdateUser command)
        {
            var efUser = _unitOfWork.Users.FirstOrDefault(false, i => i.Id == command.Id);

            if (efUser == null)
            {
                throw new DomainException($"No user with {nameof(command.Id)} {command.Id} found", DomainError.NotFound);
            }
            var userType = efUser.GetType();

            var rolesToAdd = new List<UserRole>();

            foreach (var role in efUser.Roles)
            {
                if (!command.Roles.Any(x => x.Id == role.RoleId))
                {
                    _unitOfWork.UserRoles.Delete(false, role);
                }
                else
                {
                    rolesToAdd.Add(role);
                }
            }
            var getRolesFromDb = _unitOfWork.Roles.Query().Where(x => command.Roles.Select(y => y.Id).Contains(x.Id)).ToList();

            foreach (var role in command.Roles)
            {
                if (!efUser.Roles.Any(x => x.RoleId == role.Id))
                {
                    var userRoleAdd = new UserRole() { Role = getRolesFromDb.FirstOrDefault(x => x.Id == role.Id), UserId = efUser.Id };
                    _unitOfWork.UserRoles.AttachAndInsert(userRoleAdd);
                    rolesToAdd.Add(userRoleAdd);
                }
            }

            command.Roles = null;

            foreach (var property in typeof(UpdateUser).GetProperties().Where(i => i.Name != nameof(command.Id)))
            {
                var prop = userType.GetProperty(property.Name);

                if (prop == null) { continue; }

                prop.SetValue(efUser, property.GetValue(command), null);
            }

            efUser.Roles = rolesToAdd;

            try
            {
                _unitOfWork.Users.Update(efUser);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var domainUser = _mapper.Map<Domain.Models.User>(efUser);
            SetRolesToUser(efUser, domainUser);

            return domainUser;
        }

        public async Task<ICollection<Domain.Models.User>> GetUsersAsync(GetUsers command)
        {
            //This needs to be refactored to remove the dependency on EntityFramework Directly.
            var users = _unitOfWork.Users.Query();

            if (command.Id.HasValue)
            {
                users = users.Where(i => i.Id == command.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(command.FirstName))
            {
                users = users.Where(i => i.FirstName == command.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(command.LastName))
            {
                users = users.Where(i => i.LastName == command.LastName);
            }

            if (!string.IsNullOrWhiteSpace(command.UserName))
            {
                users = users.Where(i => i.UserName == command.UserName);
            }

            if (!string.IsNullOrWhiteSpace(command.Title))
            {
                users = users.Where(i => i.Title == command.Title);
            }

            if (command.Supervisor.HasValue)
            {
                users = users.Where(i => i.SupervisorId != null && i.SupervisorId == command.Supervisor);
            }

            if (!string.IsNullOrWhiteSpace(command.PrimaryPhone))
            {
                users = users.Where(i => i.Phone == command.PrimaryPhone);
            }

            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                users = users.Where(i => i.Email == command.Email);
            }

            var userList = new List<Domain.Models.User>();

            var usersTo = users.Include(x => x.Location).Include(x => x.Supervisor).Include(x => x.Roles).ThenInclude(x => x.Role).ToList();

            foreach (var user in usersTo)
            {
                var userToAdd = _mapper.Map<Domain.Models.User>(user);
                userToAdd.SupervisorName = user?.Supervisor?.GetFullName();
                SetRolesToUser(user, userToAdd);
                userList.Add(userToAdd);
            }

            return userList;
        }

        private static void SetRolesToUser(User user, Domain.Models.User userToAdd)
        {
            foreach (var role in user.Roles ?? new List<UserRole>())
            {
                if (userToAdd.Roles == null)
                {
                    userToAdd.Roles = new List<Domain.Models.Role>();
                }
                userToAdd.Roles.Add(new Domain.Models.Role()
                {
                    Id = role.Role.Id,
                    Name = role.Role.Name
                });
            }
        }

        public async Task<Domain.Models.User> GetLoggedInUserData(int Id)
        {
            var user = _unitOfWork.Users.FirstOrDefault(false, i => i.Id == Id);

            if (user == null)
                throw new DomainException($"{nameof(Domain.Models.User)} not found", DomainError.NotFound);

            var domainUser = _mapper.Map<Domain.Models.User>(user);

            foreach (var role in user.Roles ?? new List<UserRole>())
            {
                var efRole = role.Role;
                var domainRole = new Domain.Models.Role()
                {
                    IsCertificationRole = efRole.IsCertificationRole,
                    Name = efRole.Name
                };

                foreach (var menuItem in (efRole ?? new EntityFramework.Entities.Role()).Menus)
                {
                    if (menuItem.MenuItem == null) continue;
                    var efMenuItem = menuItem.MenuItem;
                    var domainMenuItem = new Domain.Models.MenuItem()
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
                        domainMenuItem.MenuGroup = new Domain.Models.MenuGroup()
                        {
                            Icon = efMenuItem.MenuGroup.Icon,
                            Info = efMenuItem.MenuGroup.Info,
                            Name = efMenuItem.MenuGroup.Name,
                            OrderNumber = efMenuItem.MenuGroup.OrderNumber,
                            URL = efMenuItem.MenuGroup.URL
                        };
                    }

                    if (menuItem.MenuRolePermission != null)
                    {
                        var listEnumPrivilege = new List<int>();
                        if (menuItem.MenuRolePermission != null)
                        {
                            if (menuItem.MenuRolePermission.CanActivate)
                            {
                                listEnumPrivilege.Add((int)EnumPrivilege.CanActivate);
                            }
                            if (menuItem.MenuRolePermission.CanApprove)
                            {
                                listEnumPrivilege.Add((int)EnumPrivilege.CanApprove);
                            }
                            if (menuItem.MenuRolePermission.CanCreate)
                            {
                                listEnumPrivilege.Add((int)EnumPrivilege.CanCreate);
                            }
                            if (menuItem.MenuRolePermission.CanDelete)
                            {
                                listEnumPrivilege.Add((int)EnumPrivilege.CanDelete);
                            }
                            if (menuItem.MenuRolePermission.CanEdit)
                            {
                                listEnumPrivilege.Add((int)EnumPrivilege.CanEdit);
                            }
                            if (menuItem.MenuRolePermission.CanRead)
                            {
                                listEnumPrivilege.Add((int)EnumPrivilege.CanRead);
                            }
                        }
                        domainMenuItem.Permissions = listEnumPrivilege.ToArray();
                    }

                    domainRole.Menus.Add(domainMenuItem);
                }

                domainUser.Roles.Add(domainRole);
            }

            return domainUser;
        }
    }
}
