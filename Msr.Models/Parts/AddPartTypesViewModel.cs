using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Models.Parts
{
    public class AddPartTypesViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Part Type Name :")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Typically Spare :")]
        public string Spare { get; set; }

        [Required]
        [Display(Name = "Typically Consumable? :")]
        public string Consumable { get; set; }

        [Required]
        [Display(Name = "Typical Ordering Unit :")]
        public string Unit { get; set; }

        public string ObjId { get; set; }

        public string UnitShippingWeight { get; set; }

        public string NTLogin { get; set; }

        public List<SelectListItem> Spares { get; set; }
        public List<SelectListItem> Consumables { get; set; }

        public void Setup()
        {
            Spares = new List<SelectListItem>
            {
                    new SelectListItem{ Text = "Not Typically a Spare Part", Value = "PTSPARE_NO" },
                new SelectListItem
                {
                    Text = "L1 - Stock in location with 1 machine",
                    Value = "PTSPARE_1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "L2 - Stock in location with 10 machine",
                    Value = "PTSPARE_2"
                },
                new SelectListItem
                {
                    Text = "L3 - Stock in location with 50 machine",
                    Value = "PTSPARE_3"
                }
            };

            Consumables = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Not Consumable Part",
                    Value = "PTCON_NO"
                },
                new SelectListItem
                {
                    Text = "Consumable Part",
                    Value = "PTCON_YES",
                    Selected = true
                }

            };
        }
        public AddPartTypesViewModel MapToDto(PartType parttype)
        {
            return new AddPartTypesViewModel
            {
                Id = parttype.Id,
                Name = parttype.Name,
                Spare = parttype.Spare,
                Consumable = parttype.Consumable,
                Unit = parttype.Unit,
                UnitShippingWeight = parttype.Unit_Shipping_Weight,
                ObjId = parttype.Object_Id
            };

        }


    }
}