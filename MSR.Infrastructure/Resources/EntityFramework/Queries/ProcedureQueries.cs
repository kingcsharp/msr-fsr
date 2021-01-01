using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Queries
{
    public static class ProcedureQueries
    {
        public static IQueryable<ProcedureWithUsedProductCount> GetProceduresWithProductCount(this DbSet<ProcedureWithUsedProductCount> dbSet)
        {
            try
            {

                return dbSet.FromSqlRaw(@"
                        SELECT
                            [procedure].Name,
                            [procedure].ProcedureTypeId,
                            [procedure].Revision,
                            [procedure].Duration,
                            [procedure].DurationType,
                            [procedure].LastUpdatedOn,
                            [procedure].LastUpdatedBy,
                            [procedure].Id,
                            [procedure].CreatedBy,
                            [procedure].CreatedOn,
                            (
                                SELECT COUNT([product].id)
                                FROM [product]
                                WHERE [procedure].id = [product].ProcedureId
                            ) AS CountProductsUsing
                        FROM [procedure]
                ");
            }
            catch(Exception ex)
            {
                throw new DomainException(ex.Message, DomainError.InternalServerError);
            }
        }
    }
}
