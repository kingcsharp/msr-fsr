using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.Query
{
    public class QueryFilter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public string Logic { get; set; }
        public IEnumerable<QueryFilter> Filters { get; set; }
    }
}
