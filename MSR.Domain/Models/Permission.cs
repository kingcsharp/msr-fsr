namespace MSR.Domain.Models
{
    public class Permission
    {
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanActivate { get; set; }
        public bool CanApprove { get; set; }
        public bool CanDelete { get; set; }
    }
}
