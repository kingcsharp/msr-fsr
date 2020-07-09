using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdatePartRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public int Qty { get; set; }
        public string NickName { get; set; }
        public int? ParentId { get; set; }
        public int? MaximumCycles { get; set; }
    }
}
