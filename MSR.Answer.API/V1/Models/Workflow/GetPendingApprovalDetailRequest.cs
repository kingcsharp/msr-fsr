using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Models
{
    public class GetPendingApprovalDetailRequest
    {
        public EnumApprovalTables Table { get; set; }
        public int Id { get; set; }
    }
}
