using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreatePartRequest
    {
        public CreatePartRequest()
        {
            CreateSubParts = null;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public string NickName { get; set; }
        public int? MaximumCycles { get; set; }
        public virtual ICollection<SubPartModel> CreateSubParts { get; set; }
        public string Comment { get; set; }
        public List<File> Files { get; set; }
    }
}
