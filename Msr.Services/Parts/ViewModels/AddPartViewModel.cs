using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Msr.Models.Parts;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.PartTypes;
using Msr.Services.ProductionPlanning;

namespace Msr.Services.Parts.ViewModels
{
    public class AddPartViewModel
    {
        public AddPartViewModel()
        {
            PictureFiles = new List<string>();
            ReferenceTheories = new List<string>();
            ListExternalEqualParts = new List<SelectListItem>();
            ListInternalEqualParts = new List<SelectListItem>();
            ListSpecialCustomers = new List<SelectListItem>();
            ListCustomerExceptions = new List<SelectListItem>();
            ListPartsTypes = new List<SelectListItem>();
            ListSupplierCompany = new List<SelectListItem>();
            PartsTypes = new List<SelectListItem>();
            SubPartList = new List<SaveSubPartViewModel>();
            PartList = new List<SelectListItem>();
        }

        public string Id { get; set; }

        public string WfId { get; set; }

        public string ObjID { get; set; }

        public string Company { get; set; }

        [DisplayName("Part Number ( PN / IPN ) :")]
        public string CompanyPartNumber { get; set; }

        [Required]
        [DisplayName("Part Name :")]
        public string Name { get; set; }

        [DisplayName("Part Type :")]
        public string PartType { get; set; }

        [DisplayName("Spare :")]
        public string Spare { get; set; }

        [DisplayName("Consumable :")]
        public string Consumable { get; set; }

        [DisplayName("Ordering Unit :")]
        public string Unit { get; set; }

        [DisplayName("Shipping Weight Per Ordering Unit :")]
        public double? UnitShippingWeight { get; set; }

        [DisplayName("Allow customers to see actual parts availability? :")]
        public Int16? CustomerSeeAvailability { get; set; }

        [DisplayName("Allow suppliers to see actual parts availability? :")]
        public Int16? SupplierSeeAvailability { get; set; }

        [DisplayName("Allow suppliers to see actual parts install base? :")]
        public Int16? SupplierSeeInstallBase { get; set; }

        [DisplayName("Internal Equal Part :")]
        public string InternalEqualParts { get; set; }

        [NotMapped]
        [DisplayName("Selected InternalEqualPart :")]
        public string SelectedInternalEqualParts { get; set; }

        [DisplayName("External Equal Part :")]
        public string ExternalEqualParts { get; set; }

        [DisplayName("Shipping Weight Type :")]
        public string WeightType { get; set; }

        [DisplayName("Create Product Too? :")]
        public byte? CreateProd { get; set; }

        [DisplayName("Product Supplier :")]
        public string SupplierCo { get; set; }

        public string ProductType { get; set; }

        [DisplayName("Procedure Verb :")]
        public string ProcVerb { get; set; }

        [DisplayName("Special Customer List :")]
        public List<string> SpecialCustomers { get; set; }

        [DisplayName("Customers to exclude from this list price :")]
        public List<string> CustomerExceptions { get; set; }

        public string Customers { get; set; }

        [DisplayName("Unit Price :")]
        public decimal? Price { get; set; }

        public string NTLogin { get; set; }

        [DisplayName("Reference Files :")]
        public string ReferenceFiles { get; set; }

        [DisplayName("Picture Files :")]
        public List<string> PictureFiles { get; set; }

        [DisplayName("Reference Theories :")]
        public List<string> ReferenceTheories { get; set; }

        public List<SaveSubPartViewModel> SubPartList { get; set; }

        public string SubParts { get; set; }

        public List<SelectListItem> ListInternalEqualParts { get; set; }

        public IList<SelectListItem> ListExternalEqualParts { get; set; }

        public IEnumerable<SelectListItem> OrderingUnits { get; set; }

        public IEnumerable<SelectListItem> ShippingWeightTypes { get; set; }

        public IEnumerable<SelectListItem> CreateProds { get; set; }

        public IEnumerable<SelectListItem> Spares { get; set; }

        public IEnumerable<SelectListItem> Consumables { get; set; }

        public IEnumerable<SelectListItem> Availabilities { get; set; }

        public List<SelectListItem> PartsTypes { get; set; }

