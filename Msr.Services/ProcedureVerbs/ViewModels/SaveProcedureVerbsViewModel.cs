using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.ProcedureVerbs;

namespace Msr.Services.ProcedureVerbs.ViewModels
{
    public class SaveProcedureVerbsViewModel
    {

        public string Id { get; set; }

        [Required]
        [Display(Name = "Name :")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Major Group :")]
        public string VerbType { get; set; }

        public string ObjectId { get; set; }

        public string NTLogin { get; set; }

        public List<SelectListItem> VerbTypes { get; set; }

        public void Setup(ProcedureVerbsService procedureService)
        {
            //Passed Id need to be dynamic
            VerbTypes = procedureService.GetVerbTypes(id: "1618").ToList().Select(x => new SelectListItem
            {
                Text = x.NAME,
                Value = x.ID.ToString(),
            }).OrderBy(o => o.Text).ToList();
        }

        public SaveProcedureVerbsViewModel MapToDto(ProcedureVerbsView model)
        {
            return new SaveProcedureVerbsViewModel
            {
                Id = model.Id,
                Name = model.Name,
                VerbType = model.VerbType,
                ObjectId = model.ObjectId
            };
        }
    }
}