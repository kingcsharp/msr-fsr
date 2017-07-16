using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Parts;

namespace Msr.Services.Parts.ViewModels
{
    public class AddPartViewModel
    {

        public string ObjID { get; set; }

        public string Company { get; set; }

        [Required]
        [DisplayName("Part Number ( PN / IPN ) :")]
        public string CompanyPartNumber { get; set; }

        [Required]
        [DisplayName("Part Name :")]
        public string Name { get; set; }

        [DisplayName("Part Type :")]
        public string PartType { get; set; }

        public string Spare { get; set; }

        public string Consumable { get; set; }

        [DisplayName("Ordering Unit :")]
        public string Unit { get; set; }

        [DisplayName("Shipping Weight Per Ordering Unit :")]
        public double? UnitShippingWeight { get; set; }

        [DisplayName("Sub-parts :")]
        public string SubParts { get; set; }

        [DisplayName("Allow customers to see actual parts availability? :")]
        public Int16? CustomerSeeAvailability { get; set; }

        [DisplayName("Allow suppliers to see actual parts availability? :")]
        public Int16? SupplierSeeAvailability { get; set; }

        [DisplayName("Allow suppliers to see actual parts install base? :")]
        public Int16? SupplierSeeInstallBase { get; set; }

        [DisplayName("InternalEqualParts :")]
        public string InternalEqualParts { get; set; }

        [DisplayName("ExternalEqualParts :")]
        public string ExternalEqualParts { get; set; }

        [DisplayName("Shipping Weight Type :")]
        public string WeightType { get; set; }

        [DisplayName("Create Product Too? :")]
        public byte? CreateProd { get; set; }

        public string SupplierCo { get; set; }

        public string ProductType { get; set; }

        [DisplayName("Procedure Verb :")]
        public string ProcVerb { get; set; }

        public string SpecialCustomer { get; set; }

        public string CustomerExceptions { get; set; }

        public string Customers { get; set; }

        public decimal Price { get; set; }

        public string NTLogin { get; set; }

        [DisplayName("Reference Files :")]
        public List<string> ReferenceFiles { get; set; }

        [DisplayName("Picture Files :")]
        public List<string> PictureFiles { get; set; }

        [DisplayName("Reference Theories :")]
        public List<string> ReferenceTheories { get; set; }

        public IEnumerable<SelectListItem> OrderingUnits { get; set; }

        public IEnumerable<SelectListItem> ShippingWeightTypes { get; set; }

        public IEnumerable<SelectListItem> CreateProds { get; set; }

        public IEnumerable<SelectListItem> Spares { get; set; }

        public IEnumerable<SelectListItem> Consumables { get; set; }

        public IEnumerable<SelectListItem> Availabilities { get; set; }


        public void Setup()
        {
            OrderingUnits = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Item",
                    Value = "UNIT",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Ounces",
                    Value = "WT_OZ"
                },
                new SelectListItem
                {
                    Text = "Pounds",
                    Value = "WT_LBS"
                },
                new SelectListItem
                {
                    Text = "Kilograms",
                    Value = "WT_KG"
                },
                new SelectListItem
                {
                    Text = "Gallons",
                    Value = "VOL_GALLONS"
                }
            };

            ShippingWeightTypes = new List<SelectListItem>
            {
               new SelectListItem
                {
                    Text = "Ounces",
                    Value = "WT_OZ",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Pounds",
                    Value = "WT_LBS"
                },
                new SelectListItem
                {
                    Text = "Kilograms",
                    Value = "WT_KG"
                },

            };
            CreateProds = new List<SelectListItem>
            {
               new SelectListItem
                {
                    Text = "No Product",
                    Value = "0",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Yes Product",
                    Value = "1"
                }

            };
            Spares = new List<SelectListItem>
            {
               new SelectListItem
                {
                    Text = "No",
                    Value = "SPARE_NO",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Level 1",
                    Value = "SPARE_1"
                },
                 new SelectListItem
                {
                    Text = "Level 2",
                    Value = "SPARE_2"
                },
                  new SelectListItem
                {
                    Text = "Level 3",
                    Value = "SPARE_3"
                }

            };
            Consumables = new List<SelectListItem>
            {
               new SelectListItem
                {
                    Text = "No",
                    Value = "CON_NO",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Yes",
                    Value = "CON_YES"
                }
            };
            Availabilities = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text="Yes",
                    Value="1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text="No",
                    Value="0",
                }
            };
        }

        public AddPartViewModel MapToDto(Part model)
        {
            return new AddPartViewModel
            {
                ObjID = model.ID,
                Company = model.Company,
                CompanyPartNumber = model.CompanyPartNumber,
                Name = model.Name,
                PartType = model.PartType,
                Spare = model.Spare,
                Consumable = model.Consumable,
                Unit = model.Unit,
                UnitShippingWeight = model.UnitShippingWeight,
                CustomerSeeAvailability = model.CustomerSeeAvailability,
                SupplierSeeAvailability = model.SupplierSeeAvailability,
                SupplierSeeInstallBase = model.SupplierSeeInstallBase,
                WeightType = model.WeightType,
                CreateProd = model.CreateProd,
                SupplierCo = model.SupplierCo,
                ProductType = model.ProductType,
                ProcVerb = model.ProcVerb

            };
        }
    }
}
