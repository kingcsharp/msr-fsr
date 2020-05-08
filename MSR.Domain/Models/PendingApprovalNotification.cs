using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class PendingApprovalNotification
    {
        public int Customers { get; set; }
        public int Locations { get; set; }
        public int Parts { get; set; }
        public int Procedures { get; set; }
        public int ProcedureSteps { get; set; }
        public int ProcedureStepDocuments { get; set; }
        public int ProcedureStepMonitors { get; set; }
        public int PurchaseOrders { get; set; }
        public int PurchaseOrderProducts { get; set; }
        public int Users { get; set; }
        public int UserRoles { get; set; }
    }
}
