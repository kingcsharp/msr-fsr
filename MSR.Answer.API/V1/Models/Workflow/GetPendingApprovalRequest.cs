using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Models
{
    public class GetPendingApprovalRequest
    {
        public EnumApprovalTables Table { get; set; }
    }
}
