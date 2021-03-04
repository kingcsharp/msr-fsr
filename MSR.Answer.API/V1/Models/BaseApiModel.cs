using MSR.Answer.API.V1.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class BaseApiModel
    {
        public string? Term { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public bool? SortAscending { get; set; }
    }
}
