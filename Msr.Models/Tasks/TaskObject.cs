
using System;

namespace Msr.Models.Tasks
{
    public class TaskObject
    {
        public string ID { get; set; }
	public string PARENT_ID { get; set; }
	public string Title { get; set; }
	public string DESCRIPTION { get; set; }
	public string STATUS { get; set; }
	public string COMMENT { get; set; }
	public string REQUESTOR { get; set; }
	public string COMPLETED_BY { get; set; }
	public string MODBY { get; set; }
	public DateTime? DRCM { get; set; }
	public int? CHILD_ORDER { get; set; }
	public string CREATED_BY { get; set; }
	public DateTime? CREATE_DATE { get; set; }
	public Int16? CLOSED { get; set; }
	public string OPENED_BY { get; set; }
	public DateTime? OPEN_DATE { get; set; }
	public string SYSTEM_TASK { get; set; }
	public string PROCEDURE_ID { get; set; }
    }
}
