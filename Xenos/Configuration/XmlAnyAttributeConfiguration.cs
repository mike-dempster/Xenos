using System.Reflection;
using System.Xml.Serialization;

namespace Xenos.Configuration
{
    public class XmlAnyAttributeConfiguration : XmlPropertyConfiguration
    {
        internal XmlAnyAttributeConfiguration(PropertyInfo property) : base(property)
        {
        }

        internal override void BuildConfiguration(ConfigurationBuildContext buildContext)
        {
        }

        internal override void BindConfiguration(ConfigurationBindingContext bindingContext)
        {
            var attributes = new XmlAttributes
            {
                XmlAnyAttribute = new XmlAnyAttributeAttribute()
            };
            bindingContext.Overrides.Add(bindingContext.EntityType, this.Property.Name, attributes);
        }
    }
}
