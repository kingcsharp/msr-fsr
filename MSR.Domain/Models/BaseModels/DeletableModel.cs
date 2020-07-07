namespace MSR.Domain.Models.BaseModels
{
    public class DeletableModel : TrackableModel
    {
        public bool IsActive { get; set; }
    }
}
