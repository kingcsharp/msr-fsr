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
using Msr.Models.Common;

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
        public List<SelectFile> GetSelectedFiles(string id, string type)
        {
            var objID = new SqlParameter("@objID", id == null ? "0" : id);
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

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objID, selecttype, NTLogin).ToList();

            return result;
        }
        public SelectInternalPart GetInternalPart(string id)
        {
            var strID = new SqlParameter("@strID", id == null ? "0" : id);
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectInternalPart>("EXEC A_SP_PART_SHOW_EQUIVELANT_INTERNAL_PART  @strID, @strNTLogin", strID, NTLogin).SingleOrDefault();

            return result;
        }
        public List<SelectPartCustomerExecptions> GetPartCustomerExceptions(string id)
        {
            var partId = new SqlParameter("@PartId", id == null ? "0" : id);

            var result = _dbContext.Database.SqlQuery<SelectPartCustomerExecptions>("SELECT c.ID AS ID, c.NAME AS NAME FROM A_PARTS_FUTURE_EXCEPTIONS f,A_V_COMPANIES_APPROVED_DATA c WHERE c.ID = f.CUST_ID AND f.PART_ID = @PartId", partId).ToList();

            return result;
        }
        public List<string> GetPartSpecialCustomers(string id)
        {
            var partId = new SqlParameter("@PartId", id == null ? "0" : id);

            var result = _dbContext.Database.SqlQuery<string>("SELECT SPEC_ID FROM A_PARTS_FUTURE_SPECIAL_DISTRIBUTIONS WHERE PART_ID = @PartId", partId).ToList();

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
                    InternalEqualParts = null,
                    WeightType = model.WeightType,
                    CreateProd = model.CreateProd,
                    SupplierCo = model.SupplierCo,
                    ProductType = model.ProductType,
                    ProcVerb = model.ProcVerb,
                    SpecialCustomers = model.SpecialCustomers != null ? string.Join(", ", model.SpecialCustomers) : DBNull.Value.ToString(),
                    CustomerExceptions = model.CustomerExceptions != null ? string.Join(", ", model.CustomerExceptions) : DBNull.Value.ToString(),
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
        public bool Create(AddPartViewModel model)
        {
            try
            {

                var savePartProcedure = new SavePartProcedure()
                {
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
                    InternalEqualParts = null,
                    WeightType = model.WeightType,
                    CreateProd = model.CreateProd,
                    SupplierCo = model.SupplierCo,
                    ProductType = model.ProductType,
                    ProcVerb = model.ProcVerb,
                    SpecialCustomers = model.SpecialCustomers != null ? string.Join(", ", model.SpecialCustomers) : DBNull.Value.ToString(),
                    CustomerExceptions = model.CustomerExceptions != null ? string.Join(", ", model.CustomerExceptions) : DBNull.Value.ToString(),
                    Customers = model.Customers,
                    Price = model.Price,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var singlePart = GetById(savePartProcedure.NewObjID);


                var deletePictureFileProcedure = new DeleteFileProcedure() { ObjID = singlePart.Id, Type = "PICTURE", NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);


                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = singlePart.Id, Type = null, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                var deleteTheoryFileProcedure = new DeleteFileProcedure() { ObjID = singlePart.Id, Type = "THEORY", NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteTheoryFileProcedure);

                foreach (var file in model.PictureFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = singlePart.Id, DocID = file, Type = "PICTURE", NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = singlePart.Id, DocID = file, Type = null, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.ReferenceTheories)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = singlePart.Id, DocID = file, Type = "THEORY", NTLogin = model.NTLogin };

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
        public bool Delete(string id)
        {
            try
            {
                //need to be dynamic
                var NTLogin = "1618";
                var deletePartProcedure = new DeletePartProcedure() { ObjID = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePartProcedure);

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
