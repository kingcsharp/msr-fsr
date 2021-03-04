using Microsoft.EntityFrameworkCore;
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
    public static class HelpQueries
    {
        public static IQueryable<EntityFramework.Entities.HelpPage> CreateHelpQuery(this IQueryable<EntityFramework.Entities.HelpPage> query, GetHelpPage command, bool forRowCount = false)
        {
            query = query.Include(s => s.Roles).AsQueryable();

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.FriendlyURL, s => s.FriendlyUrl.Contains(command.FriendlyURL));
            query = query.Where(command.Roles, s => s.Roles.Any(m => command.Roles.Contains(m.Id)));
            query = query.Where(command.Title, s => s.Title.Contains(command.Title));


            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumHelpSortingFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumHelpSortingFields.FriendlyURL), s => s.FriendlyUrl);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumHelpSortingFields.Roles), s => s.Roles.FirstOrDefault().Role.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumHelpSortingFields.Title), s => s.Title);

            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
