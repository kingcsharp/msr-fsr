using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MSR.Answer.API.V1.Models
{ 
    public class UpdateDocumentRequest
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int? Revision { get; set; }

        public string Comments { get; set; }

        public List<int?> RoleIds { get; set; }

        public List<int?> ReferenceFileIds { get; set; }

    }
}
