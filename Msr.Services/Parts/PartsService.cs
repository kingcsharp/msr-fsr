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
using System.Data;

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
            return _dbContext.PartsViews;
        }
        public PartsView GetById(string Id)
        {
            return GetPartsQueryable().Where(x => x.ObjectId == Id).SingleOrDefault();
        }
        public List<SelectPartsFile> GetSelectedFiles(string id, string type)
        {
            var objID = new SqlParameter("@objID", id);
            var selecttype = new SqlParameter();
            if (type == null)
            {
                selecttype = new SqlParameter("@type", DBNull.Value);
            }
            else
            {
                selecttype = new SqlParameter("@type", type);
            }
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectPartsFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objID, selecttype, NTLogin).ToList();

            return result;
        }
        public SelectInternalPart GetInternalPart(string id)
        {
            var strID = new SqlParameter("@strID", id);
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectInternalPart>("EXEC A_SP_PART_SHOW_EQUIVELANT_INTERNAL_PART  @strID, @strNTLogin", strID, NTLogin).SingleOrDefault();

            return result;
        }
        public bool Save(AddPartViewModel model)
        {
            try
            {
                var deletePictureFileProcedure = new DeleteFileProcedure() { ObjID = model.Id, Type = "PICTURE", NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);


                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = model.Id, Type = null, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                var deleteTheoryFileProcedure = new DeleteFileProcedure() { ObjID = model.Id, Type = "THEORY", NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteTheoryFileProcedure);


                foreach (var file in model.PictureFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.Id, DocID = file, Type = "PICTURE", NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.Id, DocID = file, Type = null, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.ReferenceTheories)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.Id, DocID = file, Type = "THEORY", NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                var savePartProcedure = new SavePartProcedure()
                {
                    ObjID = model.ObjID,
                    Company = model.Company,
                    CompanyPartNumber = model.CompanyPartNumber,
                    Name = model.Name,
                    PartType = model.PartType,
                    Spare = model.Spare,
                    Consumable = model.Consumable,
                    Unit = model.Unit,
                    UnitShippingWeight = model.UnitShippingWeight,
                    SubParts = model.SubParts,
                    CustomerSeeAvailability = model.CustomerSeeAvailability,
                    SupplierSeeAvailability = model.SupplierSeeAvailability,
                    SupplierSeeInstallBase = model.SupplierSeeInstallBase,
                    InternalEqualParts = model.InternalEqualParts,
                    WeightType = model.WeightType,
                    CreateProd = model.CreateProd,
                    SupplierCo = model.SupplierCo,
                    ProductType = model.ProductType,
                    ProcVerb = model.ProcVerb,
                    SpecialCustomer = model.SpecialCustomer,
                    CustomerExceptions = model.CustomerExceptions,
                    Customers = model.Customers,
                    Price = model.Price,
                    NTLogin = model.NTLogin
                };

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
