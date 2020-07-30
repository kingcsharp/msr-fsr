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
                   .WithMany().HasForeignKey(i => i.PrimaryContactUserId);
            _ = builder.HasOne(i => i.SecondaryContactUser)
                   .WithMany().HasForeignKey(i => i.SecondaryContactUserId);
            _ = builder.HasOne(i => i.Created)
                   .WithMany().HasForeignKey(i => i.CreatedBy);
            _ = builder.HasOne(i => i.LastUpdated)
                   .WithMany().HasForeignKey(i => i.LastUpdatedBy);
        }
    }
}
