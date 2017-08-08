using System;

namespace Msr.Models.Companies
{
    public class CompanyView
    {
        public string Id { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string CoType { get; set; }

        public string ObjectId { get; set; }

        public string Status { get; set; }

        public string LockedBy { get; set; }

        public string UnlockedBy { get; set; }

        public string CreatedBy { get; set; }

        public string CreatingCo { get; set; }

        public int? Rev { get; set; }

        public string WfsId { get; set; }

        public string LockedByName { get; set; }

        public string Root { get; set; }

        public Int16? ChildrenCount { get; set; }

        public string TopCompany { get; set; }

        public string PicRecord { get; set; }

        public string RootCoName { get; set; }

        public string Parent { get; set; }

        public string ParentName { get; set; }

        public string Phone { get; set; }

        public string Location { get; set; }

        public string LocationName { get; set; }
    }
}
