using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class RoleQueries
    {
        public static IQueryable<Role> CreateRoleQuery(this IQueryable<Role> query, GetRoles command, bool forRowCount = false)
        {

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                return query;
            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
