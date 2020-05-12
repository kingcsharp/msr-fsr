using System;
using System.ComponentModel.DataAnnotations;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public partial class Sensors_Chandler_Values
    {
        [Key]
        [StringLength(255)]
        public string ItemID { get; set; }

        [StringLength(2000)]
        public string ItemCurrentValue { get; set; }

        public DateTime? ItemTimeStamp { get; set; }

        [StringLength(255)]
        public string ItemQuality { get; set; }

        [StringLength(100)]
        public string ServerProgId { get; set; }

        [StringLength(100)]
        public string ServerAddress { get; set; }

        [StringLength(100)]
        public string GroupName { get; set; }

        [StringLength(50)]
        public string ReadMode { get; set; }

        [StringLength(100)]
        public string ItemDataType { get; set; }

        [StringLength(50)]
        public string ItemAccessRights { get; set; }
    }
}
