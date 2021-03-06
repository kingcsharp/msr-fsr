using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
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
    }
}
