using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Parts
{
    public class PartsWithTaskAndResult
    {
        public string APPROVED_ID { get; set; }
        public string LOCKED_BY { get; set; }
        public string UNLOCKED_BY { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string ROOT { get; set; }
        public string REV_INFO { get; set; }
        public string CREATING_CO { get; set; }
        public string STATUS { get; set; }
        public int REV { get; set; }
        public string WFS_ID { get; set; }
        public string LOCKED_BY_NAME { get; set; }
        public string CREATING_CO_NAME { get; set; }
        public string APPROVAL_ACTIVITY { get; set; }
        public string OBJ_ID { get; set; }
        public string ID { get; set; }
        public string UNIT { get; set; }
        public string NAME { get; set; }
        public string PART_TYPE { get; set; }
        public string TRACK_FROM_START { get; set; }
        public string COMPANY { get; set; }
        public decimal? UNIT_SHIPPING_WEIGHT { get; set; }
        public string COMPANY_PART_NUMBER { get; set; }
        public Int16? SUPPLIER_SEE_INSTALL_BASE { get; set; }
        public Int16? SUPPLIER_SEE_AVAILABILITY { get; set; }
        public Int16? CUSTOMER_SEE_AVAILABILITY { get; set; }
        public string COMPANY_NAME { get; set; }
        public string PART_TYPE_NAME { get; set; }
        public string SPARE { get; set; }
        public string CONSUMABLE { get; set; }
        public string WEIGHT_TYPE { get; set; }
        public string ROOT_CO_NAME { get; set; }
        public string WEIGHT_TYPE_NAME { get; set; }
    }
}
