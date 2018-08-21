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
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using Msr.Models.Common;
using Msr.Services.Documents;
using Msr.Services.Objects;
using Msr.Services.Parts;
using Msr.Services.Procedures.Messages;
using Msr.Services.Procedures.Procedures;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProcedureVerbs;
using Msr.Services.PurchesOrder.ViewModels;
using Msr.Services.Roles;
using Msr.Services.Roles.Messages;
using Msr.Services.TheoryParagraph;
using Msr.Services.Users.Messages;

namespace Msr.Services.Procedures
{
    public class ProceduresService
    {
        private readonly MsrDbContext _dbContext;
        private DocumentService documentService;
        private readonly RoleService _roleService;
        private PartsService _partsService;

        public ProceduresService()
        {
            _dbContext = new MsrDbContext();
            documentService = new DocumentService();
            _partsService = new PartsService();
            _roleService = new RoleService();
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

        public List<RoleResult> GetSelectedRoles(string id, string ntlogin)
        {
            var objId = new SqlParameter("@strID", id ?? "0");
            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<RoleResult>("EXEC A_SP_PROCEDURES_GET_ROLES_TO_VIEW  @strID, @strNTLogin", objId, ntLogin).ToList();

            return result;
        }

        public List<PreProFile> GetStepTemplateList(string co)
        {
            var result = _dbContext.Database.SqlQuery<PreProFile>($"SELECT ID as Value, Title as Show,STEP_TEXT as StepText FROM A_V_PREPOP_QUICK WHERE TITLE <> '' AND CREATING_CO = '{co}'").ToList();

            return result;
        }

        public List<GetSystemTasksResult> GetSystemList(string co)
        {
            var result = _dbContext.Database.SqlQuery<GetSystemTasksResult>($"SELECT NAME as Name,SYSTEM_ID as System_Id FROM A_V_PROCEDURES_APPROVED_DATA WHERE IS_SYSTEM = 1 and CREATING_CO = {co} ORDER BY NAME").ToList();

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
                    Should_Be = model.Should_Be,
                    Opinion = model.Opinion,
                    Hide_Target = model.Hide_Target,
                    Use_Result = model.Use_Result,
                    Fail_Stop = model.Fail_Stop,
                    Step_Id = model.Step_Id,
                    Task_Id = model.Task_Id,
                    Tolerance = model.Tolerance,
                    Related_Object_Id = model.Related_Object_Id,
                    Fail_Action = model.Fail_Action,
                    Target_Object_Type = model.Target_Object_Type,
                    Skip_Mode = model.Skip_Mode,
                    Cant_Change = model.Cant_Change,
                    Always_Pass = model.Always_Pass,
                    StrNTLogin = model.StrNTLogin
                };

                if (model.Monitor_Type == "NUMBER" && model.Should_Be == "BETWEEN")
                {
                    addProcedureMonitorProcedure.Target_Object = null;
                    addProcedureMonitorProcedure.Highest_Threshold = model.Highest_Threshold;
                    addProcedureMonitorProcedure.Lowest_Threshold = model.Lowest_Threshold;
                }
                else if (model.Monitor_Type == "NUMBER" && model.Should_Be != "BETWEEN" || model.Monitor_Type == "EQUIPMENT")
                {
                    addProcedureMonitorProcedure.Target = Convert.ToSingle(model.Target_Object);
                    addProcedureMonitorProcedure.Highest_Threshold = null;
                    addProcedureMonitorProcedure.Lowest_Threshold = null;
                    addProcedureMonitorProcedure.Correct_Answer = null;
                    addProcedureMonitorProcedure.Text_Target = null;
                }
                else if (model.Monitor_Type == "YES_NO")
                {
                    addProcedureMonitorProcedure.Correct_Answer = model.Correct_Answer;
                    addProcedureMonitorProcedure.Target_Object = null;
                    addProcedureMonitorProcedure.Text_Target = null;
                    addProcedureMonitorProcedure.Target = null;
                }
                else if (model.Monitor_Type == "TEXT")
                {
                    addProcedureMonitorProcedure.Text_Target = model.Text_Target;
                    addProcedureMonitorProcedure.Target_Object = null;
                    addProcedureMonitorProcedure.Correct_Answer = null;
                    addProcedureMonitorProcedure.Target = null;
                }
                else if (model.Monitor_Type == "PASS_FAIL")
                {
                    addProcedureMonitorProcedure.Target_Object = model.Target_Object;
                    addProcedureMonitorProcedure.Target = null;
                    addProcedureMonitorProcedure.Highest_Threshold = null;
                    addProcedureMonitorProcedure.Lowest_Threshold = null;
                    addProcedureMonitorProcedure.Text_Target = null;
                    addProcedureMonitorProcedure.Correct_Answer = null;
                }

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

        public string PrePopSave(string procedureId, string preProId, string ntlogin)
        {
            try
            {
                var saveProcedureStepPreProStepProcedure = new SaveProcedureStepPreProStepProcedure()
                {
                    ProcObjId = procedureId,
                    PreProId = preProId,
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
                    IsActive = model.IsActive,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureProcedure);

                result.Entity = saveProcedureProcedure.NewObjId;

                //var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = saveProcedureProcedure.NewObjId, Type = null, NTLogin = model.NTLogin };
                //_dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                if (model.ReferenceFiles != null)
                {
                    foreach (var file in model.ReferenceFiles.Split(','))
                    {
                        var saveFileProcedure =
                            new SaveFileProcedure
                            {
                                ObjId = saveProcedureProcedure.NewObjId,
                                DocId = file,
                                Type = null,
                                NTLogin = model.NTLogin
                            };

                        _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                    }
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
                    Threshold = model.Threshold,
                    IsActive = model.IsActive
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

                var adminSqlToRunQueUpProcedure = new AdminSqlToRunQueUpProcedure() { MySql = sql, NTLogin = model.LoginId };

                _dbContext.Database.ExecuteStoredProcedure(adminSqlToRunQueUpProcedure);
            }

            return new BaseNotification();
        }

        public List<GetStepDataResult> GetStepsData(string procedureObjectId, string loginId)
        {
            var getStepsDataProcedure = new GetStepsDataProcedure() { ProcedureObjectId = procedureObjectId, NTLogin = loginId };
            var result = _dbContext.Database.ExecuteStoredProcedure<GetStepDataResult>(getStepsDataProcedure).ToList();

            foreach (var stepData in result)
            {
                stepData.Role = stepData.Roles?.Split(',').ToList();
                if (!string.IsNullOrWhiteSpace(stepData.Pre_Step))
                {
                    Regex regex = new Regex("<i>(.*)</i>");
                    var preStepMatch = regex.Match(stepData.Pre_Step);
                    stepData.Pre_Step = preStepMatch.Groups[1].ToString();
                }
                stepData.StepLaborsList = GetLaborsById(stepData.Id, loginId).Select(x => new SelectListItem
                {
                    Text = x.Role_Name,
                    Value = x.Role_id.ToString(),
                }).OrderBy(o => o.Text).ToList();

                stepData.GetMoniterViewModels = GetMoniterByStepId(stepData.Id, loginId);
                stepData.SetUp(new ProcedureVerbsService(), new ObjectsService(), new DocumentFilesService(), new TheoryParagraphService(), _roleService);
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

            getStepEditDataViewModel.ListReferenceFiles = _partsService.GetSelectedFiles(id: stepId, type: null, ntlogin: loginId).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value,
            }).OrderBy(o => o.Text).ToList();

            getStepEditDataViewModel.ListReferenceObjects = documentService.GetSelectedObjects(id: stepId, ntlogin: loginId).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();


            getStepEditDataViewModel.ListReferenceTheories = documentService.GetSelectedTheories(stepId, loginId).Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString(),
            }).OrderBy(o => o.Text).ToList();

