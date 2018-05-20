
namespace Msr.Services.Orders.Procedures
{
    public class FileSearchResult
    {
        public string CustomerName { get; set; }

        public string ProductName { get; set; }

        public string ProcedureName { get; set; }

        public string PurchItemId { get; set; }

        public int Qty { get; set; }

        public string FillObjDesc { get; set; }

        public int? FillObjectId { get; set; }

        public string ProcId { get; set; }

        public string ProcObjId { get; set; }
    }
}
