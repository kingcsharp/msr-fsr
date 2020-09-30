using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdatePurchaseOrderRequest: CreatePurchaseOrderRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
