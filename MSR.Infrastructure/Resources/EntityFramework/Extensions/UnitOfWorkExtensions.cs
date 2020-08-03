using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
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
            var userId = CurrentUser.GetId();

            var user = await unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == userId);

            return user;
        }

        public static async Task<bool> LogApprovalTransaction<T>(this IUnitOfWork unitOfWork, T entity, int entityId, string status = "Approved", string comments = null)
        {
            if (CurrentUser.GetId == null)
            {
                throw new Domain.Exceptions.DomainException("User not logged in", DomainError.BadRequest);
            }
            var log = new ApprovalTransactionLog()
            {
                ApprovalEntity = entity.GetType().Name.Replace("Proxy", ""),
                ApprovalEntityId = entityId,
                ApprovalResult = status,
                ProcessedById = CurrentUser.GetId(),
                Comments = comments,
                ProcessedOn = DateTimeOffset.UtcNow
            };

            unitOfWork.ApprovalTransactionLogs.Add(log);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public static async Task<Workflow> GetWorkflowForEntityAsync<T>(this IUnitOfWork unitOfWork, T entity)
        {
            if (CurrentUser.GetId == null)
            {
                throw new Domain.Exceptions.DomainException("User not logged in", DomainError.BadRequest);
            }

            var workFlow = await unitOfWork.WorkflowActivityMaps.Query()
                                                                .Include(i => i.WorkflowActivity)
                                                                .Include(i => i.Workflow)
                                                                .FirstOrDefaultAsync(i => i.WorkflowActivity != null && i.WorkflowActivity.ApprovalTableName.Equals(entity.GetType().Name));

            return workFlow.Workflow;
        }

        public static async Task<WorkflowGroup> GetWorkFlowGroupForWorkFlow(this IUnitOfWork unitOfWork, int workFlowId)
        {
            if (CurrentUser.GetId == null)
            {
                throw new Domain.Exceptions.DomainException("User not logged in", DomainError.BadRequest);
            }

            var workFlowStage = await unitOfWork.WorkflowStageMaps.Query()
                                                                  .Include(i => i.WorkflowStage)
                                                                  .Include(i => i.Workflow)
                                                                  .FirstOrDefaultAsync(x => x.Workflow.Id == workFlowId);

            if(workFlowStage is null)
            {
                return null;
            }

            var workFlowGroup = await unitOfWork.WorkflowGroupStageMaps.Query()
                                                                       .Include(i => i.WorkflowStage)
                                                                       .Include(i => i.WorkflowGroup)
                                                                       .FirstOrDefaultAsync(i => i.WorkflowStageId == workFlowStage.WorkflowStageId);
            if (workFlowGroup is null)
            {
                return null;
            }

            return workFlowGroup.WorkflowGroup;
        }
    }
}
