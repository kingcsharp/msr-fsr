using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Helpers
{
    public static class QueryHelper
    {
        public static async Task<TResult> GetViewDataFor<T,TResult>(DbSet<T> dbSet, Expression<Func<T, dynamic>> projection) where T: Entity
        {
            try
            {
                var dynamicData = await dbSet.Select(projection).ToListAsync();
                var data = JsonConvert.DeserializeObject<TResult>(JsonConvert.SerializeObject(dynamicData));
                return data;
            }
            catch (Exception ex)
            {
                throw new DomainException($"{ex}", DomainError.InternalServerError);
            }
        }
    }
}
