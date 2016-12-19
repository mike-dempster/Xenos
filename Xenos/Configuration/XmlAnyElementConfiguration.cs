using System.Reflection;
using System.Xml.Serialization;

namespace Xenos.Configuration
{
    public class XmlAnyElementConfiguration : XmlPropertyConfiguration
    {
        internal XmlAnyElementConfiguration(PropertyInfo property) : base(property)
        {
        }

        internal override void BuildConfiguration(ConfigurationBuildContext buildContext)
        {
        }

        internal override void BindConfiguration(ConfigurationBindingContext bindingContext)
        {
            var attributes = new XmlAttributes
            {
                XmlAnyElements =
                {
                    new XmlAnyElementAttribute()
                }
            };
            bindingContext.Overrides.Add(bindingContext.EntityType, this.Property.Name, attributes);
        }
    }
}
