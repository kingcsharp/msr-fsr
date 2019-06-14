using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Services.PurchesOrder.ViewModels
{
   public class PurchasePoViewModel
    {
        public string ACCOUNT_OBJECT_ID { get; set; }
        public string ORDER_ID { get; set; }
        public string Name { get; set; }
        public string ACCT_ID { get; set; }
        public string Progress { get; set; }
        public DateTime? Expration_Date { get; set; }
        [Required]
        public float? Qty { get; set; }
        public bool GroupWO { get; set; }

    }
}
