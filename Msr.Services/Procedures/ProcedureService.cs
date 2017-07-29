using EntityFrameworkExtras.EF6;
using Msr.Models.Procedures;
using Msr.Repositories;
using Msr.Services.Orders.Procedures;
using Msr.Services.ProcedureTypes.Procedures;
using Msr.Services.ProcedureTypes.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.Procedure;

namespace Msr.Services.Procedures
{
	public class ProcedureService
    {
		private readonly MsrDbContext _dbContext;

		public ProcedureService()
		{
			_dbContext = new MsrDbContext();
		}
		
		public ProcedureView GetProcedureById(string Id)
		{
			return _dbContext.ProcedureViews.Where(x => x.Id == Id).SingleOrDefault();
		}
		public IQueryable<ProcedureView> GetProceduresQueryable()
		{

			return _dbContext.ProcedureViews;					
		}

		public bool Save(ProcedureView model)
		{

			try
			{
				var saveProcedureProcedure = new SaveProcedureTypesProcedure {  };

				_dbContext.Database.ExecuteStoredProcedure(saveProcedureProcedure);

				return true;
			}
			catch (Exception ex)
			{
				var message = "Error occured:" + ex.Message;

				return false;
			}
		}
		public bool Edit(SaveProcedureTypesViewModel model)
		{

			try
			{
				var saveProcedureProcedure = new SaveProcedureTypesProcedure { ObjId = model.ObjectId, Name = model.Name, VerbType = model.VerbType, NTLogin = model.NTLogin };

				_dbContext.Database.ExecuteStoredProcedure(saveProcedureProcedure);

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
