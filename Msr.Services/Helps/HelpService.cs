using System;
using System.Linq;
using Msr.Repositories;
using Msr.Services.Helps.ViewModels;
using Msr.Models.Helps;
using Msr.Services.Roles;
using Msr.Services.Users;

namespace Msr.Services.Help
{
   public class HelpService
    {
        private readonly MsrDbContext _dbContext;

        private readonly RoleService _roleService;

        public HelpService()
        {
            _dbContext = new MsrDbContext();
            _roleService = new RoleService();
        }

        public IQueryable<HelpView> GetHelpQueryable()
        {
            return _dbContext.HelpViews;
        }

        public HelpView GetById(int? Id)
        {
            return GetHelpQueryable().Where(x => x.Id == Id).SingleOrDefault();
        }

        public ResultNotification<string> Create(HelpViewModel model)
        {
            var result = new ResultNotification<string>();

            try
            {
                var help = new Models.Helps.HelpPage();
                help.Title = model.Title;
                help.FriendlyUrl = model.FriendlyUrl;
                help.Content = model.Content;
                help.Roles = String.Join(",", model.Roles);
                help.Category = model.Category;                
                _dbContext.Helps.Add(help);
                _dbContext.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        public ResultNotification<string> Edit(HelpViewModel model)
        {
            var result = new ResultNotification<string>();
            try
            {
                var help = _dbContext.Helps.Where(x => x.Id == model.Id).Single();
                help.Title = model.Title;
                help.FriendlyUrl = model.FriendlyUrl;
                help.Content = model.Content;
                help.Roles = String.Join(",", model.Roles);
                help.Category = model.Category;
                _dbContext.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }
        public ResultNotification<string> Delete(int id)
        {
            var result = new ResultNotification<string>();
            try
            {
                var model = _dbContext.Helps.Single(x => x.Id == id);

                _dbContext.Helps.Remove(model);

                _dbContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return result;
            }
        }
        public HelpPage GetHelp(string pageUrl)
        {
            var result = _dbContext.Helps.Where(x => x.FriendlyUrl.Contains(pageUrl)).SingleOrDefault();

            return result;
        }

        public HelpViewModel CheckUrl(string friendlyUrl)
        {
            var result = _dbContext.Database.SqlQuery<HelpViewModel>($"SELECT * FROM Portal_HelpPage WHERE FriendlyUrl = '{friendlyUrl}'").SingleOrDefault();
            return result;
        }

        public ViewHelpPage CanViewHelpPage(string pageUrl, string userId)
        {
            var vm = new ViewHelpPage();

            var userervice = new UserService();

            var user = userervice.GetUserId(userId);

            var result = _dbContext.Helps.Where(x => x.FriendlyUrl.Contains(pageUrl)).SingleOrDefault();

            var roles = _roleService.GetAssignedRoles(user.Id);

            vm.Roles = roles;

            if (result != null)
            {
                var helpRoles = result.Roles.Split(',').ToList();

                foreach (var role in roles)
                {
                    var hasRole = helpRoles.Where(x => x == role.Role_Id).Any();

                    if (hasRole) {
                        vm.CanView = true;
                        return vm;
                    }
                }
            }

            return vm;
        }
    }
}
