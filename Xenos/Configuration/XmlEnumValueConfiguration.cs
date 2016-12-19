using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Xenos.Proxy;

namespace Xenos.Configuration
{
    public class XmlEnumValueConfiguration : XmlPropertyConfiguration
    {
        private readonly MethodInfo _proxyToBaseConversionDelegate;
        private readonly MethodInfo _baseToProxyConversionDelegate;

        private string valueName;

        internal XmlEnumValueConfiguration(PropertyInfo property) : base(property)
        {
        }

        internal XmlEnumValueConfiguration(PropertyInfo property, MethodInfo baseToProxyConversionDelegate, MethodInfo proxyToBaseConversionDelegate) : base(property)
        {
            this._baseToProxyConversionDelegate = baseToProxyConversionDelegate;
            this._proxyToBaseConversionDelegate = proxyToBaseConversionDelegate;
        }

        public void HasName(string name)
        {
            this.valueName = name;
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
                ExcludeFromProxy = false
            };
            buildContext.ProxyProperties.Add(proxyProperty);
        }

        internal override void BindConfiguration(ConfigurationBindingContext bindingContext)
        {
            var attributes = new XmlAttributes
            {
                XmlEnum = new XmlEnumAttribute()
            };

            if (false == string.IsNullOrWhiteSpace(this.valueName))
            {
                attributes.XmlEnum.Name = this.valueName;
            }

            bindingContext.Overrides.Add(bindingContext.EntityType, this.Property.Name, attributes);
        }
    }
}
