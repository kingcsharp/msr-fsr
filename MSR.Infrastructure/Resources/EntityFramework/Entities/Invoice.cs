using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Invoice))]
    public partial class Invoice: TrackableEntity
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int StatusId { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public DateTime InvoiceDate { get; set; }

        [Required]
        [StringLength(10)]
        public string InvoiceClass { get; set; }

        [Column(TypeName = "money")]
        public decimal Subtotal { get; set; }

        public decimal? TaxPercentage { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

        public virtual Status Status { get; set; }
    }
}
