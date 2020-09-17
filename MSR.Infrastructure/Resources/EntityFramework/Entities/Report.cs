using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class Report: Entity
    {
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string APIEndPointURL { get; set; }
        public string ImageURL { get; set; }

        public virtual ICollection<ReportCategoryMap> ReportCategories { get; set; }
    }
}
