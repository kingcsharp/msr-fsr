using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Orders.ViewModels
{
    public class SaveWorkOrderViewModel
    {
        public string FillId { get; set; }
        public string PurchaseItemId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public string NTLogin { get; set; }
    }
}
