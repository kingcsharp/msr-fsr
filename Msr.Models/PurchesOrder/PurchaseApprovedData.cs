using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.PurchesOrder
{
    public class PurchaseApprovedData
    {
        public string ID { get; set; }
        public string STATUS { get; set; }
        public string HISTORY_REF_ID { get; set; }
        public DateTime? DATE_CREATED { get; set; }
        public string PURCHASE_STATUS { get; set; }
        public string ORDER_ID { get; set; }
        public string OBJECT_ID { get; set; }
        public Decimal? PURCHASE_TOTAL { get; set; }
        public string PURCHASER { get; set; }
        public string PURCHASING_CO { get; set; }
        public string CUSTOMER_PERSON { get; set; }
        public string CUSTOMER_CO { get; set; }
        public string DESCRIPTION { get; set; }
        public string PROGRESS { get; set; }
        public string CUST_PURCH_NUM { get; set; }
        public string SUP_PURCH_NUM { get; set; }
        public string ACCT_FOR_ALL { get; set; }
    }
}
