using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Xenos.Conversion;

namespace Xenos.Configuration
{
    /// <summary>
    /// Extension methods for configuring properties on an entity for the serializer.
    /// </summary>
    [SuppressMessage("ReSharper", "SpecifyACultureInStringConversionExplicitly")]
    public static class PropertyConfigurationExtensions
    {
        /// <summary>
        /// Configures a property with the <see cref="System.Xml.Serialization.XmlAnyAttributeAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="T">Type of the entity that declares the property</typeparam>
        /// <typeparam name="TArrayItem">Type of the items in the array.</typeparam>
        /// <param name="configuration">An instance of a complex type configuration to configure the property in.</param>
        /// <param name="property">Property to configure with the <see cref="System.Xml.Serialization.XmlAnyAttributeAttribute"/> attribute.</param>
        /// <returns>Instance of an Any attribute configuration.</returns>
        public static XmlAnyAttributeConfiguration HasAnyAttribute<T, TArrayItem>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, IEnumerable<TArrayItem>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var anyAttributeConfig = new XmlAnyAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(anyAttributeConfig);

            return anyAttributeConfig;
        }

        public static XmlAnyElementConfiguration HasAnyElement<T, TArrayItem>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, IEnumerable<TArrayItem>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var anyElementConfig = new XmlAnyElementConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(anyElementConfig);

            return anyElementConfig;
        }

        public static XmlArrayConfiguration HasArray<T, TArrayItem>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, IEnumerable<TArrayItem>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var arrayConfig = new XmlArrayConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(arrayConfig);

            return arrayConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, bool>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<bool>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<bool>, string>(StringConversions.ConvertBoolToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<bool>>(StringConversions.ConvertStringToBool);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, byte>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<byte>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<byte>, string>(StringConversions.ConvertByteToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<byte>>(StringConversions.ConvertStringToByte);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, char>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<char>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<char>, string>(StringConversions.ConvertCharToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<char>>(StringConversions.ConvertStringToChar);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, short>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<short>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<short>, string>(StringConversions.ConvertShortToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<short>>(StringConversions.ConvertStringToShort);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, ushort>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<ushort>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<ushort>, string>(StringConversions.ConvertUShortToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<ushort>>(StringConversions.ConvertStringToUShort);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, int>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<int>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<int>, string>(StringConversions.ConvertIntToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<int>>(StringConversions.ConvertStringToInt);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, long>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<long>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<long>, string>(StringConversions.ConvertLongToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<long>>(StringConversions.ConvertStringToLong);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, ulong>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<ulong>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<ulong>, string>(StringConversions.ConvertULongToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<ulong>>(StringConversions.ConvertStringToULong);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, float>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<float>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<float>, string>(StringConversions.ConvertFloatToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<float>>(StringConversions.ConvertStringToFloat);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, double>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<double>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<double>, string>(StringConversions.ConvertDoubleToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<double>>(StringConversions.ConvertStringToDouble);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, decimal>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<decimal>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<decimal>, string>(StringConversions.ConvertDecimalToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<decimal>>(StringConversions.ConvertStringToDecimal);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, string>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, DateTime>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlAttributeConfiguration HasAttribute<T>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, Nullable<DateTime>>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var convertToString = ReflectionHelpers.GetDelegateMethodInfo<Nullable<DateTime>, string>(StringConversions.ConvertDateTimeToString);
            var convertFromString = ReflectionHelpers.GetDelegateMethodInfo<string, Nullable<DateTime>>(StringConversions.ConvertStringToDateTime);
            var attributeConfig = new XmlAttributeConfiguration(propertyInfo, typeof(string), convertFromString, convertToString);
            configuration.PropertyConfigurations.Add(attributeConfig);

            return attributeConfig;
        }

        public static XmlElementConfiguration HasElement<T, TElement>(this XmlComplexTypeConfiguration<T> configuration, Expression<Func<T, TElement>> property)
            where T : class
        {
            var propertyInfo = ReflectionHelpers.GetPropertyInfo(property);
            var elementConfig = new XmlElementConfiguration(propertyInfo);
            configuration.PropertyConfigurations.Add(elementConfig);

            return elementConfig;
        }
    }
}
