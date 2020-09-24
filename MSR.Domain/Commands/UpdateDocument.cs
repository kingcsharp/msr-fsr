using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateDocument : Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Revision { get; set; }
        public string Comments { get; set; }
        public List<int?> RoleIds { get; set; }
        public List<int?> ReferenceFileIds { get; set; }
    }
}
