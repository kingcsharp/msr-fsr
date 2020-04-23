namespace MSR.Answer.API.V1.Models
{
    public class GetUsersRequest
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Supervisor { get; set; }
        public string PrimaryPhone { get; set; }
        public string Email { get; set; }
    }
}
