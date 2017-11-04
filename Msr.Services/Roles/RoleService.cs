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
using Msr.Services.Roles.Messages;

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

        public List<SelectFile> GetAssignedPeople(string id,string ntlogin)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_POPFILL_EDITROLE_PEOPLE_ASSIGNED @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public RolesView GetRoleByid(string id)
        {
            return _dbContext.RolesViews.SingleOrDefault(x => x.ObjectId == id);
        }

        public bool Create(SaveRoleViewModel model)
        {
            try
            {

                var saveUserRoleProcedure = new SaveUserRoleProcedure { Name = model.Name, SecurityLevel = model.SecurityLevel, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveUserRoleProcedure);

                var sql =
                    "SELECT OBJECT_ID as ObjectId FROM A_ROLES_HISTORY WHERE ID = '" + saveUserRoleProcedure.ReturnID + "'";

                var ObjectId = _dbContext.Database.SqlQuery<string>(sql);

                var deleteRoleParentSaveProcedure = new DeleteRoleParentProcedure() { ObjId = ObjectId.ToString(), NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRoleParentSaveProcedure);

                var deleteRolePeopleAssignedProcedure = new DeleteRoleAssignedProcedure() { ObjId = ObjectId.ToString(), NTLogin = model.NTLogin };

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

        public bool Save(SaveRoleViewModel model)
        {
            try
            {
                var saveUserRoleProcedure = new SaveUserRoleProcedure { Id = model.Id, Name = model.Name, SecurityLevel = model.SecurityLevel, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveUserRoleProcedure);

                var deleteRoleParentSaveProcedure = new DeleteRoleParentProcedure() { ObjId = model.ObjectId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRoleParentSaveProcedure);

                var deleteRolePeopleAssignedProcedure = new DeleteRoleAssignedProcedure() { ObjId = model.ObjectId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRolePeopleAssignedProcedure);

                foreach (var file in model.ChildRoles)
                {
                    var saveFileProcedure = new SaveRoleToRoleProcedure() { Child = file, StrId = model.ObjectId, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.PeopleAssigned)
                {
                    var saveFileProcedure = new SaveRoleToRoleProcedure() { Child = file, StrId = model.ObjectId, NTLogin = model.NTLogin };

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

        public bool Delete(string id,string ntlogin)
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
            var result = _dbContext.RolesViews.Where(x => x.Status == "APPROVED").Select(s => new RoleResult
            {
                Id = s.Root,
                Name = s.RoleName,
                ObJect_Id = s.ObjectId
            }).ToList();

            return result;
        }

        public List<RoleApprovedData> GetSelectedRolesByCompanyId(string id)
        {
            var result = _dbContext.Database.SqlQuery<RoleApprovedData>("SELECT mr.*,r.NAME FROM A_MENU_ROLES mr,A_V_ROLES_APPROVED_DATA r WHERE r.ID = mr.ROLE_ID AND mr.CO = '" + id +"'").ToList();

            return result;
        }

        public List<RoleResult> GetApprovedRoles()
        {
            var result = _dbContext.Database.SqlQuery<RoleResult>("select ID as Id, NAME as Name from A_APPROVED_ROLES where STATUS='APPROVED'").ToList();

            return result;
        }

        public List<GetMyRolesResult> GetMyRoles(string personId)
        {
            var myID = new SqlParameter("@myID", personId);
            var strNTLogin = new SqlParameter("@strNTLogin", personId);

            var result = _dbContext.Database.SqlQuery<GetMyRolesResult>("EXEC A_SP_ROLES_GET_MY_ROLES @myID, @strNTLogin", myID, strNTLogin).ToList();

            return result;
        }

    }
}
