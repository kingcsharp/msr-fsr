using MSR.Domain.Commanding.Enums;
using System;

namespace MSR.Domain.Helpers
{
    public static class DelegateHandler
    {
        public static Func<int> GetCurrentUserId;
        public static Func<EnumApprovalTables, bool> CanApproveActivity;
        public static Func<EnumApprovalTables, bool> CanReadActivity;
        public static Func<EnumMenuItem, EnumPrivilege, bool> HasPrivilege;
    }
}
