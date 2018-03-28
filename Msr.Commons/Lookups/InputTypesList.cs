using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> InputTypesList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Manual", Value = "MANUAL"},
                new SelectListItem {Text = "QR Code", Value = "QR_CODE"},
                new SelectListItem {Text = "Sensor", Value = "SENSOR"}
            };
        }
    }
}
