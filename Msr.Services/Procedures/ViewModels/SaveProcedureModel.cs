using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Models.Procedures
{
	public class SaveProcedureModel
	{
		public string Id { get; set; }
		public string ProcedureName { get; set; }
		public string RootCompany { get; set; }
		public string CreatingCompany { get; set; }
		//public string ProcedureType { get; set; }
		public string SecurityClearanceLevel { get; set; }
		public int? Revision { get; set; }
		public string ApprovalStatus { get; set; }
		public string CreatedBy { get; set; }

		public List<SelectListItem> ApprovalStatusList { get
			{
				return new List<SelectListItem>
			{
					new SelectListItem{ Text = "1 View What All Users Are Allowed to View", Value = "1" },
				new SelectListItem
				{
					Text = "2 View What Managers & Above Are Allowed to View",
					Value = "2",
					Selected = true
				},
				new SelectListItem
				{
					Text = "3 Only Directors & Above Allowed To View",
					Value = "3"
				},
				new SelectListItem
				{
					Text = "4 View What VP's & Above Are Allowed to View",
					Value = "4"
				}
			};
			}
		}
		public List<SelectListItem> SecurityLevelList
		{
			get
			{
				return new List<SelectListItem>
			{
					new SelectListItem{ Text = "Creating or Approved", Value = "CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING" },
				new SelectListItem
				{
					Text = "Creating",
					Value = "CREATING, DENIED",
					Selected = true
				},
				new SelectListItem
				{
					Text = "In Approval Workflow",
					Value = "IN_WORKFLOW"
				},
				new SelectListItem
				{
					Text = "Approved",
					Value = "APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING"
				},
				new SelectListItem
				{
					Text = "Denied",
					Value = "DENIED"
				},
				new SelectListItem
				{
					Text = "Approved But Being Revised",
					Value = "APPROVED_BUT_REVISING"
				},
				new SelectListItem
				{
					Text = "Approved But Being Deleted",
					Value = "APPROVED_BUT_DELETING"
				},
				new SelectListItem
				{
					Text = "Denied",
					Value = "DENIED"
				},
				new SelectListItem
				{
					Text = "Deleted",
					Value = "DELETED"
				},
				new SelectListItem
				{
					Text = "Obsolete",
					Value = "OLD"
				}

			};
			}
		}
	}
}
