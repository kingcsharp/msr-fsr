
namespace Msr.Services.Orders.Procedures
{
    public class FileSearchResult
    {
        public string LocationID { get; set; }

        public string LocationName { get; set; }

        public string CustomerName { get; set; }

        public string ProductName { get; set; }

        public string ProcedureName { get; set; }

        public string PurchItemId { get; set; }

        public int Qty { get; set; }

        public string FillObjDesc { get; set; }

        public int? FillObjectId { get; set; }

        public string ProcId { get; set; }

        public string ProcObjId { get; set; }

        public string Rev { get; set; }

        public string CustPurchNum { get; set; }

        public string CustPurchLineNum { get; set; }

        public string PartImageId { get; set; }
    }
}
