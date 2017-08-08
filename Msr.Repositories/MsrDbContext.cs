using System.Data.Entity;
using Msr.Models.Notes;
using Msr.Models.Orders;
using Msr.Models.Parts;
using Msr.Models.Procedure;
using Msr.Models.Procedures;
using Msr.Models.Tasks;
using Msr.Models.TimeZones;
using Msr.Models.Users;
using Msr.Repositories.Configurations;
using Msr.Models.Files;
using Msr.Models.Regions;
using Msr.Models.Locations;
using Msr.Models.Roles;
using Msr.Models.Documents;
using Msr.Models.Companies;
using Msr.Models.ApprovalWorkflows;
using Msr.Models.Objects;
using Msr.Models.TheoryParagraphs;
using Msr.Models.ApprovalStages;
using Msr.Models.ApprovalGroups;
using Msr.Models.Monitor;

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
            modelBuilder.Entity<ProcedureTypesView>().ToTable("Portal_ProcedureTypesView");
            modelBuilder.Entity<ProcedureView>().ToTable("Portal_ProcedureListView");
            modelBuilder.Entity<FileView>().ToTable("Portal_FilesView");
            modelBuilder.Entity<RegionsView>().ToTable("Portal_RegionsView");
            modelBuilder.Entity<LocationView>().ToTable("Portal_LocationsView");
            modelBuilder.Entity<TaskLog>().ToTable("Portal_Task_Logs");
            modelBuilder.Entity<RolesView>().ToTable("Portal_RolesView");
            modelBuilder.Entity<DocumentView>().ToTable("Portal_DocumentsView");
            modelBuilder.Entity<ObjectView>().ToTable("Protal_ObjectsView");
            modelBuilder.Entity<TheoryParagraphView>().ToTable("Portal_TheoryParagraphsView");
            modelBuilder.Entity<DocumentFilesView>().ToTable("A_V_DOCUMENTS_LINKED");
            modelBuilder.Entity<HeadPeopleView>().ToTable("Portal_HeadPeopleView");
            modelBuilder.Entity<ApprovalWorkflowsView>().ToTable("Portal_ApprovalWorkflowsView");
            modelBuilder.Entity<ApprovalStagesView>().ToTable("Portal_ApprovalStagesView");
            modelBuilder.Entity<ApprovalGroupsView>().ToTable("Portal_ApprovalGroupsView");
            modelBuilder.Entity<MonitorView>().ToTable("A_V_MONITORS_WITH_TASK_AND_RESULT");
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<UserView> UserViews { get; set; }
        public DbSet<CompanyView> CompanyViews { get; set; }
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
        public DbSet<ProcedureTypesView> ProcedureTypes { get; set; }
        public DbSet<VerbType> VerbTypes { get; set; }
        public DbSet<ProcedureView> Procedurs { get; set; }
        public DbSet<FileView> FIleViews { get; set; }
        public DbSet<LocationView> LocationViews { get; set; }
        public DbSet<TaskLog> TaskLogs { get; set; }
        public DbSet<RolesView> RolesViews { get; set; }
        public DbSet<DocumentView> DocumentViews { get; set; }
        public DbSet<DocumentFilesView> DocumentFilesViews { get; set; }
        public DbSet<HeadPeopleView> HeadPeopleViews { get; set; }
        public DbSet<ApprovalWorkflowsView> ApprovalWorkflowsViews { get; set; }


        public DbSet<ObjectView> ObjectViews { get; set; }
        public DbSet<TheoryParagraphView> TheoryParagraphViews { get; set; }
        public DbSet<ApprovalStagesView> ApprovalStagesViews { get; set; }
        public DbSet<ApprovalGroupsView> ApprovalGroupsViews { get; set; }
        public DbSet<MonitorView> MonitorViews { get; set; }

    }
}
