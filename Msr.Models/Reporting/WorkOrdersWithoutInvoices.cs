using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Reporting
{
    public class WorkOrdersWithoutInvoices
    {
        [Key]
        public long Id { get; set; }
        public string Location_Name { get; set; }
        public string Customer_Name { get; set; }
        public string Cust_Purch_Num { get; set; }
        public string Work_Order_Item { get; set; }
        public string Product_Name { get; set; }
        public string Total_Sale_Price { get; set; }
        public string Work_Order_Completed_Date { get; set; }
    }
}