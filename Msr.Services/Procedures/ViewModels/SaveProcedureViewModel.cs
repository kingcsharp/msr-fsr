using Msr.Models.Procedures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.Procedures.ViewModels
{
    public class SaveProcedureViewModel
    {

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Verb Types")]
        public string VerbType { get; set; }

        public string ObjectId { get; set; }

        public string NTLogin { get; set; }

        public List<SelectListItem> VerbTypes { get; set; }

        public void Setup(ProcedureTypesService procedureService,string Id)
        {
            VerbTypes = procedureService.GetVerbTypes(Id).ToList().Select(x => new SelectListItem
            {
                Text = x.NAME,
                Value = x.ID.ToString(),
            }).OrderBy(o => o.Text).ToList();
        }

        public SaveProcedureViewModel MapToDto(VerbType model)
        {
            return new SaveProcedureViewModel
            {
                Id = model.ID,
                Name = model.NAME,
                VerbType = model.Verbtype,
                ObjectId = model.ObjectId
            };
        }
    }
}