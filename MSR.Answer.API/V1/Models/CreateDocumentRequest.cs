using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MSR.Domain.Models;
using Newtonsoft.Json;

namespace MSR.Answer.API.V1.Models
{ 
    public class CreateDocumentRequest 
    {
        public CreateDocumentRequest()
        {
            RoleIds = new List<int?>();
            ReferenceFileIds = new List<int?>();
            ReferenceFiles = new List<FileModel>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public int? Revision { get; set; }

        public string Comments { get; set; }

        public ICollection<int?> RoleIds { get; set; }

        public ICollection<int?> ReferenceFileIds { get; set; }

        public ICollection<FileModel> ReferenceFiles { get; set; }

    }
}
