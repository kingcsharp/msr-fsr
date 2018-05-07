using EntityFrameworkExtras.EF6;
using Msr.Models.Common;
using Msr.Repositories;
using Msr.Services.Parts.Procedures;
using Msr.Services.PrePro.Procedure;
using Msr.Services.PrePro.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Msr.Models.PrePro;
using Msr.Services.Documents.ViewModels;

namespace Msr.Services.PrePro
{

    public class PreProServices
    {
        private readonly MsrDbContext _dbContext;

        public PreProServices()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<PrePropSearchView> GetPreProQueryable()
        {
            return _dbContext.PrePropSearchView;
        }
        public PrePropSearchView GetById(string id)
        {
            return GetPreProQueryable().SingleOrDefault(x => x.ObjectId == id);
        }

        public List<SelectFile> GetSelectedRefProcedures(string id, string ntlogin)
        {
            var objId = new SqlParameter("@ID", id ?? "0");
            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_ProcedureStepGetRefProcedures @ID, @strNTLogin", objId, ntLogin).ToList();

            return result;
        }


        public List<SelectFile> GetSelectedRefFiles(string id, string ntlogin)
        {
            var objId = new SqlParameter("@procStepID", id ?? "0");
            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_ProcedureStepGetRefFiles @procStepID, @strNTLogin", objId, ntLogin).ToList();

            return result;
        }

        public List<DocFile> GetSelectedRefFilesDialog(string id, string ntlogin)
        {
            var objId = new SqlParameter("@procStepID", id ?? "0");

            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<DocFile>("EXEC Portal_ProcedureStepGetRefFilesDialog @procStepID, @strNTLogin", objId, ntLogin).ToList();

            return result;
        }

        public List<SelectFile> GetSelectedRefTheories(string id)
        {
            if (id == null) return new List<SelectFile>();

            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT t.NAME AS Show,t.ID AS Value FROM A_PROCEDURE_STEP_THEORY_LINK l, A_V_THEORY_APPROVED_DATA t  where l.PROC_STEP_ID = " + id + " and t.ID = l.THEORY_ID").ToList();

            return result;
        }

        public List<SelectFile> GetApprovedObjectList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT TOP 500 OBJ_DESC AS Show,ID AS Value FROM A_V_APPROVED_OBJECTS WHERE CREATING_CO = '2' AND  OBJ_TABLE = 'A_ROLES_HISTORY'    AND (( OBJ_DESC LIKE '%a%' AND OBJ_DESC LIKE '%%' ) )    ORDER BY OBJ_DESC").ToList();

