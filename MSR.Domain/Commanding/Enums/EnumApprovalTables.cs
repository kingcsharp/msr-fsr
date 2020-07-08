using System.ComponentModel;
namespace MSR.Domain.Commanding.Enums
{
    public enum EnumApprovalTables
    {
        [Description("Customer Approval")]
        CustomerApproval = 1,
        [Description("Document Approval")]
        DocumentApproval = 2,
        [Description("Location Approval")]
        LocationApproval = 3,
        [Description("Part Approval")]
        PartApproval = 4,
        [Description("Procedure Approval")]
        ProcedureApproval = 5,
        [Description("Product Approval")]
        ProductApproval = 6,
        [Description("Purchase Order")]
        PurchaseOrderApproval = 7,
        [Description("User Approval")]
        UserApproval = 8

        //[Description("Purchase Order Product Approval")]
        //PurchaseOrderProductApproval = 11,
        //[Description("Procedure Step Approval")]
        ////ProcedureStepApproval = 6,
        //[Description("Procedure Step Document Approval")]
        //ProcedureStepDocumentApproval = 7,
        //[Description("Procedure Step Monitor Approval")]
        //ProcedureStepMonitorApproval = 8,
        //[Description("User Role Approval")]
        //UserRoleApproval = 13
    }
}
