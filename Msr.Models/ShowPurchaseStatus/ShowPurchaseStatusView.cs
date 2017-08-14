using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ShowPurchaseStatus
{
    public class ShowPurchaseStatusView
    {
        public string ID { get; set; }
        public string SORT_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string STATUS { get; set; }
        public string REQUESTOR { get; set; }
        public decimal? CHILD_ORDER { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
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
        public byte? HAS_DISCUSSION { get; set; }
        public byte? HAS_SURVEY { get; set; }
        public byte? HAS_CHILD { get; set; }
        public byte? HAS_REF_PROC { get; set; }
        public byte? HAS_FILE { get; set; }
        public string ORIG_REQUESTOR_ID { get; set; }
        public byte? HAS_REF_OBJ { get; set; }
        public byte? HAS_MONITER { get; set; }
        public string ORIG_REQUESTOR_NAME { get; set; }
        public short? COLOR_CODE { get; set; }
        public DateTime? LAST_REQUEST_DATE { get; set; }
        public string PARENT_ID { get; set; }
        public string PARENT_LIST { get; set; }
        public short? isParent { get; set; }
        public string PRIORITY_NAME { get; set; }
        public string COMPANY_NAME { get; set; }
        public string CHILD_STATUS { get; set; }
        public string PROCEDURE_STEP_ID { get; set; }
        public string LAST_COMMENT_WRITER { get; set; }
        public DateTime? LAST_COMMENT_DRCM { get; set; }
        public string LAST_COMMENT { get; set; }
        public byte? IS_QUOTE { get; set; }
        public byte? IS_QUOTE_ACCEPT { get; set; }
        public byte? IS_FULL { get; set; }
        public string CO_ID { get; set; }
        public string FILL_ID { get; set; }
        public string PURCHASE_ITEM_ID { get; set; }
        public string PURCHASE_HIST_ID { get; set; }
        public string PURCHASE_ITEM_ROLE { get; set; }
        public string ASSIGNEE_NAME { get; set; }
        public string ASSIGNEE_ID { get; set; }
        public string ASSIGNEE_STATUS { get; set; }
    }
    public partial class A_V_TASK_SEARCH
    {
        public string DESCRIPTION { get; set; }
        public string STATUS { get; set; }
        public string REQUESTOR { get; set; }
        public Nullable<decimal> CHILD_ORDER { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string SORT_ID { get; set; }
        public string SYSTEM_TASK { get; set; }
        public string PROCEDURE_ID { get; set; }
        public string REQUESTOR_NAME { get; set; }
        public string REQUESTEE_ID { get; set; }
        public string GROUP_REQUESTEE_ID { get; set; }
        public Nullable<System.DateTime> ORIG_PLANNED_START_DATE { get; set; }
        public Nullable<System.DateTime> ORIG_PLANNED_STOP_DATE { get; set; }
        public Nullable<System.DateTime> CUR_PLANNED_START_DATE { get; set; }
        public Nullable<System.DateTime> CUR_PLANNED_STOP_DATE { get; set; }
        public Nullable<System.DateTime> ACTUAL_START_DATE { get; set; }
        public Nullable<System.DateTime> ACTUAL_STOP_DATE { get; set; }
        public Nullable<decimal> CUR_PLANNED_COUNTER_START { get; set; }
        public string LATEST_REQUESTEE_NAME { get; set; }
        public Nullable<byte> HAS_DISCUSSION { get; set; }
        public Nullable<byte> HAS_SURVEY { get; set; }
        public Nullable<byte> HAS_CHILD { get; set; }
        public Nullable<byte> HAS_REF_PROC { get; set; }
        public Nullable<byte> HAS_FILE { get; set; }
        public string ORIG_REQUESTOR_ID { get; set; }
        public Nullable<byte> HAS_REF_OBJ { get; set; }
        public Nullable<byte> HAS_MONITOR { get; set; }
        public string ORIG_REQUESTOR_NAME { get; set; }
        public Nullable<short> COLOR_CODE { get; set; }
        public string DISCUSSION_ID { get; set; }
        public string SURVEY_ID { get; set; }
        public Nullable<System.DateTime> LAST_REQUEST_DATE { get; set; }
        public string MEETING_ID { get; set; }
        public string PARENT_LIST { get; set; }
        public string PARENT_ID { get; set; }
        public Nullable<short> isParent { get; set; }
        public Nullable<short> PRIORITY { get; set; }
        public string PRIORITY_NAME { get; set; }
        public string GROUP_ID { get; set; }
        public string FAV_TYPE { get; set; }
        public string GROUP_NAME { get; set; }
        public string PERSON { get; set; }
        public string ALLOWED_CO_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public string PROJECT_ID { get; set; }
        public string ID { get; set; }
        public string CHILD_STATUS { get; set; }
        public string PROCEDURE_STEP_ID { get; set; }
        public string SPECIFIED_PERSON_ID { get; set; }
        public string LAST_COMMENT { get; set; }
        public Nullable<System.DateTime> LAST_COMMENT_DRCM { get; set; }
        public string LAST_COMMENT_WRITER { get; set; }
        public Nullable<byte> IS_QUOTE { get; set; }
        public Nullable<byte> IS_QUOTE_ACCEPT { get; set; }
        public Nullable<byte> IS_FILL { get; set; }
        public string CO_ID { get; set; }
        public string PURCHASE_HIST_ID { get; set; }
        public string PURCHASE_ITEM_ID { get; set; }
        public string FILL_ID { get; set; }
        public string PURCHASE_ITEM_ROLE { get; set; }
        public Nullable<int> RECURSION_NUMBER { get; set; }
        public string ASSIGNEE_NAME { get; set; }
        public string ASSIGNEE_ID { get; set; }
        public string ASSIGNEE_STATUS { get; set; }
    }
}
