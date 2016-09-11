using System.Web.Mvc;

namespace Msr.Services.jqGrid
{
    internal static class DatatableModelBindingContextHelper
    {
        public static T GetValue<T>(this ModelBindingContext bindingContext, string key)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(key);

            if (valueResult == null)
            {
                return default(T);
            }

            return (T) valueResult.ConvertTo(typeof (T));
        }
    }
}