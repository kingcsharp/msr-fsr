using System.Collections.Generic;
using System.Web.Mvc;
using Msr.Services.Procedures.Messages;

namespace Msr.Services.Procedures.ViewModels
{
    public class GetStepEditDataViewModel
    {
        public GetStepEditDataViewModel()
        {
            GetStepListOfOtherSteps = new List<SelectListItem>();
            BaseCounterList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "NO***",
                    Value = "0"
                },
                new SelectListItem()
                {
                    Text = "YES***",
                    Value = "1"
                }
            };

            DurationTypeList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Seconds",
                    Value = "TIME_SYS_SECONDS"
                },
                new SelectListItem()
                {
                    Text = "Minutes",
                    Value = "TIME_SYS_MINUTES"
                },
                new SelectListItem()
                {
                    Text = "Hours",
                    Value = "TIME_SYS_HOURS"
                },
                new SelectListItem()
                {
                    Text = "Days",
                    Value = "TIME_SYS_DAYS"
                },
                new SelectListItem()
                {
                    Text = "Weeks",
                    Value = "TIME_SYS_WEEKS"
                }
            };
        }

        public GetStepEditDataResult GetStepEditData { get; set; }
        public List<SelectListItem> GetStepListOfOtherSteps { get; set; }
        public List<GetStepLaborResult> GetStepLabors { get; set; }
        public List<SelectListItem> BaseCounterList { get; set; }
        public List<SelectListItem> DurationTypeList { get; set; }
    }
}
