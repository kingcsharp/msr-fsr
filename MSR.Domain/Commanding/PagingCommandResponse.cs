using System;
using System.Collections.Generic;
using System.Text;
using MSR.Domain.Commanding.Abstractions;

namespace MSR.Domain.Commanding
{
    public class PagingCommandResponse<T> : CommandResponse<T>, IPagingCommandResponse<T>
    {

        public int? TotalRows { get; set; }
        public string? Term { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public bool? SortAscending { get; set; }

        public PagingCommandResponse(T data, int totalRows, string? term, int? pageNumber, int? pageSize,
            bool? sortAscending) : base(data)
        {
            TotalRows = totalRows;
            Term = term;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortAscending = sortAscending;
        }

        public int? Skip
        {
            get
            {
                if (PageNumber == null || PageSize == null)
                {
                    return null;
                }

                return (int)(PageNumber * PageSize);
            }
        }

        public int? Take
        {
            get
            {
                if (PageSize == null)
                {
                    return null;
                }

                return (int)PageSize;
            }
        }

    }
}
