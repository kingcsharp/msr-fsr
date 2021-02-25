using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class BaseApiModel
    {
        public BaseApiModel()
        {
            PageNumber = 0;
            PageSize = 25;
            Term = string.Empty;
        }
        public string Term { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FilterProperty { get; set; }

        public int Skip
        {
            get { return PageNumber * PageSize; }
        }

        public int Take
        {
            get { return PageSize; }
        }

        public bool SortAscending { get; set; }
    }
}
