using System;

namespace Xenos.Configuration
{
    public class XmlComplexTypeConfiguration<T> : XmlEntityConfiguration
        where T : class
    {
        internal XmlComplexTypeConfiguration(Type contextType) : base(typeof(T), contextType)
        {
        }
    }
}
