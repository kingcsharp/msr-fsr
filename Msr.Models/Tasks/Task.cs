using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Tasks
{
    public class Task
    {
        public string ID { get; set; }
        public string SORT_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string STATUS { get; set; }
        public string REQUESTOR { get; set; }
        public decimal? CHILD_ORDER { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string SYSTEM_TASK { get; set; }
        public string PROCEDURE_ID { get; set; }
        public string REQUESTEE_ID { get; set; }
        public string GROUP_REQUESTEE_ID { get; set; }
        public DateTime? ORIG_PLANNED_START_DATE { get; set; }
        public DateTime? ORIG_PLANNED_STOP_DATE { get; set; }
        public DateTime? CUR_PLANNED_START_DATE { get; set; }
        public DateTime? CUR_PLANNED_STOP_DATE { get; set; }
        public DateTime? ACTUAL_START_DATE { get; set; }
        public DateTime? ACTUAL_STOP_DATE { get; set; }
        public decimal? CUR_PLANNED_COUNTER_START { get; set; }
        public string LATEST_REQUESTEE_NAME { get; set; }
        public Byte? HAS_DISCUSSION { get; set; }
        public Byte? HAS_SURVEY { get; set; }
        public Byte? HAS_CHILD { get; set; }
        public Byte? HAS_REF_PROC { get; set; }
        public Byte? HAS_FILE { get; set; }
        public string ORIG_REQUESTOR_ID { get; set; }
        public Byte? HAS_REF_OBJ { get; set; }
        public Byte? HAS_MONITOR { get; set; }
        public string ORIG_REQUESTOR_NAME { get; set; }
        public Int16? COLOR_CODE { get; set; }
        public DateTime? LAST_REQUEST_DATE { get; set; }
        public string PARENT_ID { get; set; }
        public string PARENT_LIST { get; set; }
        public Int16? isParent { get; set; }
        public Int16? PRIORITY { get; set; }
        public string PRIORITY_NAME { get; set; }
        public string COMPANY_NAME { get; set; }
        public string CHILD_STATUS { get; set; }
        public string PROCEDURE_STEP_ID { get; set; }
        public string LAST_COMMENT_WRITER { get; set; }
        public DateTime? LAST_COMMENT_DRCM { get; set; }
        public string LAST_COMMENT { get; set; }
        public Byte? IS_QUOTE { get; set; }
        public Byte? IS_QUOTE_ACCEPT { get; set; }
        public Byte? IS_FILL { get; set; }
        public string CO_ID { get; set; }
        public string FILL_ID { get; set; }
        public string PURCHASE_ITEM_ID { get; set; }
        public string PURCHASE_ITEM_ROLE { get; set; }
        public string ASSIGNEE_NAME { get; set; }
        public string ASSIGNEE_ID { get; set; }
        public string ASSIGNEE_STATUS { get; set; }
    }
}
