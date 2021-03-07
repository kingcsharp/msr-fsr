using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.Paging
{
    public class ApiPagingModel<T> : PagingModel
    {
        public ICollection<string> Filters { get; internal set; }
        public ICollection<T> Results { get; set; }
    }
}
