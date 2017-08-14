using EntityFrameworkExtras.EF6;
using Msr.Models.Procedures;
using Msr.Repositories;
using Msr.Services.Orders.Procedures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.Common;
using Msr.Services.Procedures.Messages;
using Msr.Services.Procedures.Procedures;
using Msr.Services.Procedures.ViewModels;

namespace Msr.Services.Procedures
{
    public class ProceduresService
    {
        private readonly MsrDbContext _dbContext;

        public ProceduresService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ProcedureView> GetProceduresQueryable()
        {
            return _dbContext.Procedures;
        }
        public ProcedureView GetProcedureById(string id)
        {
            return GetProceduresQueryable().SingleOrDefault(x => x.ObjectId == id);
        }
        public List<SelectFile> GetSelectedFiles(string id, string type)
        {
            var objId = new SqlParameter("@objID", id ?? "0");

            var selecttype = type == null ? new SqlParameter("@type", DBNull.Value) : new SqlParameter("@type", type);

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objId, selecttype, NTLogin).ToList();

            return result;
        }
        public List<string> GetSelectedRoles(string id)
        {
            var objId = new SqlParameter("@strID", id ?? "0");

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<string>("EXEC Portal_GetProcedureRoles  @strID, @strNTLogin", objId, NTLogin).ToList();

            return result;
        }
        public bool Create(SaveProcedureViewModel model)
        {

            try
            {
                var saveProcedureProcedure = new SaveProcedureProcedure
                {
                    Company = model.Company,
                    Verb = model.Verb,
                    Name = model.Name,
                    Comments = model.Comments,
                    StepInAp = model.StepInAp,
                    WipMsg = model.WipMsg,
                    SecurityLevel = model.SecurityLevel,
                    SystemId = model.SystemId,
                    Duration = model.Duration,
                    DurationType = model.DurationType,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureProcedure);



                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = saveProcedureProcedure.NewObjId, Type = DBNull.Value.ToString(CultureInfo.InvariantCulture), NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjId = saveProcedureProcedure.NewObjId, DocId = file, Type = DBNull.Value.ToString(CultureInfo.InvariantCulture), NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                var deleteProcedureRolesProcedure = new DeleteProcedureRolesProcedure() { ObjId = saveProcedureProcedure.NewObjId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedureRolesProcedure);

                foreach (var file in model.Roles)
                {
                    var saveProcedureRoleProcedure = new SaveProcedureRoleProcedure() { ObjId = saveProcedureProcedure.NewObjId, RoleId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveProcedureRoleProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Save(SaveProcedureViewModel model)
        {

            try
            {
                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = model.ObjectId, Type = DBNull.Value.ToString(CultureInfo.InvariantCulture), NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjId = model.ObjectId, DocId = file, Type = DBNull.Value.ToString(CultureInfo.InvariantCulture), NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                var deleteProcedureRolesProcedure = new DeleteProcedureRolesProcedure() { ObjId = model.ObjectId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedureRolesProcedure);

                foreach (var file in model.Roles)
                {
                    var saveProcedureRoleProcedure = new SaveProcedureRoleProcedure() { ObjId = model.ObjectId, RoleId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveProcedureRoleProcedure);
                }

                var saveProcedureProcedure = new SaveProcedureProcedure
                {
                    ObjId = model.ObjectId,
                    Company = model.Company,
                    Verb = model.Verb,
                    Name = model.Name,
                    Comments = model.Comments,
                    StepInAp = model.StepInAp,
                    WipMsg = model.WipMsg,
                    SecurityLevel = model.SecurityLevel,
                    SystemId = model.SystemId,
                    Duration = model.Duration,
                    DurationType = model.DurationType,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public ProceduresApprovedDataResult GetApprovedData(string id)
        {
            var sql = $"SELECT OBJECT_ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID={id}";

            var result = _dbContext.Database.SqlQuery<ProceduresApprovedDataResult>(sql).SingleOrDefault();

            return result;
        }

        public string GetProcedureName(string id)
        {
            var sql = $"SELECT* FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID ={id}";

            var result = _dbContext.Database.SqlQuery<string>(sql).SingleOrDefault();

            return result;
        }

        public BaseNotification SaveAssignProcedure(AssignProcedureViewModel model)
        {
            foreach (var people in model.AssignToPeople)
            {
                var sql = $"EXEC A_SP_PROCEDURE_ASSIGN_TO_PEOPLE {model.Id}, {people}, null, {model.DatetimeToStart}";

                _dbContext.Database.SqlQuery<string>(sql).SingleOrDefault();

                sql = $"EXEC A_SP_ADMIN_SQL_TO_RUN_QUE_UP {people}, {model.LoginId}";

                _dbContext.Database.SqlQuery<string>(sql).SingleOrDefault();
            }

            return new BaseNotification();
        }
    }
}
