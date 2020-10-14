using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateProductRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int? Revision { get; set; }

        [Required]
        public int? CustomerId { get; set; }

        public int? CustomerRequirementId { get; set; }

        [Required]
        public int? ProcedureId { get; set; }

        [Required]
        public int? PartId { get; set; }

        [Required]
        public decimal? LaborCost { get; set; }

        [Required]
        public decimal? EquipmentCost { get; set; }

        [Required]
        public decimal? MaterialCost { get; set; }

        public decimal? SalesTax { get; set; }

        [Required]
        public decimal? TotalSalePrice { get; set; }

        public int? CycleTime { get; set; }

        public int? QuoteId { get; set; }

        public string DivisionFab { get; set; }

        public ICollection<ProductStep> ProductSteps { get; set; }
    }
}
