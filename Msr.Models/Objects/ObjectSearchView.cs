using System;

namespace Msr.Models.Objects
{
    public class ObjectSearchView
    {
        public Guid Id { get; set; }
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemType { get; set; }
        public DateTime? Date { get; set; }
    }
}
