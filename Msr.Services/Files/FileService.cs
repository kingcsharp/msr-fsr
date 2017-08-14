using Msr.Models.Files;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;
using Msr.Services.Files.Procedures;
using Msr.Services.Files.ViewModels;

namespace Msr.Services.Files
{
    public class FileService
    {
        private readonly MsrDbContext _dbContext;

        public FileService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<FileView> GetFilesQueryable()
        {
           // _dbContext.FIleViews.AsQueryable().Where(x=>log).ToList();
            return _dbContext.FIleViews.AsQueryable();
        }
        public List<ListFileView> SelectListedFiles(string id)
        {
            var ObjIdParm = new SqlParameter("@ObjId", id);

            var fileListView = _dbContext.Database.SqlQuery<ListFileView>("Portal_SelectListedFiles @ObjId", ObjIdParm).AsQueryable();

            return fileListView.ToList();
        }
        public bool SaveFileUpload(SaveFileUploadViewModel model)
        {
            try
            {
                var saveWorkItemImagesProcedure = new SaveFileUploadProcedure { DocId = model.DocId, OldDocId = model.OldDocId, Name = model.Name, Desc = model.Desc, Path = model.Path, ContentType = model.ContentType, SrcId = model.SrcId, SrcName = model.SrcName, SrcDesc = model.SrcDesc, SrcPath = model.SrcPath, SrcContentType = model.SrcContentType, SrcChanged = model.SrcChanged, DocChanged = model.DocChanged, DropSrc = model.DropSrc, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveWorkItemImagesProcedure);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
