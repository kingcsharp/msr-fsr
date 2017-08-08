using Msr.Models.Files;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
