using MSR.Domain.Commanding.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Domain.Models.Config
{
    public class CurrentUserInformation
    {
        public int Id { get; private set; }
        public Dictionary<int, int[]> ApprovalPrivileges { get; private set; }
        public int[][] UserPrivileges { get; private set; }

        public CurrentUserInformation(int id, Dictionary<int, int[]> approvalPrivileges, int[][] userPrivileges)
        {
            Id = id;
            ApprovalPrivileges = approvalPrivileges;
            UserPrivileges = userPrivileges;
        }

        public bool HasPrivilege(EnumMenuItem menuItem, EnumPrivilege privilege)
        {
            var menuItemPrivileges = UserPrivileges[(int)menuItem];
            if (Array.IndexOf(menuItemPrivileges, (int)privilege) == -1)
            {
                return false;
            }

            return true;
        }

        public bool CanReadActivity(EnumApprovalTables approvalTables)
        {
            var activityToBeApproved = (int)approvalTables;

            ApprovalPrivileges.TryGetValue(activityToBeApproved, out var privileges);

            return privileges == null ? false : privileges.Contains((int)EnumPrivilege.CanRead);
        }

        public bool CanApproveActivity(EnumApprovalTables approvalTables)
        {
            var activityToBeApproved = (int)approvalTables;

            ApprovalPrivileges.TryGetValue(activityToBeApproved, out int[] privileges);

            return privileges == null ? false : privileges.Contains((int)EnumPrivilege.CanApprove);
        }
    }
}
