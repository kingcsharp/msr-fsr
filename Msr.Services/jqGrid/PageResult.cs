using System.Collections.Generic;

namespace Msr.Services.jqGrid
{
    public class PageResult<T> : IPageResult<T>
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<T> rows { get; set; }
    }
}
