using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task<ICollection<User>> GetUsersAsync(GetUsers command);
        Task<User> GetUserById(int id);
        Task<User> CreateUserAsync(CreateUser command);
        Task<User> UpdateUserAsync(UpdateUser command);
        Task DeactivateUserAsync(DeactivateUser command);
        Task<User> GetLoggedInUserData(int Id);
        Task<User> CreateUserRoleAsync(CreateUserRole command);
        Task UpdateUserRoleAsync(UpdateUserRole command);
        Task DeleteUserRoleAsync(DeleteUserRole command);
    }
}
