using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zheng.Utilities.Annotations
{
    public static class TypeExtension
    {
        public static T GetAttributeFrom<T>(this Type instance, string propertyName) where T : Attribute
        {
            var attributeType = typeof(T);
            var property = instance.GetProperty(propertyName);
            if (property == null) return default(T);
            return (T)property.GetCustomAttributes(attributeType, false).FirstOrDefault();
        }

        public static IEnumerable<T> GetAttributeFrom<T>(this Type instance) where T : Attribute
        {
            var attributeType = typeof(T);
            var properties = instance.GetProperties();
            if (properties != null && properties.Length == 0) return default(IEnumerable<T>);
            return properties?.Select(property =>
                (T)property.GetCustomAttributes(attributeType, false).FirstOrDefault());
        }
    }
}
