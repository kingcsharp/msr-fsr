using EntityFrameworkExtras.EF6;
using Msr.Models.Roles;
using Msr.Repositories;
using Msr.Services.Roles.Procedures;
using Msr.Services.Roles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        public RolesView GetRoleByid(string Id)
        {
            return _dbContext.RolesViews.SingleOrDefault(x => x.ObjectId == Id);
        }

        public bool Create(SaveRoleViewModel model)
        {
            try
            {
             
   var saveUserRoleProcedure = new SaveUserRoleProcedure { Name = model.Name,SecurityLevel = model.SecurityLevel, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveUserRoleProcedure);

               

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
                var saveUserRoleProcedure = new SaveUserRoleProcedure { Id = model.Id , Name = model.Name, SecurityLevel = model.SecurityLevel, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveUserRoleProcedure);
            
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
