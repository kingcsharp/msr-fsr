using Msr.Services.Parts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Msr.Models.ActualParts;
using Msr.Services.Locations;
using Msr.Services.Users;
using Msr.Services.Products;

namespace Msr.Services.ActualParts.ViewModels
{
    public class SaveActualPartsViewModel
    {
        public SaveActualPartsViewModel()
        {
            Products = new List<string>();
            ListParts = new List<SelectListItem>();
            ListParents = new List<SelectListItem>();
            ListLocations = new List<SelectListItem>();
            ListCurOwners = new List<SelectListItem>();
            ListProducts = new List<SelectListItem>();
            ListAPStatus = new List<SelectListItem>();
            ListPersons = new List<SelectListItem>();
        }
        public string Id { get; set; }

        public string ObjectId { get; set; }

        [Required]
        [Display(Name = "This Actual is a :")]
        public string PartId { get; set; }

        [Display(Name = "QTY :")]
        public double? Qty { get; set; }

        public string Serial { get; set; }

        [Display(Name = "NICK NAME :")]
        public string NickName { get; set; }

        [Display(Name = "Location :")]
        public string LocationId { get; set; }

        [Required]
        [Display(Name = "Current OWner :")]
        public string CurOwner { get; set; }

        [Display(Name = "AP Status :")]
        public string APStatus { get; set; }

        [Display(Name = "Products Installed :")]
        public List<string> Products { get; set; }

        [Display(Name = "This Part Installed in :")]
        public string ParentId { get; set; }

        [Display(Name = "Sub Parts Action :")]
        public string SubpartAction { get; set; }

        [Display(Name = "Responsible Person :")]
        public string ResponsiblePerson { get; set; }

        public string NTLogin { get; set; }

        public List<SelectListItem> ListParts { get; set; }

        public List<SelectListItem> ListParents { get; set; }

        public List<SelectListItem> ListLocations { get; set; }

        public List<SelectListItem> ListCurOwners { get; set; }

        public List<SelectListItem> ListProducts { get; set; }

        public List<SelectListItem> ListAPStatus { get; set; }

        public List<SelectListItem> ListPersons { get; set; }

        public List<SelectListItem> ListSubPartActions { get; set; }

        public void SetUp(ActualPartsService actualPartsService, PartsService partsService, LocationService locationService, UserService userService, ProductService productService)
        {
            ListSubPartActions = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Create New",
                    Value = "CREATE_NEW",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Grab From Location and sub locations",
                    Value = "GET_AT_LOC"
                }
            };

            ListParents = actualPartsService.GetActualParts("APPROVED").Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
            ListParents.Insert(0, new SelectListItem { Text = @"Select Actual Part", Value = "" });

            ListParts = partsService.GetPartsQueryable().Where(x => x.Status.StartsWith("APPROVED")).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.PartId.ToString()
            }).OrderBy(o => o.Text).ToList();
            ListParts.Insert(0, new SelectListItem { Text = @"Select Part", Value = "" });

            ListLocations = locationService.GetLocationsQueryable().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();
            ListLocations.Insert(0, new SelectListItem { Text = @"Select Location", Value = "" });

            ////ListCurOwners = actualPartsService.GetActualPartCompanies().Select(x => new SelectListItem
            ////{
            ////    Text = x.Name,
            ////    Value = x.Id.ToString()
            ////}).OrderBy(o => o.Text).ToList();
            ListCurOwners.Insert(0, new SelectListItem { Text = @"Select Owner", Value = "" });

            ////ListPersons = userService.GetPeoplesQueryable().ToList().Select(x => new SelectListItem
            ////{
            ////    Text = x.FullName,
            ////    Value = x.ObjectId.ToString()
            ////}).OrderBy(o => o.Text).ToList();
            ListPersons.Insert(0, new SelectListItem { Text = @"Select Responsible Person", Value = "" });

            ////ListProducts = productService.GetProductsQueryable().ToList().Select(x => new SelectListItem
            ////{
            ////    Text = x.Name,
            ////    Value = x.ObjectId.ToString()
            ////}).OrderBy(o => o.Text).ToList();

            ////Products = actualPartsService.GetActualpartProductsInstalled(id: Id).ToList();

            ListAPStatus = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Available",
                    Value = "ap_available",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Consumed",
                    Value = "ap_consumed"
                },
                new SelectListItem
                {
                    Text = @"Filled",
                    Value = "ap_filled"
                },
                new SelectListItem
                {
                    Text = @"Held For Pickup",
                    Value = "ap_held"
                },
                new SelectListItem
                {
                    Text = @"In Call",
                    Value = "ap_in_call"
                },
                new SelectListItem
                {
                    Text = @"In Fill",
                    Value = "ap_in_fill"
                },
                new SelectListItem
                {
                    Text = @"In Transit",
                    Value = "ap_in_transit"
                },
                new SelectListItem
                {
                    Text = @"Installed",
                    Value = "ap_installed"
                },
                new SelectListItem
                {
                    Text = @"Received",
                    Value = "ap_received"
                }
            };
        }

        public SaveActualPartsViewModel MapToDto(ActualPartsView model)
        {
            return new SaveActualPartsViewModel
            {
                Id = model.Id,
                ObjectId = model.ObjectId,
                PartId = model.PartId,
                Qty = model.Qty,
                Serial = model.Serial,
                NickName = model.NickName,
                LocationId = model.LocationObjectId,
                CurOwner = model.CurOwner,
                APStatus = model.ApStatus,
                ParentId = model.ParentId,
                SubpartAction = null,
                ResponsiblePerson = model.RespPersonFullName
            };
        }
    }
}
