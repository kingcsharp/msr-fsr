using MSR.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateFileRequest
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string Name { get; set; }
        public string Base64String { get; set; }
        public string ContentType { get; set; }
    }
}
