using Msr.Services.Parts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.ActualParts;
using Msr.Services.Companies;
using Msr.Services.Locations;
using Msr.Services.Orders;
using Msr.Services.Products;
using Msr.Services.Users.Messages;

namespace Msr.Services.ActualParts.ViewModels
{
    public class SaveActualPartsViewModel
    {
        public SaveActualPartsViewModel()
        {
            ListParts = new List<SelectListItem>();
            ListParents = new List<SelectListItem>();
            ListLocations = new List<SelectListItem>();
            ListCurOwners = new List<SelectListItem>();
            ListProducts = new List<SelectListItem>();
            ListAPStatus = new List<SelectListItem>();
            ListPersons = new List<SelectListItem>();
            Products = new List<string>();
        }
        public string Id { get; set; }

        public string ObjectId { get; set; }

        [Required]
        [Display(Name = "This Actual Part is a:")]
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

        [Display(Name = "This Part installed in:")]
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

        public void SetUp(ActualPartsService actualPartsService, PartsService partsService, LocationService locationService, PeopleService peopleService, ProductService productService, CompanyService companyService, LoggedUserIdResult getCurrentUser)
        {
            ListSubPartActions = Commons.Lookups.LookupItems.ListSubPartActions();

            ListParents = actualPartsService.GetActualPartsApprovedQueryable().OrderBy(x => x.ComapnyPartNumber).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ListParents.Insert(0, new SelectListItem { Text = @"Select Actual Part", Value = "" });

            ListParts = partsService.GetPartsApprovedQueryable(getCurrentUser.Root_Company).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            ListParts.Insert(0, new SelectListItem { Text = @"Select Part", Value = "" });

            ListLocations = locationService.GetActiveLocations(getCurrentUser.Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
            ListLocations.Insert(0, new SelectListItem { Text = @"Select Location", Value = "" });

            ListCurOwners.Insert(0, new SelectListItem { Text = @"Select Owner", Value = "" });

            ListCurOwners.AddRange(companyService.GetPersonRootCompanyTreeViews(getCurrentUser.Id).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
                Selected = x.Id == getCurrentUser.Root_Company
            }).ToList());

            ListPersons.Insert(0, new SelectListItem { Text = @"Select Responsible Person", Value = "" });
            ListPersons.AddRange(peopleService.GetPeopleApprovedSearch(getCurrentUser.Id, getCurrentUser.Root_Company).ToList().Select(x => new SelectListItem
            {
                Text = x.Full_Name,
                Value = x.Root.ToString()
            }).ToList());

            ListProducts.AddRange(productService.GetProductsQueryable().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList());

            Products = actualPartsService.GetActualpartProductsInstalled(Id, getCurrentUser.Id).ToList();

            ListAPStatus = Commons.Lookups.LookupItems.ListAPStatus();
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
                ResponsiblePerson = model.ResponsibleName
            };
        }
    }
}
