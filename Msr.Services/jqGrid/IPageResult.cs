using System.Collections.Generic;

namespace Msr.Services.jqGrid
{
    public interface IPageResult<T>
    {
        int total { get; set; }
        int page { get; set; }
        int records { get; set; }
        List<T> rows { get; set; }
    }
}
