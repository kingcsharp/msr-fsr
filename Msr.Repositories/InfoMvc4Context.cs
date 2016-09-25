using System.Data.Entity;
using Msr.Models.Orders;
using Msr.Models.Users;

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
            modelBuilder.Entity<AspNetUser>().ToTable("AspNetUsers");

            modelBuilder.Entity<WorkOrderView>().ToTable("Portal_WorkOrders");
            modelBuilder.Entity<BuyerView>().ToTable("Portal_BuyerView");
            modelBuilder.Entity<ApprovedPeopleView>().ToTable("Portal_ApprovedPeople");
            modelBuilder.Entity<UserView>().ToTable("Portal_UserView");
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<UserView> UserViews { get; set; }

        public DbSet<WorkOrderView> WorkOrders { get; set; }
        public DbSet<BuyerView> BuyerViews { get; set; }
        public DbSet<ApprovedPeopleView> ApprovedPeoples { get; set; }
    }
}
