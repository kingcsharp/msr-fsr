using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            _ = builder.ToTable(nameof(Customer));
            _ = builder.HasOne(i => i.PrimaryContactUser)
                   .WithOne()
                   .HasForeignKey<User>(c => c.Id);
            _ = builder.HasOne(i => i.SecondaryContactUser)
                   .WithOne()
                   .HasForeignKey<User>(c => c.Id);
        }
    }
}
