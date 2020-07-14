using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Commands
{
    public class GetPendingApproval: Command
    {
        public EnumApprovalTables Table { get; set; }
    }
}