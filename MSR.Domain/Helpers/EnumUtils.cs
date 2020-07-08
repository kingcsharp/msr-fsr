using System;
using MSR.Domain.Commanding.Enums;
using System.ComponentModel;
using System.Reflection;

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

        public static string GetDescription<T>(T enumValue)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fieldInfo)
            {
                object[] attrs = fieldInfo.GetCustomAttributes
                    (typeof(DescriptionAttribute), true);
                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return Convert.ToString(enumValue);
        }

    }
}
