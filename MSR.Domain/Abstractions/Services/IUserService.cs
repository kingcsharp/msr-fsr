using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task<ICollection<User>> GetUsersAsync(GetUsers command);
        Task<User> CreateUserAsync(CreateUser command);
    }
}
