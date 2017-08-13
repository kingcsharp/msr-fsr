using System;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.Parts;
using Msr.Repositories;
using Msr.Services.Parts.Procedures;
using Msr.Services.Parts.ViewModels;
using Msr.Services.PartTypes.ViewModels;

namespace Msr.Services.PartTypes
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
        public PartTypesView GetById(string id)
        {
            return GetPartTypesQueryable().Where(x => x.Id == id).SingleOrDefault();
        }
        public bool Create(AddPartTypesViewModel model)
        {
            try
            {
                var savePartTypeProcedure = new SavePartTypeProcedure { Name = model.Name, Spare = model.Spare, Consumable = model.Consumable, Unit = model.Unit, StrNTlogin = model.NTLogin, UnitShippingWeight = model.UnitShippingWeight };

                _dbContext.Database.ExecuteStoredProcedure<SavePartTypeProcedure>(savePartTypeProcedure);

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

        public bool Delete(string id)
        {
            try
            {
                //need to be dynamic
                var NTLogin = "1618";
                var deletePartTypeProcedure = new DeletePartTypeProcedure() { ObjID = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePartTypeProcedure);

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
