using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool GroupPo { get; set; }
    }
}
