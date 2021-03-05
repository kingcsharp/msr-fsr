using MSR.Domain.Commanding;
using MSR.Domain.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public static (IQueryable<T> data, int totalRows) ToFilterView<T>(
            this IQueryable<T> query, QueryBase filter)
        {
            query = Filter(query, filter.Filters);
            var totalRows = query.Count();
            //sort
            if (filter.Sort != null)
            {
                query = Sort(query, filter.Sort);
                // EF does not apply skip and take without order
                query = Limit(query, filter.PageSize, filter.PageNumber);
            }

            return (query, totalRows);

        }

        private static IQueryable<T> Filter<T>(
            IQueryable<T> queryable, IEnumerable<QueryFilter> filterList)
        {
            if ((filterList != null) && (filterList.Any()))
            {
                var filters = filterList.Where(i => i.Logic != null).ToList();
                var values = new string[filters.Count];

                var where = "1=1";
                var i = 0;
                foreach (var filter in filters)
                {
                    values[i] = filter.Value;
                    where += $" {filter.Logic} ({Transform(filter, i)})";
                    i++;
                }

                queryable = queryable.Where(where, values);
            }
            return queryable;
        }

        private static IQueryable<T> Sort<T>(
            IQueryable<T> queryable, IEnumerable<QuerySort> sort)
        {
            if (sort != null && sort.Any())
            {
                var ordering = string.Join(",", sort.Select(s => $"{s.Field} {s.Dir}"));
                return queryable.OrderBy(ordering);
            }
            return queryable;
        }

        private static IQueryable<T> Limit<T>(IQueryable<T> queryable, int limit, int offset)
        {
            return queryable.Skip(offset).Take(limit);
        }

        private static readonly IDictionary<string, string>
        Operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "Contains"},
        };

        //public static IList<QueryFilter> GetAllFilters(QueryFilter filter)
        //{
        //    var filters = new List<QueryFilter>();
        //    GetFilters(filter, filters);
        //    return filters;
        //}

        //private static void GetFilters(QueryFilter filter, IList<QueryFilter> filters)
        //{
        //    if (filter.Filters != null && filter.Filters.Any())
        //    {
        //        foreach (var item in filter.Filters)
        //        {
        //            GetFilters(item, filters);
        //        }
        //    }
        //    else
        //    {
        //        filters.Add(filter);
        //    }
        //}

        public static string Transform(QueryFilter filter, int index)
        {
            var comparison = Operators[filter.Operator];
            if (filter.Operator == "doesnotcontain")
            {
                return String.Format("({0} != null && !{0}.ToString().{1}(@{2}))",
                    filter.Field, comparison, index);
            }
            if (comparison == "StartsWith" ||
                comparison == "EndsWith" ||
                comparison == "Contains")
            {
                return String.Format("({0} != null && {0}.ToString().{1}(@{2}))",
                filter.Field, comparison, index);
            }
            return String.Format("{0} {1} @{2}", filter.Field, comparison, index);
        }
    }
}

