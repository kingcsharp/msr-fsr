using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class SearchView
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
