using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> SecurityLevelList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Creating or Approved", Value = "CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING"
                    
                },
                new SelectListItem
                {
                    Text = @"Creating",
                    Value = "CREATING, DENIED",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"In Approval Workflow",
                    Value = "IN_WORKFLOW"
                },
                new SelectListItem
                {
                    Text = @"Approved",
                    Value = "APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING"
                },
                new SelectListItem
                {
                    Text = @"Denied",
                    Value = "DENIED"
                },
                new SelectListItem
                {
                    Text = @"Approved But Being Revised",
                    Value = "APPROVED_BUT_REVISING"
                },
                new SelectListItem
                {
                    Text = @"Approved But Being Deleted",
                    Value = "APPROVED_BUT_DELETING"
                },
                new SelectListItem
                {
                    Text = @"Denied",
                    Value = "DENIED"
                },
                new SelectListItem
                {
                    Text = @"Deleted",
                    Value = "DELETED"
                },
                new SelectListItem
                {
                    Text = @"Obsolete",
                    Value = "OLD"
                }
            };
        }
    }
}
