using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task<ICollection<UserModel>> GetUsersAsync(GetUsers command);
        Task<UserModel> GetUserById(int id);
        Task<UserModel> CreateUserAsync(CreateUser command);
        Task<UserModel> UpdateUserAsync(UpdateUser command);
        Task DeactivateUserAsync(DeactivateUser command);
        Task<UserModel> GetLoggedInUserData(int Id);
        Task<UserModel> GetUserAsync(int Id);
        Task<UserModel> CreateUserRoleAsync(CreateUserRole command);
        Task UpdateUserRoleAsync(UpdateUserRole command);
        Task DeleteUserRoleAsync(DeleteUserRole command);
        Task<IEnumerable<TrainingCertificationView>> GetTrainingCertificationAsync(GetTrainingCertification command);
    }
}
