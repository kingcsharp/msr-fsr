using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commanding.Abstractions
{
    public interface IPagingCommandResponse<TResult> : ICommandResponse<TResult>
    {
        public int? TotalRows { get; set; }
        public string? Term { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public bool? SortAscending { get; set; }
        public int? Skip { get; }
        public int? Take { get; }

    }
}
