using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Companies.Procedures
{
    [StoredProcedure("A_SP_COMPANIES_UPDATE_ONE_COMPANY")]
    public class AddCompanyProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "objID")]
        public string ObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "TYPE")]
        public string Type { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "PARENT_COMPANY")]
        public string ParentType { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "PHONE")]   
        public string Phone { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "HEAD_PEOPLE")]
        public string HeadPeople { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "LOCATION_ID")]
        public string LocationId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "CO_SUP_PRODS")]
        public string CoSupProds { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "PIC_FILES")]
        public string PicFiles { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "LOGO_FILES")]
        public string LogoFiles { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "REFERENCE_FILES")]
        public string ReferenceFiles { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 200, ParameterName = "NEW_PERSON_LOGIN")]
        public string NewPersonLogin { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "NEW_PERSON_PASSWORD")]
        public string NewPersonPassword { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 200, ParameterName = "NEW_PERSON_EMAIL")]
        public string NewPersonEmail { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 200, ParameterName = "NEW_PERSON_FIRST_NAME")]
        public string NewPersonFirstName { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 200, ParameterName = "NEW_PERSON_LAST_NAME")]
        public string NewPersonLastName { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTlogin")]
        public string StrNTlogin { get; set; }
    }
}