            return getStepEditDataViewModel;
        }

        public ResultNotification<string> CreateStepData(GetStepEditDataViewModel viewModel)
        {
            var result = new ResultNotification<string>();
            try
            {
                var stepsList = GetStepsData(viewModel.ProcObjId, viewModel.NtLogin);
                var selectedPrecedingStep = "";

                if (stepsList != null)
                {
                    selectedPrecedingStep = stepsList.LastOrDefault()?.Id;
                }

                var updateOneStep = new UpdateOneStepProcedure()
                {
                    Title = viewModel.GetStepEditData.Title,
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
                    ReferenceTheories = string.Join(", ", viewModel.ReferenceTheories),
                    GoToStep = viewModel.GetStepEditData.GOTO_STEP?.ToString(),
                    GoToStepId = viewModel.GetStepEditData.GOTO_STEP_ID,
                    Cycles = viewModel.GetStepEditData.Cycles,
                    CycleOnCounter = viewModel.GetStepEditData.Cycle_On_Counter?.ToString(),
                    CycleCount = viewModel.GetStepEditData.Cycle_Count?.ToString(),
                    CycleUnit = viewModel.GetStepEditData.Cycle_Unit,
                    ReferenceProcs = String.Join(",", viewModel.SelectedReferenceProcedures),
                    PrecedingSteps = selectedPrecedingStep,
                    Duration = viewModel.GetStepEditData.Duration,
                    EquipmentTime = viewModel.GetStepEditData.EquipmentTime,
                    DurationType = viewModel.GetStepEditData.Duration_Type,
                    ReplacementCost = viewModel.ReplacementCost,
                    Utilization = viewModel.Utilization,
                    UsefulLife = viewModel.UsefulLife
                };

                _dbContext.Database.ExecuteStoredProcedure(updateOneStep);

                var deleteProcedureStepFile = new DeleteProcedureStepFile() { StepId = updateOneStep.NewId, NTLogin = viewModel.NtLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedureStepFile);

                foreach (var file in viewModel.ReferenceFiles)
                {
                    var saveProcedureStepFileProcedure = new SaveProcedureStepFileProcedure() { StepId = updateOneStep.NewId, FileId = file, NTLogin = viewModel.NtLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveProcedureStepFileProcedure);
                }

                result.Entity = updateOneStep.NewId;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                result.AddError(message);
                return result;
            }

            return result;

        }

