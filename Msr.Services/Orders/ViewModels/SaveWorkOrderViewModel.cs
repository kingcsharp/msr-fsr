using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Orders.ViewModels
{
    public class SaveWorkOrderViewModel
    {
        public string PurchaseItemId { get; set; }
        public string CustPurchNum { get; set; }
        [Required]
        public string Qty { get; set; }
        public string DueDate { get; set; }
        public string NTLogin { get; set; }
    }
}
