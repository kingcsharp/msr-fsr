using Msr.Models.Parts;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using Msr.Services.Parts.Procedures;
using EntityFrameworkExtras.EF6;
using Msr.Services.Parts.ViewModels;
using Msr.Models.Common;
using ExcelDataReader;

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

        public IQueryable<PartApprovedView> GetPartsApprovedQueryable(string co)
        {
            return _dbContext.PartApprovedViews.Where(x => x.Status.Contains("APPROVED") && x.CreatingCo == co);
        }

        public PartsView GetByObjectId(string objectId)
        {
            return GetPartsQueryable().SingleOrDefault(x => x.ObjectId == objectId);
        }

        public List<SelectFile> GetSelectedFiles(string id, string type, string ntlogin)
        {
            var objID = new SqlParameter("@objID", id ?? "0");
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

            var result = _dbContext.Database
                .SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objID, selecttype,
                    NTLogin).ToList();

            return result;
        }

        public SelectInternalPart GetInternalPart(string id, string ntlogin)
        {
            var strID = new SqlParameter("@strID", id ?? "0");
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectInternalPart>("EXEC A_SP_PART_SHOW_EQUIVELANT_INTERNAL_PART  @strID, @strNTLogin", strID, NTLogin).SingleOrDefault();

            return result;
        }

        public List<SelectPartCustomerExecptions> GetPartCustomerExceptions(string id)
        {
            var partId = new SqlParameter("@ProductPartId", id ?? "0");

            var result = _dbContext.Database.SqlQuery<SelectPartCustomerExecptions>("SELECT c.ID AS ID, c.NAME AS NAME FROM A_PARTS_FUTURE_EXCEPTIONS f,A_V_COMPANIES_APPROVED_DATA c WHERE c.ID = f.CUST_ID AND f.PART_ID = @ProductPartId", partId).ToList();

            return result;
        }

        public List<string> GetPartSpecialCustomers(string id)
        {
            var partId = new SqlParameter("@ProductPartId", id ?? "0");

            var result = _dbContext.Database.SqlQuery<string>("SELECT SPEC_ID FROM A_PARTS_FUTURE_SPECIAL_DISTRIBUTIONS WHERE PART_ID = @ProductPartId", partId).ToList();

            return result;
        }

        public ResultNotification<string> Update(AddPartViewModel model)
        {
            var result = new ResultNotification<string>();
            try
            {
                var savePartProcedure = new SavePartProcedure()
                {
                    ObjID = model.ObjID,
                    Company = model.Company,
                    CompanyPartNumber = model.CompanyPartNumber,
                    OemPartNumber = model.OemPartNumber,
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
                    SpecialCustomers = model.SpecialCustomers != null ? string.Join(", ", model.SpecialCustomers) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    CustomerExceptions = model.CustomerExceptions != null ? string.Join(", ", model.CustomerExceptions) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    Customers = model.Customers,
                    Price = model.Price,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                SaveSubParts(model);

                return result;
            }

            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        public ResultNotification<string> Create(AddPartViewModel model)
        {
            var result = new ResultNotification<string>();
            try
            {

                var savePartProcedure = new SavePartProcedure()
                {
                    Company = model.Company,
                    CompanyPartNumber = model.CompanyPartNumber,
                    OemPartNumber = model.OemPartNumber,
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
                    SpecialCustomers = model.SpecialCustomers != null ? string.Join(", ", model.SpecialCustomers) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    CustomerExceptions = model.CustomerExceptions != null ? string.Join(", ", model.CustomerExceptions) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    Customers = model.Customers,
                    Price = model.Price,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);
                result.Entity = savePartProcedure.NewObjID;

                var deleteReferenceFileProcedure = new DeleteFileProcedure()
                {
                    ObjID = savePartProcedure.NewObjID,
                    Type = null,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                if (model.ReferenceFiles != null)
                {
                    foreach (var file in model.ReferenceFiles.Split(','))
                    {
                        var saveFileProcedure =
                            new SaveFileProcedure()
                            {
                                ObjID = savePartProcedure.NewObjID,
                                DocID = file,
                                Type = null,
                                NTLogin = model.NTLogin
                            };

                        _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                    }
                }

                model.ObjID = result.Entity;
                SaveSubParts(model);

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        public bool Delete(string id, string ntlogin)
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

        public List<PartsSafetyStock> GetPartSafetyStocksByLocation(IEnumerable<string> locationIds,
            string partObjectId)
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

        public List<ProductSupplierView> GetProductSuppliersByCreatingCo(string co)
        {
            var result = _dbContext.Database.SqlQuery<ProductSupplierView>($"SELECT DISTINCT TOP 500 NAME,ID FROM A_V_COMPANIES_DROP_SEARCH WHERE ROOT_CO_ID = '{co}'").ToList();
            return result;
        }


        public List<SubPartView> GetSubPartByObjId(string Id, string ntLogin)
        {
            var strId = new SqlParameter("@objID", Id);
            var login = new SqlParameter("@strNTLogin", ntLogin);

            var result = _dbContext.Database.SqlQuery<SubPartView>("Exec A_SP_PARTS_GET_SUB_PART_DATA_FROM_PARENT_OBJ_ID @objID, @strNTLogin", strId, login).ToList();

            return result;
        }

        public ResultNotification<List<ImportPartViewModel>> ImportParts(HttpPostedFileBase postedFile, string ntLogin)
        {
            var result = new ResultNotification<List<ImportPartViewModel>>
            {
                Entity = new List<ImportPartViewModel>()
            };

            try
            {
                if (!postedFile.FileName.EndsWith(".csv"))
                {
                    result.AddError("The import file must be a comma delimited text file");
                    return result;
                }

                DataSet resultAsDataSet;
                using (var reader = ExcelReaderFactory.CreateCsvReader(postedFile.InputStream))
                {
                    resultAsDataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                }

                var columnNames = (from dc in resultAsDataSet.Tables[0].Columns.Cast<DataColumn>() select dc.ColumnName)
                    .ToList();

                var primes = ImportPartViewModel.GetHeaderColumns();

                var results = primes.Where(m => !columnNames.Contains(m));

                var isSubset = primes.Intersect(columnNames).Count() == primes.Count();

                if (!isSubset)
                {
                    result.AddError("Columns missing : (" + string.Join(",", results) + ") to create Part");
                    return result;
                }

                var parts = resultAsDataSet.Tables[0].AsEnumerable().Select(item => new ImportPartViewModel
                {
                    PartId = item[nameof(ImportPartViewModel.PartId)].ToString(),
                    Name = item[nameof(ImportPartViewModel.Name)].ToString(),
                    OemPartNumber = item[nameof(ImportPartViewModel.OemPartNumber)].ToString()
                }).ToList();

                ProcessRow(ntLogin, parts, result);

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        private void ProcessRow(string ntLogin, List<ImportPartViewModel> parts,
            ResultNotification<List<ImportPartViewModel>> result)
        {
            foreach (var part in parts)
            {
                if (string.IsNullOrWhiteSpace(part.PartId))
                {
                    part.Messages.Add("PartId is required");
                }

                if (string.IsNullOrWhiteSpace(part.Name))
                {
                    part.Messages.Add("Name is required");
                }

                if (!part.Messages.Any()) continue;
                result.Entity.Add(part);
                return;
            }

            var connectionStr = ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString;
            using (var conn = new SqlConnection(connectionStr))
            {
                using (var cmd = new SqlCommand("A_SP_PARTS_IMPORT_AND_UPDATE_AN_EXTERNAL_PART", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    foreach (var part in parts)
                    {
                        try
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("@newID", SqlDbType.VarChar, 2000).Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@retPartID", SqlDbType.VarChar, 2000).Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@externalPartID", SqlDbType.VarChar, 100).Value = part.PartId.Trim();
                            cmd.Parameters.Add("@partName", SqlDbType.VarChar, 2000).Value = part.Name.Trim();
                            cmd.Parameters.Add("@strNTLogin", SqlDbType.VarChar, 50).Value = ntLogin;
                            cmd.Parameters.Add("@oemPartNumber", SqlDbType.VarChar, 100).Value = part.OemPartNumber.Trim();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            part.Messages.Add($"Unable to process part with PartName:'{part.Name}' PartId:'{part.PartId}' OemPartNumber:'{part.OemPartNumber}' Exception:'{e} ");
                            result.Entity.Add(part);
                        }
                    }
                }
            }
        }

        public ResultNotification<string> SubPartDeleteById(string id)
        {
            var ressult = new ResultNotification<string>();
            try
            {
                var numOfRowEffected = _dbContext.Database.ExecuteSqlCommand($"DELETE FROM A_PARTS_SUB_PARTS WHERE ID = '{id}'");

                if (numOfRowEffected == 0)
                {
                    ressult.AddError("There is an eroor deleting part");
                }
            }
            catch (Exception ex)
            {
                ressult.AddError("There is an eroor deleting part. ERROR: " + ex);
            }

            return ressult;
        }

        private void SaveSubParts(AddPartViewModel model)
        {
            if (model.SubPartList != null && model.SubPartList.Any())
            {
                foreach (var subPart in model.SubPartList)
                {
                    var saveSubPartEditProcedure = new UpdateSubPartProcedure
                    {
                        NtLogin = model.NTLogin,
                        Qty = subPart.Qty,
                        ParentObjId = model.ObjID,
                        NickName = subPart.NickName,
                        PartId = subPart.PartId
                    };

                    if (!string.IsNullOrWhiteSpace(subPart.Id))
                    {
                        saveSubPartEditProcedure.Id = subPart.Id;
                    }

                    _dbContext.Database.ExecuteStoredProcedure(saveSubPartEditProcedure);
                }
            }
        }

    }
}