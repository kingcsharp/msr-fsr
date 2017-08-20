using System.Collections.Generic;
using System.Web.Mvc;
using Msr.Models.Parts;

namespace Answer.Web.ViewModel
{
    public class EditSafetyStockViewModel
    {
        public EditSafetyStockViewModel()
        {
            Roles = new List<SelectListItem>();
            PartsSafetyStocks = new List<PartsSafetyStock>();
        }

        public string PartObjectId { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<PartsSafetyStock> PartsSafetyStocks { get; set; }
    }
}