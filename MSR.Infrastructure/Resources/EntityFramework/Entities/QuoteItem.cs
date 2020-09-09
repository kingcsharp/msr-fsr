using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(QuoteItem))]
    public partial class QuoteItem : Entity
    {
        [Required]
        public int? QuoteId { get; set; }

        [ForeignKey("QuoteId")]
        public virtual Quote Quote { get; set; }

        [MaxLength(50)]
        public string ItemNo { get; set; }

        public int? Qty { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string LeadTime { get; set; }
        [MaxLength(50)]
        public string CustomerPartNo { get; set; }

        public decimal? Price { get; set; }

        public decimal? Extension { get; set; }
    }
}
