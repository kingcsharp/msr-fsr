using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Domain.Abstractions.Services;
using Microsoft.Extensions.Caching.Memory;

namespace MSR.Infrastructure.Resources.Services.SessionManagement
{
   class SessionManagementService : ISessionManagementService
    {
        private readonly Dictionary<int, string> _activeSessions = new Dictionary<int, string>();

        public  Task<bool> IsActiveSessionAsync(int userId)
        {
            return Task.FromResult(_activeSessions.ContainsKey(userId));
        }

        public Task ExpireUserSessionAsync(int userId)
        {
            _activeSessions.Remove(userId);
            return Task.CompletedTask;
        }

        public Task AddOrUpdateSessionAsync(int userId, string token)
        {
            _activeSessions[userId] = token;
            return Task.CompletedTask;
        }
    }
}
