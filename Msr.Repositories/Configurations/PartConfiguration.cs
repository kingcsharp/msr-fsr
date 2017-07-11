using Msr.Models.Parts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Repositories.Configurations
{
    public class PartConfiguration : EntityTypeConfiguration<Part>
    {
        public PartConfiguration()
        {
            Property(x => x.ID).IsRequired().HasColumnName("ID");
            Property(x => x.Unit).HasColumnName("UNIT");
            Property(x => x.Name).HasColumnName("NAME");
            Property(x => x.PartType).HasColumnName("PART_TYPE");
            Property(x => x.Spare).HasColumnName("SPARE");
            Property(x => x.Consumable).HasColumnName("CONSUMABLE");
            Property(x => x.TrackStartFrom).HasColumnName("TRACK_FROM_START");
            Property(x => x.Drcm).HasColumnName("DRCM");
            Property(x => x.ModBy).HasColumnName("MODBY");
            Property(x => x.Company).HasColumnName("COMPANY");
            Property(x => x.ObjectID).HasColumnName("OBJECT_ID");
            Property(x => x.CompanyName).HasColumnName("COMPANY_NAME");
            Property(x => x.PartTypeName).HasColumnName("PART_TYPE_NAME");
            Property(x => x.UnitShippingWeight).HasColumnName("UNIT_SHIPPING_WEIGHT");
            Property(x => x.CompanyPartNumber).HasColumnName("COMPANY_PART_NUMBER");
            Property(x => x.CustomerSeeAvailability).HasColumnName("SUPPLIER_SEE_INSTALL_BASE");
            Property(x => x.SupplierSeeAvailability).HasColumnName("SUPPLIER_SEE_AVAILABILITY");
            Property(x => x.SupplierSeeInstallBase).HasColumnName("CUSTOMER_SEE_AVAILABILITY");
            Property(x => x.WeightType).HasColumnName("WEIGHT_TYPE");
            Property(x => x.CreateProd).HasColumnName("CREATE_PROD");
            Property(x => x.SupplierCo).HasColumnName("SUPPLIER_CO");
            Property(x => x.ProductType).HasColumnName("PRODUCT_TYPE");
            Property(x => x.ProcVerb).HasColumnName("PROC_VERB");
            Property(x => x.Price).HasColumnName("PRICE");

            ToTable("A_PARTS_HISTORY");
        }
    }
}
