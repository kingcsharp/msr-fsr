using System;

namespace Msr.Models.Companies
{
    public class PersonRootCompanyTreeView
    {
        public Int32 Tree_Level { set; get; }
        public Int16 Tree_Has_Child { set; get; }
        public Int16 Expanded { set; get; }
        public string Id { set; get; }
        public string History_Ref_Id { set; get; }
        public string Name { set; get; }
        public string Co_Type { set; get; }
        public string Parent { set; get; }
        public string Phone { set; get; }
        public string Location { set; get; }
        public string Location_Name { set; get; }
        public DateTime? Drcm { set; get; }
        public string ModBy { set; get; }
        public string Object_Id { set; get; }
        public string Root_Co { set; get; }
        public string Status { set; get; }
        public string Top_Company { set; get; }
        public string Parent_Name { set; get; }
    }
}
