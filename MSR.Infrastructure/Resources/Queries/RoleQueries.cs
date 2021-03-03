using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class RoleQueries
    {
        public static IQueryable<EntityFramework.Entities.Role> CreateRoleQuery(this IQueryable<EntityFramework.Entities.Role> query, GetRoles command, bool forRowCount = false)
        {

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.IsCertificationRole, s => s.IsCertificationRole == command.IsCertificationRole);
            query = query.Where(command.ParentRoles, s => s.ParentRoles.Any(m => command.ParentRoles.Contains(m.Id)));
            query = query.Where(command.AssignedUsers, s => s.Users.Any(m => command.AssignedUsers.Contains(m.Id)));
            query = query.Where(command.CreatedOn, s => s.CreatedOn == command.CreatedOn);
            query = query.Where(command.CreatedByName, s => s.Created.FirstName.Contains(command.CreatedByName));
            query = query.Where(command.LastUpdatedOn, s => s.LastUpdatedOn == command.LastUpdatedOn);
            query = query.Where(command.LastUpdatedByName, s => s.LastUpdated.FirstName.Contains(command.LastUpdatedByName));


            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.IsCertificationRole), s => s.IsCertificationRole);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.ParentRoles), s => s.ParentRoles);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.AssignedUsers), s => s.Users.FirstOrDefault().User.FirstName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.CreatedByName), s => s.Created.FirstName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.LastUpdatedOn), s => s.LastUpdatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumRoleSortFields.LastUpdatedByName), s => s.LastUpdated.FirstName);

            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
