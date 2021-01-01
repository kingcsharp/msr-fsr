namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    // read-only view for the procedure grid
    public partial class ProcedureWithUsedProductCount: TrackableEntity
    {
        public string Name { get; protected set; }
        public int ProcedureTypeId { get; protected set; }
        public int Revision { get; protected set; }
        public double Duration { get; protected set; }
        public string DurationType { get; protected set; }
        public virtual ProcedureType ProcedureType { get; protected set; }
        public int CountProductsUsing { get; set; }
    }
}
