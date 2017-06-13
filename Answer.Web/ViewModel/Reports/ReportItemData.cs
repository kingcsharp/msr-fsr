using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Msr.Web.ViewModel.Reports
{
    public class ReportItemData
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("data")]
        public List<decimal> data { get; set; }
    }
}