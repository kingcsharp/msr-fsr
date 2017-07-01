using System.Data.Entity;
using Msr.Models.Notes;
using Msr.Models.Orders;
using Msr.Models.Parts;
using Msr.Models.Tasks;
using Msr.Models.TimeZones;
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
            modelBuilder.Entity<TimeZoneView>().ToTable("Portal_TimeZoneView");
            modelBuilder.Entity<MonitorsWithTaskAndResult>().ToTable("Portal_MonitorsWithTaskAndResults");
            modelBuilder.Entity<PartsView>().ToTable("Portal_PartsView");
            modelBuilder.Entity<PartTypesView>().ToTable("Portal_PartTypesView");
            modelBuilder.Entity<PartType>().ToTable("A_PART_TYPES_HISTORY");
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
        public DbSet<TimeZoneView> TimeZoneView { get; set; }
        public DbSet<MonitorsWithTaskAndResult> MonitorsWithTaskAndResults { get; set; }
        public DbSet<PartsView> PartsViews { get; set; }
        public DbSet<PartTypesView> PartTypesViews { get; set; }
        public DbSet<PartType> PartTypes { get; set; }

    }
}
