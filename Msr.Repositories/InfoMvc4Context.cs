using System.Data.Entity;
using Msr.Models.Orders;

namespace Msr.Repositories
{
    public partial class MsrDbContext : DbContext
    {
        public MsrDbContext()
            : base("name=MsrPortal")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkOrderView>().ToTable("Portal_WorkOrders");
        }

        public DbSet<WorkOrderView> WorkOrders { get; set; }
    }
}
