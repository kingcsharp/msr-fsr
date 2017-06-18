using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Notes.Procedures
{
    [StoredProcedure("A_SP_TASK_COMMENT_UPDATE_ONE_COMMENT")]
    public class AddCommentToTaskProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "@newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "@messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "@commentID")]
        public string CommentId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "@taskID")]
        public string TaskId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "@comment")]
        public string Comment { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "@allowedToView")]
        public string AllowedToView { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "@strNTlogin")]
        public string StrNTlogin { get; set; }
    }
}
