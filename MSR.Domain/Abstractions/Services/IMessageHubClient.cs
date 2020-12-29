using System;
using System.Threading.Tasks;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Hub;
using MSR.Domain.Models;

namespace MSR.Domain.Abstractions.Services
{
    public interface IMessageHubClient
    {
        public Task SendNotification(string userId, Toaster message);
        public Task SendNotification(Guid guid, PendingNotificationItem message);
        public void SendApprovalNotification(EnumApprovalTables approvalTable, int count = 1);
    }
}
