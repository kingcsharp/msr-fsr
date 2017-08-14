using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Models.People
{
    public class LanguagesView
    {
        [Key]
        public string LanguageCode { get; set; }
        public string Language { get; set; }
    }
}
