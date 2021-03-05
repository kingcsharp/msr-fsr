using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class WorkflowQueries
    {
        public static IQueryable<WorkflowModel> CreateWorkflowApprovalQuery(this IQueryable<WorkflowModel> query, GetWorkflowModel command, bool forRowCount = false)
        {

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.MemberStages, s => s.MemberStages.Any(m => command.MemberStages.Contains(m.WorkflowStageId)));
            query = query.Where(command.ActivityMaps, s => s.ActivityMaps.Any(m => command.ActivityMaps.Contains(m.WorkflowActivityId)));
            query = query.Where(command.IsActive, s => s.IsActive == command.IsActive);
            query = query.Where(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date, command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.CreatedByName, s => s.CreatedByName.Contains(command.CreatedByName));
            query = query.Where(command.LastUpdatedOn, s => DateTime.Compare(s.LastUpdatedOn.Value.Date, command.LastUpdatedOn.Value.Date) == 0);
            query = query.Where(command.LastUpdatedByName, s => s.LastUpdatedByName.Contains(command.LastUpdatedByName));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.MemberStages), s => s.MemberStages.FirstOrDefault().WorkflowStageName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.ActivityMaps), s => s.ActivityMaps.FirstOrDefault().WorkflowActivityName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.IsActive), s => s.IsActive);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.CreatedByName), s => s.CreatedByName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.LastUpdatedOn), s => s.LastUpdatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumApprovalWorkflowsSortingFields.LastUpdatedByName), s => s.LastUpdatedByName);
            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }

        public static IQueryable<WorkflowStage> CreateWorkflowStagesQuery(this IQueryable<WorkflowStage> query, GetWorkflowStageModel command, bool forRowCount = false)
        {
            query = query.Include(x => x.Group).AsQueryable();

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.IsActive, s => s.IsActive == command.IsActive);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date,command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.CreatedByName, s => s.Created.FullName.Contains(command.CreatedByName));
            query = query.Where(command.LastUpdatedOn, s => DateTime.Compare(s.LastUpdatedOn.Value.Date,command.LastUpdatedOn.Value.Date) == 0);
            query = query.Where(command.LastUpdatedByName, s => s.LastUpdated.FullName.Contains(command.LastUpdatedByName));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.IsActive), s => s.IsActive);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.CreatedOn), s => s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.CreatedByName), s => s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.LastUpdatedOn), s => s.LastUpdatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.LastUpdatedByName), s => s.LastUpdated.FullName);
            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }

        public static IQueryable<WorkflowGroup> CreateWorkflowGroupQuery(this IQueryable<WorkflowGroup> query, GetWorkflowGroupsModel command, bool forRowCount = false)
        {
            query = query.Include(x => x.GroupRoles).Include(x => x.GroupUsers).AsQueryable();

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.IsActive, s => s.IsActive == command.IsActive);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date, command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.CreatedByName, s => s.Created.FullName.Contains(command.CreatedByName));
            query = query.Where(command.LastUpdatedOn, s => DateTime.Compare(s.LastUpdatedOn.Value.Date, command.LastUpdatedOn.Value.Date) == 0);
            query = query.Where(command.LastUpdatedByName, s => s.LastUpdated.FullName.Contains(command.LastUpdatedByName));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.IsActive), s => s.IsActive);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.CreatedOn), s => s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.CreatedByName), s => s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.LastUpdatedOn), s => s.LastUpdatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumWorkflowStageSortFields.LastUpdatedByName), s => s.LastUpdated.FullName);
            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }

    }

}
