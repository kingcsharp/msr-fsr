using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models.Paging
{
    public class QueryRequestBase
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<Sort> Sort { get; set; }
        public IEnumerable<Filter> Filters { get; set; }
    }
}
