using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Xenos.Configuration
{
    /// <summary>
    /// Configures a property on an entity to be serialized as an Xml element.
    /// </summary>
    public class XmlElementConfiguration : XmlPropertyConfiguration
    {
        private string elementName;
        private string elementNamespace;

        internal XmlElementConfiguration(PropertyInfo property) : base(property)
        {
            this.elementName = property.Name;
        }

        public XmlElementConfiguration WithElementName(string name)
        {
            this.elementName = name;

            return this;
        }

        public XmlElementConfiguration WithElementNamespace(string ns)
        {
            this.elementNamespace = ns;

            return this;
        }

        internal override void BuildConfiguration(ConfigurationBuildContext buildContext)
        {
        }

        internal override void BindConfiguration(ConfigurationBindingContext bindingContext)
        {
            var elementAttribute = new XmlElementAttribute();

            if (false == string.IsNullOrWhiteSpace(this.elementName))
            {
                elementAttribute.ElementName = this.elementName;
            }

            if (false == string.IsNullOrWhiteSpace(this.elementNamespace))
            {
                elementAttribute.Namespace = this.elementNamespace;
            }

            var propertyType = this.Property.PropertyType;

            if (this.Property.PropertyType.IsArray)
            {
                propertyType = this.Property.PropertyType.GetElementType();
            }

            Type proxyType;

            if (bindingContext.ProxyTypes.TryGetValue(propertyType, out proxyType))
            {
                elementAttribute.Type = proxyType;
            }

            var attributes = new XmlAttributes();
            attributes.XmlElements.Add(elementAttribute);
            bindingContext.Overrides.Add(bindingContext.EntityType, this.Property.Name, attributes);
        }
    }
}
