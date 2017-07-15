using Msr.Models.Procedures;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Repositories.Configurations
{
    public class VerbTypeConfiguration : EntityTypeConfiguration<VerbType>
    {
        public VerbTypeConfiguration()
        {
            Property(x => x.ID).IsRequired().HasColumnName("ID");
            Property(x => x.NAME).HasColumnName("NAME");
            Property(x => x.Verbtype).HasColumnName("VERB_TYPE");
            Property(x => x.ShowName).HasColumnName("SHOW_NAME");
            Property(x => x.DRCM).HasColumnName("DRCM");
            Property(x => x.MODBY).HasColumnName("MODBY");
            Property(x => x.ObjectId).HasColumnName("OBJECT_ID");

            ToTable("A_TT_VERBS_HISTORY");
        }
    }
}
