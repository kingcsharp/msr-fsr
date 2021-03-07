namespace MSR.Answer.API.V1.Models
{
    public class GetHelpPageRequest:BaseApiModel
    {
        public int? Id { get; set; }
        public string FriendlyURL { get; set; }
        public string Title { get; set; }
        public string[]? Roles { get; set; }
    }
}
