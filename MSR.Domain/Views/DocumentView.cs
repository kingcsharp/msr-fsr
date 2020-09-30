using MSR.Domain.Models;
using MSR.Domain.Models.BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Views
{
    public class DocumentView : TrackableModel
    {
        public DocumentView()
        {
            RoleIds = new HashSet<int>();
            ReferenceFiles = new HashSet<FileModel>();
        }

        public string Name { get; set; }

        public int Revision { get; set; }

        public string Comments { get; set; }

        public ICollection<int> RoleIds { get; set; }

        public ICollection<FileModel> ReferenceFiles { get; set; }

        [JsonIgnore]
        public ICollection<int> ReferenceFlieIds { get; set; }
    }
}
