using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xenos.Proxy;

namespace Xenos.Configuration
{
    /// <summary>
    /// Manages the configuration of an entity as it is setup for use with the Xml serializer.
    /// </summary>
    public abstract class XmlEntityConfiguration
    {
        private readonly Type _contextType;

        /// <summary>
        /// Type of the entity that is being configured.
        /// </summary>
        internal Type EntityType { get; }

        /// <summary>
        /// Collection of property configurations for the properties of this entity.
        /// </summary>
        internal ICollection<XmlPropertyConfiguration> PropertyConfigurations { get; }

        /// <summary>
        /// Creates a new instance of the entity configuration for the given class type.
        /// </summary>
        /// <param name="entityType">Type of the entity being setup for serialization.</param>
        /// <param name="contextType">Type of the context that is being configured.</param>
        protected XmlEntityConfiguration(Type entityType, Type contextType)
        {
            this.EntityType = entityType;
            this._contextType = contextType;
            this.PropertyConfigurations = new Collection<XmlPropertyConfiguration>();
        }

        /// <summary>
        /// Prepares the entity serialization configuration to be bound to the Xml serializer.
        /// </summary>
        /// <param name="context">Xml serializer context that the entity will be bound to.</param>
        internal void ConfigureEntity(SerializerConfigurationContext context)
        {
            var buildContext = new ConfigurationBuildContext();

            foreach (var p in this.PropertyConfigurations)
            {
                p.BuildConfiguration(buildContext);
            }

            if (false == buildContext.ProxyProperties.Any())
            {
                return;
            }

            var moduleBuilder = ProxyModuleBuilder.GetInstance(this._contextType);
            var proxyImplementer = ProxyTypeImplementer.GetInstance(this.EntityType, moduleBuilder);
            var proxyProperties = this.EntityType.GetProperties() // Get all of the properties from the entity
                                      .Where(e => e.CanRead && e.CanWrite) // That have setters and getters
                                      .Where(e => false == buildContext.ProxyProperties.Any(i => i.PropertyName == e.Name)) // That are not overridden by one of the property configs
                                      .Select(this.CreateProxyProperty) // And build up a proxy property info object for each
                                      .Union(buildContext.ProxyProperties) // Combine these with the overridden proxy properties
                                      .ToArray();

            foreach (var p in proxyProperties)
            {
                proxyImplementer.AddPropertyInfo(p);
            }

            var proxyType = proxyImplementer.BuildProxyType();
            context.ProxyTypes.Add(this.EntityType, proxyType);
        }

        /// <summary>
        /// Binds the entity configuration to the Xml serializer context.
        /// </summary>
        /// <param name="context">Xml serializer context to bind the configured entity to.</param>
        internal void BindEntityToModel(SerializerConfigurationContext context)
        {
            var bindingContext = new ConfigurationBindingContext
            {
                Overrides = context.Attributes,
                ProxyTypes = new ReadOnlyDictionary<Type, Type>(context.ProxyTypes),
                EntityType = this.EntityType
            };

            Type entityProxyType;

            if (context.ProxyTypes.TryGetValue(this.EntityType, out entityProxyType))
            {
                bindingContext.EntityType = entityProxyType;
            }

            foreach (var p in this.PropertyConfigurations)
            {
                p.BindConfiguration(bindingContext);
            }
        }

        /// <summary>
        /// Creates a proxy property info object from a .NET property info descriptor.
        /// </summary>
        /// <param name="property">Descriptor of the property to create the proxy property info for.</param>
        /// <returns>Proxy property info instance for the given .NET property descriptor.</returns>
        private ProxyPropertyInfo CreateProxyProperty(PropertyInfo property)
        {
            return new ProxyPropertyInfo
            {
                PropertyName = property.Name,
                BasePropertyInfo = property,
                ProxyPropertyType = property.PropertyType,
                Attributes = property.GetCustomAttributes()
                                     .ToList(),
                ExcludeFromProxy = false,
                ConversionToProxyDelegate = null,
                ConversionFromProxyDelegate = null
            };
        }
    }
}
