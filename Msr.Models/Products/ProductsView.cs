using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Products
{
    public class ProductsView
    {
        public string Id { get; set; }
        public string Object_Id { get; set; }

        public string Parent_Id { get; set; }

        public string Name { get; set; }

        public string Supplier_Id { get; set; }
        public string CustomerId { get; set; }

        public string Supplier_Name { get; set; }

        public string Comments { get; set; }

        public string Procedure_Id { get; set; }

        public string Verb { get; set; }

        public string Verb_Name { get; set; }

        public string App_Object { get; set; }

        public string App_Obj_Name { get; set; }

        public int? Ship_Or_Labor { get; set; }

        public Int16? Customizable { get; set; }

        public string Req_Form { get; set; }

        public string Mgr_Team { get; set; }

        public string Mgr_Team_Name { get; set; }

        public Int16? Sales_Tax { get; set; }

        public string Procedure_Name { get; set; }

        public string Obj_Id { get; set; }
        public string Locked_By { get; set; }
        public string UnLocked_By { get; set; }
        public string Created_By { get; set; }

        public DateTime? Create_Date { get; set; }
        public string Root { get; set; }
        public string Rev_Info { get; set; }
        public string Creating_Co { get; set; }
        public string Status { get; set; }
        public int? Rev { get; set; }
        public string Wfs_id { get; set; }
        public string Locked_By_Name { get; set; }
        public string Creating_Co_Name { get; set; }
        public string Approval_activity { get; set; }
        public Int16? Has_Usage { get; set; }
        public string Cust_Mgr_Role { get; set; }
        public int? CustomerRequirementId { get; set; }
        public float? TotalSalePrice { get; set; }
        public float? MaterialCost { get; set; }
        public bool? IsProduct { get; set; }
        public string Division { get; set; }
        public string LocationId { get; set; }
    }
}
