using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MSR.Domain.Hub;
using MSR.Domain.Models;

namespace MSR.Domain.Abstractions.Services
{
    public interface IMessageHubClient
    {
        public void SendNotification(string userId, Toaster message);
        public void SendNotification(Guid guid, PendingNotificationItem message);
    }
}
