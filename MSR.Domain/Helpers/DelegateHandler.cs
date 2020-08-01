using MSR.Domain.Commanding.Enums;
using System;

namespace MSR.Domain.Helpers
{
    public static class DelegateHandler
    {
        public static Func<int> GetCurrentUserId;
        [Obsolete("Please use CurrentUserInformation class through DI")]
        public static Func<EnumApprovalTables, bool> CanApproveActivity;
        [Obsolete("Please use CurrentUserInformation class through DI")]
        public static Func<EnumApprovalTables, bool> CanReadActivity;
        [Obsolete("Please use CurrentUserInformation class through DI")]
        public static Func<EnumMenuItem, EnumPrivilege, bool> HasPrivilege;
    }
}
