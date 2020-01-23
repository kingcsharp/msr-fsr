using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Reporting
{
    public class OperationsCustomerRevenueData
    {
        [Key]
        public Guid Id { get; set; }
        public string Site { get; set; }
        public string Month { get; set; }
        public string CustomerName { get; set; }
        public decimal Revenue { get; set; }
    }
    public class OperationsKitRevenueData
    {
        [Key]
        public Guid Id { get; set; }
        public string Site { get; set; }
        public string Month { get; set; }
        public string KitName { get; set; }
        public decimal Revenue { get; set; }
    }
    public class OperationsKitCountData
    {
        [Key]
        public Guid Id { get; set; }
        public string Site { get; set; }
        public string Month { get; set; }
        public string KitName { get; set; }
        public int? Count { get; set; }
    }
}
