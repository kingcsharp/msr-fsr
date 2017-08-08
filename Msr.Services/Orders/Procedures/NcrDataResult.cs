using System;

namespace Msr.Services.Orders.Procedures
{
    public class NcrDataResult
    {
        public string Id { get; set; }
        public string Serial { get; set; }
        public string Fill_Id { get; set; }
        public string Parent_Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string Requestor { get; set; }
        public string Completed_By { get; set; }
        public string ModBy { get; set; }
        public string Drcm { get; set; }
        public string Child_Order { get; set; }
        public string Customer_Name { get; set; }
        public string Cust_Purch_Num { get; set; }
        public DateTime Actual_Start_Date { get; set; }
        public DateTime Actual_Stop_Date { get; set; }
    }
}
