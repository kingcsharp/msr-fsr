using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Reporting
{
    public class OperationsData
    {
        [Key]
        public Guid Id { get; set; }
        public string Site { get; set; }
        public string Month { get; set; }
        public string CustomerName { get; set; }
        public string KitName { get; set; }
        public double Qty { get; set; }
        public decimal Revenue { get; set; }
    }
}
