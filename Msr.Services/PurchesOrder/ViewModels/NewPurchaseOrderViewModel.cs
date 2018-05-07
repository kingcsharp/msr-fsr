using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.PurchesOrder;
using System.ComponentModel.DataAnnotations;
using Msr.Services.ProductionPlanning;
using Msr.Services.Users.Messages;

namespace Msr.Services.PurchesOrder.ViewModels
{
    public class NewPurchaseOrderViewModel
    {
        public NewPurchaseOrderViewModel()
        {
            ClientList = new List<SelectListItem>();
            ProductsList = new List<SelectListItem>();
            Products = new List<string>();

        }
        public string Id { get; set; }
        public string ObjId { get; set; }

        public string Client { get; set; }

        [Display(Name = "PO Name")]
        public string POName { get; set; }
        
        public string AccountType { get; set; }

        [Display(Name = "Reference Cust PO")]
        public string RefCustPO { get; set; }
        
        public string SupplierDepartment { get; set; }
        public string CustRefNum { get; set; }
        
        [Display(Name = "Open Date")]
        public string OpenDate { get; set; }
        
        [Display(Name = "Close Date")]
        public string CloseDate { get; set; }
        
        public decimal? TotalPurchaseLimit { get; set; }
        public double? Tax { get; set; }
        public string InvoiceTrigger { get; set; }
        public string InvoicePeriod { get; set; }
        public string InvoicePeriodType { get; set; }

        [Display(Name = "First Invoice Date")]
        public DateTime? FirstInvoiceDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid number")]
        public int? GracePeriod { get; set; }
        public float? LatePaymentFee { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid number")]
        public int? ReApplyFrequency { get; set; }
        public List<string> Products { get; set; }
        public string Save { get; set; }
        public string SaveClose { get; set; }
        public string SaveWorkflow { get; set; }
        public string NTLogin { get; set; }

        public List<SelectListItem> ProductsList { get; set; }
        public List<SelectListItem> ClientList { get; set; }
        public List<SelectListItem> AccountTypeList { get; set; }
        public List<SelectListItem> SupplierDepartmentList { get; set; }
        public List<SelectListItem> InvoiceTriggerList { get; set; }
        public List<SelectListItem> InvoicePeriodTypeList { get; set; }

        public void Setup(PurchesOrderService purchesOrderService, ProductionPlanningService productionPlanningService, LoggedUserIdResult currentUser)
        {
            ClientList = productionPlanningService.GetCustomerList(currentUser).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();
            ClientList.Insert(0, new SelectListItem() { Value = "", Text = @"Select Client" });

            SupplierDepartmentList = productionPlanningService.GetSupplierList(currentUser).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            SupplierDepartmentList.Insert(0, new SelectListItem() { Value = "", Text = @"Select Supplier Department" });

            ProductsList = purchesOrderService.GetCompinesProducts(Client, SupplierDepartment).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Order_id.ToString()
            }).OrderBy(o => o.Text).ToList();

            Products = purchesOrderService.GetProductsById(ObjId);

            AccountTypeList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Select Type",
                    Value = "",
                },
                new SelectListItem
                {
                    Text = @"Purchasing",
                    Value = "PURCHASING_ACCOUNT",
                },
                new SelectListItem
                {
                    Text = @"Warranty/Maintenance",
                    Value = "WARRANTY_ACCOUNT",
                }
            };

            InvoiceTriggerList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"All Line Items On Purchase Debited",
                    Value = "ALL_PURCHASE_ITEMS",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Each Line Item On Purchase Debited",
                    Value = "EVERY_DEBIT"
                },
                new SelectListItem
                {
                    Text = @"Periodically",
                    Value = "PERIODIC"
                },
                new SelectListItem
                {
                    Text = @"Manually",
                    Value = "MANUAL"
                }
            };

            InvoicePeriodTypeList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Days",
                    Value = "DAYS"
                },
                new SelectListItem
                {
                    Text = @"Weeks",
                    Value = "WEEKS"
                },
                new SelectListItem
                {
                    Text = @"Months",
                    Value = "MONTHS"
                }
            };

        }

        public NewPurchaseOrderViewModel MaptoDto(PurchesOrderView model)
        {
            return new NewPurchaseOrderViewModel()
            {
                Id = model.Id,
                ObjId = model.ObjId,
                Client = model.CustomerCo,
                POName = model.Name,
                SupplierDepartment = model.SupplierCo,
                AccountType = model.AccType,
                RefCustPO = model.ReferencePo,
                CustRefNum = model.ReferenceName,
                OpenDate = model.OpenDate,
                CloseDate = model.CloseDate,
                TotalPurchaseLimit = model.TotalPurchaseLimit,
                Tax = model.TaxRate,
                InvoiceTrigger = model.InvoiceTrigger,
                InvoicePeriod = model.InvoicePeriodNumber,
                InvoicePeriodType = model.InvoicePeriodType,
                FirstInvoiceDate = model.FirstInvoiceDate,
                GracePeriod = model.PaymentGracePeriod,
                LatePaymentFee = model.LateFeePercentage,
                ReApplyFrequency = model.ReapplyLateFee
            };
        }
    }
}
