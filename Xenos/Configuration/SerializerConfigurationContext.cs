using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Xenos.Configuration
{
    /// <summary>
    /// Configuration context for an Xml serializer.
    /// </summary>
    public class SerializerConfigurationContext
    {
        private readonly Type _contextType;

        /// <summary>
        /// Collection of entities configured within this context.
        /// </summary>
        internal ICollection<XmlEntityConfiguration> Entities { get; }

        /// <summary>
        /// Namespace to use for serialization.
        /// </summary>
        internal XmlSerializerNamespaces GlobalNamespaces { get; }

        /// <summary>
        /// Proxy types to use for entities that have special requirements for serialization.
        /// </summary>
        internal IDictionary<Type, Type> ProxyTypes { get; }

        /// <summary>
        /// Attributes for the entities that are configured for this context.
        /// </summary>
        internal XmlAttributeOverrides Attributes { get; }

        /// <summary>
        /// Initializes a new instance of the configuration context with the default values.
        /// </summary>
        internal SerializerConfigurationContext(Type contextType)
        {
            this._contextType = contextType;
            this.Attributes = new XmlAttributeOverrides();
            this.Entities = new Collection<XmlEntityConfiguration>();
            this.ProxyTypes = new Dictionary<Type, Type>();
            this.GlobalNamespaces = new XmlSerializerNamespaces();
            this.GlobalNamespaces.Add(string.Empty, string.Empty);
        }

        /// <summary>
        /// Defines a namespace in the context's namespace table.
        /// </summary>
        /// <param name="prefix">Prefix for the namespace.</param>
        /// <param name="ns">Namespace to add to the table.</param>
        public void HasNamespace(string prefix, string ns)
        {
            this.GlobalNamespaces.Add(prefix, ns);
        }

        /// <summary>
        /// Sets the default namespace for the context.
        /// </summary>
        /// <param name="ns">Default namespace for the context.</param>
        public void HasDefaultNamespace(string ns)
        {
            this.GlobalNamespaces.Add(string.Empty, ns);
        }

        /// <summary>
        /// Adds an entity to the configuration for this serializer context.
        /// </summary>
        /// <typeparam name="T">Type of the entity to add to the context.</typeparam>
        /// <returns>Configuration object for the entity type <typeparamref name="T"/>.</returns>
        public XmlComplexTypeConfiguration<T> Entity<T>()
            where T : class
        {
            var entityConfiguration = new XmlComplexTypeConfiguration<T>(this._contextType);
            this.Entities.Add(entityConfiguration);

            return entityConfiguration;
        }

        /// <summary>
        /// Adds an enum to the configuration for this serializer context.
        /// </summary>
        /// <typeparam name="TEnum">Type of the enum to add to the context.</typeparam>
        /// <returns>Configuration object for the enum type <typeparamref name="TEnum"/></returns>
        public XmlEnumConfiguration<TEnum> Enum<TEnum>()
            where TEnum : struct
        {
            var enumConfiguration = new XmlEnumConfiguration<TEnum>(this._contextType);
            this.Entities.Add(enumConfiguration);

            return enumConfiguration;
        }
    }
}
