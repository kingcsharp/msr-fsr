using MSR.Domain.Commands;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ISessionManagementService
    {
        Task<bool> IsActiveSessionAsync(int userId);
        Task ExpireUserSessionAsync(int userId);
        Task AddOrUpdateSessionAsync(int userId, string token);
    }
}
