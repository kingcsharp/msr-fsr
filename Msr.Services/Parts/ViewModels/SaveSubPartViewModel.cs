using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Msr.Services.Parts.ViewModels
{
    public class SaveSubPartViewModel
    {
        public SaveSubPartViewModel()
        {
            PartList = new List<SelectListItem>();
        }

        public string ParentObjId { get; set; }

        public string Id { get; set; }

        [Required]
        public string PartId { get; set; }

        [Required]
        public int? Qty { get; set; }

        public string NickName { get; set; }

        public string StrNtLogin { get; set; }

        public List<SelectListItem> PartList { get; set; }
    }
}
