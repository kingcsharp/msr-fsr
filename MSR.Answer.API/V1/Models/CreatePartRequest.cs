using MSR.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreatePartRequest
    {
        public CreatePartRequest()
        {
            CreateSubParts = new List<SubPartModel>();
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public string NickName { get; set; }
        public int? MaximumCycles { get; set; }
        public virtual ICollection<SubPartModel> CreateSubParts { get; set; }
    }
}
