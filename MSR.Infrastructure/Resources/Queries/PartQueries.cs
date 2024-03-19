using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models.Query;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class PartQueries
    {
        public static IQueryable<Part> CreatePartQuery(this IQueryable<Part> query, GetParts command, bool forRowCount = false) 
        {
            query = query.Include(x => x.Subparts).AsQueryable();
            query = query.Where(command.CreatedByName, s => s.Created.FirstName.Contains(command.CreatedByName));
            query = query.Where(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date,command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.SegregationType, s => command.SegregationType.Select(s => EnumUtils.GetDescription(s)).ToList().Contains(s.SegregationType));
            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.IsActive, s => s.IsActive == command.IsActive);
            query = query.Where(command.IsKit, s => s.IsKit == command.IsKit);
            query = query.Where(command.LastUpdatedByName, s => s.LastUpdated.FullName.Contains(command.LastUpdatedByName));
            query = query.Where(command.lastUpdatedOn, s => DateTime.Compare(s.LastUpdatedOn.Value.Date,command.lastUpdatedOn.Value.Date) == 0);
            query = query.Where(command.MaximumCycles, s => s.MaximumCycles == command.MaximumCycles);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.OemPartNumber, s => s.OEMPartNumber.Contains(command.OemPartNumber));
            query = query.Where(command.PartNumber, s => s.PartNumber.Contains(command.PartNumber));

            if(command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term)) {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.CreatedByName), s => s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.SegregationType), s => s.SegregationType);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.IsActive), s => s.IsActive);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.IsKit), s => s.IsKit);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.LastUpdatedByName), s => s.LastUpdated.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.LastUpdateOn), s => s.LastUpdatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.MaximumCycles), s => s.MaximumCycles);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.OemPartNumber), s => s.OEMPartNumber);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumPartSortFields.PartNumber), s => s.PartNumber);
            }
            

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }

        public static async Task<ICollection<Domain.Models.PartModel>> ExportParts(this DbSet<Part> dbSet, Expression<Func<Part, dynamic>> projection, PartExportQueryFilters filters)
        {
            try
            {
                var segregationTypes = Enum.GetNames(typeof(EnumSegregationType)).ToList();
                var query = dbSet.AsQueryable();

                Expression<Func<Part, bool>> segregationPredicate = null;
                if (filters.SegregationType != null && filters.SegregationType.Length > 0)
                {
                    segregationPredicate = s => filters.SegregationType.Select(x => EnumUtils.GetDescription(x)).Contains(s.SegregationType);
                }

                query = query.Where(filters.SegregationType, segregationPredicate);

                query = query.Where(filters.Id, s => s.Id == filters.Id)
                             .Where(filters.Name, s => s.Name.Contains(filters.Name))
                             .Where(filters.PartNumber, s => s.PartNumber.Contains(filters.PartNumber))
                             .Where(filters.OEMPartNumber, s => s.OEMPartNumber.Contains(filters.OEMPartNumber))
                             .Where(filters.IsKit, s => s.IsKit == filters.IsKit)
                             .Where(filters.IsActive, s => s.IsActive == filters.IsActive)
                             .Where(filters.MaximumCycles, s => s.MaximumCycles == filters.MaximumCycles)
                             .Select(x => new Part 
                             { 
                                 Id = x.Id,
                                 Name = x.Name,
                                 PartNumber = x.PartNumber,
                                 OEMPartNumber = x.OEMPartNumber,
                                 MaximumCycles = x.MaximumCycles,
                                 SegregationType = !string.IsNullOrWhiteSpace(x.SegregationType) ? EnumUtils.GetValueFromDescription<EnumSegregationType>(x.SegregationType).ToString() : "NONCU",
                                 IsActive = x.IsActive,
                                 IsKit = x.IsKit,
                             });

                var dynamicData = await query.Select(projection).ToListAsync();
                var partViews = JsonConvert.DeserializeObject<ICollection<Domain.Models.PartModel>>(JsonConvert.SerializeObject(dynamicData));
                return partViews;
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message, DomainError.InternalServerError);
            }

        }

    }
}
