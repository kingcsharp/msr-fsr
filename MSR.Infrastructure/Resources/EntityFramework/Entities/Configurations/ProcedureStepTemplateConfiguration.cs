using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class ProcedureStepTemplateConfiguration : IEntityTypeConfiguration<ProcedureStepTemplate>
    {
        public void Configure(EntityTypeBuilder<ProcedureStepTemplate> builder)
        {
            builder.ToTable(nameof(ProcedureStepTemplate));
            builder.HasKey(x => x.Id);
        }
    }
}
