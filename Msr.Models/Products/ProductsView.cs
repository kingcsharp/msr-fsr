using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Products
{
    public class ProductsView
    {
        public string ObjectId { get; set; }

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string Comments { get; set; }

        public string ProcedureId { get; set; }

        public string Verb { get; set; }

        public string VerbName { get; set; }

        public string AppObject { get; set; }

        public string AppObjName { get; set; }

        public int? ShipOrLabor { get; set; }

        public Int16? Customizable { get; set; }

        public string ReqForm { get; set; }

        public string MgrTeam { get; set; }

        public string MgrTeamName { get; set; }

        public Int16? SalesTax { get; set; }

        public string ProcedureName { get; set; }

        public string Id { get; set; }

        public string HistoryRefId { get; set; }

        public string CreatingCoName { get; set; }

        public string CreatingCo { get; set; }

        public string CustMgrRole { get; set; }

        public string Status { get; set; }

        public string PersonSupplier { get; set; }

        public int? Availability { get; set; }

        public string SystemProcedure { get; set; }
    }
}
