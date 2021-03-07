using System;
using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class GetUsersRequest: BaseApiModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public int? SupervisorName { get; set; }
        public string PrimaryPhone { get; set; }
        public string Email { get; set; }
        public List<int>? Roles { get; set; }
        public bool? IsActive { get;set;}
        public bool? IsAnswerUser { get;set;}
        public DateTime? CreatedOn { get;set;}
        public string LocationName { get;set;}

    }
}
