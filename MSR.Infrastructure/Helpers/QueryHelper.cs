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

        public static async Task<(TResult data, int totalRows)> GetPagedViewDataFor<T, TResult>(DbSet<T> dbSet, Expression<Func<T, dynamic>> projection, int skip = 0, int take = 0) where T : Entity
        {
            try
            {
                var dynamicDataQuery = dbSet.Select(projection);
                var totalRows = dynamicDataQuery.Count();
                if (take > 0)
                {
                    //If Skip is 0 it means we want the first X records, so that is why we don't check for that. Only take has to be > 0 for paging to be set.
                    dynamicDataQuery = dynamicDataQuery.Skip(skip).Take(take);
                }

                var dynamicData = await dynamicDataQuery.ToListAsync();
                var data = JsonConvert.DeserializeObject<TResult>(JsonConvert.SerializeObject(dynamicData));
                return (data, totalRows);
            }
            catch (Exception ex)
            {
                throw new DomainException($"{ex}", DomainError.InternalServerError);
            }
        }
    }
}
