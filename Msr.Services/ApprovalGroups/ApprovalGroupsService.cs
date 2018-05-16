using EntityFrameworkExtras.EF6;
using Msr.Models.ApprovalGroups;
using Msr.Models.Common;
using Msr.Models.People;
using Msr.Repositories;
using Msr.Services.ApprovalGroups.Procedures;
using Msr.Services.ApprovalGroups.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Roles.Messages;

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

        public List<string> GetGroupMembers(string id, string ntlogin)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<string>("EXEC Portal_SelectGroupsMembers  @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public List<SelectFile> GetGroupRoles(string id)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);

            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT R.ROLE_ID as Value,R.ROLE_NAME as Show FROM A_V_WF_GROUP_ROLE_LINK R WHERE R.GROUP_ID = @ID", strID).ToList();

            return result;
        }

        public List<SelectFile> GetGroupSpecialMembers(string id)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);

            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT s.SPECIAL_ID as Value,s.NAME as Show FROM A_V_WF_GROUP_SPECIAL_MEMBERS s WHERE s.GROUP_ID = @ID", strID).ToList();

            return result;
        }

        public List<PeopleApprovedSearch> GetApprovedMember(string ntlogin, string co)
        {
            return _dbContext.Database.SqlQuery<PeopleApprovedSearch>($"exec A_SP_PEOPLE_SEARCH ' (FULL_NAME LIKE ''%%'' OR FULL_NAME is NULL ) AND  (ROOT LIKE ''%%'' OR ROOT is NULL ) AND  (POSITION_NAME LIKE ''%%'' OR POSITION_NAME is NULL ) AND  (BOSS_NAME LIKE ''%%'' OR BOSS_NAME is NULL ) AND  (COMPANY_NAME LIKE ''%%'' OR COMPANY_NAME is NULL ) AND (( ROOT_CO_ID LIKE ''%{co}%'' ) ) AND  STATUS LIKE ''APPROVED%'' AND  (LOGIN IS NOT NULL) AND  (LOCATION_NAME LIKE ''%%'' OR LOCATION_NAME is NULL )',' ORDER BY LAST_NAME,NAME',NULL,NULL,'{ntlogin}'").ToList();
        }

        public List<RoleResult> GetGroupMemberRoles(string ntLogin)
        {
            var result = _dbContext.Database.SqlQuery<RoleResult>($"EXEC A_SP_ROLE_SELECT NULL, NULL, NULL,NULL, '{ntLogin}',' ORDER BY NAME'").ToList();

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

                foreach (var file in model.SpecialMembers)
                {
                    var saveSpecialMemberProcedure = new SaveSpecialMemberProcedure() { GroupId = saveApprovalGroupProcedure.NewId, SpecialId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveSpecialMemberProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<SelectListItem> HideGroupWorkFlow(string id)
        {
            var sql =
                "UPDATE A_WF_GROUPS SET HIDE = 1 WHERE ID ='" + id + "'";

            var result = _dbContext.Database.SqlQuery<SelectListItem>(sql).ToList().AsQueryable();

            return result;
        }
    }
}
