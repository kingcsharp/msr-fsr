using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Commands
{
    public class GetPendingApprovalModel: Command
    {
        public EnumApprovalTables Table { get; set; }
    }
}