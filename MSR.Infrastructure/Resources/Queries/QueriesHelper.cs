using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class QueriesHelper
    {
        /// <summary>
        /// Custom OrderBy to test if PagingCommand contains Term and SortAscending values to apply sorting based on predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="query"></param>
        /// <param name="command"></param>
        /// <param name="shouldOrderBy"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<K> OrderBy<T, K>(this IQueryable<K> query, PagingCommand command, bool shouldOrderBy, Expression<Func<K, T>> predicate)
        {

            if (string.IsNullOrEmpty(command.Term) || !command.SortAscending.HasValue || !shouldOrderBy)
            {
                return query;
            }

            if (command.SortAscending.Value)
            {
                return query.OrderBy(predicate);
            }
            else
            {
                return query.OrderByDescending(predicate);
            }

        }

        /// <summary>
        /// Custom Where to test if property is null or string property is empty to apply predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="property"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> query, object property, Expression<Func<T, bool>> predicate)
        {

            if (property is null)
            {
                return query;
            }

            if (property.GetType() == typeof(string))
            {

                if (!string.IsNullOrEmpty(Convert.ToString(property)))
                {
                    return query.Where(predicate);
                }
                else
                {
                    return query;
                }
            }

            return query.Where(predicate);
        }
    }
}
