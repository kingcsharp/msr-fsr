using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Helps
{
   public class HelpView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FriendlyUrl { get; set; }
       
        public string Content { get; set; }
        public string Roles { get; set; }
        public string RoleName { get; set; }
        public string Category { get; set; }
    }
}
