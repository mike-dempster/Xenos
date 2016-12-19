using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Xenos.Configuration
{
    /// <summary>
    /// Configures a property on an entity to be serialized as an Xml array.
    /// </summary>
    public class XmlArrayConfiguration : XmlPropertyConfiguration
    {
        private string elementName;
        private string childElementName;

        internal XmlArrayConfiguration(PropertyInfo property) : base(property)
        {
            this.elementName = property.Name;
            this.childElementName = property.Name;
        }

        public XmlArrayConfiguration WithElementName(string name)
        {
            this.elementName = name;

            return this;
        }

        public XmlArrayConfiguration WithArrayItemName(string name)
        {
            this.childElementName = name;

            return this;
        }

        internal override void BuildConfiguration(ConfigurationBuildContext buildContext)
        {
        }

        internal override void BindConfiguration(ConfigurationBindingContext bindingContext)
        {
            var attributes = new XmlAttributes
            {
                XmlArray = new XmlArrayAttribute()
            };

            if (false == string.IsNullOrWhiteSpace(this.elementName))
            {
                attributes.XmlArray.ElementName = this.elementName;
            }

            if (false == this.Property.PropertyType.IsArray)
            {
                bindingContext.Overrides.Add(bindingContext.EntityType, this.Property.Name, attributes);
                return;
            }

            var elementType = this.Property.PropertyType.GetElementType();
            Type elementProxyType;

            if (bindingContext.ProxyTypes.TryGetValue(elementType, out elementProxyType))
            {
                var itemAttribute = new XmlArrayItemAttribute
                {
                    ElementName = this.childElementName,
                    Type = elementProxyType
                };
                attributes.XmlArrayItems.Add(itemAttribute);
            }

            bindingContext.Overrides.Add(bindingContext.EntityType, this.Property.Name, attributes);
        }
    }
}
