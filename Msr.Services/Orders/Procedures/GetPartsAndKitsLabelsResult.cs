
namespace Msr.Services.Orders.Procedures
{
    public class GetPartsAndKitsLabelsResult
    {
        public string Proc_Name { get; set; }

        public string Serial { get; set; }

        public string Company_Part_Number { get; set; }

        public string Part_Desc { get; set; }

        public string Actual_Part_ID { get; set; }

        public int Cycle_Count { get; set; }

        public string Site_Name { get; set; }

        public string Due_Date { get; set; }

        public string WO_Item_Number { get; set; }

        public string PO_Number { get; set; }
    }

}
