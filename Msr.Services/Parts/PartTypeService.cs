using EntityFrameworkExtras.EF6;
using Msr.Models.Parts;
using Msr.Repositories;
using Msr.Services.Parts.Messages;
using Msr.Services.Parts.Procedures;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Parts
{
    public class PartTypeService
    {
        private readonly MsrDbContext _dbContext;

        public PartTypeService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<PartTypesView> GetPartTypesQueryable()
        {
            return _dbContext.PartTypesViews;
        }
        public PartType GetById(string Id)
        {
            return _dbContext.PartTypes.Where(x => x.Id == Id).Single();
        }
        public bool Create(AddPartTypesViewModel model)
        {
            try
            {
                var savePartTypeProcedure = new SavePartTypeProcedure { Name = model.Name, Spare = model.Spare, Consumable = model.Consumable, Unit = model.Unit, StrNTlogin = model.NTLogin, UnitShippingWeight = model.UnitShippingWeight };

                var task = _dbContext.Database.ExecuteStoredProcedure<SavePartTypeProcedure>(savePartTypeProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Edit(AddPartTypesViewModel model)
        {
            try
            {
                var savePartTypeProcedure = new SavePartTypeProcedure { ObjID = model.ObjId, Name = model.Name, Spare = model.Spare, Consumable = model.Consumable, Unit = model.Unit, UnitShippingWeight = model.UnitShippingWeight };

                _dbContext.Database.ExecuteStoredProcedure(savePartTypeProcedure);

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
