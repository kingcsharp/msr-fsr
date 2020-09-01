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
            _ = builder.HasOne(q => q.Customer)
                       .WithMany(c => c.Quotes)
                       .HasForeignKey(q => q.CustomerId)
                       .IsRequired();
            _ = builder.HasOne(q => q.SubmittedBy)
                   .WithMany()
                   .HasForeignKey(u => u.SubmittedById)
                   .IsRequired();
        }
    }
}
