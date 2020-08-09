using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class RoleChildRoleMapEntityConfiguration : IEntityTypeConfiguration<RoleChildRoleMap>
    {
        public void Configure(EntityTypeBuilder<RoleChildRoleMap> builder)
        {
            _ = builder.ToTable(nameof(RoleChildRoleMap));
            _ = builder.HasOne(i => i.ParentRole).WithMany(i => i.ChildRoles).HasForeignKey(i => i.ParentRoleId);
            _ = builder.HasOne(i => i.ChildRole).WithMany(i => i.ParentRoles).HasForeignKey(i => i.ChildRoleId);
            _ = builder.HasOne(i => i.Created)
                   .WithMany().HasForeignKey(i => i.CreatedBy);
            _ = builder.HasOne(i => i.LastUpdated)
                   .WithMany().HasForeignKey(i => i.LastUpdatedBy);
        }
    }
}
