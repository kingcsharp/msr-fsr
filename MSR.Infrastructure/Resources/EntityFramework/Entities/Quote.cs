using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Quote))]
    public class Quote : Entity
    {
        [Required]
        public DateTime SubmittedDate { get; set; }

        [Required]
        public int SubmittedById { get; set; }

        [ForeignKey("SubmittedById")]
        public virtual User SubmittedBy { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [StringLength(50)]
        public string PartKitNo { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Representative { get; set; }

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public string QuoteJson { get; set; }

        public string CustomerRequirementJson { get; set; }

        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
