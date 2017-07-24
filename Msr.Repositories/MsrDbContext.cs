using System.Data.Entity;
using Msr.Models.Notes;
using Msr.Models.Orders;
using Msr.Models.Parts;
using Msr.Models.Procedures;
using Msr.Models.Tasks;
using Msr.Models.TimeZones;
using Msr.Models.Users;
using Msr.Repositories.Configurations;
using Msr.Models.Files;
using Msr.Models.Locations;
using Msr.Models.Procedure;
using Msr.Models.Regions;

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
            modelBuilder.Configurations.Add(new PartConfiguration());
            modelBuilder.Configurations.Add(new PartTypesConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new VerbTypeConfiguration());

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
            modelBuilder.Entity<Part>().ToTable("A_PARTS_HISTORY");
            modelBuilder.Entity<ProcedureTypesView>().ToTable("Portal_ProcedureTypesView");
            modelBuilder.Entity<FileView>().ToTable("Portal_FilesView");
            modelBuilder.Entity<RegionsView>().ToTable("Portal_RegionsView");
            modelBuilder.Entity<LocationView>().ToTable("Portal_LocationsView");

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
        public DbSet<RegionsView> RegionsViews { get; set; }
        public DbSet<PartTypesView> PartTypesViews { get; set; }
        public DbSet<PartType> PartTypes { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ProcedureTypesView> ProcedureTypes { get; set; }
        public DbSet<VerbType> VerbTypes { get; set; }
        public DbSet<ProcedureView> Procedurs { get; set; }
        public DbSet<FileView> FIleViews { get; set; }
        public DbSet<LocationView> LocationViews { get; set; }


    }
}
