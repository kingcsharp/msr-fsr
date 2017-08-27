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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Amazon.S3.Model;
using Msr.Models.Common;
using Msr.Services.Documents;
using Msr.Services.Parts;
using Msr.Services.Procedures.Messages;
using Msr.Services.Procedures.Procedures;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProcedureVerbs;

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
        public List<SelectFile> GetSelectedFiles(string id, string type, string ntlogin)
        {
            var objId = new SqlParameter("@objID", id ?? "0");

            var selecttype = type == null ? new SqlParameter("@type", DBNull.Value) : new SqlParameter("@type", type);

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objId, selecttype, NTLogin).ToList();

            return result;
        }
        public List<string> GetSelectedRoles(string id, string ntlogin)
        {
            var objId = new SqlParameter("@strID", id ?? "0");

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

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
                    NTLogin = model.NTLogin,
                    Threshold = model.Threshold
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

        public List<SelectFile> GetSelectedRefProcedures(string id, string ntlogin)
        {
            var objId = new SqlParameter("@ID", id ?? "0");

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_ProcedureStepGetRefProcedures @ID, @strNTLogin", objId, NTLogin).ToList();

            return result;
        }

        public ProceduresApprovedDataResult GetApprovedData(string id)
        {
            var sql = $"SELECT OBJECT_ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID={id}";

            var result = _dbContext.Database.SqlQuery<ProceduresApprovedDataResult>(sql).SingleOrDefault();

            return result;
        }

        public string GetProcedureName(string id)
        {
            var sql = $"SELECT NAME FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID ={id}";

            var result = _dbContext.Database.SqlQuery<string>(sql).FirstOrDefault();

            return result;
        }

        public BaseNotification SaveAssignProcedure(AssignProcedureViewModel model)
        {
            foreach (var people in model.AssignToPeople)
            {
                var sql = $"EXEC A_SP_PROCEDURE_ASSIGN_TO_PEOPLE '{model.Id}', '{people}', null, '{model.DatetimeToStart}'";

                //_dbContext.Database.SqlQuery<string>(sql).SingleOrDefault();

                var finalSql = $"EXEC A_SP_ADMIN_SQL_TO_RUN_QUE_UP {sql}, {model.LoginId}";
                var adminSqlToRunQueUpProcedure = new AdminSqlToRunQueUpProcedure() { MySql = sql, NTLogin = model.LoginId };

                _dbContext.Database.ExecuteStoredProcedure(adminSqlToRunQueUpProcedure);

                //_dbContext.Database.SqlQuery<string>(finalSql).SingleOrDefault();
            }

            return new BaseNotification();
        }

        public List<GetStepDataResult> GetStepsData(string procedureObjectId, string loginId)
        {
            var getStepsDataProcedure = new GetStepsDataProcedure() { ProcedureObjectId = procedureObjectId, NTLogin = loginId };
            var result = _dbContext.Database.ExecuteStoredProcedure<GetStepDataResult>(getStepsDataProcedure).ToList();
            foreach (var stepData in result)
            {
                stepData.Step_Text = stepData.Step_Text.Replace("<<bb>>", "<br/><h4>")
                        .Replace("<</bb>>", "</h4>")
                        .Replace("<<nl/>>", "<br/>");

                if (!string.IsNullOrWhiteSpace(stepData.Pre_Step))
                {
                    Regex regex = new Regex("<i>(.*)</i>");
                    var preStepMatch = regex.Match(stepData.Pre_Step);
                    stepData.Pre_Step = preStepMatch.Groups[1].ToString();
                }
            }

            return result;
        }

        public GetStepEditDataViewModel GetStepData(string stepId, string procedureObjectId, string loginId)
        {
            GetStepEditDataViewModel getStepEditDataViewModel = new GetStepEditDataViewModel()
            {
                StepId = stepId,
                ProcObjId = procedureObjectId
            };
            var getStepEditDataProcedure = new GetStepEditDataProcedure() { Id = stepId, NTLogin = loginId };
            getStepEditDataViewModel.GetStepEditData = _dbContext.Database.ExecuteStoredProcedure<GetStepEditDataResult>(getStepEditDataProcedure).FirstOrDefault();

            var getStepListOfOtherStepsProcedure = new GetStepListOfOtherStepsProcedure() { CurStepID = stepId, Id = procedureObjectId, NTLogin = loginId };
            var getStepListOfOtherStepsResult = _dbContext.Database.ExecuteStoredProcedure<GetStepListOfOtherStepsResult>(getStepListOfOtherStepsProcedure).ToList();

            var getPrecedingStepsProcedure = new GetPrecedingStepsProcedure() { CurStepID = stepId, NTLogin = loginId };
            getStepEditDataViewModel.SelectedPrecedingSteps = _dbContext.Database.ExecuteStoredProcedure<string>(getPrecedingStepsProcedure).ToList();

            getStepEditDataViewModel.GetStepListOfOtherSteps.AddRange(
                getStepListOfOtherStepsResult.Select(x => new SelectListItem()
                {
                    Text = x.Step_Text,
                    Value = x.Id,
                    Selected = getStepEditDataViewModel.SelectedPrecedingSteps.Contains(x.Id)
                }));

            var getStepLaborProcedure = new GetStepLaborProcedure() { Id = stepId, NTLogin = loginId };
            getStepEditDataViewModel.GetStepLabors = _dbContext.Database.ExecuteStoredProcedure<GetStepLaborResult>(getStepLaborProcedure).ToList();

            var getSystemTasksProcedure = new GetSystemTasksProcedure() { NTLogin = loginId };
            getStepEditDataViewModel.SystemTasks = _dbContext.Database.ExecuteStoredProcedure<GetSystemTasksResult>(getSystemTasksProcedure).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.System_Id,
                Selected = x.System_Id == getStepEditDataViewModel.GetStepEditData?.System_Task
            }).ToList();

            var getReferenceProceduresForStepProcedure = new GetReferenceProceduresForStepProcedure() { Id = stepId, NTLogin = loginId };
            getStepEditDataViewModel.SelectedReferenceProcedures = _dbContext.Database.ExecuteStoredProcedure<GetReferenceProceduresForStepResult>(getReferenceProceduresForStepProcedure).Select(x => x.Proc_Obj_Id).ToList();

            getStepEditDataViewModel.ReferenceProcedures = GetProceduresQueryable().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ObjectId,
                Selected = getStepEditDataViewModel.SelectedReferenceProcedures.Contains(x.Id)
            }).ToList();

            var procedureTypesService = new ProcedureVerbsService();

            getStepEditDataViewModel.ReferenceProcedureTypes = procedureTypesService.GetProceduresVerbs().Where(x => x.Status != "DELETED").Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ObjectId
            }).ToList();

            PartsService partsService = new PartsService();
            DocumentService documentService = new DocumentService();

            getStepEditDataViewModel.ListReferenceFiles = partsService.GetSelectedFiles(id: stepId, type: null, ntlogin: loginId).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value,
            }).OrderBy(o => o.Text).ToList();

            getStepEditDataViewModel.ListReferenceObjects = documentService.GetSelectedObjects(id: stepId, ntlogin: loginId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();

            getStepEditDataViewModel.ListReferenceTheories = documentService.GetSelectedTheories(id: stepId, ntlogin: loginId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).OrderBy(o => o.Text).ToList();

            return getStepEditDataViewModel;
        }

        public void UpdateStepData(GetStepEditDataViewModel viewModel)
        {
            var updateOneStep = new UpdateOneStepProcedure()
            {
                Id = viewModel.StepId,
                StepText = viewModel.GetStepEditData.Step_Text,
                ProcObjId = viewModel.ProcObjId,
                Comments = viewModel.GetStepEditData.Comments,
                StartOnCounter = viewModel.GetStepEditData.Start_On_Counter?.ToString(),
                CounterValue = viewModel.GetStepEditData.Counter_Value?.ToString(),
                CounterUnit = viewModel.GetStepEditData.Counter_Unit,
                FromStartOrStop = viewModel.GetStepEditData.FROM_START_OR_STOP,
                RelOrAbs = viewModel.GetStepEditData.REL_OR_ABS,
                SystemTask = viewModel.GetStepEditData.System_Task,
                Destination = viewModel.GetStepEditData.Destination,
                SpecificLocation = viewModel.GetStepEditData.Specific_Location,
                ReferenceVerb = viewModel.GetStepEditData.REFERENCE_VERB,
                ReferenceObject = viewModel.GetStepEditData.REFERENCE_OBJECT,
                ReferenceTheories = viewModel.GetStepEditData.REFERENCE_THEORIES,
                GoToStep = viewModel.GetStepEditData.GOTO_STEP?.ToString(),
                GoToStepId = viewModel.GetStepEditData.GOTO_STEP_ID,
                Cycles = viewModel.GetStepEditData.Cycles,
                CycleOnCounter = viewModel.GetStepEditData.Cycle_On_Counter?.ToString(),
                CycleCount = viewModel.GetStepEditData.Cycle_Count?.ToString(),
                CycleUnit = viewModel.GetStepEditData.Cycle_Unit,
                ReferenceProcs = String.Join(",", viewModel.SelectedReferenceProcedures),
                PrecedingSteps = String.Join(",", viewModel.SelectedPrecedingSteps),
                Duration = viewModel.GetStepEditData.Duration,
                DurationType = viewModel.GetStepEditData.Duration_Type
            };

            _dbContext.Database.ExecuteStoredProcedure(updateOneStep);
        }
    }
}
