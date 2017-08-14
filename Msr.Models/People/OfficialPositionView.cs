using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Models.People
{
    public class OfficialPositionView
    {
        [Key]
        public string Id { get; set; }
        public string CreatingCo { get; set; }
        public string Name { get; set; }
        public string HistoryRefId { get; set; }
    }
}
