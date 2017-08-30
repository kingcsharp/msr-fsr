using System;

namespace Msr.Models.Delivery
{
    public class DeliveryScreenView
    {
        public string TASK_ID { get; set; }
        public string PURCHASE_ITEM_ID { get; set; }
        public string CUST_PURCH_NUM { get; set; }
        public string COMPANY_PART_NUMBER { get; set; }
        public string CUST_NAME { get; set; }
        public string PART_DESC { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PROCEDURE_NAME { get; set; }
        public string LOCATION_NAME { get; set; }
        public DateTime? DUE_DATE { get; set; }
    }
}
