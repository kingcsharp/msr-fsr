using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));
            builder.HasOne(i => i.PrimaryContactUser)
                   .WithOne()
                   .HasForeignKey("PrimaryConteactUserId");
            builder.HasOne(i => i.SecondaryContactUser)
                   .WithOne()
                   .HasForeignKey("SecondaryConteactUserId");
        }
    }
}
