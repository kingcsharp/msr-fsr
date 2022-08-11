using System.Collections.Generic;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Models
{
    public class WorkOrderPartModel
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int PartId { get; set; }
        public int CycleCount { get; set; }
        public int? ParentId { get; set; }
        public string SerialNumber { get; set; }
        public string CustomerLineNumber { get; set; }
        public int? Qty { get; set; }
        public EnumSegregationType? SegregationType { get; set; }
        public virtual PartModel Part { get; set; }
        public virtual WorkOrderModel WorkOrder { get; set; }
        public virtual ICollection<WorkOrderPartModel> Children { get; set; }
        public virtual WorkOrderPartModel Parent { get; set; }
        public List<NCRHistoryItemModel> NCRHistoryItems { get; set; }
        public string TagType { get; set; }
        public string NCNumber { get; set; }
        public byte[] DataMatrix { get; set; }
        public string Detail { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string PartData { get; set; }

        public WorkOrderPartModel Clone()
        {
            return new WorkOrderPartModel()
            {
                Id = this.Id,
                WorkOrderId = this.WorkOrderId,
                PartId = this.PartId,
                CycleCount = this.CycleCount,
                ParentId = this.ParentId,
                SerialNumber = this.SerialNumber,
                CustomerLineNumber = this.CustomerLineNumber,
                Qty = this.Qty,
                SegregationType = this.SegregationType,
                Part = this.Part,
                WorkOrder = this.WorkOrder,
                Children = this.Children,
                Parent = this.Parent,
                NCRHistoryItems = this.NCRHistoryItems,
                TagType = this.TagType,
                NCNumber = this.NCNumber,
                DataMatrix = this.DataMatrix,
                Detail = this.Detail,
                PartNumber = this.PartNumber,
                Name = this.Name,
                PartData = this.PartData
            };
        }
    }
}
