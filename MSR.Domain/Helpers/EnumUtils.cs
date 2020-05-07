using System;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Helpers
{
    public static class EnumUtils
    {
        public static EnumMenuItem ParseMenuType(string name)
        {
            var strippedName = name.Replace(" ", string.Empty).Replace("/", string.Empty);
            foreach (EnumMenuItem enumContentType in Enum.GetValues(typeof(EnumMenuItem)))
            {
                if (enumContentType.ToString().Equals(strippedName))
                {
                    return enumContentType;
                }
            }

            throw new ArgumentException("Unknown content type: " + strippedName);
        }

    }
}
