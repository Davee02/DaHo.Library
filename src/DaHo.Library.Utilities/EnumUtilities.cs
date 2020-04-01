using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DaHo.Library.Utilities
{
    public static class EnumUtilities
    {
        public static IEnumerable<T> GetEnumValues<T>() where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static T GetAttributeFromEnum<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);

            return attributes.Length > 0 ?
                (T)attributes[0]
                : null;
        }

        public static T GetEnumValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (string.Equals(attribute.Description, description, StringComparison.OrdinalIgnoreCase))
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (string.Equals(field.Name, description, StringComparison.OrdinalIgnoreCase))
                        return (T)field.GetValue(null);
                }
            }

            return default;
        }
    }
}
