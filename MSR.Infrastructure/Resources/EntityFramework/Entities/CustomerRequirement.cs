using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(CustomerRequirement))]
    public partial class CustomerRequirement
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime SubmittedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string SubmittedBy { get; set; }

        [StringLength(50)]
        public string CustomerId { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(50)]
        public string Division { get; set; }

        [StringLength(50)]
        public string PartKitNo { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Respresentative { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "ntext")]
        public string QuoteJson { get; set; }

        [Column(TypeName = "ntext")]
        public string CustomerRequirementJson { get; set; }

        [StringLength(50)]
        public string SupplierId { get; set; }

        [StringLength(50)]
        public string LocationId { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string PartId { get; set; }

        [StringLength(50)]
        public string ProcedureId { get; set; }

        [StringLength(50)]
        public string ProductWorkflowId { get; set; }

        public double? LeadTime { get; set; }

        public double? Price { get; set; }
    }
}
