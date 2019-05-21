using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.PurchesOrder
{
    public class ProductsCanPurchase
    {
        public string Name { get; set; }
        public string Order_id { get; set; }
        public string Customer_co { get; set; }
        public string supplier_id { get; set; }
        public decimal? Price { get; set; }
        public string Customer_root_name { get; set; }
        public string supplier_name { get; set; }
        public string Root_co_id { get; set; }
        public string Progress { get; set; }
        public DateTime? expriation_date { get; set; }
    }
}
