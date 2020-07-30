using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(nameof(UserRole));
            builder.HasOne(ur => ur.User).WithMany(u => u.Roles);
            builder.HasOne(ur => ur.Role).WithMany(r => r.Users);
            _ = builder.HasOne(i => i.Created)
                   .WithMany().HasForeignKey(i => i.CreatedBy);
            _ = builder.HasOne(i => i.LastUpdated)
                   .WithMany().HasForeignKey(i => i.LastUpdatedBy);
        }
    }
}
