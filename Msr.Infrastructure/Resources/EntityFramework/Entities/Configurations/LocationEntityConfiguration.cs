using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable(nameof(Location));
            builder.HasKey(nameof(Location.Id));
        }
    }
}
