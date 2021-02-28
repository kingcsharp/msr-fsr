using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commanding
{
    public abstract class PagingCommand: Command
    {
        public string? Term { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? FilterProperty { get; set; }

        public int? Skip
        {
            get { 
                    if(PageNumber == null || PageSize == null) { 
                        return null;
                    }

                    return (int)(PageNumber * PageSize); 
                }
        }

        public int? Take
        {
            get { 
                    if(PageSize == null) { 
                        return null;
                    }

                    return (int)PageSize; 
                }
        }

        public bool? SortAscending { get; set; }
    }
}
