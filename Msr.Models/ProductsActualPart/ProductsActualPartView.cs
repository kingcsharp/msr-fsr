using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ProductsActualPart
{
    public class ProductsActualPartView
    {
        public string Id { get; set; }

        public string OrderHistId { get; set; }

        public string ProductId { get; set; }

        public string SupplierId { get; set; }

        public string AppObject { get; set; }

        public string Description { get; set; }

        public string CustomerCo { get; set; }

        public string CustomerPerson { get; set; }

        public Int16? BudgetaryOnly { get; set; }

        public string ExpirationDate { get; set; }

        public string ProductName { get; set; }

        public string CustomerName { get; set; }

        public string SupplierName { get; set; }

        public string Progress { get; set; }

        public string ProcSysId { get; set; }

        public string SourceId { get; set; }

        public string AddCostId { get; set; }

        public string Parent { get; set; }

        public string Status { get; set; }
    }
}
