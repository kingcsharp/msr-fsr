using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    public class WorkOrderPartRequest
    {
        /// <summary>
        ///
        /// </summary>
        public int WorkOrderId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int PartId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual UpdatePartRequest Part { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual UpdateWorkOrderRequest WorkOrder { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual ICollection<WorkOrderPartRequest> Children { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual WorkOrderPartRequest Parent { get; set; }
    }
}
