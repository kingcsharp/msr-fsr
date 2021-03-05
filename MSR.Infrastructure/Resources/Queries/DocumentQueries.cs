using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class DocumentQueries
    {
        public static IQueryable<Document> CreateDocumentQuery(this IQueryable<Document> query, GetDocument command, bool forRowCount = false)
        {

            query = query.Include(d => d.DocumentEntityMaps)
                .Include(d => d.Roles)
                .ThenInclude(r => r.Role).AsQueryable();

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.LastUpdatedFullName, s => s.LastUpdated.FullName.Contains(command.LastUpdatedFullName));
            query = query.Where(command.LastUpdatedOn, s => DateTime.Compare(s.LastUpdatedOn.Value.Date, command.LastUpdatedOn.Value.Date) == 0);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.Revision, s => s.Revision == command.Revision);

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumDocumentSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumDocumentSortFields.LastUpdatedFullName), s => s.LastUpdated.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumDocumentSortFields.LastUpdatedOn), s => s.LastUpdatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumDocumentSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumDocumentSortFields.Revision), s => s.Revision);
            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
