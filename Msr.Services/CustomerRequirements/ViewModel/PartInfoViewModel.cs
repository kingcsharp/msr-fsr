using System.ComponentModel.DataAnnotations;

namespace Msr.Services.CustomerRequirements.ViewModel
{
    public class PartInfoViewModel
    {
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string PartDescription { get; set; }
        public string Substrate { get; set; }
        public string CoatingSurface { get; set; }
        [Required]
        public string CustPartNo { get; set; }
        public string MfgPartNo { get; set; }
        public string PartsPerKit { get; set; }
    }
}
