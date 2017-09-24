using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.PurchesOrder
{
   public class PurchesOrderView
    {
        public string Id { get; set; }

        public string ObjectId { get; set; }

        public string Name { get; set; }

        public string ReferencePo { get; set; }

        public string ReferenceName { get; set; }

        public string OpenDate { get; set; }

        public string CloseDate { get; set; }

        public string SupplierCo { get; set; }

        public string CustomerCo { get; set; }

        public string CustomerBill { get; set; }

        public int? MaximumUses { get; set; }

        public Decimal? TotalPurchaseLimit { get; set; }

        public Decimal? CreditLimit { get; set; }

        public string ApprovalWf { get; set; }

        public string InvoiceTrigger { get; set; }

        public string InvoicePeriodNumber { get; set; }

        public string InvoicePeriodType { get; set; }

        public DateTime? FirstInvoiceDate { get; set; }

        public DateTime? NextInvoiceDate { get; set; }

        public int? PaymentGracePeriod { get; set; }

        public Single? LateFeePercentage { get; set; }

        public int? ReapplyLateFee { get; set; }

        public DateTime? Drcm { get; set; }

        public string ModBy { get; set; }

        public string AccType { get; set; }

        public Decimal? TotalPurchases { get; set; }

        public Decimal? Balance { get; set; }

        public Decimal? AmtInvoiced { get; set; }

        public string AcctStatus { get; set; }

        public string SupplierName { get; set; }

        public string CustomerName { get; set; }

        public string CustBillName { get; set; }

        public string ObjId { get; set; }

        public string LockedBy { get; set; }

        public string UnlockedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Root { get; set; }

        public string RevInfo { get; set; }

        public string CreatingCo { get; set; }

        public string Status { get; set; }

        public int? Rev { get; set; }

        public string WfsId { get; set; }

        public string LockedByName { get; set; }

        public string CreatingCoName { get; set; }

        public string ApprovalActivity { get; set; }

        public Decimal? InvoicedBalance { get; set; }

        public Decimal? UninvoicedBalance { get; set; }

        public Decimal? TotalCredits { get; set; }

        public Decimal? TotalDebits { get; set; }

        public string BillingEmail { get; set; }

        public decimal? UnusedAmmount { get; set; }
    }
}