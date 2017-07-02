
using System;

namespace Msr.Services.Orders.Procedures
{
    public class GetMonitorLabelTsrDetailsResult
    {
        public string Company_Part_Number { get; set; }

        public string Part_Desc { get; set; }

        public string Purch_Item_Id { get; set; }

        public string Description { get; set; }

        public string Latest_Requestee_Name { get; set; }

        public string Task_Description { get; set; }

        public DateTime Actual_Stop_Date { get; set; }
    }
}
