using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.Query
{
    public class QueryBase
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<QuerySort> Sort { get; set; }
        public IEnumerable<QueryFilter> Filters { get; set; }
    }
}