            return result;
        }
        public List<LaborObjectsView> GetProcedureStepLaborList(string id, string ntlogin)
        {
            var objId = new SqlParameter("@ID", id ?? "0");

            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<LaborObjectsView>("EXEC Portal_GetProcedureStepLabors @ID, @strNTLogin", objId, ntLogin).ToList();

            return result;
        }
        public bool Delete(string id, string ntlogin)
        {
            try
            {
                var ntLogin = ntlogin;
                var deletePreproProcedure = new DeleteProcedureStepProcedure() { Objid = id, NTLogin = ntLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePreproProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Save(ProcedurePreProViewModel model)
        {
            try
            {
                var savePartProcedure = new SaveProcedureStepProcedure()
                {
                    Id = model.PkId,
                    StepText = model.StepText,
                    ProcObjId = model.ProcObjId,
                    Comments = model.Comments,
                    StartOnCounter = model.StartOnCounter.ToString(),
                    CounterValue = null,
                    CounterUnit = null,
                    FromStartOrStop = null,
                    RelOrAbs = null,
                    SystemTask = model.SystemTask,
                    Destination = model.Destination,
                    SpecificLocation = null,
                    ReferenceVerb = model.ReferenceVerb,
                    ReferenceObject = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    ReferenceTheories = model.ReferenceTheories != null ? string.Join(", ", model.ReferenceTheories) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    GotoStep = null,
                    GotoStepId = null,
                    Cycles = null,
                    CycleOnCounter = null,
                    CycleCount = null,
                    CycleUnit = null,
                    ReferenceProcs = model.ReferenceProcedures != null ? string.Join(", ", model.ReferenceProcedures) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    PrecedingSteps = null,
                    Duration = model.Duration.ToString(),
                    DurationType = model.DurationType,
                    NTLogin = model.NTLogin,
                    Title = model.Title,
                    EquipmentTime = null
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var savePrePopProcedure = new SavePrePopProcedure() { ObjId = model.ObjectId, ProcStepId = model.PkId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(savePrePopProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Create(ProcedurePreProViewModel model)
        {

            try
            {
                var procedureStepProcedure = new SaveProcedureStepProcedure(model);
                _dbContext.Database.ExecuteStoredProcedure(procedureStepProcedure);

                var prepopUpdateOnePrepop = new PrepopUpdateOnePrepop
                {
                    StrNTLogin = model.NTLogin,
                    ProcStepID = procedureStepProcedure.NewId
                };
                _dbContext.Database.ExecuteStoredProcedure(prepopUpdateOnePrepop);

                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = procedureStepProcedure.NewId, Type = null, NTLogin = model.NTLogin };
                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                var deletePictureFileProcedure = new DeleteProcedureStepFileLinkProcedure() { id = procedureStepProcedure.NewId, NTLogin = model.NTLogin };
                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);


                if (model.ReferenceFiles != null)
                    foreach (var file in model.ReferenceFiles.Split(','))
                    {
                        var saveFileProcedure = new SaveProcedurePreProFileProcedure()
                        {
                            ObjId = procedureStepProcedure.NewId,
                            DocId = file,
                            NtLogin = model.NTLogin
                        };

                        _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                    }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<SelectFile> GetApprovedVerbsByCreatingCo(string creatingCo)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SET QUOTED_IDENTIFIER OFF SELECT NAME AS Show, ID AS Value, CREATING_CO as CreatingCo FROM A_APPROVED_VERBS where CREATING_CO='" + creatingCo + "'").ToList();

            return result;
        }
        public List<SelectFile> GetReferenceObjectsByCreatingCo(string creatingCo)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT ID as Value, OBJ_TABLE as Show, CREATING_CO as CreatingCo FROM A_V_APPROVED_OBJECTS where CREATING_CO='" + creatingCo + "'").ToList();

            return result;
        }
        public IQueryable<ProcedureObjectsLaborStepView> GetLaborStepsList()
        {
            return _dbContext.ProcedureObjectsLaborStepViews;
        }

        public bool AddLabor(ProcedureObjectViewModel model)
        {
            try
            {
                var saveProcedureObjectLink = new SaveProcedureObjectLink()
                {
                    ProcedureObjectId = model.ProcedureObjectId,
                    ProcId = model.ProcId,
                    StepId = model.ProcedureStepId,
                    ApprovedObjectId = model.ApprovedObjectId,
                    Qty = model.Qty,
                    QtyType = model.QtyType,
                    RelationShip = model.Relationship,
                    LaborRole = model.LaborRole,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureObjectLink);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool SaveLabor(ProcedureObjectViewModel model)
        {
            try
            {
                var saveProcedureObjectLink = new SaveProcedureObjectLink()
                {
                    Id = model.Id,
                    ProcedureObjectId = model.ProcedureObjectId,
                    ProcId = model.ProcId,
                    StepId = model.ProcedureStepId,
                    ApprovedObjectId = model.ApprovedObjectId,
                    Qty = model.Qty,
                    QtyType = model.QtyType,
                    RelationShip = model.Relationship,
                    LaborRole = model.LaborRole,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureObjectLink);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<SelectFile> GetApplicableObjectsByCreatingCo(string creatingCo)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT ID as Value, OBJ_TABLE as Show FROM A_V_APPROVED_OBJECTS where CREATING_CO='" + creatingCo + "'").ToList();

            return result;
        }
        public bool UpdateApplicableObjects(ApplicableObjectsView model)
        {
            try
            {
                var saveUpdateApplicableObjectsProcedure = new SaveUpdateApplicableObjectsProcedure()
                {

                    StepId = model.StepId,
                    LinkId = model.LinkId,
                    Quantity = model.Quantity,
                    ObjectId = model.ObjectId,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveUpdateApplicableObjectsProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SavePreProSingleFileReference(string objectId, string file, string currentUserId)
        {
            try
            {
                var saveFileProcedure = new SaveProcedurePreProFileProcedure() { ObjId = objectId, DocId = file, NtLogin = currentUserId };

                _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }

        }

        public bool DeletePreProRefLinkImageById(string id, string fileId)
        {
            try
            {
                var result =
                    _dbContext.Database.ExecuteSqlCommand(
                        $"delete from dbo.A_PROCEDURE_STEP_FILE_LINK  where STEP_ID ={id} and  FILE_ID ={fileId}");
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

