using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateInvoiceItemRequest
    {
        [Required]
        public int PurchaseOrderId { get; set; }

        [Required]
        public int WorkOrderId { get; set; }
    }
}
