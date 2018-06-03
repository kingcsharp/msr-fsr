using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Msr.Repositories;
using Msr.Models.ActualParts;
using System.Data.SqlClient;
using System.Web;
using Amazon.Runtime.Internal;
using Msr.Services.ActualParts.ViewModels;
using EntityFrameworkExtras.EF6;
using ExcelDataReader;
using Msr.Services.ActualParts.Procedures;
using Msr.Models.Common;
using Msr.Services.Users.Messages;

namespace Msr.Services.ActualParts
{
    public class ActualPartsService
    {
        private readonly MsrDbContext _dbContext;

        public ActualPartsService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<ActualPartsView> GetActualPartsQueryable()
        {
            return _dbContext.ActualPartsViews;
        }

        public IQueryable<ActualPartApprovedView> GetActualPartsApprovedQueryable()
        {
            return _dbContext.ActualPartApprovedViews;
        }

        public IQueryable<ActualPartViewHistoryView> GetActualPartViewHistoryQueryable()
        {
            return _dbContext.ActualPartViewHistoryViews;
        }

        public ActualPartsView GetActualPartById(string id)
        {
            return GetActualPartsQueryable().SingleOrDefault(x => x.ObjectId == id);
        }

        public List<SelectFile> GetActualParts(string status)
        {
            var result = GetActualPartsQueryable().Where(x => x.Status.StartsWith(status) & !(x.SysName == null || x.SysName.Trim() == string.Empty))
                .Select(x => new SelectFile { Value = x.ObjectId, Show = x.SysName }).ToList();

            return result;
        }

        public List<string> GetActualpartProductsInstalled(string id, string ntlogin)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<string>("Exec Portal_ActualPartProductInstalled @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public List<RootCompanyTree> GetActualPartCompanies(string ntlogin)
        {
            var strID = new SqlParameter("@PERSON_ID", ntlogin);
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<RootCompanyTree>("Exec A_SP_COMPANIES_SHOW_PERSONS_ROOT_COMPANY_TREE @PERSON_ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public bool Edit(SaveActualPartsViewModel model)
        {
            try
            {
                var saveActualPartsProcedure = new SaveActualPartsProcedure
                {
                    ObjId = model.ObjectId,
                    PartId = model.PartId,
                    Qty = model.Qty.ToString(),
                    Serial = model.Serial,
                    NickName = model.NickName,
                    LocationId = model.LocationId,
                    CurOwner = model.CurOwner,
                    ApStatus = model.APStatus,
                    Products = model.Products != null ? string.Join(", ", model.Products) : DBNull.Value.ToString(),
                    ParentId = model.ParentId,
                    SubPartAction = model.SubpartAction,
                    ResponsiblePerson = model.ResponsiblePerson,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveActualPartsProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Create(SaveActualPartsViewModel model)
        {
            try
            {
                var saveActualPartsProcedure = new SaveActualPartsProcedure
                {
                    //Qty = model.Qty <= 0 ? 1 : model.Qty,
                    PartId = model.PartId,
                    Qty = model.Qty.ToString(),
                    Serial = model.Serial,
                    NickName = model.NickName,
                    LocationId = model.LocationId,
                    CurOwner = model.CurOwner,
                    ApStatus = model.APStatus,
                    Products = model.Products != null ? string.Join(", ", model.Products) : DBNull.Value.ToString(),
                    ParentId = model.ParentId,
                    SubPartAction = model.SubpartAction,
                    ResponsiblePerson = model.ResponsiblePerson,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveActualPartsProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Close(string id, string ntlogin)
        {
            try
            {
                var NTLogin = ntlogin;
                var closeViewHistoryTaskProcedure = new CloseViewHistoryTaskProcedure() { Id = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(closeViewHistoryTaskProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool ReAssignTask(ReassignTaskViewModel model)
        {
            try
            {
                var reAssignTaskProcedure = new ReAssignTaskProcedure
                {

                    Id = model.Id,
                    Requestee_Id = model.PersonToReAssign,
                    Group_Requestee_Id = model.GroupToReassign,
                    Comments = model.ReAssignComments,
                    StrNTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(reAssignTaskProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool DeleteViewHistory(string id, string loginId)
        {
            try
            {
                var deleteHistoryProcedure = new DeleteHistoryProcedure
                {
                    ID = id,
                    StrNTlogin = loginId
                };

                _dbContext.Database.ExecuteStoredProcedure(deleteHistoryProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public ResultNotification<List<ActualPartImportViewModel>> ImportActualParts(HttpPostedFileBase postedFile, LoggedUserIdResult ntLogin)
        {
            var result = new ResultNotification<List<ActualPartImportViewModel>>
            {
                Entity = new AutoConstructedList<ActualPartImportViewModel>()
            };

            try
            {
                if (!postedFile.FileName.EndsWith(".csv"))
                {
                    result.AddError("The import file must be a tab delimited text file");
                    return result;
                }

                var reader = ExcelReaderFactory.CreateCsvReader(postedFile.InputStream);

                var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                reader.Close();

                var columnNames = (from dc in ds.Tables[0].Columns.Cast<DataColumn>()
                                   select dc.ColumnName).ToList();

                var primes = ActualPartImportViewModel.GetHeaderColumns();

                var results = primes.Where(m => !columnNames.Contains(m));
                bool isSubset = primes.Intersect(columnNames).Count() == primes.Count();
                if (!isSubset)
                {
                    result.AddError("Coloums missing : (" + string.Join(",", results) + ") to create Part");
                    return result;
                }

                var modelList = Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new ActualPartImportViewModel
                {
                    Id = item["Id"].ToString(),
                    Sn = item["SN"].ToString(),
                    NickName = item["NickName"].ToString(),
                    Owner = item["Owner"].ToString(),
                    PartId = item["PartId"].ToString(),
                    Qty = item["Qty"].ToString(),
                    LocationId = item["LocationId"].ToString(),
                    ParentId = item["ParentId"].ToString()
                }).ToList();


                foreach (var model in modelList)
                {

                    if (string.IsNullOrWhiteSpace(model.Id))
                    {
                        model.Messages.Add("Id is required");
                    }

                    if (string.IsNullOrWhiteSpace(model.Sn))
                    {
                        model.Messages.Add("SN is required");
                    }
                    if (string.IsNullOrWhiteSpace(model.NickName))
                    {
                        model.Messages.Add("NickName is required");
                    }
                    if (model.Messages.Any())
                    {
                        result.Entity.Add(model);
                        continue;
                    }

                    var actualPartsImportUpdateExternalProcedure = new ActualPartsImportUpdateExternalProcedure();

                    actualPartsImportUpdateExternalProcedure.Id = model.Id;
                    actualPartsImportUpdateExternalProcedure.PartId = model.PartId;
                    actualPartsImportUpdateExternalProcedure.PartOwner = model.Owner;
                    actualPartsImportUpdateExternalProcedure.PartSN = model.Sn;
                    actualPartsImportUpdateExternalProcedure.PartNickName = model.NickName;
                    actualPartsImportUpdateExternalProcedure.PartQty = model.Qty;
                    actualPartsImportUpdateExternalProcedure.LocationId = model.LocationId;
                    actualPartsImportUpdateExternalProcedure.ParentId = model.ParentId;
                    actualPartsImportUpdateExternalProcedure.NtLogin = ntLogin.Id;
                    _dbContext.Database.ExecuteStoredProcedure(actualPartsImportUpdateExternalProcedure);

                    model.Processed = true;
                    model.Messages.Add(actualPartsImportUpdateExternalProcedure.NewId);
                    result.Entity.Add(model);
                }

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }
    }
}