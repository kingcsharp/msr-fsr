using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class MenuRoleEntityConfiguration : IEntityTypeConfiguration<MenuRole>
    {
        public void Configure(EntityTypeBuilder<MenuRole> builder)
        {
            builder.ToTable(nameof(MenuRole));
            builder.HasOne(mr => mr.Role).WithMany(r => r.Menus);
            builder.HasOne(mr => mr.MenuItem).WithMany(mi => mi.Roles);
            builder.HasOne(mr => mr.MenuRolePermission).WithOne(mrp => mrp.MenuRole);
        }
    }
}
