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
using Msr.Models.ActualParts;
using Msr.Models.AdminCostSettings;
using Msr.Models.CustomerRequirement;
using Msr.Models.CustomerRequirements;
using Msr.Models.EquipmentMaintenances;
using Msr.Models.Products;
using Msr.Models.Monitor;
using Msr.Models.ProcedureVerbs;
using Msr.Models.People;
using Msr.Models.ProductsActualPart;
using Msr.Models.PrePro;
using Msr.Models.ProductionPlanning;
using Msr.Models.PurchesOrder;
using Msr.Models.Invoices;
using Msr.Models.Helps;
using Msr.Models.Menus;
using Msr.Models.PartTypes;
using Msr.Models.Workflows;
using Msr.Models.Training;
using Msr.Models.Reporting;

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
            modelBuilder.Entity<ProcedureVerbsView>().ToTable("Portal_ProceduresVerbsView");
            modelBuilder.Entity<ProcedureView>().ToTable("Portal_ProceduresView");
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
            modelBuilder.Entity<ApprovalWorkflowsActivitiesView>().ToTable("A_V_WORKFLOWS_FOR_ACTIVITIES");
            modelBuilder.Entity<ApprovalWorkflowStagesView>().ToTable("A_V_WORKFLOWS_WITH_STAGES");
            modelBuilder.Entity<ApprovalStagesView>().ToTable("Portal_ApprovalStagesView");
            modelBuilder.Entity<ApprovalGroupsView>().ToTable("Portal_ApprovalGroupsView");
            modelBuilder.Entity<ActivitiesView>().ToTable("Portal_ActivitiesView");
            modelBuilder.Entity<WorkflowStagesView>().ToTable("Portal_WorkflowStages");
            modelBuilder.Entity<ActualPartsView>().ToTable("Portal_ActualPartsView");
            modelBuilder.Entity<ProductsView>().ToTable("Portal_ProductsView");
            modelBuilder.Entity<PeopleObjectView>().ToTable("Portal_PeopleObjectSearchView");
            modelBuilder.Entity<ActualPartViewHistoryView>().ToTable("Portal_ActualPartsViewHistory");
            modelBuilder.Entity<ProductsActualPartView>().ToTable("Portal_ProductsActualPart");
            modelBuilder.Entity<MonitorView>().ToTable("Portal_MonitorView");
            modelBuilder.Entity<LanguagesView>().ToTable("Portal_Languages");
            modelBuilder.Entity<TimeZonesView>().ToTable("Portal_TimeZones");
            modelBuilder.Entity<OfficialPositionView>().ToTable("Portal_RolesApprovedDataQuick");
            modelBuilder.Entity<PrePropSearchView>().ToTable("Portal_PrePropSearchView");
            modelBuilder.Entity<ProcedureObjectsLaborStepView>().ToTable("Portal_ProcedureObjectLaborStepsView");
            modelBuilder.Entity<PurchesOrderView>().ToTable("Portal_PurchaseOrders");
            modelBuilder.Entity<EquipmentMaintenance>().ToTable("Portal_EquipmentMaintenance");
            modelBuilder.Entity<EquipmentMaintenanceView>().ToTable("Portal_EquipmentMaintenanceView");
            modelBuilder.Entity<CustomerRequirementView>().ToTable("Portal_CustomerRequirementView");
            modelBuilder.Entity<ProcessInfoView>().ToTable("Portal_ProcessInfo");
            modelBuilder.Entity<PartInfoView>().ToTable("Portal_PartInfo");
            modelBuilder.Entity<RequirementStep>().ToTable("Portal_RequirementSteps");
            modelBuilder.Entity<CustomerSubmittedRequirement>().ToTable("Portal_CustomerSubmittedRequirement");
            modelBuilder.Entity<PurchaseView>().ToTable("Portal_PurchaseView");
            modelBuilder.Entity<InvoiceView>().ToTable("Portal_InvoiceView");
            modelBuilder.Entity<Invoice>().ToTable("Portal_Invoice");
            modelBuilder.Entity<HelpPage>().ToTable("Portal_HelpPage");
            modelBuilder.Entity<HelpView>().ToTable("Portal_HelpView");
            modelBuilder.Entity<PartTypesApproved>().ToTable("Portal_PartTypesApprovedView");
            modelBuilder.Entity<ApprovedCompaniesView>().ToTable("Portal_ApprovedCompaniesView");
            modelBuilder.Entity<PartApprovedView>().ToTable("Portal_PartsApprovedView");
            modelBuilder.Entity<ActualPartApprovedView>().ToTable("Portal_ActualPartApprovedView");
            modelBuilder.Entity<ActualPartApprovedView>().ToTable("Portal_ActualPartApprovedView");
            modelBuilder.Entity<AdminCostSetting>().ToTable("Portal_AdminCostSetting");
            modelBuilder.Entity<MenuView>().ToTable("Portal_MenuView");
            modelBuilder.Entity<PendingApprovalView>().ToTable("Protal_PendingApprovals");
            modelBuilder.Entity<ObjectSearchView>().ToTable("Portal_ObjectSearch");
            modelBuilder.Entity<InvoiceWorkItem>().ToTable("Portal_InvoiceWorkItem");
            modelBuilder.Entity<TrainingView>().ToTable("Portal_TrainingView");
            modelBuilder.Entity<ApprovedSitesAndRoomsView>().ToTable("A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS");
            modelBuilder.Entity<ProductsSearchDataView>().ToTable("Portal_ProductsSearchDataView");
            modelBuilder.Entity<CombinedFinancialData>().ToTable("Report_CombinedFinancialData");
            modelBuilder.Entity<WorkOrdersWithoutInvoices>().ToTable("Report_WorkOrdersWithoutInvoices");
            modelBuilder.Entity<ActualPartsHistory>().ToTable("Report_ActualPartHistory");
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<UserView> UserViews { get; set; }
        public DbSet<CompanyView> CompanyViews { get; set; }
        public DbSet<ApprovedCompaniesView> ApprovedCompaniesViews { get; set; }
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
        public DbSet<PartApprovedView> PartApprovedViews { get; set; }
        public DbSet<RegionsView> RegionsViews { get; set; }
        public DbSet<PartTypesView> PartTypesViews { get; set; }
        public DbSet<ProcedureVerbsView> ProcedureVerbs { get; set; }
        public DbSet<ProcedureView> Procedures { get; set; }
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
        public DbSet<ActualPartsView> ActualPartsViews { get; set; }
        public DbSet<ProductsView> ProductsViews { get; set; }
        public DbSet<MonitorView> MonitorViews { get; set; }
        public DbSet<ApprovalWorkflowsActivitiesView> ApprovalWorkflowsActivitiesViews { get; set; }
        public DbSet<ApprovalWorkflowStagesView> ApprovalWorkflowStagesViews { get; set; }
        public DbSet<ActivitiesView> ActivitiesViews { get; set; }
        public DbSet<WorkflowStagesView> WorkflowStagesViews { get; set; }
        public DbSet<PeopleObjectView> PeopleObjectViews { get; set; }
        public DbSet<ActualPartViewHistoryView> ActualPartViewHistoryViews { get; set; }
        public DbSet<ProductsActualPartView> ProductsActualPartViews { get; set; }
        public DbSet<PrePropSearchView> PrePropSearchView { get; set; }
        public DbSet<LanguagesView> LanguagesViews { get; set; }
        public DbSet<TimeZonesView> TimeZonesViews { get; set; }
        public DbSet<OfficialPositionView> OfficialPositionViews { get; set; }
        public DbSet<ProcedureObjectsLaborStepView> ProcedureObjectsLaborStepViews { get; set; }
        public DbSet<PurchesOrderView> PurchesOrderViews { get; set; }
        public DbSet<EquipmentMaintenance> EquipmentMaintenances { get; set; }
        public DbSet<EquipmentMaintenanceView> EquipmentMaintenanceViews { get; set; }
        public DbSet<CustomerRequirementView> CustomerRequirementViews { get; set; }
        public DbSet<ProcessInfoView> ProcessInfoViews { get; set; }
        public DbSet<PartInfoView> PartInfoViews { get; set; }
        public DbSet<CustomerSubmittedRequirement> CustomerSubmittedRequirements { get; set; }
        public DbSet<PurchaseView> PurchaseViews { get; set; }
        public DbSet<InvoiceView> InvoicesViews { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<HelpPage> Helps { get; set; }
        public DbSet<HelpView> HelpViews { get; set; }
        public DbSet<PartTypesApproved> PartTypesApproveds { get; set; }
        public DbSet<ActualPartApprovedView> ActualPartApprovedViews { get; set; }
        public DbSet<AdminCostSetting> AdminCostSettings { get; set; }
        public DbSet<MenuView> MenuViews { get; set; }
        public DbSet<PendingApprovalView> PendingApprovalViews { get; set; }
        public DbSet<ObjectSearchView> ObjectSearchViews { get; set; }
        public DbSet<InvoiceWorkItem> InvoiceWorkItems { get; set; }
        public DbSet<TrainingView> TrainingViews { get; set; }
        public DbSet<ApprovedSitesAndRoomsView> ApprovedSitesAndRoomsViews { get; set; }
        public DbSet<ProductsSearchDataView> ProductsSearchDataView { get; set; }
        public DbSet<CombinedFinancialData> CombinedFinancialData { get; set; }
        public DbSet<WorkOrdersWithoutInvoices> WorkOrdersWithoutInvoices { get; set; }
        public DbSet<ActualPartsHistory> ActualPartsHistory { get; set; }
    }
}
