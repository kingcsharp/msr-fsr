using System.Collections.Generic;

namespace Msr.Services.jqGrid
{
    public class PageResult<T> : IPageResult<T>
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Records { get; set; }
        public List<T> rows { get; set; }
    }
}
