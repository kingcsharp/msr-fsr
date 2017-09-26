using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.PurchesOrder;

namespace Msr.Services.PurchesOrder.ViewModels
{
    public class NewPurchaseOrderViewModel
    {
        public NewPurchaseOrderViewModel()
        {
            ClientList = new List<SelectListItem>();
            AccountTypeList = new List<SelectListItem>();
        }

        public string ObjId { get; set; }
        public string Client { get; set; }
        public string POName { get; set; }
        public string AccountType { get; set; }
        public string RefCustPO { get; set; }
        public int? SupplierDepartment { get; set; }
        public int? CustRefNum { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal TotalPurchaseLimit { get; set; }
        public decimal Tax { get; set; }
        public string InvoiceTrigger { get; set; }
        public int? InvoicePeriod { get; set; }
        public string InvoicePeriodType { get; set; }
        public DateTime? FirstInvoiceDate { get; set; }
        public int? GracePeriod { get; set; }
        public int? LatePaymentFee { get; set; }
        public int? ReApplyFrequency { get; set; }
        public string Products { get; set; }

        public List<ProductsCanPurchase> ProductsList { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ClientName { get; set; }
        public string NTLogin { get; set; }
        public List<SelectListItem> ClientList { get; set; }
        public List<SelectListItem> AccountTypeList { get; set; }
        public List<SelectListItem> SupplierDepartmentList { get; set; }
        public List<SelectListItem> InvoiceTriggerList { get; set; }
        public List<SelectListItem> InvoicePeriodTypeList { get; set; }
        public void Setup(PurchesOrderService purchesOrderService)
        {
            ClientList.Add(new SelectListItem() { Value = "", Text = "Select Client" });
            
            ClientList.AddRange(purchesOrderService.GetCompaniesList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList());

            SupplierDepartmentList = purchesOrderService.GetCompaniesList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();


            AccountTypeList.Add(new SelectListItem() { Value = "", Text = "Select Type" });

            AccountTypeList.AddRange(new List<SelectListItem>
            {
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
            });

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
    }
}
