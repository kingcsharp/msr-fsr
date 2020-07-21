using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class PartEntityConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable(nameof(Part));
            builder.HasMany(part => part.Subparts).WithOne().HasForeignKey(sp => sp.ParentPartId);
        }
    }
}
