using System;
using System.Linq;

namespace Msr.Services.jqGrid
{
    public static class GridParser
    {
        public static IPageResult<T> ApplyPaging<T>(this IQueryable<T> entities, JqGridParam param)
        {
            entities = param.sortOrder == "desc" ? entities.OrderByDescending(param.sortColumn) : entities.OrderBy(param.sortColumn);

            var totalRecords = entities.Count();

            entities = entities.Skip(param.pageSize * (param.pageIndex - 1));

            entities = entities.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling(a: (float)totalRecords / param.pageSize);

            var results = entities.ToList();

            var result = new PageResult<T>
            {
                Total = totalPages,
                Page = param.pageIndex,
                Records = totalRecords,
                rows = results
            };

            return result;
        }

        public static bool HasGridFilters(this JqGridParam param)
        {
            return param.where != null && param.where.rules.Any();
        }
    }
}
