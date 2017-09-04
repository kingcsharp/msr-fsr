using EntityFrameworkExtras.EF6;
using Msr.Models;
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
using System.Text;
using System.Threading.Tasks;
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

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_ProcedureStepGetRefProcedures @ID, @strNTLogin", objId, NTLogin).ToList();

            return result;
        }


        public List<SelectFile> GetSelectedRefFiles(string id, string ntlogin)
        {
            var objId = new SqlParameter("@procStepID", id ?? "0");

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_ProcedureStepGetRefFiles @procStepID, @strNTLogin", objId, NTLogin).ToList();

            return result;
        }

        public List<DocFile> GetSelectedRefFilesDialog(string id, string ntlogin)
        {
            var objId = new SqlParameter("@procStepID", id ?? "0");

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<DocFile>("EXEC Portal_ProcedureStepGetRefFilesDialog @procStepID, @strNTLogin", objId, NTLogin).ToList();

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

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<LaborObjectsView>("EXEC Portal_GetProcedureStepLabors @ID, @strNTLogin", objId, NTLogin).ToList();

            return result;
        }
        public bool Delete(string id, string ntlogin)
        {
            try
            {
                var NTLogin = ntlogin;
                var deletePreproProcedure = new DeleteProcedureStepProcedure() { Objid = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePreproProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Save(ProcedurePreProViewModel model)
        {
            try
            {
                var savePartProcedure = new SaveProcedureStepProcedure()
                {
                    Id = model.Id,
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
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var savePrePopProcedure = new SavePrePopProcedure() { ObjId = model.ObjectId, ProcStepId = model.Id, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(savePrePopProcedure);

                var deletePictureFileProcedure = new DeleteProcedureStepFileLinkProcedure() { id = model.Id, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveProcedureStepFileLinkProcedure() { ObjId = model.Id, DocId = file, NTLogin = model.NTLogin };

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


        public bool Create(ProcedurePreProViewModel model)
        {

            try
            {

                var savePartProcedure = new SaveProcedureStepProcedure()
                {
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
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var savePrePopProcedure = new SavePrePopProcedure() { ObjId = null, ProcStepId = savePartProcedure.NewId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(savePrePopProcedure);

                var deletePictureFileProcedure = new DeleteProcedureStepFileLinkProcedure() { id = savePartProcedure.NewId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveProcedureStepFileLinkProcedure() { ObjId = savePartProcedure.NewId, DocId = file, NTLogin = model.NTLogin };

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
                var message = "Error occured:" + ex.Message;

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
                    var message = "Error occured:" + ex.Message;

                    return false;
                }
            }
        }
    }

