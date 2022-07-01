using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table("WorkOrderPartNCRMap")]
    public class WorkOrderPartNCRMapItem: TrackableEntity
    {
        public int WorkOrderTaskId { get; set; }
        public int WorkOrderPartId { get; set; }
        public string TagType { get; set; }
        public string Detail { get; set; }
    }
}
