using Msr.Models.Parts;
using Msr.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Msr.Services.Parts.Procedures;
using EntityFrameworkExtras.EF6;
using Msr.Services.Parts.ViewModels;
using System.Data;
using Msr.Models.Common;
using Msr.Models.People;

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
        public List<SelectFile> GetSelectedFiles(string id, string type,string ntlogin)
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
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objID, selecttype, NTLogin).ToList();

            return result;
        }
        public SelectInternalPart GetInternalPart(string id,string ntlogin)
        {
            var strID = new SqlParameter("@strID", id == null ? "0" : id);
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectInternalPart>("EXEC A_SP_PART_SHOW_EQUIVELANT_INTERNAL_PART  @strID, @strNTLogin", strID, NTLogin).SingleOrDefault();

            return result;
        }
        public List<SelectPartCustomerExecptions> GetPartCustomerExceptions(string id)
        {
            var partId = new SqlParameter("@ProductPartId", id == null ? "0" : id);

            var result = _dbContext.Database.SqlQuery<SelectPartCustomerExecptions>("SELECT c.ID AS ID, c.NAME AS NAME FROM A_PARTS_FUTURE_EXCEPTIONS f,A_V_COMPANIES_APPROVED_DATA c WHERE c.ID = f.CUST_ID AND f.PART_ID = @ProductPartId", partId).ToList();

            return result;
        }
        public List<string> GetPartSpecialCustomers(string id)
        {
            var partId = new SqlParameter("@ProductPartId", id == null ? "0" : id);

            var result = _dbContext.Database.SqlQuery<string>("SELECT SPEC_ID FROM A_PARTS_FUTURE_SPECIAL_DISTRIBUTIONS WHERE PART_ID = @ProductPartId", partId).ToList();

            return result;
        }
        public bool Edit(AddPartViewModel model)
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
                    InternalEqualParts = model.InternalEqualParts,
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
        public bool Delete(string id,string ntlogin)
        {
            try
            {
                //need to be dynamic
                var NTLogin = ntlogin;
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

        public List<PartsSafetyStock> GetPartSafetyStocksByLocation(IEnumerable<string> locationIds, string partObjectId)
        {
            var locationIdsParam = new SqlParameter("@locationIdsParam", String.Join("','", locationIds));
            var partObjectIdParam = new SqlParameter("@partObjectIdParam", partObjectId);
            string sql =
                string.Format(
                    @"SELECT Id, LOCATION_ID AS LocationId, MIN_LEVEL AS MinLevel, MIN_WARNING_LEVEL AS MinWarningLevel, MAX_WARNING_LEVEL AS MaxWarningLevel, " +
                    "MAX_LEVEL AS MaxLevel FROM A_PARTS_SAFETY_STOCK_LEVELS WHERE LOCATION_ID IN ('{0}') AND PART_OBJ_ID = '{1}'",
                    String.Join("','", locationIds), partObjectId);
            var result =
                _dbContext.Database.SqlQuery<PartsSafetyStock>(sql).ToList();

            return result;
        }

        public List<string> GetSafetyStockRoles(string safetyStockId, string emailType)
        {
            var safetyStockIdParam = new SqlParameter("@safetyStockIdParam", safetyStockId);
            var emailTypeParam = new SqlParameter("@emailTypeParam", emailType);

            var safetyStockRoles = _dbContext.Database.SqlQuery<string>("SELECT ROLE_ID FROM A_V_PARTS_SAFETY_STOCK_ROLES WHERE SAFETY_STOCK_ID = @safetyStockIdParam AND EMAIL_TYPE = @emailTypeParam ORDER BY ROLE_NAME",
                safetyStockIdParam, emailTypeParam);
            return safetyStockRoles.ToList();
        }

        public void UpdatePartsSafetyStocks(PartsSafetyStock partsSafetyStock, string partObjectId, string loginId)
        {
            var partSaveSafetyLevelSettingProcedure = new PartSaveSafetyLevelSettingProcedure()
            {
                LocId = partsSafetyStock.LocationId,
                StrNTLogin = "1618",
                MinLevel = partsSafetyStock.MinLevel,
                MaxWarningLevel = partsSafetyStock.MaxWarningLevel,
                MaxLevel = partsSafetyStock.MaxLevel,
                MinWarningLevel = partsSafetyStock.MinWarningLevel,
                PartObjId = partObjectId
            };

            _dbContext.Database.ExecuteStoredProcedure(partSaveSafetyLevelSettingProcedure);

            string safetyStockId =
                GetSinglePartSafetyStockIdByLocation(partsSafetyStock.LocationId, partObjectId);

            if (!string.IsNullOrEmpty(safetyStockId))
            {
                var safetyStockIdParam = new SqlParameter("@safetyStockIdParam", safetyStockId);

                var deleteSafetyStockRoles =
                    _dbContext.Database.ExecuteSqlCommand(
                        "DELETE FROM A_PARTS_SAFETY_STOCK_ROLES WHERE SAFETY_STOCK_ID = @safetyStockIdParam",
                        safetyStockIdParam);

                if (partsSafetyStock.RolesAssignedToFail != null)
                {
                    foreach (var role in partsSafetyStock.RolesAssignedToFail)
                    {
                        InsertSafetyStock(role, safetyStockId, loginId, "FAIL");
                    }
                }

                if (partsSafetyStock.RolesAssignedToWarn != null)
                {
                    foreach (var role in partsSafetyStock.RolesAssignedToWarn)
                    {
                        InsertSafetyStock(role, safetyStockId, loginId, "WARN");
                    }
                }
            }
        }

        private void InsertSafetyStock(string role, string safetyStockId, string loginId, string emailType)
        {
            var safetyStockIdParam = new SqlParameter("@safetyStockIdParam", safetyStockId);
            var roleParam = new SqlParameter("@roleParam", role);
            var loginIdParam = new SqlParameter("@loginIdParam", loginId);
            var emailTypeParam = new SqlParameter("@emailTypeParam", emailType);
            var safetyStockRole = _dbContext.Database.ExecuteSqlCommand("INSERT INTO A_PARTS_SAFETY_STOCK_ROLES (ID,SAFETY_STOCK_ID,ROLE_ID,EMAIL_TYPE,DRCM,MODBY) " +
                                                                        "VALUES (newID(), @safetyStockIdParam, @roleParam, @emailTypeParam, getDate(), @loginIdParam)",
                safetyStockIdParam, roleParam, emailTypeParam, loginIdParam);
        }

        public string GetSinglePartSafetyStockIdByLocation(string locationId, string partObjectId)
        {
            var locationIdsParam = new SqlParameter("@locationIdsParam", locationId);
            var partObjectIdParam = new SqlParameter("@partObjectIdParam", partObjectId);
            var result =
                _dbContext.Database.SqlQuery<string>(
                    "SELECT Id FROM A_PARTS_SAFETY_STOCK_LEVELS WHERE LOCATION_ID = @locationIdsParam AND PART_OBJ_ID = @partObjectIdParam",
                    locationIdsParam, partObjectIdParam).SingleOrDefault();

            return result;
        }

        public string GetInternalEqualPartByPartId(string id)
        {
            var result =
                _dbContext.Database.SqlQuery<string>(
                        "SELECT EQUAL_PART_ID FROM A_PARTS_INTERNAL_EQUALS where PART_ID='" + id + "'")
                    .FirstOrDefault();

            return result;
        }
        public List<ProductSupplierView> GetProductSuppliersByCreatingCo(string id)
        {
            var result = _dbContext.Database.SqlQuery<ProductSupplierView>("SELECT DISTINCT TOP 500 NAME,ID FROM A_V_COMPANIES_DROP_SEARCH WHERE ROOT_CO_ID = '" + id + "'").ToList();
            return result;
        }
    }
}