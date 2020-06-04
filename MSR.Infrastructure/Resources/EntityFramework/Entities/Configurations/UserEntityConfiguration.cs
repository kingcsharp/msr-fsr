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
        }
    }
}
