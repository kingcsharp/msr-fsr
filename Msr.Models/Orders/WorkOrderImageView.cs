using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Orders
{
    public class WorkOrderImageView
    {
        public string ACTUAL_PART_ID { get; set; }
        public string LOCATION_NAME { get; set; }
        public string COMPANY_PART_NUMBER { get; set; }
        public string NICK_NAME { get; set; }
        public double QTY { get; set; }
        public string SERIAL { get; set; }
        public string CURRENT_OWNER_NAME { get; set; }
        public string PART_DESC { get; set; }
        public string NAME { get; set; }
        public string FILE_LINK_ID { get; set; }
        public string FILE_ID { get; set; }
        public string STATUS { get; set; }
        public DateTime? DATE_DELETED { get; set; }
        public string DELETED_BY { get; set; }
        public string FILE_NAME { get; set; }
        public string FILE_DESCRIPTION { get; set; }
        public string DELETED_BY_NAME { get; set; }
        public string CREATOR_ID { get; set; }
    }
}
