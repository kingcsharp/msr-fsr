using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;
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
    }
}
