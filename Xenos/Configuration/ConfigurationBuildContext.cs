using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xenos.Proxy;

namespace Xenos.Configuration
{
    /// <summary>
    /// Build context for configuring an entity to be bound to a serializer context.
    /// </summary>
    internal class ConfigurationBuildContext
    {
        /// <summary>
        /// Collection of properties that require a proxy between the serializer and the property itself.
        /// </summary>
        internal ICollection<ProxyPropertyInfo> ProxyProperties { get; }

        /// <summary>
        /// Initializes the state of a new instance of the build context.
        /// </summary>
        public ConfigurationBuildContext()
        {
            this.ProxyProperties = new Collection<ProxyPropertyInfo>();
        }
    }
}
