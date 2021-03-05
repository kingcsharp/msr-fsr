using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetDocument : PagingCommand
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Revision { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedFullName { get; set; }
    }
}
