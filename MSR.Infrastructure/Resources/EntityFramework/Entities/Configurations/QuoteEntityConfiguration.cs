using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class QuoteEntityConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            _ = builder.ToTable(nameof(Quote));
            _ = builder.HasKey(nameof(Quote.Id));
            //_ = builder.HasOne(i => i.Customer)
            //       .WithOne()
            //       .HasForeignKey<Customer>(c => c.Id);
            //_ = builder.HasOne(i => i.SubmittedBy)
            //       .WithOne()
            //       .HasForeignKey<User>(c => c.Id);
        }
    }
}
