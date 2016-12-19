using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xenos.Configuration
{
    /// <summary>
    /// Build context for binding the serializer configuration to a serializer context.
    /// </summary>
    internal class ConfigurationBindingContext
    {
        /// <summary>
        /// Xml configuration overrides.
        /// </summary>
        internal XmlAttributeOverrides Overrides { get; set; }

        /// <summary>
        /// Type of entity that is being configured in the serializer context.
        /// </summary>
        internal Type EntityType { get; set; }

        /// <summary>
        /// Proxy types used within the serializer context.
        /// </summary>
        internal IDictionary<Type, Type> ProxyTypes { get; set; }
    }
}
