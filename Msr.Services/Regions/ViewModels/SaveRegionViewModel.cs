using Msr.Models.Regions;
using System.ComponentModel.DataAnnotations;

namespace Msr.Services.Regions.ViewModels
{
    public class SaveRegionViewModel
    {
        public string ObjectId { get; set; }

        [Required]
        [Display(Name = "Region Name")]
        public string Name { get; set; }

        public string LogId { get; set; }

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
