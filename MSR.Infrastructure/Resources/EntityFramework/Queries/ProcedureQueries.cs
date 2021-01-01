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
        public static IQueryable<ProcedureExtra> GetProceduresWithProductCount(this DbSet<ProcedureExtra> dbSet)
        {
            try
            {
                return dbSet.FromSqlRaw(@"
                        SELECT [procedure].*, (
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
