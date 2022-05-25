using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models.Query;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class ProcedureQueries
    {
        public async static Task<ICollection<ProcedureWithUsedProductCountView>> GetProceduresWithProductCount(this DbSet<Procedure> dbSet, Expression<Func<Procedure, dynamic>> projection)
        {
            try
            {
                var dynamicData = await dbSet.Select(projection)
                    .ToListAsync();

                var procedureViews = JsonConvert.DeserializeObject<ICollection<ProcedureWithUsedProductCountView>>(JsonConvert.SerializeObject(dynamicData));

                return procedureViews;
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message, DomainError.InternalServerError);
            }
        }

        public static IQueryable<ProcedureType> CreateProcedureTypeQuery(this IQueryable<ProcedureType> query, GetProcedureType command, bool forRowCount = false)
        {

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureTypeSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureTypeSortFields.Name), s => s.Name);
            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }

        public static IQueryable<MSR.Domain.Models.Procedure> CreateProcedureQuery(this IQueryable<MSR.Domain.Models.Procedure> query, GetProcedure command, bool forRowCount = false)
        {

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.Name, s => s.Name.ToLower().Contains(command.Name.ToLower()));
            query = query.Where(command.ProcedureTypeName, s => s.ProcedureType.Name.ToLower().Contains(command.ProcedureTypeName.ToLower()));
            query = query.Where(command.Duration, s => s.Duration == command.Duration);
            query = query.Where(command.DurationType, s => s.DurationType.ToLower().Contains(command.DurationType.ToLower()));
            query = query.Where(command.Revision, s => s.Revision == command.Revision);
            query = query.Where(command.CreatedFullName, s => s.Created.FullName.ToLower().Contains(command.CreatedFullName.ToLower()));
            query = query.Where(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date, command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.LastUpdatedFullName, s => s.LastUpdated.FullName.ToLower().Contains(command.LastUpdatedFullName.ToLower()));
            query = query.Where(command.LastUpdatedOn, s => DateTime.Compare(s.LastUpdatedOn.Value.Date, command.LastUpdatedOn.Value.Date) == 0);

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.ProcedureTypeName),s => s.ProcedureType == null ? "" : s.ProcedureType.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.Duration), s => s.Duration);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.DurationType), s => s.DurationType);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.Revision), s => s.Revision);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.CreatedFullName), s => s.Created == null ? "" : s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.LastUpdatedFullName), s => s.LastUpdated == null ? "" : s.LastUpdated.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureSortFields.LastUpdatedOn), s => s.LastUpdatedOn);
            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }

        public static async Task<ICollection<Domain.Models.Procedure>> ExportProcedures(this DbSet<Procedure> dbSet, Expression<Func<Procedure, dynamic>> projection, QueryBase filters)
        {
            try
            {
                var dynamicData = await dbSet.Filter(filters.Filters).Select(projection).ToListAsync();
                var procedureViews = JsonConvert.DeserializeObject<ICollection<Domain.Models.Procedure>>(JsonConvert.SerializeObject(dynamicData));
                return procedureViews;
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message, DomainError.InternalServerError);
            }

        }
    }
}
