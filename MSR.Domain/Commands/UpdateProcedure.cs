using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateProcedure : Command
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsRelatedToAProduct { get; set; }

        public int? ProcedureTypeId { get; set; }

        public int? Revision { get; set; }

        public string Comments { get; set; }

        public ICollection<int> RoleIds { get; set; }

        public double? Duration { get; set; }

        public string DurationType { get; set; }

        public ICollection<int> ReferenceFileIds { get; set; }
        
        public ICollection<FileModel> ReferenceFiles { get; set; }
    }
}
