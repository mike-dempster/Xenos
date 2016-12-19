using System;
using System.Linq.Expressions;
using Xenos.Conversion;

namespace Xenos.Configuration
{
    public class XmlEnumConfiguration<TEnum> : XmlEntityConfiguration
        where TEnum : struct
    {
        internal XmlEnumConfiguration(Type contextType) : base(typeof(TEnum), contextType)
        {
            if (false == typeof(TEnum).IsEnum)
            {
                throw new Exception($"{typeof(XmlEnumConfiguration<>).Name} can only be used with an enum type. The given type '{typeof(TEnum).AssemblyQualifiedName}' is not an enum type.");
            }
        }

        public XmlEnumValueConfiguration HasValue<TValue>(Expression<Func<TEnum, TValue>> property)
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var enumValueConfig = new XmlEnumValueConfiguration(propertyInfo);
            this.PropertyConfigurations.Add(enumValueConfig);

            return enumValueConfig;
        }

        public XmlEnumValueConfiguration HasValue<TValue>(Expression<Func<TEnum, Nullable<TValue>>> property)
            where TValue : struct
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<TEnum>, string>(StringConversions.ConvertEnumToString<TEnum>);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<TEnum>>(StringConversions.ConvertStringToEnum<TEnum>);
            var enumValueConfig = new XmlEnumValueConfiguration(propertyInfo, convertFromString, convertToString);
            this.PropertyConfigurations.Add(enumValueConfig);

            return enumValueConfig;
        }
    }
}
