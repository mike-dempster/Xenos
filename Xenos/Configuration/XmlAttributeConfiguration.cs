using System;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Xenos.Proxy;

namespace Xenos.Configuration
{
    public class XmlAttributeConfiguration : XmlPropertyConfiguration
    {
        private readonly Type _proxyPropertyType;
        private readonly MethodInfo _proxyToBaseConversionDelegate;
        private readonly MethodInfo _baseToProxyConversionDelegate;

        private string attributeName;
        private string attributeNamespace;

        internal XmlAttributeConfiguration(PropertyInfo property): base(property)
        {
            this.attributeName = property.Name;
        }

        internal XmlAttributeConfiguration(
            PropertyInfo property,
            Type proxyPropertyType,
            MethodInfo proxyToBaseConversionExpression,
            MethodInfo baseToProxyConversionExpression)
            : base(property)
        {
            this._proxyPropertyType = proxyPropertyType;
            this._proxyToBaseConversionDelegate = proxyToBaseConversionExpression;
            this._baseToProxyConversionDelegate = baseToProxyConversionExpression;
        }

        public XmlAttributeConfiguration WithName(string name)
        {
            this.attributeName = name;

            return this;
        }

        public XmlAttributeConfiguration WithNamespace(string ns)
        {
            this.attributeNamespace = ns;

            return this;
        }

        internal override void BuildConfiguration(ConfigurationBuildContext buildContext)
        {
            if ((null == this._baseToProxyConversionDelegate) && (null == this._proxyToBaseConversionDelegate))
            {
                return;
            }

            var proxyProperty = new ProxyPropertyInfo
            {
                Attributes = this.Property.GetCustomAttributes().ToList(),
                PropertyName = this.Property.Name,
                BasePropertyInfo = this.Property,
                ConversionToProxyDelegate = this._baseToProxyConversionDelegate,
                ConversionFromProxyDelegate = this._proxyToBaseConversionDelegate,
                ExcludeFromProxy = false,
                ProxyPropertyType = this._proxyPropertyType
            };
            buildContext.ProxyProperties.Add(proxyProperty);
        }

        internal override void BindConfiguration(ConfigurationBindingContext bindingContext)
        {
            var attributes = new XmlAttributes
            {
                XmlAttribute = new XmlAttributeAttribute()
            };

            if (false == string.IsNullOrWhiteSpace(this.attributeName))
            {
                attributes.XmlAttribute.AttributeName = this.attributeName;
            }

            if (false == string.IsNullOrWhiteSpace(this.attributeNamespace))
            {
                attributes.XmlAttribute.Namespace = this.attributeNamespace;
            }

            bindingContext.Overrides.Add(bindingContext.EntityType, this.Property.Name, attributes);
        }
    }
}
