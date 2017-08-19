using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.People
{
    public class PeopleObjectView
    {
        public string Id { get; set; }
        public string ObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PositionName { get; set; }
        public string BossName { get; set; }
        public string CompanyName { get; set; }
        public string LocationName { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public string WorkEmailAddress { get; set; }
        public string SystemStatus { get; set; }
        public string DateHired { get; set; }
        public string Status { get; set; }
        public int? Rev { get; set; }
        public string PicRecord { get; set; }
        public string RootCoName { get; set; }
        public string ScreenType { get; set; }
        public string LanguageId { get; set; }
        public Int16? IsHead { get; set; }
        public string TimeZone { get; set; }
        public string LoginId { get; set; }
        public string Company { get; set; }
        public string LockedByName { get; set; }
    }
}