        public List<SelectListItem> ListPartsTypes { get; set; }

        public IEnumerable<SelectListItem> ProductTypes { get; set; }

        public IEnumerable<SelectListItem> ListSpecialCustomers { get; set; }

        public IEnumerable<SelectListItem> ListCustomerExceptions { get; set; }

        public List<SelectListItem> ListSupplierCompany { get; set; }

        public List<DocLink> DocLinks { get; set; }

        public List<SelectListItem> PartList { get; set; }

        public void Setup(DocumentFilesService documentFilesService, PartsService partsService, PartTypeService partTypeService, ProductionPlanningService productionPlanningService, string creatingCo, string ntlog)
        {

            OrderingUnits = Commons.Lookups.LookupItems.OrderingUnits();

            ShippingWeightTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Ounces",
                    Value = "WT_OZ",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Pounds",
                    Value = "WT_LBS"
                },
                new SelectListItem
                {
                    Text = @"Kilograms",
                    Value = "WT_KG"
                },

            };
            CreateProds = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"No Product",
                    Value = "0",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Yes Product",
                    Value = "1"
                }

            };
            Spares = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"No",
                    Value = "SPARE_NO",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Level 1",
                    Value = "SPARE_1"
                },
                new SelectListItem
                {
                    Text = @"Level 2",
                    Value = "SPARE_2"
                },
                new SelectListItem
                {
                    Text = @"Level 3",
                    Value = "SPARE_3"
                }

            };
            Consumables = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"No",
                    Value = "CON_NO",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Yes",
                    Value = "CON_YES"
                }
            };

            Availabilities = Commons.Lookups.LookupItems.YesNo();

            ProductTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"PROD_SERVICE",
                    Value = "SERVICE",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"PROD_GOOD",
                    Value = "GOOD"
                }

            };
            ListSpecialCustomers = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = @"All Insternal Customers Except My Dept",
                    Value = "ALL_INTERNAL_BUT_ME"
                },
                new SelectListItem
                {
                    Text = @"All External Customers",
                    Value = "ALL_EXTERNAL"
                }
            };

            ListSupplierCompany.Add(new SelectListItem { Value = creatingCo, Text = @"[MSR-FSR] MSR-FSR" });

            ListSupplierCompany.AddRange(partsService.GetProductSuppliersByCreatingCo(creatingCo).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList());

            ListInternalEqualParts.Add(new SelectListItem { Value = "", Text = @"Select Internal Part" });
            ListInternalEqualParts.AddRange(partsService.GetPartsApprovedQueryable(creatingCo).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId.ToString()
            }).ToList());

            ListPartsTypes.Add(new SelectListItem { Value = "", Text = "" });
            ListPartsTypes.AddRange(partTypeService.GetPartTypesApproved(creatingCo).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList());

            PartsTypes.Add(new SelectListItem { Value = "", Text = "" });
            PartsTypes.AddRange(partTypeService.GetPartTypesQueryable().ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList());

            ListCustomerExceptions = partsService.GetPartCustomerExceptions(id: Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            SpecialCustomers = partsService.GetPartSpecialCustomers(id: Id).ToList();

            DocLinks = documentFilesService.GetDocByObjectId(ObjID);

            PartList = productionPlanningService.GetPartList().Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();
            PartList.Insert(0, new SelectListItem { Text = "Select", Value = "" });

        }

        public AddPartViewModel MapToDto(PartsView model)
        {
            return new AddPartViewModel
            {
                Id = model.Id,
                WfId = model.Id,
                ObjID = model.ObjectId,
                Company = model.CompanyName,
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
                ProcVerb = model.ProcVerb,
                Price = model.Price
            };
        }

        public List<SaveSubPartViewModel> SubPartMapToDto(List<SubPartView> model)
        {
            var subparts = new List<SaveSubPartViewModel>();

            foreach (var item in model)
            {
                var subPartModel = new SaveSubPartViewModel
                {
                    Id = item.ID,
                    PartId = item.SUB_PART_ID,
                    Qty = Convert.ToInt32(item.QTY),
                    NickName = item.NICK_NAME
                };
                subparts.Add(subPartModel);
            }

            return subparts;
        }
    }
}
