using System.ComponentModel;
namespace MSR.Domain.Commanding.Enums
{
    public enum EnumApprovalTables
    {
        [Description("CustomerApproval")]
        CustomerApproval = 1,
        [Description("DocumentApproval")]
        DocumentApproval = 2,
        [Description("LocationApproval")]
        LocationApproval = 3,
        [Description("PartApproval")]
        PartApproval = 4,
        [Description("ProcedureApproval")]
        ProcedureApproval = 5,
        [Description("ProductApproval")]
        ProductApproval = 6,
        [Description("PurchaseOrderApproval")]
        PurchaseOrderApproval = 7,
        [Description("UserApproval")]
        UserApproval = 8,
        [Description("All")]
        All = 9
    }
}