        public void UpdateStepData(GetStepDataResult viewModel)
        {
            var updateOneStep = new UpdateOneStepProcedure
            {
                Id = viewModel.Id,
                StepText = viewModel.Step_Text,
                ProcObjId = viewModel.ProcObjId,
                Comments = viewModel.Comments,
                StartOnCounter = viewModel.Start_On_Counter?.ToString(),
                CounterValue = viewModel.Counter_Value?.ToString(),
                CounterUnit = viewModel.Counter_Unit,
                FromStartOrStop = viewModel.FROM_START_OR_STOP,
                RelOrAbs = viewModel.REL_OR_ABS,
                SystemTask = viewModel.StepSystemTask,
                Destination = viewModel.Destination,
                SpecificLocation = viewModel.Specific_Location,
                ReferenceVerb = viewModel.REFERENCE_VERB,
                ReferenceObject = viewModel.REFERENCE_OBJECT,
                ReferenceTheories = string.Join(", ", viewModel.ReferenceTheories),
                GoToStep = viewModel.GOTO_STEP?.ToString(),
                GoToStepId = viewModel.GOTO_STEP_ID,
                Cycles = viewModel.Cycles,
                CycleOnCounter = viewModel.Cycle_On_Counter?.ToString(),
                CycleCount = viewModel.Cycle_Count?.ToString(),
                CycleUnit = viewModel.Cycle_Unit,
                PrecedingSteps = viewModel.Pre_Step,
                Title = viewModel.Title,
                EquipmentTime = viewModel.EquipmentTime,
                Roles = viewModel.Role != null ? string.Join(",", viewModel.Role) : null,
                Duration = viewModel.Duration,
                DurationType = viewModel.Duration_Type,
                ReplacementCost = viewModel.ReplacementCost,
                Utilization = viewModel.Utilization,
                UsefulLife = viewModel.UsefulLife
            };

            if (viewModel.SelectedReferenceProcedureTypes != null && viewModel.SelectedReferenceProcedureTypes.Count > 0)
            {
                updateOneStep.ReferenceProcs = String.Join(",", viewModel.SelectedReferenceProcedureTypes);
            }

            if (viewModel.SelectedPrecedingSteps != null && viewModel.SelectedPrecedingSteps.Count > 0)
            {
                updateOneStep.PrecedingSteps = String.Join(",", viewModel.SelectedPrecedingSteps);
            }

            updateOneStep.Duration = viewModel.Duration;
            updateOneStep.DurationType = viewModel.Duration_Type;

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

        public bool DeleteMonitor(string id, string ntLogin)
        {
            try
            {
                _dbContext.Database.ExecuteSqlCommand($"DELETE FROM A_MONITOR_TEMPLATES WHERE ID = {id}");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SaveReorderSteps(Dictionary<string, double?> formCollection, string id, string currentUserId)
        {
            try
            {
                var procedureId = _dbContext.Database.SqlQuery<string>("SELECT ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID =" + id + "").SingleOrDefault();

                _dbContext.Database.ExecuteSqlCommand("DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP IN (SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID =" + procedureId + ")");

                foreach (var step in formCollection)
                {
                    var order = step.Value;

                    if (!step.Value.HasValue)
                    {
                        order = formCollection.OrderByDescending(x => x.Value).FirstOrDefault().Value + 1;
                    }

                    var sql = "UPDATE A_PROCEDURE_STEPS SET PRINT_ORDER =" + order + " WHERE ID = " + step.Key + "";

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

        public bool DeleteProcedure(string id, string ntlogin)
        {
            try
            {
                var deleteProcedure = new DeleteProcedure() { ID = id, NTLogin = ntlogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public IList<ProcedureStepOtherStepListView> GetReorderSteps(string procObjectId)
        {
            var sql = $"SELECT * FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = (SELECT ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = '" + procObjectId + "') ORDER BY PRINT_ORDER";

            var result = _dbContext.Database.SqlQuery<ProcedureStepOtherStepListView>(sql).ToList();
            var selectedIndex = 0;
            foreach (var step in result)
            {
                step.print_Order = ++selectedIndex;
            }
            return result;
        }

        public ResultNotification<List<ProcedureImportViewModel>> ImportProcedures(HttpPostedFileBase postedFile, LoggedUserIdResult ntLogin)
        {
            var result = new ResultNotification<List<ProcedureImportViewModel>>
            {
                Entity = new List<ProcedureImportViewModel>()
            };

            try
            {
                if (!postedFile.FileName.EndsWith(".csv"))
                {
                    result.AddError("The import file must be a tab delimited text file");
                    return result;
                }

                var reader = ExcelReaderFactory.CreateCsvReader(postedFile.InputStream);

                var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                reader.Close();

                var columnNames = (from dc in ds.Tables[0].Columns.Cast<DataColumn>()
                                   select dc.ColumnName).ToList();

                var primes = ProcedureImportViewModel.GetHeaderColumns();

                var results = primes.Where(m => !columnNames.Contains(m));
                var isSubset = primes.Intersect(columnNames).Count() == primes.Count();

                if (!isSubset)
                {
                    result.AddError("Coloums missing : (" + string.Join(",", results) + ") to create Procedure");
                    return result;
                }

                var modelList = Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new ProcedureImportViewModel
                {
                    ProcedureId = item["ProcedureId"].ToString(),
                    ProcedureName = item["ProcedureName"].ToString(),
                    AnsId = item["AnsId"].ToString(),
                    ProcType = item["ProcType"].ToString()
                }).ToList();

                ProcessRow(ntLogin, modelList, result);

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        public ResultNotification<string> DeleteProcedureStep(string id, string ntLogin)
        {
            var result = new ResultNotification<string>();

            try
            {
                var deleteProcedureStep = new ProcedureStepDelete() { Id = id, NtLogin = ntLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedureStep);

                return result;

            }
            catch (Exception ex)
            {
                result.AddError("There is an error deleting procedure step");
                return result;
            }
        }

        private void ProcessRow(LoggedUserIdResult ntLogin, List<ProcedureImportViewModel> modelList, ResultNotification<List<ProcedureImportViewModel>> result)
        {
            foreach (var model in modelList)
            {
                if (string.IsNullOrWhiteSpace(model.ProcedureId))
                {
                    model.Messages.Add("Id is required");
                }

                if (string.IsNullOrWhiteSpace(model.ProcedureName))
                {
                    model.Messages.Add("ProcedureName is required");
                }

                if (string.IsNullOrWhiteSpace(model.AnsId))
                {
                    model.Messages.Add("AnsId is required");
                }

                if (string.IsNullOrWhiteSpace(model.ProcType))
                {
                    model.Messages.Add("ProcType is required");
                }


                if (model.Messages.Any())
                {
                    result.Entity.Add(model);
                    continue;
                }

                var procedureImportExternalProcedure = new ProcedureImportExternalProcedure();
                procedureImportExternalProcedure.Id = model.ProcedureId;
                procedureImportExternalProcedure.Name = model.ProcedureName;
                procedureImportExternalProcedure.AnsId = model.AnsId;
                procedureImportExternalProcedure.ProcType = model.ProcType;
                procedureImportExternalProcedure.NtLogin = ntLogin.Id;
                _dbContext.Database.ExecuteStoredProcedure(procedureImportExternalProcedure);

                model.Processed = true;
                model.Messages.Add(procedureImportExternalProcedure.NewId);
                result.Entity.Add(model);
            }
        }

        public IQueryable<ProcedureApprovedView> GetProcedureApprovedQueryable(string ntLogin)
        {
            var sql =
                $"EXEC A_SP_PROCEDURES_SELECT_FOR_PROCEDURE_STEPS ' (NAME LIKE ''%%'' OR NAME is NULL ) AND  (ROOT LIKE ''%%'' OR ROOT is NULL ) AND  (CREATING_CO LIKE ''%%'' OR CREATING_CO is NULL )',NULL,' ORDER BY NAME','{ntLogin}'";

            var result = _dbContext.Database.SqlQuery<ProcedureApprovedView>(sql).ToList().AsQueryable();

            return result;
        }

        public AddMonitorForProcedureViewModel EditMonitorSteps(string objectId, string ntLogin)
        {
            var objId = new SqlParameter("@ID", objectId);

            var NTLogin = new SqlParameter("@strNTLogin", ntLogin);

            var result = _dbContext.Database.SqlQuery<AddMonitorForProcedureViewModel>("EXEC A_SP_MONITOR_TEMPLATE_GET_DATA_BY_ID  @ID, @strNTLogin", objId, NTLogin).SingleOrDefault();

            if (result.Monitor_Type == "EQUIPMENT" || result.Monitor_Type == "NUMBER" && result.Should_Be != "BETWEEN")
            {
                result.Target_Object = result.Target.ToString();
            }

            return result;
        }

    }
}

