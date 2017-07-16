using Msr.Models.Parts;
using Msr.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Msr.Services.Parts.Procedures;
using EntityFrameworkExtras.EF6;
using Msr.Services.Parts.ViewModels;

namespace Msr.Services.Parts
{
    public class PartsService
    {
        private readonly MsrDbContext _dbContext;

        public PartsService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<PartsView> GetPartsQueryable()
        {
            try
            {
                return _dbContext.PartsViews;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Part GetById(string Id)
        {
            return _dbContext.Parts.Where(x => x.ObjectID == Id).SingleOrDefault();
        }
        public bool Save(AddPartViewModel model)
        {
            try
            {
                foreach (var file in model.PictureFiles)
                {
                    //exec A_SP_FILES_CREATE_LINK '114434','10187',NULL,'1618'
                }

                foreach (var file in model.ReferenceFiles)
                {
                    //exec A_SP_FILES_CREATE_LINK '114434','10187',NULL,'1618'
                }

                foreach (var file in model.ReferenceTheories)
                {
                    //exec A_SP_FILES_CREATE_LINK '114434','10187',NULL,'1618'
                }
                var savePartProcedure = new SavePartProcedure() { Company = model.Company, CompanyPartNumber = model.CompanyPartNumber, Name = model.Name, PartType = model.PartType, Spare = model.Spare, Consumable = model.Consumable, Unit = model.Unit, UnitShippingWeight = model.UnitShippingWeight, SubParts = model.SubParts, CustomerSeeAvailability = model.CustomerSeeAvailability, SupplierSeeAvailability = model.SupplierSeeAvailability, SupplierSeeInstallBase = model.SupplierSeeInstallBase, InternalEqualParts = model.InternalEqualParts, WeightType = model.WeightType, CreateProd = model.CreateProd, SupplierCo = model.SupplierCo, ProductType = model.ProductType, ProcVerb = model.ProcVerb, SpecialCustomer = model.SpecialCustomer, CustomerExceptions = model.CustomerExceptions, Customers = model.Customers, Price = model.Price, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);
                
                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Edit(AddPartViewModel model)
        {
            try
            {
                var savePartProcedure = new SavePartProcedure() { ObjID = model.ObjID, Company = model.Company, CompanyPartNumber = model.CompanyPartNumber, Name = model.Name, PartType = model.PartType, Spare = model.Spare, Consumable = model.Consumable, Unit = model.Unit, UnitShippingWeight = model.UnitShippingWeight, SubParts = model.SubParts, CustomerSeeAvailability = model.CustomerSeeAvailability, SupplierSeeAvailability = model.SupplierSeeAvailability, SupplierSeeInstallBase = model.SupplierSeeInstallBase, InternalEqualParts = model.InternalEqualParts, WeightType = model.WeightType, CreateProd = model.CreateProd, SupplierCo = model.SupplierCo, ProductType = model.ProductType, ProcVerb = model.ProcVerb, SpecialCustomer = model.SpecialCustomer, CustomerExceptions = model.CustomerExceptions, Customers = model.Customers, Price = model.Price, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

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
