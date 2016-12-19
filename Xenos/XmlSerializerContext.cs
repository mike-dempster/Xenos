using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Xenos.Configuration;

namespace Xenos
{
    /// <summary>
    /// Base class for Xml serializer contexts.
    /// </summary>
    public abstract class XmlSerializerContext
    {
        private XmlSerializer instanceSerializer;
        private SerializerConfigurationContext serializerContext;

        /// <summary>
        /// When implemented in a deriving class this method configures the context for serialzing the target entities.
        /// </summary>
        /// <param name="configurationContext">Xml serializer context to configure.</param>
        protected abstract void BuildSerializerContext(SerializerConfigurationContext configurationContext);

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="stream">Stream to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(Stream stream, object o)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(stream, o, this.serializerContext.GlobalNamespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given text writer.
        /// </summary>
        /// <param name="textWriter">Text writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(TextWriter textWriter, object o)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(textWriter, o, this.serializerContext.GlobalNamespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given XML writer.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(XmlWriter xmlWriter, object o)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(xmlWriter, o, this.serializerContext.GlobalNamespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="stream">Stream to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        public void Serialize(Stream stream, object o, XmlSerializerNamespaces namespaces)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(stream, o, namespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="textWriter">Text writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        public void Serialize(TextWriter textWriter, object o, XmlSerializerNamespaces namespaces)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(textWriter, o, namespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(xmlWriter, o, namespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        /// <param name="encodingStyle">Encoding style to use for the serialize process.</param>
        public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(xmlWriter, o, namespaces, encodingStyle);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        /// <param name="encodingStyle">Encoding style to use for the serialize process.</param>
        /// <param name="id">For SOAP encoded messages, the base used to generate id attributes.</param>
        public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(xmlWriter, o, namespaces, encodingStyle, id);
        }

        /// <summary>
        /// Deserializes a stream of XML data to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlStream">Stream of XML data representing the object instance.</param>
        /// <returns>Instance of the object in the stream.</returns>
        public TEntity Deserialize<TEntity>(Stream xmlStream)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof(TEntity));
            var response = serializer.Deserialize(xmlStream);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given text reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="textReader">Text reader of XML data representing the object instance.</param>
        /// <returns>Instance of the object in the text reader.</returns>
        public TEntity Deserialize<TEntity>(TextReader textReader)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof(TEntity));
            var response = serializer.Deserialize(textReader);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public TEntity Deserialize<TEntity>(XmlReader xmlReader)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof(TEntity));
            var response = serializer.Deserialize(xmlReader);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="encodingStyle">Encoding style of the data in the XML reader.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public TEntity Deserialize<TEntity>(XmlReader xmlReader, string encodingStyle)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof(TEntity));
            var response = serializer.Deserialize(xmlReader, encodingStyle);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="events">An instance of the System.Xml.Serialization.XmlDeserializationEvents class.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public TEntity Deserialize<TEntity>(XmlReader xmlReader, XmlDeserializationEvents events)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof(TEntity));
            var response = serializer.Deserialize(xmlReader, events);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="encodingStyle">Encoding style of the data in the XML reader.</param>
        /// <param name="events">An instance of the System.Xml.Serialization.XmlDeserializationEvents class.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public TEntity Deserialize<TEntity>(XmlReader xmlReader, string encodingStyle, XmlDeserializationEvents events)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof(TEntity));
            var response = serializer.Deserialize(xmlReader, encodingStyle, events);

            return response as TEntity;
        }

        /// <summary>
        /// Get an instance of the underlying serializer configured for this context.
        /// </summary>
        /// <param name="rootType">Type of the root element in the Xml.</param>
        /// <returns>Instance of a serializer configured for the entities in this context.</returns>
        private XmlSerializer GetSerializer(Type rootType)
        {
            if (null != this.instanceSerializer)
            {
                return this.instanceSerializer;
            }

            this.serializerContext = new SerializerConfigurationContext(this.GetType());
            this.BuildSerializerContext(this.serializerContext);
            this.BuildContext(this.serializerContext);
            this.instanceSerializer = new XmlSerializer(rootType, this.serializerContext.Attributes);

            return this.instanceSerializer;
        }

        /// <summary>
        /// Builds the context configuration and configures the override attributes for the serializer.
        /// </summary>
        private void BuildContext(SerializerConfigurationContext configurationContext)
        {
            foreach (var e in configurationContext.Entities)
            {
                e.ConfigureEntity(configurationContext);
            }

            foreach (var e in configurationContext.Entities)
            {
                e.BindEntityToModel(configurationContext);
            }
        }
    }
}
