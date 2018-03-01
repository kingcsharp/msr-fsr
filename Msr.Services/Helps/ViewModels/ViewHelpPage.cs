using Msr.Services.Roles.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Helps.ViewModels
{
    public class ViewHelpPage
    {
        public bool CanView { get; set; }

        public List<GetMyRolesResult> Roles { get; set; }
    }
}
