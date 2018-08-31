using System.Collections.Generic;

namespace Msr.Services.jqGrid
{
    public interface IPageResult<T>
    {
        int Total { get; set; }
        int Page { get; set; }
        int Records { get; set; }
        List<T> rows { get; set; }
    }
}
