using System.Collections.Generic;

namespace Msr.Services.Orders.Procedures
{
    public class WipHistoryDetailResult
    {
        public string PurchaseItemId { get; set; }

        public string SupplierName { get; set; }

        public string ProcedureName { get; set; }

        public string CustomerPerson { get; set; }

        public string FillObjectDescription { get; set; }
    }
}
