using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Emums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Users
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(CreateUser command)
        {
            var efUser = _mapper.Map<EntityFramework.Entities.User>(command);

            if (efUser.EmailAlreadyExists(_unitOfWork))
            {
                throw new DomainException($"{nameof(command.Email)} already Exists", DomainError.Conflict);
            }

            if (efUser.UserNameAlreadyExists(_unitOfWork))
            {
                throw new DomainException($"{nameof(command.UserName)} already Exists", DomainError.Conflict);
            }
            
            var password = AuthenticationHelper.CreateRandomPassword();
            AuthenticationHelper.CreatePasswordHash(password, out var hash, out var salt);
            efUser.PasswordHash = hash;
            efUser.PasswordSalt = salt;

            efUser.CreatedBy = command.CurrentUser;
            efUser.CreatedOn = DateTime.UtcNow;

            _unitOfWork.Users.Add(efUser);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var data = ex.Message;
            }

            return _mapper.Map<User>(efUser);
        }

        public async Task DeactivateUserAsync(DeactivateUser command)
        {
            var user = _unitOfWork.Users.FirstOrDefault(false,i => i.Id == command.AccountId);

            if(user == null) { return; }

            user.IsActive = false;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User> UpdateUserAsync(UpdateUser command)
        {
            var efUser = _unitOfWork.Users.FirstOrDefault(false, i => i.Id == command.Id);

            if (efUser == null)
            {
                throw new DomainException($"No user with {nameof(command.Id)} {command.Id} found", DomainError.NotFound);
            }
            var userType = efUser.GetType();

            foreach(var property in typeof(UpdateUser).GetProperties().Where(i => i.Name != nameof(command.Id)))
            {
                var prop = userType.GetProperty(property.Name);

                if(prop == null) { continue; }

                prop.SetValue(efUser, property.GetValue(command), null);
            }

            _unitOfWork.Users.Update(efUser);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<User>(efUser);
        }

        public async Task<ICollection<User>> GetUsersAsync(GetUsers command)
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

            var userList = new List<User>();

            foreach (var user in users)
            {
                userList.Add(_mapper.Map<User>(user));
            }

            return userList;
        }
    }
}
