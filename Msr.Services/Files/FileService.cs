using Msr.Models.Files;
using Msr.Repositories;
using System;
using System.Linq;
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
      
        public NewFile SaveFileUpload(SaveFileUploadViewModel model)
        {
            NewFile newFile = new NewFile();

            try
            {
                var saveWorkItemImagesProcedure = new SaveFileUploadProcedure
                {
                    DocId = model.DocId,
                    OldDocId = model.OldDocId,
                    Name = model.Name,
                    Desc = model.Desc,
                    Path = model.Path,
                    ContentType = model.ContentType,
                    SrcId = model.SrcId,
                    SrcName = model.SrcName,
                    SrcDesc = model.SrcDesc,
                    SrcPath = model.SrcPath,
                    SrcContentType = model.SrcContentType,
                    SrcChanged = model.SrcChanged,
                    DocChanged = model.DocChanged,
                    DropSrc = model.DropSrc,
                    NTLogin = model.NTLogin,
                    FileKey = model.FileKey,
                    FileUrl = model.FileUrl
                };

                _dbContext.Database.ExecuteStoredProcedure(saveWorkItemImagesProcedure);

                newFile.Id = saveWorkItemImagesProcedure.NewId;

                newFile.Name = saveWorkItemImagesProcedure.Name;

                newFile.Status = true;

                return newFile;
            }
            catch (Exception ex)
            {
                newFile.Status = false;

                return newFile;
            }
        }
    }
}
