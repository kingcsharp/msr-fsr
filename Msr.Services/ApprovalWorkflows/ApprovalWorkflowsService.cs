using System;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.ApprovalWorkflows;
using Msr.Models.Documents;
using Msr.Models.Files;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.ApprovalStages.Procedures;
using Msr.Services.ApprovalWorkflows.Procedures;
using Msr.Services.ApprovalWorkflows.ViewModels;
using Msr.Services.Companies.Procedures;
using Msr.Services.Companies.ViewModels;

namespace Msr.Services.ApprovalWorkflows
{
    public class ApprovalWorkflowsService
    {
        private readonly MsrDbContext _dbContext;

        public ApprovalWorkflowsService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ApprovalWorkflowsView> GetApprovalWorkflowsQueryable()
        {
            return _dbContext.ApprovalWorkflowsViews.Where(x => x.Hide != true);
        }
        public ApprovalWorkflowsView GetApprovalWorkflowsById(string id)
        {
            return _dbContext.ApprovalWorkflowsViews.Where(x => x.Id == id).SingleOrDefault();
        }       
        public IQueryable<ApprovalWorkflowsActivitiesView> GetApprovalWorkflowsActivitiesByWF_Id(string id)
        {
            return _dbContext.ApprovalWorkflowsActivitiesViews.Where(x=>x.WF_Id==id);
        }
        public IQueryable<ApprovalWorkflowStagesView> GetApprovalWorkflowStagesByWF_Id(string id)
        {
            return _dbContext.ApprovalWorkflowStagesViews.Where(x => x.WF_Id == id);
        }
        public IQueryable<ApprovalWorkflowStagesView> GetApprovalWorkflowStagesByWF()
        {
            return _dbContext.ApprovalWorkflowStagesViews;
        }
        public bool Edit(ApprovalWorkflowsViewModel model)
        {
            try
            {
                var editApprovalWorkflowsProcedure = new EditApprovalWorkflowsProcedure
                {                    
                    Name = model.Name,
                    Id = model.Id,
                    AP_STAMP = model.Stamp_Name,
                    AP_STAMP_PIC = model.PictureFiles != null ? string.Join(", ", model.PictureFiles) : "",
                    NTLogin = "1618"
                };
                _dbContext.Database.ExecuteStoredProcedure(editApprovalWorkflowsProcedure);
                               
                var deleteWorkFlowStageProcedure = new DeleteWorkFlowStageProcedure
                {                   
                    WfId = model.Id,
                    NTLogin = model.NTLogin
                };
                _dbContext.Database.ExecuteStoredProcedure(deleteWorkFlowStageProcedure);
                
                foreach (var item in model.Wf_Stages)
                {
                    var addWorkFlowStageProcedure = new AddWorkFlowStageProcedure
                    {
                        StageId = item,
                        WfId = model.Id,
                        Num = model.StagePosition,
                        NTLogin = model.NTLogin
                    };
                    _dbContext.Database.ExecuteStoredProcedure(addWorkFlowStageProcedure);
                }
               

                var deleteWorkFlowActivityProcedure = new DeleteWorkFlowActivityProcedure
                {
                    WfId = model.Id,
                    NTLogin = model.NTLogin
                };
                _dbContext.Database.ExecuteStoredProcedure(deleteWorkFlowActivityProcedure);
                if (model.Activities!=null)
                {
                    foreach (var item in model.Activities)
                    {                       
                        var addWorkFlowActivityProcedure = new AddWorkFlowActivityProcedure
                        {
                            ActivityId = item,
                            WfId = model.Id,
                            NTLogin = model.NTLogin
                        };
                        _dbContext.Database.ExecuteStoredProcedure(addWorkFlowActivityProcedure);
                    }
                }
                //var deleteDocumentProcedure = new DeleteDocumentProcedure
                //{
                //    ObjectId = model.Object_Id
                //};
                //_dbContext.Database.ExecuteStoredProcedure(deleteDocumentProcedure);
                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Add(ApprovalWorkflowsViewModel model)
        {
            try
            {
                var editApprovalWorkflowsProcedure = new EditApprovalWorkflowsProcedure
                {
                    Name = model.Name,
                    Id = model.Id,
                    AP_STAMP = model.Stamp_Name,
                    AP_STAMP_PIC = model.PictureFiles != null ? string.Join(", ", model.PictureFiles) : "",
                    NTLogin = "1618"
                };
                _dbContext.Database.ExecuteStoredProcedure(editApprovalWorkflowsProcedure);
                model.Id = editApprovalWorkflowsProcedure.newID;
                model.Object_Id = editApprovalWorkflowsProcedure.objID;
                var deleteWorkFlowStageProcedure = new DeleteWorkFlowStageProcedure
                {
                    WfId = model.Id,
                    NTLogin = model.NTLogin
                };
                _dbContext.Database.ExecuteStoredProcedure(deleteWorkFlowStageProcedure);

                foreach (var item in model.Wf_Stages)
                {
                    var addWorkFlowStageProcedure = new AddWorkFlowStageProcedure
                    {
                        StageId = item,
                        WfId = model.Id,
                        Num = model.StagePosition,
                        NTLogin = model.NTLogin
                    };
                    _dbContext.Database.ExecuteStoredProcedure(addWorkFlowStageProcedure);
                }


                var deleteWorkFlowActivityProcedure = new DeleteWorkFlowActivityProcedure
                {
                    WfId = model.Id,
                    NTLogin = model.NTLogin
                };
                _dbContext.Database.ExecuteStoredProcedure(deleteWorkFlowActivityProcedure);
                if (model.Activities != null)
                {
                    foreach (var item in model.Activities)
                    {
                        var addWorkFlowActivityProcedure = new AddWorkFlowActivityProcedure
                        {
                            ActivityId = item,
                            WfId = model.Id,
                            NTLogin = model.NTLogin
                        };
                        _dbContext.Database.ExecuteStoredProcedure(addWorkFlowActivityProcedure);
                    }
                }
                //var deleteDocumentProcedure = new DeleteDocumentProcedure
                //{
                //    ObjectId = model.Object_Id
                //};
                //_dbContext.Database.ExecuteStoredProcedure(deleteDocumentProcedure);
                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public IQueryable<DocumentFilesView> GetDocumentsQueryable()
        {
            return _dbContext.DocumentFilesViews;
        }
        public DocumentFilesView GetDocumentById(string id)
        {
            return _dbContext.DocumentFilesViews.Where(x => x.DOC_ID == id).FirstOrDefault();
        }
        public IQueryable<DocumentFilesView> GetDocumentsById(string id)
        {
            return _dbContext.DocumentFilesViews.Where(x => x.DOC_ID == id);
        }
        public IQueryable<FileView> GetFilesById(string id)
        {
            return _dbContext.FIleViews.Where(x => x.Id == id);
        }
        public IQueryable<FileView> GetFilesList()
        {
            return _dbContext.FIleViews;
        }
        public string GetFileById(string id)
        {
            var getPictureFile = _dbContext.FIleViews.Where(x => x.Id == id).FirstOrDefault();
            return getPictureFile.Name;
        }
        public IQueryable<ActivitiesView> GetActivitiesList()
        {
            return _dbContext.ActivitiesViews;
        }
        public IQueryable<WorkflowStagesView> GetWorkflowStages()
        {
            return _dbContext.WorkflowStagesViews;
        }
        public bool HideApplrovalWorkflow(string id)
        {
            try
            {
                var hideApprovalWorkflowProcedure = new HideApprovalWorkflowProcedure
                {
                    Id = id
                };
                _dbContext.Database.ExecuteStoredProcedure(hideApprovalWorkflowProcedure);
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
