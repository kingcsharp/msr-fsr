using Msr.Models.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Regions.ViewModels
{
    public class SaveRegionViewModel
    {
        public string ObjectId { get; set; }
        [Display(Name = "Region Name:")]
        public string Name { get; set; }

        public string NTLogin { get; set; }

        public SaveRegionViewModel MapToDto(RegionsView model)
        {
            return new SaveRegionViewModel
            {
                ObjectId = model.ObjectId,
                Name = model.Name
            };
        }
    }
    
}
