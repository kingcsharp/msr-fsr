using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Msr.Services.Procedures.ViewModels;

namespace Msr.Services.Procedures.Messages
{
    public class GetStepDataResult
    {
        public GetStepDataResult()
        {
            GetStepLaborsList = new List<SelectListItem>();
            GetStepLabors = new List<SelectListItem>();
            AddMonitorForProcedureViewModel = new AddMonitorForProcedureViewModel();
            GetMoniterViewModels = new List<GetMoniterViewModel>();
        }
        private string DurationType;

        public string Id { get; set; }
        [AllowHtml]
        public string Step_Text { get; set; }
        public string StepTitle {
            get
            {
                try
                {
                    return Step_Text.Substring(Step_Text.IndexOf("<h4>"), Step_Text.IndexOf("</h4>")).Replace("<h4>","").Replace("</h4>","");
                }
                catch
                {
                    return "";
                }
            }
        }
        public double? Duration { get; set; }
        public string Duration_Type
        {
            get { return DurationType; }
            set
            {
                SetDurationType(value);
            }
        }

        private void SetDurationType(string value)
        {
            switch (value)
            {
                case "TIME_SYS_HOURS":
                {
                    DurationType = "Hours";
                    break;
                }
                default:
                {
                    DurationType = String.Empty;
                    break;
                }
            }
        }

        public double? Print_Order { get; set; }
        public string Pre_Step { get; set; }

        public AddMonitorForProcedureViewModel AddMonitorForProcedureViewModel { get; set; }
        public List<GetStepDataResult> GetStepDataResults { get; set; }
        public IList<SelectListItem> GetStepLabors { get; set; }
        public IList<SelectListItem> GetStepLaborsList { get; set; }
        public List<GetMoniterViewModel> GetMoniterViewModels { get; set; }


    }
}
