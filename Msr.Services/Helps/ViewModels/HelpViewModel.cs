using Msr.Models.Helps;
using Msr.Services.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Msr.Services.Helps.ViewModels
{
   public class HelpViewModel
    {
        public HelpViewModel()
        {
            Roles = new List<string>();
            RolesList = new List<SelectListItem>();     
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FriendlyUrl { get; set; }

        [AllowHtml]
        public string Content { get; set; }
        [Required]
        public List<string> Roles { get; set; }

        public List<SelectListItem> RolesList { get; set; }

        public void SetUp(RoleService roleService)
        {
            RolesList = roleService.GetActiveRoles().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
        }

        public HelpViewModel MapToDto(HelpView model)
        {
            return new HelpViewModel
            {
                
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                FriendlyUrl = model.FriendlyUrl,
                Roles = model.Roles.Split(',').ToList()
            };
        }
    }
}
