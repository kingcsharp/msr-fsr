using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Invoices.ViewModel
{
   public class InvoiceViewModel
    {
        [Key]
        public string InvoiceId { get; set; }

        public string AcctName { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string Status { get; set; }

        public Decimal? AmtPaid { get; set; }

        public Decimal? NewItemsAmt { get; set; }

        public Decimal? PreviousBalance { get; set; }

        public Decimal? PaymentAmount { get; set; }

        public Decimal? DisputedAmount { get; set; }

        public Decimal? TotalDue { get; set; }

        public DateTime? DueDate { get; set; }

        public string AcctType { get; set; }

        public string CreatingCo { get; set; }

        public string ReferencePo { get; set; }

        public string ReferenceName { get; set; }

        public DateTime? OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public string SupplierCo { get; set; }

        public string CustomerCo { get; set; }

        public string CustomerBillCo { get; set; }

        public Decimal? TotalPurchaseLimit { get; set; }

        public Decimal? CreaditLimit { get; set; }

        public string SupplierName { get; set; }

        public string CustomerName { get; set; }

        public string CustBillName { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string AccountId { get; set; }

        public Decimal? LateFees { get; set; }

        public Decimal? TotalPurchases { get; set; }

        public DateTime? DateSentToCustomer { get; set; }

        public string PurchaseId { get; set; }

        public string InvoiceName { get; set; }

        public DateTime? CreateDate { get; set; }

        public string InvoiceType { get; set; }

        public int? MaximumUses { get; set; }

        public string InvoiceTrigger { get; set; }

        public string InvoicePeriodNumber { get; set; }

        public string InvoicePeriodType { get; set; }

        public DateTime? FirstInvoiceDate { get; set; }

        public DateTime? NextInvoiceDate { get; set; }

        public int? PaymentGracePeriod { get; set; }

        public Single? LateFeePercentage { get; set; }

        public Decimal? AmtInvoiced { get; set; }

        public Decimal? Balance { get; set; }

        public Decimal? InvoicedBalance { get; set; }

        public Decimal? UninvoicedBalance { get; set; }

        public string BellingEmail { get; set; }

        public Decimal? TotalDebits { get; set; }

        public Decimal? TotalCredits { get; set; }

        public string CustPurchNum { get; set; }

        public Decimal? InvoiceBalance { get; set; }

        public Decimal? TotalTax { get; set; }

        public string PoNumber { get; set; }
    }
}
