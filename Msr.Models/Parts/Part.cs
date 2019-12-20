using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Parts
{
    public class Part
    {
        public string ID { get; set; }

        public string Unit { get; set; }

        public string Name { get; set; }

        public string PartType { get; set; }

        public string Spare { get; set; }

        public string Consumable { get; set; }

        public string TrackStartFrom { get; set; }

        public DateTime Drcm { get; set; }

        public string ModBy { get; set; }

        public string Company { get; set; }

        public string ObjectID { get; set; }

        public string CompanyName { get; set; }

        public string PartTypeName { get; set; }

        public double? UnitShippingWeight { get; set; }

        public string CompanyPartNumber { get; set; }

        public string OemPartNumber { get; set; }

        public Int16? CustomerSeeAvailability { get; set; }

        public Int16? SupplierSeeAvailability { get; set; }

        public Int16? SupplierSeeInstallBase { get; set; }

        public string WeightType { get; set; }

        public byte? CreateProd { get; set; }

        public string SupplierCo { get; set; }

        public string ProductType { get; set; }

        public string ProcVerb { get; set; }

        public string Price { get; set; }

    }
}
