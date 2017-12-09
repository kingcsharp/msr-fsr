using EntityFrameworkExtras.EF6;
using Msr.Models.Procedures;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Msr.Models.Common;
using Msr.Services.Documents;
using Msr.Services.Parts;
using Msr.Services.Procedures.Messages;
using Msr.Services.Procedures.Procedures;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProcedureVerbs;
using Msr.Services.PurchesOrder.ViewModels;

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

        public List<SelectFile> GetProcedurelist()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT ID as Value, Title as Show FROM A_V_PREPOP_QUICK WHERE CREATING_CO = '2' ").ToList();

            return result;
        }

        public List<ProcedureStepOtherStepListView> GetProcedureStepOtherStepsList(string procObjectId, string curStepID)
        {
            var poid = new SqlParameter("@POID", procObjectId ?? "0");

            var cStepId = new SqlParameter("@curStepID", curStepID ?? "0");
            //need to be dynamic
            var ntLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<ProcedureStepOtherStepListView>("EXEC A_SP_PROCEDURE_STEP_GET_LIST_OF_OTHER_STEPS  @POID, @curStepID, @strNTLogin", poid, cStepId, ntLogin).ToList();

            return result;
        }
        public ResultNotification<AddPurchaseResponse> AddMonitorForProcedure(AddMonitorForProcedureViewModel model)
        {
            var responsePurchase = new ResultNotification<AddPurchaseResponse> { Entity = new AddPurchaseResponse() };
            try
            {
                var addProcedureMonitorProcedure = new AddProcedureMonitorProcedure()
                {

                    Id = model.Id,

                    Monitor_Type = model.Monitor_Type,
                    Input_Type = model.Input_Type,

                    Description = model.Description,

                    Start_System_Task = model.Start_System_Task,

                    Start_Type = model.Start_Type,

                    Stop_System_Task = model.Stop_System_Task,

                    Stop_Type = model.Stop_Type,

                    Counter_Or_Clock = model.Counter_Or_Clock,

                    Clock_Unit = model.Clock_Unit,

                    Highest_Threshold = model.Highest_Threshold,

                    High_Threshold = model.High_Threshold,

                    Target = model.Target,

                    Low_Threshold = model.Low_Threshold,

                    Lowest_Threshold = model.Lowest_Threshold,

                    Should_Be = model.Should_Be,

                    Opinion = model.Opinion,

                    Hide_Target = model.Hide_Target,

                    Use_Result = model.Use_Result,

                    Fail_Stop = model.Fail_Stop,

                    Step_Id = model.Step_Id,

                    Correct_Answer = model.Correct_Answer,

                    Text_Target = model.Text_Target,

                    Task_Id = model.Task_Id,

                    Tolerance = model.Tolerance,

                    Related_Object_Id = model.Related_Object_Id,

                    Fail_Action = model.Fail_Action,

                    Target_Object_Type = model.Target_Object_Type,

                    Target_Object = model.Target_Object,

                    Skip_Mode = model.Skip_Mode,

                    Cant_Change = model.Cant_Change,

                    Always_Pass = model.Always_Pass,

                    StrNTLogin = model.StrNTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(addProcedureMonitorProcedure);

                responsePurchase.Entity.NewId = addProcedureMonitorProcedure.NewId;

                return responsePurchase;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                responsePurchase.AddError(message);

                return responsePurchase;
            }
        }
        public bool EditMonitorForProcedure(AddMonitorForProcedureViewModel model)
        {
            try
            {
                var addProcedureMonitorProcedure = new AddProcedureMonitorProcedure()
                {

                    Id = model.Id,

                    Monitor_Type = model.Monitor_Type,

                    Description = model.Description,

                    Start_System_Task = model.Start_System_Task,

                    Start_Type = model.Start_Type,

                    Stop_System_Task = model.Stop_System_Task,

                    Stop_Type = model.Stop_Type,

                    Counter_Or_Clock = model.Counter_Or_Clock,

                    Clock_Unit = model.Clock_Unit,

                    Highest_Threshold = model.Highest_Threshold,

                    High_Threshold = model.High_Threshold,

                    Target = model.Target,

                    Low_Threshold = model.Low_Threshold,

                    Lowest_Threshold = model.Lowest_Threshold,

                    Should_Be = model.Should_Be,

                    Opinion = model.Opinion,

                    Hide_Target = model.Hide_Target,

                    Use_Result = model.Use_Result,

                    Fail_Stop = model.Fail_Stop,

                    Step_Id = model.Step_Id,

                    Correct_Answer = model.Correct_Answer,

                    Text_Target = model.Text_Target,

                    Task_Id = model.Task_Id,

                    Tolerance = model.Tolerance,

                    Related_Object_Id = model.Related_Object_Id,

                    Fail_Action = model.Fail_Action,

                    Target_Object_Type = model.Target_Object_Type,

                    Target_Object = model.Target_Object,

                    Skip_Mode = model.Skip_Mode,

                    Cant_Change = model.Cant_Change,

                    Always_Pass = model.Always_Pass,

                    StrNTLogin = model.StrNTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(addProcedureMonitorProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public string PrePopSave(string procObjId, string prevStepId, string ntlogin)
        {
            try
            {
                var saveProcedureStepPreProStepProcedure = new SaveProcedureStepPreProStepProcedure()
                {
                    ProcObjId = procObjId,
                    Preproid = null,
                    prevStepId = prevStepId,
                    NtLogin = ntlogin
                };
                _dbContext.Database.ExecuteStoredProcedure(saveProcedureStepPreProStepProcedure);

                return saveProcedureStepPreProStepProcedure.NewObjId;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return string.Empty;
            }
        }
        public List<ProcedureEditObjectView> GetSelectedProcedureObject(string procobjid, string sid, string relationship, string ntlogin)
        {
            string ss = null;
            var procId = new SqlParameter("@PROC_OBJ_ID", procobjid);
            var stepid = new SqlParameter("@PROC_STEP_ID", value: DBNull.Value);

            var relation = new SqlParameter("@RELATIONSHIP", relationship);
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);
            var result = _dbContext.Database.SqlQuery<ProcedureEditObjectView>("EXEC A_SP_PROCEDURE_GET_APPROVED_OBJECTS_BY_RELATIONSHIP @PROC_OBJ_ID,@PROC_STEP_ID,@RELATIONSHIP, @strNTLogin", procId, stepid, relation, NTLogin).ToList();

            return result;
        }
        public List<EditProcedureObjestFile> GetProcedureEditObjects()
        {
            var result = _dbContext.Database.SqlQuery<EditProcedureObjestFile>("SELECT DISTINCT TOP 500 OBJ_DESC,ID as Id FROM A_V_APPROVED_OBJECTS WHERE CREATING_CO = '2' AND OBJ_TABLE = 'A_PARTS_HISTORY' AND (( OBJ_DESC LIKE '%m%' ) ) ORDER BY OBJ_DESC").ToList();

            return result;
        }
        public ResultNotification<string> Create(SaveProcedureViewModel model)
        {
            var result = new ResultNotification<string>();
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

                result.Entity = saveProcedureProcedure.NewObjId;

                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = saveProcedureProcedure.NewObjId, Type = DBNull.Value.ToString(CultureInfo.InvariantCulture), NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjId = saveProcedureProcedure.NewObjId, DocId = file, Type = null, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                var deleteProcedureRolesProcedure = new DeleteProcedureRolesProcedure() { ObjId = saveProcedureProcedure.NewObjId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedureRolesProcedure);

                foreach (var file in model.Roles)
                {
                    var saveProcedureRoleProcedure = new SaveProcedureRoleProcedure() { ObjId = saveProcedureProcedure.NewObjId, RoleId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveProcedureRoleProcedure);
                }
                return result;
            }
            catch (Exception ex)
            {

                var message = "Error occured:" + ex.Message;
                result.AddError(message);
                return result;
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
                    var saveFileProcedure = new SaveFileProcedure() { ObjId = model.ObjectId, DocId = file, Type = null, NTLogin = model.NTLogin };

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
                stepData.GetStepLaborsList = GetLaborsById(stepData.Id, loginId).Select(x => new SelectListItem
                {
                    Text = x.Role_Name,
                    Value = x.Role_id.ToString(),
                }).OrderBy(o => o.Text).ToList();
                stepData.GetMoniterViewModels = GetMoniterByStepId(stepData.Id, loginId);
            }

            return result.OrderBy(x => x.Print_Order).ToList();
        }

        public List<GetStepLaborResult> GetLaborsById(string stepId, string loginId)
        {

            var getStepLaborProcedure = new GetStepLaborProcedure() { Id = stepId, NTLogin = loginId };
            return _dbContext.Database.ExecuteStoredProcedure<GetStepLaborResult>(getStepLaborProcedure).ToList();
        }
        public GetStepEditDataViewModel GetStepData(string stepId, string procedureObjectId, string loginId)
        {
            GetStepEditDataViewModel getStepEditDataViewModel = new GetStepEditDataViewModel
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
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();

            getStepEditDataViewModel.ListReferenceTheories = documentService.GetSelectedTheories(id: stepId, ntlogin: loginId).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();

            return getStepEditDataViewModel;
        }

        public bool CreateStepData(GetStepEditDataViewModel viewModel)
        {
            try
            {
                var updateOneStep = new UpdateOneStepProcedure()
                {
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
                    DurationType = viewModel.GetStepEditData.Duration_Type,
                    ReplacementCost = viewModel.ReplacementCost,
                    Utilization = viewModel.Utilization,
                    UsefulLife = viewModel.UsefulLife,
                    EquipExpensePerMinute = viewModel.EquipExpensePerMinute,
                    AnnualRM = viewModel.AnnualRM,
                    RMPerMinute = viewModel.RMPerMinute
                };

                _dbContext.Database.ExecuteStoredProcedure(updateOneStep);

                var deleteProcedureStepFile = new DeleteProcedureStepFile() { StepId = updateOneStep.NewId, NTLogin = viewModel.NtLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedureStepFile);

                foreach (var file in viewModel.ReferenceFiles)
                {
                    var saveProcedureStepFileProcedure = new SaveProcedureStepFileProcedure() { StepId = updateOneStep.NewId, FileId = file, NTLogin = viewModel.NtLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveProcedureStepFileProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }



        }
        public void UpdateStepData(GetStepEditDataViewModel viewModel)
        {
            var updateOneStep = new UpdateOneStepProcedure();

            updateOneStep.Id = viewModel.StepId;
            updateOneStep.StepText = viewModel.GetStepEditData.Step_Text;
            updateOneStep.ProcObjId = viewModel.ProcObjId;
            updateOneStep.Comments = viewModel.GetStepEditData.Comments;
            updateOneStep.StartOnCounter = viewModel.GetStepEditData.Start_On_Counter?.ToString();
            updateOneStep.CounterValue = viewModel.GetStepEditData.Counter_Value?.ToString();
            updateOneStep.CounterUnit = viewModel.GetStepEditData.Counter_Unit;
            updateOneStep.FromStartOrStop = viewModel.GetStepEditData.FROM_START_OR_STOP;
            updateOneStep.RelOrAbs = viewModel.GetStepEditData.REL_OR_ABS;
            updateOneStep.SystemTask = viewModel.GetStepEditData.System_Task;
            updateOneStep.Destination = viewModel.GetStepEditData.Destination;
            updateOneStep.SpecificLocation = viewModel.GetStepEditData.Specific_Location;
            updateOneStep.ReferenceVerb = viewModel.GetStepEditData.REFERENCE_VERB;
            updateOneStep.ReferenceObject = viewModel.GetStepEditData.REFERENCE_OBJECT;
            updateOneStep.ReferenceTheories = viewModel.GetStepEditData.REFERENCE_THEORIES;
            updateOneStep.GoToStep = viewModel.GetStepEditData.GOTO_STEP?.ToString();
            updateOneStep.GoToStepId = viewModel.GetStepEditData.GOTO_STEP_ID;
            updateOneStep.Cycles = viewModel.GetStepEditData.Cycles;
            updateOneStep.CycleOnCounter = viewModel.GetStepEditData.Cycle_On_Counter?.ToString();
            updateOneStep.CycleCount = viewModel.GetStepEditData.Cycle_Count?.ToString();
            updateOneStep.CycleUnit = viewModel.GetStepEditData.Cycle_Unit;

            if (viewModel.SelectedReferenceProcedures != null && viewModel.SelectedReferenceProcedures.Count > 0)
            {
                updateOneStep.ReferenceProcs = String.Join(",", viewModel.SelectedReferenceProcedures);
            }

            if (viewModel.SelectedPrecedingSteps != null && viewModel.SelectedPrecedingSteps.Count > 0)
            {
                updateOneStep.PrecedingSteps = String.Join(",", viewModel.SelectedPrecedingSteps);
            }

            updateOneStep.Duration = viewModel.GetStepEditData.Duration;
            updateOneStep.DurationType = viewModel.GetStepEditData.Duration_Type;

            _dbContext.Database.ExecuteStoredProcedure(updateOneStep);
        }

        public bool RollBack(SaveProcedureViewModel model)
        {
            try
            {
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

                var deleteReferenceFileProcedure = new DeleteFileProcedure()
                {
                    ObjID = model.ObjectId,
                    Type = DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                var deleteProcedureRolesProcedure =
                    new DeleteProcedureRolesProcedure() { ObjId = model.ObjectId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedureRolesProcedure);

                var objId = new SqlParameter("@objID", model.ObjectId);

                var NTLogin = new SqlParameter("@strNTLogin", model.NTLogin);

                var retVal = new SqlParameter("@Success", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };

                string command =
                    string.Format("exec A_SP_OBJECT_UNLOCK_AND_DELETE  @objID, @strNTLogin");

                int result = _dbContext.Database.ExecuteSqlCommand(command, objId,
                    NTLogin, retVal);

                if (retVal.SqlValue.ToString() == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public List<GetMoniterViewModel> GetMoniterByStepId(string id, string loginId)
        {
            try
            {
                var objId = new SqlParameter("@RELATED_OBJECT_ID", DBNull.Value);

                var proStepId = id == null ? new SqlParameter("@PROCEDURE_STEP_ID", DBNull.Value) : new SqlParameter("@PROCEDURE_STEP_ID", id);

                var taskId = new SqlParameter("@TASK_ID", DBNull.Value);

                var ntLogin = new SqlParameter("@strNTLogin", loginId);

                var result = _dbContext.Database.SqlQuery<GetMoniterViewModel>("EXEC A_SP_MONITOR_TEMPLATES_GET_DATA_FOR_OBJECT @RELATED_OBJECT_ID, @PROCEDURE_STEP_ID, @TASK_ID, @strNTLogin",
                    objId, proStepId, taskId, ntLogin).ToList();

                return result;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return new List<GetMoniterViewModel>();
            }
        }
        public IQueryable<GetMoniterViewModel> GetMonitors(GetMoniterViewModel model, bool reorderByLatestMonitor)
        {
            var objId = new SqlParameter("@RELATED_OBJECT_ID", model.Related_Object_Id);

            var TaskId = new SqlParameter();

            var ProStepId = new SqlParameter();

            if (model.Task_Id == null)
            {
                TaskId = new SqlParameter("@TASK_ID", DBNull.Value);
            }
            else
            {
                TaskId = new SqlParameter("@TASK_ID", model.Task_Id);
            }
            if (model.Task_Id == null)
            {
                ProStepId = new SqlParameter("@PROCEDURE_STEP_ID", DBNull.Value);
            }
            else
            {
                ProStepId = new SqlParameter("@PROCEDURE_STEP_ID", model.Procedure_Step_Id);
            }

            var NTLogin = new SqlParameter("@strNTLogin", model.StrNTLogin);

            List<GetMoniterViewModel> result = _dbContext.Database.SqlQuery<GetMoniterViewModel>("EXEC A_SP_MONITOR_TEMPLATES_GET_DATA_FOR_OBJECT @RELATED_OBJECT_ID, @PROCEDURE_STEP_ID, @TASK_ID, @strNTLogin",
                objId, ProStepId, TaskId, NTLogin).ToList();

            if (reorderByLatestMonitor)
            {
                result = result.OrderByDescending(x => x.Id).ToList();
            }

            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].MonitorNumber = i + 1;
                    if (result[i].Opinion != 0)
                    {
                        result[i].OpinionText = "This Monitor is based on Opinion";
                    }
                    else
                    {
                        result[i].OpinionText = "";
                    }
                    if (result[i].Hide_Target != 0)
                    {
                        result[i].Hide_Target_Text = "This Monitor will not show the user the target at entry time";
                    }
                    else
                    {
                        result[i].Hide_Target_Text = "";
                    }
                    if (result[i].Use_Result != 0)
                    {
                        result[i].Use_Result_Text = "This Monitor uses its result to match cases";
                    }
                    else
                    {
                        result[i].Use_Result_Text = "";
                    }
                    if (result[i].Fail_Stop != 0)
                    {
                        result[i].Fail_Stop_Text = "This Monitor stops it's procedure when it fails";
                    }
                    else
                    {
                        result[i].Fail_Stop_Text = "";
                    }

                }
            }
            var resultWithNumbers = result;

            return resultWithNumbers.AsQueryable();
        }
        public GetMoniterViewModel GetMonitorById(string id, string loginId)
        {
            var MonitorId = new SqlParameter("@ID", id);

            var NTLogin = new SqlParameter("@strNTLogin", loginId);

            var result = _dbContext.Database
                .SqlQuery<GetMoniterViewModel>(
                    "EXEC A_SP_MONITOR_TEMPLATE_GET_DATA_BY_ID @ID, @strNTLogin",
                    MonitorId, NTLogin).SingleOrDefault();

            return result;
        }



        public bool CreateProcedureObject(EditProcedureObjectViewModel model)
        {

            try
            {
                var saveProcedureProcedure = new SaveProcedureObjects
                {
                    ProcedureObjId = model.PROCEDURE_ID,
                    ProcId = model.ProcID,
                    StepId = model.STEP_ID,
                    ApprovedObjectId = model.APPROVED_OBJECT_ID,
                    Qty = model.QTY,
                    QtyType = model.QTY_TYPE,
                    Relationship = model.RELATIONSHIP,
                    LaborRole = model.LaberRole,
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
        public bool SaveProcedureObject(EditProcedureObjectViewModel model)
        {

            try
            {
                var saveProcedureProcedure = new SaveProcedureObjects
                {
                    Id = model.ID,
                    ProcedureObjId = model.PROCEDURE_ID,
                    ApprovedObjectId = model.APPROVED_OBJECT_ID,
                    Qty = model.QTY,
                    QtyType = model.QTY_TYPE,
                    Relationship = model.RELATIONSHIP,
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
        public bool CreatePartsProvideTakeBack(PartsProvideTakeBackViewModel model)
        {

            try
            {
                var saveProcedureProcedure = new SaveProcedureObjects
                {
                    ProcedureObjId = model.PROCEDURE_ID,
                    ApprovedObjectId = model.APPROVED_OBJECT_ID,
                    Qty = model.QTY,
                    QtyType = model.QTY_TYPE,
                    Relationship = model.RELATIONSHIP,
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
        public bool SavePartsProvideTakeBack(PartsProvideTakeBackViewModel model)
        {

            try
            {
                var saveProcedureProcedure = new SaveProcedureObjects
                {
                    Id = model.ID,
                    ProcedureObjId = model.PROCEDURE_ID,
                    ApprovedObjectId = model.APPROVED_OBJECT_ID,
                    Qty = model.QTY,
                    QtyType = model.QTY_TYPE,
                    Relationship = model.RELATIONSHIP,
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
        public bool Delete(string id, string ntlogin)
        {
            try
            {
                var NTLogin = ntlogin;
                var deleteobjectProcedure = new DeleteEditObjectProcedure() { ID = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteobjectProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public ProcedureEditObjectView EditProcedureObject(string id, string ntlogin)
        {
            var objId = new SqlParameter("@ID", id);

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<ProcedureEditObjectView>("EXEC A_SP_PROCEDURE_OBJECT_LINK_GET_ONE_ITEM_DATA  @ID, @strNTLogin", objId, NTLogin).SingleOrDefault();

            return result;
        }

        public bool DeleteMonitor(string id)
        {
            try
            {
                string query = "DELETE FROM A_MONITOR_TEMPLATES WHERE ID = '" + id + "'";

                _dbContext.Database.ExecuteSqlCommand(query);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public void UpdatePrintOrder(string id, string printOrder)
        {
            try
            {
                string query = "UPDATE A_MONITOR_TEMPLATES SET PRINT_ORDER = '" + printOrder + "' WHERE ID = '" + id + "'";

                _dbContext.Database.ExecuteSqlCommand(query);

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
            }
        }

        public IQueryable<ProcedureSelectResult> GetProcedureSelect(string logiId, string verbName, string co, string root)
        {
            var sql = $"EXEC A_SP_PROCEDURES_SELECT ' (NAME LIKE ''%%'' OR NAME is NULL ) AND  (ROOT LIKE ''%{root}%'' OR ROOT is NULL ) AND" +
                      $"  (CREATING_CO LIKE ''%{co}%'' OR CREATING_CO is NULL ) AND (( VERB_NAME LIKE ''%{verbName}%'' ) ) AND STATUS LIKE ''APPROVED%''',NULL,' ORDER BY NAME','{logiId}'";

            var result = _dbContext.Database.SqlQuery<ProcedureSelectResult>(sql).ToList().AsQueryable();

            return result;
        }

        public bool DeleteStep(string id, string ntLogin)
        {
            try
            {
                _dbContext.Database.ExecuteSqlCommand($"DELETE FROM A_MONITOR_TEMPLATES WHERE ID = {id}");

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool SaveReorderSteps(string[] formCollection, string id, string currentUserId)
        {
            try
            {
                var procedureId = _dbContext.Database.SqlQuery<string>("SELECT ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID =" + id + "").SingleOrDefault();

                _dbContext.Database.ExecuteSqlCommand("DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP IN (SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID =" + procedureId + ")");

                for (var index = 0; index < formCollection.Length; index++)
                {
                    var step = formCollection[index];
                    var split = step.Split(',');
                    var sql = "UPDATE A_PROCEDURE_STEPS SET PRINT_ORDER =" + (index + 1) + " WHERE ID = " + split[1] + "";
                    _dbContext.Database.ExecuteSqlCommand(sql);
                }

                var procedureStepBasedonPrintOrders =
                    new ProcedureStepBasedonPrintOrders { ProcHistId = procedureId, NTLogin = currentUserId };

                _dbContext.Database.ExecuteStoredProcedure(procedureStepBasedonPrintOrders);

                var procedureSetPrintOrderProcedure =
                    new ProcedureSetPrintOrderProcedure { Pid = procedureId, NTLogin = currentUserId };

                _dbContext.Database.ExecuteStoredProcedure(procedureSetPrintOrderProcedure);

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
