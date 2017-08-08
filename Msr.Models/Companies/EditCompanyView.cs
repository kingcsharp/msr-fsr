using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Companies
{
    public class EditCompanyView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CoType { get; set; }
        public string Parent { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string LocationName { get; set; }
        public string ParentName { get; set; }
        public string ModBy { get; set; }
        public string ObjectId { get; set; }
        public string RootCoID { get; set; }
        public string NTLogin { get; set; }
        public string ReferenceFile { get; set; }
        public string PictureFile { get; set; }
        public string LogoFile { get; set; }
        public string HeadPeople { get; set; }
    }
}
