using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class PartQueries
    {
        public static IQueryable<Part> CreatePartQuery(this IQueryable<Part> query, GetParts command, bool forRowCount = false) {
            
            query = query.Include(x => x.Subparts).AsQueryable();

            query = query.Where<Part>(command.CreatedByName, s => s.Created.FirstName.Contains(command.CreatedByName));
            query = query.Where<Part>(command.CreatedOn, s => s.CreatedOn == command.CreatedOn);
            query = query.Where<Part>(command.EnumSegregationType, s => s.SegregationType == EnumUtils.GetDescription(command.EnumSegregationType.Value));
            query = query.Where<Part>(command.Id, s => s.Id == command.Id);
            query = query.Where<Part>(command.IsActive, s => s.IsActive == command.IsActive);
            query = query.Where<Part>(command.IsKit, s => s.IsKit == command.IsKit);
            query = query.Where<Part>(command.LastUpdatedByName, s => s.LastUpdated.FirstName == command.LastUpdatedByName);
            query = query.Where<Part>(command.LastUpdateOn, s => s.LastUpdatedOn == command.LastUpdateOn);
            query = query.Where<Part>(command.MaximumCycles, s => s.MaximumCycles == command.MaximumCycles);
            query = query.Where<Part>(command.Name, s => s.Name == command.Name);
            query = query.Where<Part>(command.OEMPartNumber, s => s.OEMPartNumber == command.OEMPartNumber);
            query = query.Where<Part>(command.PartNumber, s => s.PartNumber == command.PartNumber);

            if(command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term)) {
                query = query.OrderBy<int,Part> (command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.Id), s => s.Id);
                query = query.OrderBy<string, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.CreatedByName), s => s.Created.FirstName);
                query = query.OrderBy<DateTime, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy<string, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.EnumSegregationType), s => s.SegregationType);
                query = query.OrderBy<bool, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.IsActive), s => s.IsActive);
                query = query.OrderBy<bool, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.IsKit), s => s.IsKit);
                query = query.OrderBy<string, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.LastUpdatedByName), s => s.LastUpdated.FirstName);
                query = query.OrderBy<DateTime?, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.LastUpdateOn), s => s.LastUpdatedOn);
                query = query.OrderBy<int?, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.MaximumCycles), s => s.MaximumCycles);
                query = query.OrderBy<string, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.Name), s => s.Name);
                query = query.OrderBy<string, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.OEMPartNumber), s => s.OEMPartNumber);
                query = query.OrderBy<string, Part>(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.PartNumber), s => s.PartNumber);
            }
            

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }

    }
}
