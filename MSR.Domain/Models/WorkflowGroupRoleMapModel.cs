namespace MSR.Domain.Models
{
    public class WorkflowGroupRoleMapModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int? WorkflowGroupId { get; set; }
    }
}