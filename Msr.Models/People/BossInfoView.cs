using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Models.People
{
    public class BossInfoView
    {
        [Key]
        public string Id { get; set; }
        public string RootCompany { get; set; }
        public string FullName { get; set; }
    }
}
