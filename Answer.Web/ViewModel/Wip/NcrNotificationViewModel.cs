using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Answer.Web.ViewModel.Wip
{
    public class NcrNotificationViewModel
    {
        [DisplayName("Client\\Customer Emails")]
        public List<SelectListItem> ClientEmails { get; set; } = new List<SelectListItem>();
            
        public string FillId { get; set; }

        public string StepId { get; set; }

        public string PhStepId { get; set; }

        [DisplayName("Email Recipient")]
        public string EmailDestination { get; set; }

    }
}