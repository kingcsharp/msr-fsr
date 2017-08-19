using EntityFrameworkExtras.EF6;
using Msr.Models.Roles;
using Msr.Repositories;
using Msr.Services.Roles.Procedures;
using Msr.Services.Roles.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Msr.Models.Common;
using Msr.Models.Parts;
using Msr.Services.PrePro.Procedure;
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

        public List<SelectFile> GetChildRoles(string id)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_POPFILL_EDITROLE_CHILD_ROLES  @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }
        public List<SelectFile> GetAssignedPeople(string id)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

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

                var singleRole = GetRoleByid(saveUserRoleProcedure.ReturnID);

                var deleteRoleParentSaveProcedure = new DeleteRoleParentProcedure() { ObjId = singleRole.ObjectId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRoleParentSaveProcedure);

                var deleteRolePeopleAssignedProcedure = new DeleteRoleAssignedProcedure() { ObjId = singleRole.ObjectId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteRolePeopleAssignedProcedure);

                foreach (var file in model.ChildRoles)
                {
                    var saveFileProcedure = new SaveRoleToRoleProcedure() { Child = file, StrId = singleRole.ObjectId, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.PeopleAssigned)
                {
                    var saveFileProcedure = new SaveRoleToRoleProcedure() { Child = file, StrId = singleRole.ObjectId, NTLogin = model.NTLogin };

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

        public bool Edit(SaveRoleViewModel model)
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

        public bool Delete(string id)
        {
            try
            {
                //need to be dynamic
                var NTLogin = "1618";
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

    }
}
