using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Administration;

namespace Msr.Services.Administration.ViewModels
{
    public class ReAssignBossView
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string ToBossId { get; set; }

        public string NTLogin { get; set; }

    }
}
