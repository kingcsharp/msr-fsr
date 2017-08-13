using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Msr.Models.Administration;
using Msr.Repositories;
using Msr.Services.Administration.Messages;
using Msr.Services.Administration.ViewModels;

namespace Msr.Services.Administration
{
    public class AdministrationService
    {
        private const string AppDataGlobalsettingsXml = @"../App_Data/GlobalSettings.xml";
        private readonly MsrDbContext _dbContext;

        public AdministrationService()
        {
            _dbContext = new MsrDbContext();
        }

        public BaseNotification AssignNewsProcedure()
        {
            var result = new BaseNotification();

            try
            {
                /*
                             declare @p1 varchar(4000)
                set @p1=NULL
                declare @p2 varchar(500)
                set @p2=NULL
                exec A_SP_ADMIN_PROCEDURE_SAVE_NEWS_PROCEDURE @p1 output,@p2 output,'69524','1618'
                select @p1, @p2

                             */

                return new BaseNotification();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public void GetAssignProcedure()
        {
            ////////EXEC A_SP_ADMIN_GET_NEWS_PROCEDURE '1618'
            throw new NotImplementedException();
        }

        public IQueryable<ViewCompanyUsage> ViewCompanyUsage()
        {
            var sql =
                "EXEC A_SP_ADMIN_GET_PEOPLE_PAGE_HIT_DATA ' (FULL_NAME LIKE ''%%'' OR FULL_NAME is NULL ) AND  (PAGE LIKE ''%%'' OR PAGE is NULL ) AND  (MO LIKE ''%%'' OR MO is NULL ) AND  (YR LIKE ''%%'' OR YR is NULL ) AND  (DA LIKE ''%%'' OR DA is NULL ) AND  (CO_NAME LIKE ''%%'' OR CO_NAME is NULL ) AND  (DEPT_NAME LIKE ''%%'' OR DEPT_NAME is NULL )',' ORDER BY',NULL,'1618'";

            var result = _dbContext.Database.SqlQuery<ViewCompanyUsage>(sql).ToList().AsQueryable();

            return result;
        }

        public IList<SelectListItem> GetAssignRoleToJob()
        {
            var sql = "SELECT ID AS Text,ID AS Value FROM A_ADMIN_ROLE_JOBS_LIST_OF_JOBS ORDER BY ID";

            var result = _dbContext.Database.SqlQuery<SelectListItem>(sql).ToList();

            return result;
        }

        public IList<RoleForJobItem> GetRolesForJob(string jobId)
        {
            var sql = "exec A_SP_ADMIN_GET_ROLE_FOR_JOB '" + jobId + "','1618'";

            var result = _dbContext.Database.SqlQuery<RoleForJobItem>(sql).ToList();

            return result;
        }

        public BaseNotification UpdateAssignRoleToJob(UpdateAssignRoleToJobRequest jobRequest)
        {
            var result = new BaseNotification();

            try
            {
                foreach (var job in jobRequest.RoleToJobItems)
                {
                    var sql = $"exec A_SP_ADMIN_JOB_ROLE_UPDATE '{job.CoId}','{job.RoleId}','1618'";

                    _dbContext.Database.ExecuteSqlCommand(sql);
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public BaseNotification UpdateAssignAccRecievableRole(List<AssignAccRecievableRoleItem> items)
        {
            var result = new BaseNotification();

            try
            {
                foreach (var job in items)
                {
                    var sql = $"exec A_SP_ADMIN_SERVICE_CALL_ACCT_RECEIVABLE_ROLE_UPDATE '{job.CoId}','{job.RoleId}','{job.LocationId}','1618'";

                    _dbContext.Database.ExecuteSqlCommand(sql);
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public List<AssignAccRecievableRoleResult> GetAssignAccRecievableRole()
        {
            var sql = "exec A_SP_ADMIN_SERVICE_CALL_GET_ACCT_RECEIVED_ROLE_DATA 1618";

            var result = _dbContext.Database.SqlQuery<AssignAccRecievableRoleResult>(sql).ToList();

            return result;
        }

        public List<AssignCompaniesToViewResult> GetAssignCompaniesToView()
        {
            var sql = "exec A_SP_ADMIN_GET_COMPANIES_TO_VIEW_MY_COMPANY '1618'";

            var result = _dbContext.Database.SqlQuery<AssignCompaniesToViewResult>(sql).ToList();

            return result;
        }

        public BaseNotification UpdateAssignCompaniesToView(List<AssignCompaniesToViewItemViewModel> items)
        {
            var result = new BaseNotification();

            try
            {
                foreach (var job in items)
                {
                    var sql = $"exec A_SP_ADMIN_COMPANIES_CAN_VIEW_ME_UPDATE '{job.CoId}','{job.ViewCoId}','1618'";

                    _dbContext.Database.ExecuteSqlCommand(sql);
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public List<ModuleAccessResult> GeModuleAccess()
        {
            var sql = "SELECT * FROM A_MENUS ORDER BY MENU_GROUP,ID,NAME";

            var result = _dbContext.Database.SqlQuery<ModuleAccessResult>(sql).ToList();

            return result;
        }

        public BaseNotification UpdateModuleAccess(List<ModuleAccessViewModel> items)
        {
            var result = new BaseNotification();

            try
            {
                foreach (var job in items)
                {
                    var sql = $"exec A_SP_ADMIN_MENU_ROLES_UPDATE '{job.Id}','{job.RoleId}','1618'";

                    _dbContext.Database.ExecuteSqlCommand(sql);
                }

            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public List<GlobalSettingsViewModel> ReadGlobalSettings()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(AppDataGlobalsettingsXml));
            XmlNode rootNode = doc.DocumentElement?.SelectSingleNode("/txt");
            List<GlobalSettingsViewModel> globalSettings = new List<GlobalSettingsViewModel>();
            if (rootNode?.ChildNodes != null)
            {
                globalSettings.AddRange(from XmlNode node in rootNode.ChildNodes
                    select new GlobalSettingsViewModel
                    {
                        Id = node.Attributes["id"]?.InnerText, Value = node.Attributes["value"]?.InnerText
                    });
            }
            return globalSettings;
        }

        public void UpdateGlobalSettings(List<GlobalSettingsViewModel> globalSettings)
        {
            string filePath = HttpContext.Current.Server.MapPath(AppDataGlobalsettingsXml);
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("txt");
            xmlDoc.AppendChild(rootNode);

            foreach (var globalSetting in globalSettings)
            {
                XmlNode userNode = xmlDoc.CreateElement("textString");
                XmlAttribute idAttribute = xmlDoc.CreateAttribute("id");
                idAttribute.Value = globalSetting.Id;
                userNode.Attributes.Append(idAttribute);
                XmlAttribute valueAttribute = xmlDoc.CreateAttribute("value");
                valueAttribute.Value = globalSetting.Value;
                userNode.Attributes.Append(valueAttribute);
                rootNode.AppendChild(userNode);
            }

            xmlDoc.Save(filePath);
        }
    }
}
