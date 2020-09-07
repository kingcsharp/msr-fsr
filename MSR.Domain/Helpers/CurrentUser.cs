using MSR.Domain.Commanding.Enums;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Helpers
{
    public static class CurrentUser
    {
        public static Func<int> GetId;
        public static Func<EnumApprovalTables, bool> CanApproveActivity;
        public static Func<EnumApprovalTables, bool> CanReadActivity;
        public static Func<EnumMenuItem, EnumPrivilege, bool> HasPrivilege;

        //public static Func<ICollection<int>> GetRoles;
        public static Func<string> TokenString;
    }
}
