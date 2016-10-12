using System.Data.Entity.ModelConfiguration;
using Msr.Models.Users;

namespace Msr.Repositories.Configurations
{
    public class AspNetUserConfiguration : EntityTypeConfiguration<AspNetUser>
    {
        public AspNetUserConfiguration()
        {
            Property(x => x.Id).IsRequired();
            Property(x => x.UserName).IsRequired();


            HasMany(x => x.AspNetRoles).WithMany(e => e.AspNetUsers).Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
                m.ToTable("AspNetUserRoles");
            });

            ToTable("AspNetUsers");
        }
    }
}
