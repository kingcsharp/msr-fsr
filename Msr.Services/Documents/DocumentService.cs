using EntityFrameworkExtras.EF6;
using Msr.Models.Comman;
using Msr.Models.Documents;
using Msr.Repositories;
using Msr.Services.Documents.Procedures;
using Msr.Services.Documents.ViewModels;
using Msr.Services.Parts.Procedures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Documents
{
    public class DocumentService
    {
        private readonly MsrDbContext _dbContext;

        public DocumentService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<DocumentView> GetDocumentsQueryable()
        {
            return _dbContext.DocumentViews;
        }

        public DocumentView GetById(string id)
        {
            return GetDocumentsQueryable().Where(x => x.ObjectId == id).SingleOrDefault();
        }

        public List<SelectFile> GetSelectedObjects(string id)
        {
            var objID = new SqlParameter("@ID", id == null ? "0" : id);

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_GetTheoryObjects  @ID, @strNTLogin", objID, NTLogin).ToList();

            return result;
        }

        public List<SelectRole> GetSelectedRoles(string id)
        {
            var objID = new SqlParameter("@ID", id == null ? "0" : id);

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectRole>("EXEC Portal_GetTheoryRoles  @ID, @strNTLogin", objID, NTLogin).ToList();

            return result;
        }

        public List<SelectFile> GetSelectedTheories(string id)
        {

            var objID = new SqlParameter("@ID", id == null ? "0" : id);

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_THEORY_GET_REF_THEORY  @ID, @strNTLogin", objID, NTLogin).ToList();

            return result;


        }

        public bool Save(SaveDocumentViewModel model)
        {
            try
            {
                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = model.Id, Type = null, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.Id, DocID = file, Type = null, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                var saveDocumentProcedure = new SaveDocumentProcedure
                {
                    Id = model.Id,
                    ObjID = model.ObjectId,
                    Company = model.Company,
                    Name = model.Name,
                    Comments = model.Comments,
                    SecurityLevel = model.ApprovalStatus,
                    RolesToView = model.Roles != null ? string.Join(", ", model.Roles) : null,
                    ReferenceObjects = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : null,
                    ReferenceTheory = model.ReferenceTheory != null ? string.Join(", ", model.ReferenceTheory) : null,
                    NTLogin = model.NTLogin

                };

                _dbContext.Database.ExecuteStoredProcedure(saveDocumentProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Create(SaveDocumentViewModel model)
        {
            try
            {


                var saveDocumentProcedure = new SaveDocumentProcedure
                {
                    Company = model.Company,
                    Name = model.Name,
                    Comments = model.Comments,
                    SecurityLevel = model.ApprovalStatus,
                    RolesToView = model.Roles != null ? string.Join(", ", model.Roles) : null,
                    ReferenceObjects = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : null,
                    ReferenceTheory = model.ReferenceTheory != null ? string.Join(", ", model.ReferenceTheory) : null,
                    NTLogin = model.NTLogin

                };

                _dbContext.Database.ExecuteStoredProcedure(saveDocumentProcedure);

                var createdDocument = GetById(saveDocumentProcedure.NewObjID);

                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = createdDocument.ObjectId, Type = null, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = createdDocument.ObjectId, DocID = file, Type = null, NTLogin = model.NTLogin };

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
    }
}
