using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.AdminCostSettings;
using Msr.Models.CustomerRequirements;
using Msr.Models.Products;
using Msr.Services.CustomerRequirements.ViewModel;
using Msr.Services.PrePro;
using Msr.Services.Procedures;
using Msr.Services.Quotes.ViewModels;
using Msr.Services.Users.Messages;
using Newtonsoft.Json;

namespace Msr.Services.ProductionPlanning.ViewModels
{
    public class RequirementStepsViewModel
    {
        public RequirementStepsViewModel()
        {
            Steps = new List<RequirementStepsDetailsViewModel>();
        }

        public CustomerRequirementView SubmittedRequirement { get; set; }

        public bool HasQuote { get; set; }

        public int Id { get; set; }

        public string LoginId { get; set; }

        public string ObjectId { get; set; }

        public string OldProductProcedureId { get; set; }

        public string ProductProcedureId { get; set; }

        [Required]
        public string ProductPartId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductSupplierId { get; set; }

        public string ProductLocationId { get; set; }

        [Required]
        public string ProductCustomerId { get; set; }

        public string ProductCustomerDivision { get; set; }

        [Display(Name = "Total Direct Mins")]

        public decimal TotalDirectMins { get; set; }

        [Display(Name = "Total Machine Mins")]

        public decimal TotalMachineMins { get; set; }

        [Display(Name = "Total Direct Dollar")]

        public decimal TotalDirectDollar { get; set; }

        [Display(Name = "Standard Machine Dollar")]

        public decimal StandardMachineDollar { get; set; }

        [Display(Name = "Total Sale Price")]

        public Single? MaterialCost { get; set; }

        public Single? TotalSalePrice { get; set; }
        public int CustomerSubmitId { get; set; }

        public List<RequirementStepsDetailsViewModel> Steps { get; set; }

        public List<SelectListItem> ProcessList { get; set; }

        public List<SelectListItem> Suppliers { get; set; }

        public List<SelectListItem> Customers { get; set; }

        public List<SelectListItem> ProductLocationList { get; set; }

        public List<SelectListItem> ProcedureList { get; set; }

        public List<SelectListItem> PartList { get; set; }

        public string ProductStatus { get; set; }

        public AdminCostSetting AdminCostSettings { get; set; }

        public void Setup(ProductionPlanningService productionPlanningService, PreProServices preProServices,
            LoggedUserIdResult currentUser)
        {
            ProcedureList = productionPlanningService.GetProceduretList().Select(x => new SelectListItem
            {

                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            PartList = productionPlanningService.GetPartList().Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            Suppliers = productionPlanningService.GetSupplierList(currentUser).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            Customers = productionPlanningService.GetCustomerList(currentUser).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            ProductLocationList = productionPlanningService.GetLocationsQueryable().Where(x => x.Status == "APPROVED").Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjId,
                Selected = false
            }).OrderBy(o => o.Text).ToList();

            ProcessList = preProServices.GetPreProQueryable().Where(x => x.Status == "APPROVED").Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.ObjectId.ToString()
            }).OrderBy(o => o.Text).ToList();
            ProcessList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
        }

        public void Read(ProductionPlanningService productionPlanService, ProductsView productsView, CustomerRequirementView requirment)
        {
            Id = productsView.CustomerRequirementId.Value;
            CustomerSubmitId = productsView.CustomerRequirementId.GetValueOrDefault();
            ProductCustomerDivision = productsView.Division;
            ProductSupplierId = productsView.Supplier_Id;
            ProductLocationId = productsView.LocationId;
            ProductCustomerId = productsView.CustomerId;
            ProductName = productsView.Name;
            ProductPartId = productsView.App_Object;
            ProductProcedureId = productsView.Procedure_Id;
            OldProductProcedureId = productsView.Procedure_Id;
            TotalSalePrice = productsView.TotalSalePrice;
            MaterialCost = productsView.MaterialCost;
            PObjectId = productsView.Object_Id;
            ProductStatus = productsView.Status;

            if (!string.IsNullOrWhiteSpace(requirment.QuoteJson))
            {
                HasQuote = true;
            }

            if (HasQuote)
            {
                var result = JsonConvert.DeserializeObject<FreeFormQuoteViewModel>(requirment.QuoteJson);

                var procedureId = result.ExistingProcess;
                var customerPartNo = result.QuoteItems?.FirstOrDefault().CustomerPartNo;

                if (string.IsNullOrWhiteSpace(ProductSupplierId))
                {
                    ProductSupplierId = result.Supplier;
                }

                if (string.IsNullOrWhiteSpace(ProductCustomerId))
                {
                    ProductCustomerId = result.CustomerId;
                }

                if (string.IsNullOrWhiteSpace(ProductProcedureId))
                {
                    ProductProcedureId = productionPlanService.GetProceduretIdById(procedureId);
                }

                if (string.IsNullOrWhiteSpace(ProductPartId))
                {
                    ProductPartId = productionPlanService.GetPartIdByCompanyPartNumber(customerPartNo);
                }
                if (string.IsNullOrWhiteSpace(ProductName))
                {
                    ProductName = result.QuoteItems?.FirstOrDefault().Description;
                }
            }
            else
            {
                var result = JsonConvert.DeserializeObject<CustomerRequirementViewModel>(requirment.CustomerRequirementJson);

                if (string.IsNullOrWhiteSpace(ProductName))
                {
                    ProductName = result.RequirementName;
                }
            }

        }

        public string PObjectId { get; set; }
        public bool NewProcedure { get; set; }
    }
}
