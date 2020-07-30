using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable(nameof(Location));
            _ = builder.HasOne(i => i.Created)
                   .WithMany().HasForeignKey(i => i.CreatedBy);
            _ = builder.HasOne(i => i.LastUpdated)
                   .WithMany().HasForeignKey(i => i.LastUpdatedBy);
        }
    }
}
