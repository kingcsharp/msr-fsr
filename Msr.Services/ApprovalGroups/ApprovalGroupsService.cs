using EntityFrameworkExtras.EF6;
using Msr.Models.ApprovalGroups;
using Msr.Models.Comman;
using Msr.Repositories;
using Msr.Services.ApprovalGroups.Procedures;
using Msr.Services.ApprovalGroups.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.ApprovalGroups
{
    public class ApprovalGroupsService
    {
        private readonly MsrDbContext _dbContext;

        public ApprovalGroupsService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ApprovalGroupsView> GetApprovalGroupsQueryable()
        {
            return _dbContext.ApprovalGroupsViews;
        }
        public ApprovalGroupsView GetApprovalGroupById(string id)
        {
            return GetApprovalGroupsQueryable().Where(x => x.Id == id).SingleOrDefault();
        }
        public List<string> GetGroupMembers(string id)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<string>("EXEC Portal_SelectGroupsMembers  @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }
        public List<SelectFile> GetGroupRoles(string id)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);

            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT R.ROLE_ID as Id,R.ROLE_NAME as Name FROM A_V_WF_GROUP_ROLE_LINK R WHERE R.GROUP_ID = @ID", strID).ToList();

            return result;
        }
        public List<SelectFile> GetGroupSpecialMembers(string id)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);

            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT s.SPECIAL_ID as Id,s.NAME as Name FROM A_V_WF_GROUP_SPECIAL_MEMBERS s WHERE s.GROUP_ID = @ID", strID).ToList();

            return result;
        }
        public bool Edit(EditApprovalGroupsViewModel model)
        {
            try
            {
                var deleteMembersProcedure = new DeleteMembersProcedure() { Id = model.Id };

                _dbContext.Database.ExecuteStoredProcedure(deleteMembersProcedure);

                foreach (var file in model.MemberPeoples)
                {
                    var saveGroupMemberProcedure = new SaveGroupMemberProcedure() { GroupId = model.Id, PersonId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveGroupMemberProcedure);
                }

                foreach (var file in model.MemberRoles)
                {
                    var saveGroupRoleProcedure = new SaveGroupRoleProcedure() { GroupId = model.Id, RoleId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveGroupRoleProcedure);
                }

                //dont know from where these values are coming 
                foreach (var file in model.SpecialMembers)
                {
                    var saveSpecialMemberProcedure = new SaveSpecialMemberProcedure() { GroupId = model.Id, SpecialId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveSpecialMemberProcedure);
                }

                var saveApprovalGroupProcedure = new SaveApprovalGroupProcedure
                {
                    Id = model.Id,
                    Name = model.Name,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveApprovalGroupProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Create(EditApprovalGroupsViewModel model)
        {
            try
            {
                var saveApprovalGroupProcedure = new SaveApprovalGroupProcedure
                {
                    Name = model.Name,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveApprovalGroupProcedure);

                var deleteMembersProcedure = new DeleteMembersProcedure() { Id = saveApprovalGroupProcedure.NewId };

                _dbContext.Database.ExecuteStoredProcedure(deleteMembersProcedure);

                foreach (var file in model.MemberPeoples)
                {
                    var saveGroupMemberProcedure = new SaveGroupMemberProcedure() { GroupId = saveApprovalGroupProcedure.NewId, PersonId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveGroupMemberProcedure);
                }

                foreach (var file in model.MemberRoles)
                {
                    var saveGroupRoleProcedure = new SaveGroupRoleProcedure() { GroupId = saveApprovalGroupProcedure.NewId, RoleId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveGroupRoleProcedure);
                }

                //dont know from where these values are coming 
                foreach (var file in model.SpecialMembers)
                {
                    var saveSpecialMemberProcedure = new SaveSpecialMemberProcedure() { GroupId = saveApprovalGroupProcedure.NewId, SpecialId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveSpecialMemberProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
    }
}
