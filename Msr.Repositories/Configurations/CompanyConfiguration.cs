using Msr.Models.Users;
using System.Data.Entity.ModelConfiguration;

namespace Msr.Repositories.Configurations
{
    class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            Property(x => x.ID).IsRequired().HasColumnName("ID");
            Property(x => x.Name).HasColumnName("NAME");
            Property(x => x.CoType).HasColumnName("CO_TYPE");
            Property(x => x.Parent).HasColumnName("PARENT");
            Property(x => x.Phone).HasColumnName("PHONE");
            Property(x => x.Location).HasColumnName("LOCATION");
            Property(x => x.LocationName).HasColumnName("LOCATION_NAME");
            Property(x => x.ParentName).HasColumnName("PARENT_NAME");
            Property(x => x.Drcm).HasColumnName("DRCM");
            Property(x => x.ModBy).HasColumnName("MODBY");
            Property(x => x.ObjectId).HasColumnName("OBJECT_ID");
            Property(x => x.RootCoID).HasColumnName("ROOT_CO_ID");

            ToTable("A_COMPANIES_HISTORY");
        }
    }
}
