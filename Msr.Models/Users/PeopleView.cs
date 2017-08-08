using System;

namespace Msr.Models.Users
{
    public class PeopleView
    {
        public string Id { get; set; }
        public string ObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string TimeZone { get; set; }
        public string PrimaryPhone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Status { get; set; }
        public string CompanyName { get; set; }
        public string RoleName { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
