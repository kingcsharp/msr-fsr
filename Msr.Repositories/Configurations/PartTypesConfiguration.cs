using Msr.Models.Parts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Repositories.Configurations
{
    class PartTypesConfiguration : EntityTypeConfiguration<PartType>
    {
        public PartTypesConfiguration()
        {
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.Spare).IsRequired();
            Property(x => x.Consumable).IsRequired();
            Property(x => x.Object_Id).IsRequired();
            Property(x => x.Unit).IsRequired();
            Property(x => x.Unit_Shipping_Weight).IsRequired();

            ToTable("A_PART_TYPES_HISTORY");
        }
    }
}
