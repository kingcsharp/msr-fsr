using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MSR.Domain.Helpers
{
    public static class IQueryableHelpers
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> source, string sortField, bool ascending)
        {
            var root = Expression.Parameter(typeof(T), "x");
            //with this i can sort with eg OrderBy = "Customer.Name"
            var member = sortField.Split('.').Aggregate((Expression)root, Expression.PropertyOrField);
            var selector = Expression.Lambda(member, root);
            var method = ascending ? "OrderBy" : "OrderByDescending";
            var types = new[] { typeof(T), member.Type };
            var mce = Expression.Call(typeof(Queryable), method, types,
                source.Expression, Expression.Quote(selector));
            return source.Provider.CreateQuery<T>(mce);
        }
    }
}
