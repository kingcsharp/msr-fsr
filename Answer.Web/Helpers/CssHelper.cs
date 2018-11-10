using Msr.Models.Workflows;

namespace Answer.Web.Helpers
{
    public static class CssHelper
    {
        public static string GetNotificationIconCss(this string itemType)
        {
            if (itemType == TableConstants.PartsHistory)
            {
                return "fa fa-gear icon";
            }

            if (itemType == TableConstants.PartsTypes)
            {
                return "fa fa-list";
            }

            if (itemType == TableConstants.PurchaseOrders)
            {
                return "fa fa-list-ul";
            }

            if (itemType == TableConstants.AccountHistory)
            {
                return "icon fa fa-list-ul";
            }

            if (itemType == TableConstants.ProductHistory)
            {
                return "icon fa fa-paper-plane-o";
            }

            if (itemType == TableConstants.ProcedureHistory)
            {
                return "fa fa-puzzle-piece";
            }

            if (itemType == TableConstants.Roles)
            {
                return "fa fa-group";
            }

            if (itemType == TableConstants.People)
            {
                return "icon fa fa-user";
            }

            if (itemType == TableConstants.Regions)
            {
                return "icon fa fa-globe";
            }

            if (itemType == TableConstants.Locations)
            {
                return "icon fa fa-map";
            }

            if (itemType == TableConstants.Companies)
            {
                return "icon fa fa-building";
            }

            if (itemType == TableConstants.Companies)
            {
                return "icon fa fa-building";
            }

            if (itemType == TableConstants.Documents)
            {
                return "icon fa fa-file-text";
            }

            return "fa-list-ul";
        }
    }
}
