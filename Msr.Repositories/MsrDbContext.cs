using System.Data.Entity;
using Msr.Models.Notes;
using Msr.Models.Orders;
using Msr.Models.Tasks;
using Msr.Models.Users;
using Msr.Repositories.Configurations;

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
            modelBuilder.Configurations.Add(new AspNetUserConfiguration());
            modelBuilder.Entity<AspNetRole>().ToTable("AspNetRoles");           
            modelBuilder.Entity<ClientUser>().ToTable("Portal_ClientUsers");

            modelBuilder.Entity<WorkOrderView>().ToTable("Portal_WorkOrders");
            modelBuilder.Entity<BuyerView>().ToTable("Portal_BuyerView");
            modelBuilder.Entity<ApprovedPeopleView>().ToTable("Portal_ApprovedPeople");
            modelBuilder.Entity<UserView>().ToTable("Portal_UserView");
            modelBuilder.Entity<CompanyView>().ToTable("Portal_CompanyView");
            modelBuilder.Entity<Note>().ToTable("Portal_Note");
            modelBuilder.Entity<PeopleView>().ToTable("Portal_PeopleView");
            modelBuilder.Entity<MonitorResult>().ToTable("Portal_MonitorResults");
            modelBuilder.Entity<FileSearchView>().ToTable("Portal_FileSearchView");
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<UserView> UserViews { get; set; }
        public DbSet<CompanyView> CompanyView { get; set; }

        public DbSet<WorkOrderView> WorkOrders { get; set; }
        public DbSet<BuyerView> BuyerViews { get; set; }
        public DbSet<ApprovedPeopleView> ApprovedPeoples { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<PeopleView> Peoples { get; set; }
        public DbSet<MonitorResult> MonitorResults { get; set; }
        public DbSet<FileSearchView> FileSearchView { get; set; }
        
    }
}
