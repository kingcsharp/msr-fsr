
using System;

namespace Msr.Services.Orders.Messaging
{
    public class NcrDetails
    {
        public string SupName { get; set; }
        public string CustName { get; set; }
        public string FillObjDesc { get; set; }
        public string AppObjDesc { get; set; }
        public string PurchItemId { get; set; }
        public string PartNumber { get; set; }
        public string PartDesc { get; set; }
        public string NickName { get; set; }
        public string FillObjId { get; set; }
        public string Technician { get; set; }
        public string Serial { get; set; }
        public string Comments { get; set; }
        public DateTime? DateComplete { get; set; }
        
    }
}
