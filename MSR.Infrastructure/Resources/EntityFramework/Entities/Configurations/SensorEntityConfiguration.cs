using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class SensorEntityConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            _ = builder.ToTable(nameof(Sensor));
            _ = builder.HasOne(i => i.Site).WithMany().HasForeignKey(i => i.SiteId);
            _ = builder.HasOne(i => i.AssignedLocation).WithMany(i => i.Sensors).HasForeignKey(i => i.AssignedLocationId);
        }
    }
}
