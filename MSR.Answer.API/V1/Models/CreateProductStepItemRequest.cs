using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateProductStepItemRequest
    {
        public int PrintOrder { get; set; }
        public string Title { get; set; }
        public int? LaborTime { get; set; }
        public int? EquipmentTime { get; set; }
        public decimal? ReplacementCost { get; set; }
        public decimal? Utilization { get; set; }
        public decimal? UsefulLife { get; set; }
    }
}
