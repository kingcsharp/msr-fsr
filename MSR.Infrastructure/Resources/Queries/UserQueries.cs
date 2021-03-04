using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Linq;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class UserQueries
    {
        public static IQueryable<User> CreateUserQuery(this IQueryable<User> query, GetUsers command, bool forRowCount = false)
        {
            query = query.Include(x => x.TimeZone).Include(x => x.Location).Include(x => x.Supervisor).Include(x => x.Roles).ThenInclude(x => x.Role).AsQueryable();

            query = query.Where<User>(command.Id, s => s.Id == command.Id);
            query = query.Where<User>(command.FirstName, s => s.FirstName == command.FirstName);
            query = query.Where<User>(command.LastName, s => s.LastName == command.LastName);
            query = query.Where<User>(command.LastName, s => s.UserName == command.UserName);
            query = query.Where<User>(command.Title, s => s.Title == command.Title);
            query = query.Where<User>(command.Supervisor, s => s.SupervisorId == command.Supervisor);
            query = query.Where<User>(command.PrimaryPhone, s => s.Phone == command.PrimaryPhone);
            query = query.Where<User>(command.Email, s => s.Email == command.Email);
            query = query.Where<User>(command.HasRoleIDs, s => s.Roles.Any(m => command.HasRoleIDs.Contains(m.RoleId)));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.IsActive), s => s.IsActive);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.isAnswerUser), s => s.IsAnswerUser);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.FirstName), s => s.FirstName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.LastName), s => s.LastName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.UserName), s => s.UserName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.Email), s => s.Email);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.Roles), s => s.Roles.FirstOrDefault().Role.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.LocationName), s => s.Location.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumUserSortFields.SupervisorName), s => s.Supervisor.FirstName);

            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
