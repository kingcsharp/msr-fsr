using Msr.Repositories;
using System;
using System.Linq;
using Msr.Models.AdminCostSettings;

namespace Msr.Services.AdminCostSettings
{
    public class AdminCostSettingService
    {
        private readonly MsrDbContext _dbContext;

        public AdminCostSettingService()
        {
            _dbContext = new MsrDbContext();
        }

        public AdminCostSetting GetAdminCostSettings()
        {
            return _dbContext.AdminCostSettings.FirstOrDefault();
        }

        public ResultNotification<bool> Update(AdminCostSetting model)
        {
            var result = new ResultNotification<bool>();

            try
            {
                var adminCostSettings = _dbContext.AdminCostSettings.FirstOrDefault();

                if (adminCostSettings != null)
                {
                    adminCostSettings.RmAnnualRate = model.RmAnnualRate;
                    adminCostSettings.LaborRateMinute = model.LaborRateMinute;
                    adminCostSettings.YearsHours = model.YearsHours;
                    adminCostSettings.HourMinutes = model.HourMinutes;

                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.AdminCostSettings.Add(model);
                    _dbContext.SaveChanges();
                }

                result.Entity = true;
            }
            catch (Exception e)
            {
                result.ErrorList.Add("There is an error updating Admin Cost Settings.");
            }

            return result;
        }
    }
}
