
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;

namespace MSR.Domain.Models
{
    public class PendingNotificationItem
    {
        public string Name
        {
            get
            {
                var approvalTable = (EnumApprovalTables)Table;
                var description = EnumUtils.GetDescription(approvalTable);
                description = description.Replace("Approval", "");
                return System.Text.RegularExpressions.Regex.Replace(description, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim() + "s";
            }
        }

        public int Count { get; set; }
        public int Table { get; set; }
    }
}
