using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
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

        public async Task<ICollection<User>> GetUsers(GetUsers command)
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

            if (!string.IsNullOrWhiteSpace(command.Supervisor))
            {
                users = users.Where(i => i.Supervisor != null && i.Supervisor.FirstName == command.Supervisor);
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
