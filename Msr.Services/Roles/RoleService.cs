using EntityFrameworkExtras.EF6;
using Msr.Models.Roles;
using Msr.Repositories;
using Msr.Services.Roles.Procedures;
using Msr.Services.Roles.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Msr.Models.Common;
using Msr.Models.People;
using Msr.Services.Roles.Messages;
using Msr.Services.Documents;

namespace Msr.Services.Roles
{
    public class RoleService
    {
        private readonly MsrDbContext _dbContext;

        public RoleService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<RolesView> GetUserRolesQueryable()
        {
            return _dbContext.RolesViews;
        }

        public List<SelectFile> GetChildRoles(string id, string ntlogin)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_POPFILL_EDITROLE_CHILD_ROLES  @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public List<SelectFile> GetAssignedPeople(string id, string ntlogin)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_POPFILL_EDITROLE_PEOPLE_ASSIGNED @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public List<CertificationRole> GetAssignedWithCetificatePeople(string id, string ntlogin)
        {
            var strID = new SqlParameter("@ID", id ?? "0");

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<CertificationRole>("EXEC A_SP_POPFILL_EDITROLE_PEOPLE_ASSIGNED @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public RolesView GetRoleById(string id)
        {
            return _dbContext.RolesViews.SingleOrDefault(x => x.ObjectId == id);
        }
        public bool Create(SaveRoleViewModel model)
        {
            try
            {
                var documentFilesService = new DocumentFilesService();
                var refFile = string.Join(", ", model.ReferenceFiles);

                var saveUserRoleProcedure = new SaveUserRoleProcedure { Name = model.Name,
                                                                        SecurityLevel = model.SecurityLevel,
                                                                        TrainingIDRev = model.TrainingIdRev,
                                                                        Comments = model.Comments,
                                                                        ReferenceFiles = refFile,
                                                                        NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveUserRoleProcedure);

                var sql =
                    "SELECT OBJECT_ID as ObjectId FROM A_ROLES_HISTORY WHERE ID = '" + saveUserRoleProcedure.ReturnID + "'";

                var ObjectId = _dbContext.Database.SqlQuery<string>(sql);

                var deleteRoleParentSaveProcedure = new DeleteRoleParentProcedure() { ObjId = saveUserRoleProcedure.ReturnID, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRoleParentSaveProcedure);

                var deleteRolePeopleAssignedProcedure = new DeleteRoleAssignedProcedure() { ObjId = saveUserRoleProcedure.ReturnID, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRolePeopleAssignedProcedure);

                foreach (var file in model.ChildRoles)
                {
                    var saveFileProcedure = new SaveRoleToRoleProcedure() { Child = file, StrId = saveUserRoleProcedure.ReturnID, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.PeopleAssigned)
                {
                    var saveFileProcedure = new SaveRoleAssignPersonRoleProcedure() { Child = file, StrId = saveUserRoleProcedure.ReturnID, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }


                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Update(SaveRoleViewModel model)
        {
            try
            {
                var documentFilesService = new DocumentFilesService();
                var refFile = string.Join(", ", documentFilesService.GetDocByObjectId(model.ObjectId).Select(x => x.LINKED_DOC_ID).ToList());

                var saveUserRoleProcedure = new SaveUserRoleProcedure
                {
                    Id = model.WfId,
                    Name = model.Name,
                    SecurityLevel = model.SecurityLevel,
                    TrainingIDRev = model.TrainingIdRev,
                    ReferenceFiles = refFile,
                    Comments = model.Comments,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveUserRoleProcedure);

                var deleteRoleParentSaveProcedure =
                    new DeleteRoleParentProcedure { ObjId = model.WfId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRoleParentSaveProcedure);

                var deleteRolePeopleAssignedProcedure =
                    new DeleteRoleAssignedProcedure { ObjId = model.WfId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRolePeopleAssignedProcedure);

                foreach (var childRole in model.ChildRoles)
                {
                    var saveRoleToRoleProcedure = new SaveRoleToRoleProcedure { Child = childRole, StrId = model.WfId, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveRoleToRoleProcedure);
                }

                foreach (var personId in model.PeopleAssigned)
                {
                    var saveRoleAssignPersonRoleProcedure = new SaveRoleAssignPersonRoleProcedure
                    {
                        Child = personId,
                        StrId = model.WfId,
                        NTLogin = model.NTLogin

                        //bpp
                        //StartDate = model.StartDate,
                        //EndDate = model.EndDate
                    };

                    _dbContext.Database.ExecuteStoredProcedure(saveRoleAssignPersonRoleProcedure);

                    RefreshUserRoles(personId);
                }

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Delete(string id, string ntlogin)
        {
            try
            {
                var NTLogin = ntlogin;
                var deleteRoleProcedure = new DeleteRoleProcedure() { ObjID = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRoleProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public List<RoleResult> GetActiveRoles()
        {
            var result = _dbContext.RolesViews.Where(x => x.Status.Contains("APPROVED")).Select(s => new RoleResult
            {
                Id = s.Root,
                Name = s.RoleName,
                ObJect_Id = s.ObjectId
            }).ToList();

            return result;
        }

        public List<RoleApprovedData> GetSelectedRolesByCompanyId(string id)
        {
            var result = _dbContext.Database.SqlQuery<RoleApprovedData>("SELECT mr.*,r.NAME FROM A_MENU_ROLES mr,A_V_ROLES_APPROVED_DATA r WHERE r.ID = mr.ROLE_ID AND mr.CO = '" + id + "'").ToList();

            return result;
        }

        public List<RoleResult> GetApprovedRoles()
        {
            var result = _dbContext.Database.SqlQuery<RoleResult>("select ID as Id, NAME as Name from A_APPROVED_ROLES where STATUS LIKE '%APPROVED%'").ToList();

            return result;
        }

        public List<GetMyRolesResult> RefreshUserRoles(string personId)
        {
            var myID = new SqlParameter("@myID", personId);
            var strNTLogin = new SqlParameter("@strNTLogin", personId);

            var result = _dbContext.Database.SqlQuery<GetMyRolesResult>("EXEC A_SP_ROLES_GET_MY_ROLES @myID, @strNTLogin", myID, strNTLogin).ToList();

            return result;
        }

        public List<GetMyRolesResult> GetAssignedRoles(string personId)
        {
            RefreshUserRoles(personId);

            var personIdParam = new SqlParameter("@personId", personId);

            var result = _dbContext.Database.SqlQuery<GetMyRolesResult>($"SELECT distinct ROLE_ID, ROLE, PERSON, STATUS, ROLE_NAME, StartDate, EndDate FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = {personId}").ToList();

            return result.Where(x => x.Status == "ACTIVE").ToList();
        }

        public List<GetMyRolesResult> GetAssignedRolesByLogin(string personId)
        {
            RefreshUserRoles(personId);

            var personIdParam = new SqlParameter("@personId", personId);
            var sql = "SELECT distinct pa.ROLE_ID, pa.ROLE, pa.PERSON, pa.STATUS, pa.ROLE_NAME  FROM A_APPROVED_ROLE_ASSIGNEES pa INNER JOIN Portal_PeopleView pv on pv.ObjectId = pa.PERSON WHERE  pv.Login = @personId";

            var result = _dbContext.Database.SqlQuery<GetMyRolesResult>(sql, personIdParam).ToList();

            return result.Where(x => x.Status == "ACTIVE").ToList();
        }

        public List<RoleResult> GetRolesList(string ntLogin)
        {
            var result = _dbContext.Database.SqlQuery<RoleResult>($"EXEC A_SP_ROLE_SELECT NULL, NULL, NULL,NULL, '{ntLogin}',' ORDER BY NAME'").ToList();
            return result;
        }

        public List<PeopleApprovedSearch> GetPeopleAssigned(string ntlogin, string co)
        {
            return _dbContext.Database.SqlQuery<PeopleApprovedSearch>($"exec A_SP_PEOPLE_SEARCH ' (FULL_NAME LIKE ''%%'' OR FULL_NAME is NULL ) AND  (ROOT LIKE ''%%'' OR ROOT is NULL ) AND  (POSITION_NAME LIKE ''%%'' OR POSITION_NAME is NULL ) AND  (BOSS_NAME LIKE ''%%'' OR BOSS_NAME is NULL ) AND  (COMPANY_NAME LIKE ''%%'' OR COMPANY_NAME is NULL ) AND (( ROOT_CO_ID LIKE ''%{co}%'' ) ) AND  STATUS LIKE ''APPROVED%'' AND  (LOGIN IS NOT NULL) AND  (LOCATION_NAME LIKE ''%%'' OR LOCATION_NAME is NULL )',' ORDER BY LAST_NAME,NAME',NULL,NULL,'{ntlogin}'").ToList();
        }

    }
}
