using System.Reflection;

namespace Xenos.Configuration
{
    public abstract class XmlPropertyConfiguration
    {
        protected readonly PropertyInfo Property;

        internal XmlPropertyConfiguration(PropertyInfo property)
        {
            this.Property = property;
        }

        internal abstract void BuildConfiguration(ConfigurationBuildContext buildContext);
        internal abstract void BindConfiguration(ConfigurationBindingContext bindingContext);
    }
}
