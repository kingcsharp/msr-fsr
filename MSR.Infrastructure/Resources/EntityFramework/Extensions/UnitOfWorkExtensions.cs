
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static async Task<User> GetLoggedInUserAsync(this IUnitOfWork unitOfWork)
        {
            var userId = DelegateHandler.GetCurrentUserId();

            var user = await unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == userId);

            return user;
        }

        public static async Task<bool> LogApprovalTransaction<T>(this IUnitOfWork unitOfWork, T entity, int entityId, string status = "Approved")
        {
            var user = await unitOfWork.GetLoggedInUserAsync();
            string approvalEntity = entity.GetType().Name;

            // DB field is limited to 20 characters
            if (approvalEntity.Length > 20) {
                approvalEntity = approvalEntity.Substring(0,20);
            }

            var log = new ApprovalTransactionLog()
            {
                ApprovalEntity = approvalEntity,
                ApprovalEntityId = entityId,
                ApprovalResult = status,
                ProcessedBy = user,
                ProcessedById = user.Id,
                ProcessedOn = DateTimeOffset.UtcNow
            };

            unitOfWork.ApprovalTransactionLogs.Add(log);
            await unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
