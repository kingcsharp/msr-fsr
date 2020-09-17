using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Commands
{
    public class DeactivateApprovalModel : Command
    {
        public EnumApprovalTables Table { get; set; }
        public int Id { get; set; }
        public string Comment { get; set; }
    }
}