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
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using MSR.Domain.Views;

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

        public async Task<Domain.Models.UserModel> CreateUserAsync(CreateUser command)
        {
            var rolesToAdd = new List<UserRole>();
            List<EntityFramework.Entities.Role> getRolesFromDb;
            if (command.Roles != null && command.Roles.Count > 0)
            {
                getRolesFromDb = _unitOfWork.Roles.Query().Where(x => command.Roles.Select(y => y.Id).Contains(x.Id)).ToList();
            }
            else
            {
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
            if (command.TimeZoneId != 0)
            {
                efUser.TimeZoneId = command.TimeZoneId;
            }


            if (command.LocationId.HasValue && command.IsAnswerUser.HasValue && !command.IsAnswerUser.Value)
            {
                efUser.Customer = _unitOfWork.Customers.Query().FirstOrDefault(x => x.Id == command.CustomerId);
            }

            var password = _authenticationHelper.CreateRandomPassword();
            _authenticationHelper.CreatePasswordHash(password, out var hash, out var salt);
            efUser.PasswordHash = hash;
            efUser.PasswordSalt = salt;

            var createdByUser = await _unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == command.CurrentUser);
            efUser.Created = createdByUser;
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

            var domainUser = _mapper.Map<Domain.Models.UserModel>(efUser);
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

        public async Task<Domain.Models.UserModel> UpdateUserAsync(UpdateUser command)
        {
            var efUser = _unitOfWork.Users.Query().Include(x => x.Roles).FirstOrDefault(i => i.Id == command.Id);

            if (efUser == null)
            {
                throw new DomainException($"No user with {nameof(command.Id)} {command.Id} found", DomainError.NotFound);
            }
            var userType = efUser.GetType();

            var rolesToAdd = new List<UserRole>();

            foreach (var role in efUser.Roles)
            {
                _unitOfWork.UserRoles.Delete(false, role);
            }

            var getRolesFromDb = _unitOfWork.Roles.Query().Where(x => command.Roles.Select(y => y.Id).Contains(x.Id)).ToList();

            foreach (var role in command.Roles)
            {
                var userRoleAdd = new UserRole() { Role = getRolesFromDb.FirstOrDefault(x => x.Id == role.Id), UserId = efUser.Id };
                _unitOfWork.UserRoles.AttachAndInsert(userRoleAdd);
                rolesToAdd.Add(userRoleAdd);
            }

            command.Roles = null;
            var timezone = efUser.TimeZoneId;

            foreach (var property in typeof(UpdateUser).GetProperties().Where(i => i.Name != nameof(command.Id)))
            {
                var prop = userType.GetProperty(property.Name);

                if (prop == null) { continue; }

                prop.SetValue(efUser, property.GetValue(command), null);
            }

            efUser.Roles = rolesToAdd;

            if (command.TimeZoneId != 0)
            {
                efUser.TimeZoneId = command.TimeZoneId;
            }
            else
            {
                efUser.TimeZoneId = timezone;
            }

            _unitOfWork.Users.Update(efUser);
            await _unitOfWork.SaveChangesAsync();

            var domainUser = _mapper.Map<Domain.Models.UserModel>(efUser);
            SetRolesToUser(efUser, domainUser);

            return domainUser;
        }

        public async Task<ICollection<Domain.Models.UserModel>> GetUsersAsync(GetUsers command)
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

            if (command.HasRoleIDs != null && command.HasRoleIDs.Any())
            {
                users = users.Where(i => i.Roles.Any(r => command.HasRoleIDs.Contains(r.RoleId)));
            }

            var userList = new List<Domain.Models.UserModel>();

            var usersTo = await users.Include(x => x.TimeZone).Include(x => x.Location).Include(x => x.Supervisor)
                .Include(x => x.Roles).ThenInclude(x => x.Role).ToListAsync();

            foreach (var user in usersTo)
            {
                var userToAdd = _mapper.Map<Domain.Models.UserModel>(user);
                userToAdd.SupervisorName = user?.Supervisor?.GetFullName();
                SetRolesToUser(user, userToAdd);
                userList.Add(userToAdd);
            }

            return userList;
        }

        private static void SetRolesToUser(EntityFramework.Entities.User user, Domain.Models.UserModel userToAdd)
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

        public async Task<Domain.Models.UserModel> GetUserAsync(int Id)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == Id);

            if (user == null)
                return null;

            return _mapper.Map<Domain.Models.UserModel>(user);
        }

        public async Task<Domain.Models.UserModel> GetLoggedInUserData(int Id)
        {
            var user = await _unitOfWork.Users.Query().Include(x => x.TimeZone)
                .Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.Menus)
                .ThenInclude(x => x.MenuItem).ThenInclude(x => x.MenuGroup)
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new DomainException($"{nameof(Domain.Models.UserModel)} not found", DomainError.NotFound);

            var domainUser = _mapper.Map<Domain.Models.UserModel>(user);

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

                    domainRole.Menus.Add(domainMenuItem);
                }

                domainUser.Roles.Add(domainRole);
            }

            return domainUser;
        }

        public async Task<Domain.Models.UserModel> CreateUserRoleAsync(CreateUserRole command)
        {
            var curUser = await _unitOfWork.GetLoggedInUserAsync();
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == command.UserId);
            var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == command.RoleId);

            if (user is null || role is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.User)} OR {nameof(Role)} not found.", DomainError.NotFound);
            }

            var curUserRole = await _unitOfWork.UserRoles.FirstOrDefaultAsync(false, i => i.RoleId == command.RoleId && i.UserId == command.UserId);

            if (curUserRole != null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.User)} already assigned to {nameof(Role)}", DomainError.Conflict);
            }

            if (curUser.CanApprove(EnumMenuItem.Users))
            {
                var userRole = new UserRole()
                {
                    CertificationFromDate = command.CertificationFromDate.HasValue ? command.CertificationFromDate.Value : (DateTime?)null,
                    CertificationToDate = command.CertificationToDate.HasValue ? command.CertificationToDate.Value : (DateTime?)null,
                    Role = role,
                    RoleId = role.Id,
                    User = user,
                    UserId = user.Id
                };

                _unitOfWork.UserRoles.Add(userRole);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.LogApprovalTransaction(userRole, userRole.Id);

                return _mapper.Map<Domain.Models.UserModel>(user);
            }

            var userRoleApproval = new UserRoleApproval()
            {
                CertificationFromDate = command.CertificationFromDate.HasValue ? command.CertificationFromDate.Value : (DateTime?)null,
                CertificationToDate = command.CertificationToDate.HasValue ? command.CertificationToDate.Value : (DateTime?)null,
                Role = role,
                RoleId = role.Id,
                User = user,
                UserId = user.Id
            };

            _unitOfWork.UserRoleApprovals.Add(userRoleApproval);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Domain.Models.UserModel>(user);
        }

        public async Task UpdateUserRoleAsync(UpdateUserRole command)
        {
            var curUser = await _unitOfWork.GetLoggedInUserAsync();
            var curUserRole = await _unitOfWork.UserRoles.FirstOrDefaultAsync(false, i => i.RoleId == command.RoleId && i.UserId == command.UserId);

            if (curUserRole is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.User)} not assigned to {nameof(Role)}", DomainError.BadRequest);
            }

            if (curUser.CanApprove(EnumMenuItem.Users))
            {
                curUserRole.CertificationFromDate = command.CertificationFromDate.HasValue ? command.CertificationFromDate.Value : curUserRole.CertificationFromDate.HasValue ? curUserRole.CertificationFromDate.Value : (DateTime?)null;
                curUserRole.CertificationToDate = command.CertificationToDate.HasValue ? command.CertificationToDate.Value : curUserRole.CertificationToDate.HasValue ? curUserRole.CertificationToDate.Value : (DateTime?)null;

                _unitOfWork.UserRoles.Update(curUserRole);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.LogApprovalTransaction(curUserRole, curUserRole.Id);
            }
            else
            {
                var userApproval = _mapper.Map<UserApproval>(curUserRole.User);
                _unitOfWork.UserApprovals.Add(userApproval);
                await _unitOfWork.SaveChangesAsync();

                var userRoleApproval = new UserRoleApproval()
                {
                    UserApproval = userApproval,
                    UserApprovalId = userApproval.Id,
                    CertificationFromDate = command.CertificationFromDate.HasValue ? command.CertificationFromDate.Value : (DateTime?)null,
                    CertificationToDate = command.CertificationToDate.HasValue ? command.CertificationToDate.Value : (DateTime?)null,
                    Role = curUserRole.Role,
                    RoleId = curUserRole.RoleId,
                    User = curUserRole.User,
                    UserId = curUserRole.UserId
                };

                _unitOfWork.UserRoleApprovals.Add(userRoleApproval);
                await _unitOfWork.SaveChangesAsync();
            }

        }

        public async Task DeleteUserRoleAsync(DeleteUserRole command)
        {
            var curUserRole = await _unitOfWork.UserRoles.FirstOrDefaultAsync(false, i => i.Id == command.UserRoleId);

            if (curUserRole is null)
            {
                throw new DomainException($"No {nameof(UserRole)} with ID: {command.UserRoleId} found", DomainError.NotFound);
            }


            await _unitOfWork.LogApprovalTransaction(curUserRole, curUserRole.Id);
            _unitOfWork.UserRoles.Delete(false, curUserRole);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Domain.Models.UserModel> GetUserById(int id)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == id);

            if (user is null)
            {
                return new Domain.Models.UserModel();
            }

            var retUser = _mapper.Map<Domain.Models.UserModel>(user);

            return retUser;
        }

        public async Task<IEnumerable<TrainingCertificationView>> GetTrainingCertificationAsync(GetTrainingCertification command)
        {
            var userRoles = _unitOfWork.UserRoles.Query();

            if (command.Id.HasValue)
            {
                userRoles = userRoles.Where(i => i.UserId == command.Id.Value);
            }

            return userRoles.Include(i => i.User).Include(i => i.Role).Where(i => i.Role.IsCertificationRole.HasValue && i.Role.IsCertificationRole.Value).Select(i => new TrainingCertificationView()
            {
                CertificationFromDate = i.CertificationFromDate,
                CertificationToDate = i.CertificationToDate,
                EmployeeName = i.User.GetFullName(),
                Status = (i.CertificationToDate.HasValue ? DateTime.Compare(i.CertificationToDate.Value, DateTime.UtcNow) <= 0 ? "Expired" : "Active" : "Active"),
                CertificationName = i.Role.Name
            }).AsEnumerable();
        }
    }
}
