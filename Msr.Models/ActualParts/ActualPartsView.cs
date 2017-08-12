using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ActualParts
{
    public class ActualPartsView
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string SysName { get; set; }
        public string Serial { get; set; }
        public string ParentName { get; set; }
        public string Location { get; set; }
        public string ObjectId { get; set; }
        public byte? Mergable { get; set; }
        public string ParentId { get; set; }
        public string PartId { get; set; }
        public double? Qty { get; set; }
        public string CurOwner { get; set; }
        public double? AssemblyWT { get; set; }
        public string ApStatus { get; set; }
        public string RootId { get; set; }
        public string RootStatus { get; set; }
        public string PartType { get; set; }
        public string PartTypeName { get; set; }
        public string PartDesc { get; set; }
        public string Unit { get; set; }
        public Int16? SupplierSeeInstallBase { get; set; }
        public Int16? SupplierSeeAvaliability { get; set; }
        public Int16? CustomerSeeAvailability { get; set; }
        public string LocationObjectId { get; set; }
        public string CompanyPartNumber { get; set; }
        public string LockedBy { get; set; }
        public string UnlockedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Root { get; set; }
        public string RevInfo { get; set; }
        public string Creating { get; set; }
        public string Status { get; set; }
        public int? Rev { get; set; }
        public string WfsId { get; set; }
        public string LockedByName { get; set; }
        public string CreatingCoName { get; set; }
        public DateTime? Drcm { get; set; }
        public string ModBy { get; set; }
        public string ApprovalActivity { get; set; }
        public string ApprovalDate { get; set; }
        public string CurrentOwnerName { get; set; }
        public int? HasChild { get; set; }
        public string ResponsibleName { get; set; }
        public string RespPersonFullName { get; set; }
        public string LocationName { get; set; }
        public string ObjId { get; set; }
    }
}
