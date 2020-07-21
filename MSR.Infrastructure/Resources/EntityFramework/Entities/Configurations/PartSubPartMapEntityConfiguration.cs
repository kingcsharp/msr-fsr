using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class PartSubPartMapEntityConfiguration : IEntityTypeConfiguration<PartSubPartMap>
    {
        public void Configure(EntityTypeBuilder<PartSubPartMap> builder)
        {
            builder.ToTable(nameof(PartSubPartMap));
            builder.HasOne(sp => sp.ParentPart).WithMany(p => p.Subparts);
            builder.HasOne(sp => sp.Part);
        }
    }
}
