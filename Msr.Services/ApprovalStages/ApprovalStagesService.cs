using EntityFrameworkExtras.EF6;
using Msr.Models.ApprovalStages;
using Msr.Repositories;
using Msr.Services.ApprovalStages.Procedures;
using Msr.Services.ApprovalStages.VIewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Msr.Services.ApprovalStages
{
    public class ApprovalStagesService
    {
        private readonly MsrDbContext _dbContext;

        public ApprovalStagesService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<ApprovalStagesView> GetApprovalStagesQueryable()
        {
            return _dbContext.ApprovalStagesViews;
        }

        public ApprovalStagesView GetApprovalStageById(string id)
        {
            return GetApprovalStagesQueryable().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<SelectMemberGroup> GetStageMemberGroups(string id,string ntlog)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);

            var NTLogin = new SqlParameter("@strNTLogin", ntlog);

            var result = _dbContext.Database.SqlQuery<SelectMemberGroup>("EXEC Portal_SelectStageMemberGroups  @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public bool Create(EditApprovalStagesViewModel model)
        {
            try
            {
                var saveApprovalStagesProcedure = new SaveApprovalStagesProcedure
                {
                    Name = model.Name,
                    NTLogin = model.NtLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveApprovalStagesProcedure);


                var deleteApprovalGroupsProcedure = new DeleteApprovalGroupsProcedure() { Id = saveApprovalStagesProcedure.NewId, NTLogin = model.NtLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteApprovalGroupsProcedure);

                foreach (var file in model.Groups)
                {
                    var saveFileProcedure = new SaveStageGroupProcedure() { GroupId = file, StageId = saveApprovalStagesProcedure.NewId, NTLogin = model.NtLogin };

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

        public bool Update(EditApprovalStagesViewModel model)
        {
            try
            {
                var deleteApprovalGroupsProcedure = new DeleteApprovalGroupsProcedure() { Id = model.Id, NTLogin = model.NtLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteApprovalGroupsProcedure);

                foreach (var file in model.Groups)
                {
                    var saveFileProcedure = new SaveStageGroupProcedure() { GroupId = file, StageId = model.Id, NTLogin = model.NtLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                var saveApprovalStagesProcedure = new SaveApprovalStagesProcedure
                {
                    Id = model.Id,
                    Name = model.Name,
                    NTLogin = model.NtLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveApprovalStagesProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public IQueryable<SelectListItem> HideStageWorkFlow(string id)
        {
            var sql =
                "UPDATE A_WF_STAGES SET HIDE = 1 WHERE ID  = '" + id + "'";

            var result = _dbContext.Database.SqlQuery<SelectListItem>(sql).ToList().AsQueryable();

            return result;
        }
    }
}
