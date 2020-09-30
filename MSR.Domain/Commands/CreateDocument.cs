using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateDocument : Command
    {
        public CreateDocument()
        {
            RoleIds = new List<int>();
            ReferenceFileIds = new List<int>();
            ReferenceFiles = new List<FileModel>();
        }

        public string Name { get; set; }
        public int? Revision { get; set; }
        public string Comments { get; set; }
        public ICollection<int> RoleIds { get; set; }
        public ICollection<int> ReferenceFileIds { get; set; }
        public ICollection<FileModel> ReferenceFiles { get; set; }
    }
}
