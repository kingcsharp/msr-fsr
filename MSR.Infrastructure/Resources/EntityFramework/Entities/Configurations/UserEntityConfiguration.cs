using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasOne(u => u.Customer).WithMany().HasForeignKey(u => u.CustomerId);
            builder.HasOne(u => u.Supervisor).WithMany().HasForeignKey(u => u.SupervisorId);
            builder.HasOne(u => u.Location).WithMany().HasForeignKey(u => u.LocationId);
            _ = builder.HasOne(i => i.Created)
                   .WithMany().HasForeignKey(i => i.CreatedBy);
            _ = builder.HasOne(i => i.LastUpdated)
                   .WithMany().HasForeignKey(i => i.LastUpdatedBy);
        }
    }
}
