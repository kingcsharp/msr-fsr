using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities.Configurations
{
    public class FileEntityMapConfiguration : IEntityTypeConfiguration<FileEntityMap>
    {
        public void Configure(EntityTypeBuilder<FileEntityMap> builder)
        {
            builder.ToTable(nameof(FileEntityMap));

            builder.HasOne(x => x.ProcedureTemplate)
                .WithMany(y => y.ReferenceFiles)
                .HasForeignKey(z => z.EntityId);

            builder.HasOne(x => x.Procedure)
                .WithMany(y => y.ReferenceFiles)
                .HasForeignKey(z => z.EntityId);

            builder.HasOne(x => x.WorkOrderTask)
                .WithMany(y => y.ReferenceFiles)
                .HasForeignKey(z => z.EntityId);
        }
    }
}
