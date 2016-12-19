using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Xenos.Proxy
{
    /// <summary>
    /// Implements a dynamic type in memory.
    /// </summary>
    internal class ProxyTypeImplementer
    {
        private readonly Type _baseType;
        private readonly ProxyModuleBuilder _moduleBuilder;
        private readonly IDictionary<string, ProxyPropertyImplementer> _properties;

        /// <summary>
        /// Creates a new instance of the proxy type implementer. The constructor is private to force the use of the Create method which uses the compiler to enforce that 
        /// the base type is a class type without making this class a generic type.
        /// </summary>
        /// <param name="baseType">Base type to implement a proxy type for.</param>
        /// <param name="moduleBuilder">Instance of the module builder for this serializer context.</param>
        private ProxyTypeImplementer(Type baseType, ProxyModuleBuilder moduleBuilder)
        {
            this._baseType = baseType;
            this._moduleBuilder = moduleBuilder;
            this._properties = baseType.GetProperties(BindingFlags.Instance)
                                       .Where(e => e.CanRead && e.CanWrite)
                                       .ToDictionary(e => e.Name, this.GetDefaultProxyPropertyImplementer);
        }

        /// <summary>
        /// Creates a new instance of a proxy type implementer for type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Base type that the proxy is to be implemented for.</typeparam>
        /// <returns>New instance of a proxy type implementer for the given type <typeparamref name="T"/>.</returns>
        internal static ProxyTypeImplementer GetInstance<T>(ProxyModuleBuilder moduleBuilder)
            where T : class
        {
            return new ProxyTypeImplementer(typeof(T), moduleBuilder);
        }

        /// <summary>
        /// Creates a new instance of a proxy type implementer for the given type.
        /// </summary>
        /// <param name="baseType">Base type that the proxy is to be implemented for.</param>
        /// <param name="moduleBuilder">Instance of the module builder for this serializer context.</param>
        /// <returns>New instance of a proxy type implementer for the given type <paramref name="baseType"/>.</returns>
        internal static ProxyTypeImplementer GetInstance(Type baseType, ProxyModuleBuilder moduleBuilder)
        {
            return new ProxyTypeImplementer(baseType, moduleBuilder);
        }

        /// <summary>
        /// Adds a proxy property descriptor for a property to be implemented on the type when it is generated.
        /// </summary>
        /// <param name="propertyInfo">Descriptor of the property to add.</param>
        internal void AddPropertyInfo(ProxyPropertyInfo propertyInfo)
        {
            if (this._properties.ContainsKey(propertyInfo.PropertyName))
            {
                this._properties.Remove(propertyInfo.PropertyName);
            }

            var implementer = new ProxyPropertyImplementer(propertyInfo);
            this._properties.Add(propertyInfo.PropertyName, implementer);
        }

        /// <summary>
        /// Implements the concrete type.
        /// </summary>
        /// <returns>Type descriptor of the concrete type.</returns>
        internal Type BuildProxyType()
        {
            var typeInstanceId = Guid.NewGuid()
                                     .ToString()
                                     .Replace("-", "");
            var moduleBuilder = this._moduleBuilder.GetModuleBuilder();
            var typeBuilder = moduleBuilder.DefineType($"{moduleBuilder.ScopeName}.{this._baseType.Name}{typeInstanceId}", TypeAttributes.Public | TypeAttributes.BeforeFieldInit);
            var defaultCtor = typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            foreach (var p in this._properties)
            {
                p.Value.ImplementProperty(typeBuilder);
            }

            this.BuildProxyToBaseConversionOperator(typeBuilder);
            this.BuildBaseToProxyConversionOperator(typeBuilder, defaultCtor);
            var proxyType = typeBuilder.CreateType();
            var assembly = this._moduleBuilder.GetAssembly();
            var loadedProxyType = assembly.GetType(proxyType.FullName);

            return loadedProxyType;
        }

        /// <summary>
        /// Gets a proxy property implementer for a property that does not have any special requirements for the proxy.
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo of the property to implement.</param>
        /// <returns>Proxy property implementer for the property that does not have any special requirements for the proxy.</returns>
        private ProxyPropertyImplementer GetDefaultProxyPropertyImplementer(PropertyInfo propertyInfo)
        {
            var proxyPropertyInfo = new ProxyPropertyInfo
            {
                PropertyName = propertyInfo.Name,
                BasePropertyInfo = propertyInfo,
                ProxyPropertyType = propertyInfo.PropertyType,
                Attributes = propertyInfo.GetCustomAttributes(true).OfType<Attribute>().ToArray(),
                ExcludeFromProxy = false,
                ConversionToProxyDelegate = null,
                ConversionFromProxyDelegate = null
            };
            var implementer = new ProxyPropertyImplementer(proxyPropertyInfo);

            return implementer;
        }

        /// <summary>
        /// Builds an implicit operator to cast the proxy to the base type.
        /// </summary>
        /// <param name="typeBuilder">Type builder that defines the proxy type being built.</param>
        private void BuildProxyToBaseConversionOperator(TypeBuilder typeBuilder)
        {
            var conversionOperator = typeBuilder.DefineMethod(
                "op_Implicit",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Static,
                this._baseType,
                new Type[] { typeBuilder });
            conversionOperator.DefineParameter(1, ParameterAttributes.None, "src");
            var defaultCtor = this._baseType.GetConstructor(Type.EmptyTypes);

            if (null == defaultCtor)
            {
                throw new Exception($"Default constructor does not exist on type '{this._baseType.AssemblyQualifiedName}'.");
            }

            var ilGenerator = conversionOperator.GetILGenerator();

            ilGenerator.Emit(OpCodes.Newobj, defaultCtor);

            foreach (var p in this._properties)
            {
                ilGenerator.Emit(OpCodes.Dup);
                p.Value.AddPropertyToProxyToBaseOperator(ilGenerator);
            }

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Builds an implicit operator to cast the base type to the proxy type.
        /// </summary>
        /// <param name="typeBuilder">Type builder that defines the proxy type being built.</param>
        /// <param name="defaultCtor">Default constructor of the proxy type.</param>
        private void BuildBaseToProxyConversionOperator(TypeBuilder typeBuilder, ConstructorInfo defaultCtor)
        {
            var conversionOperator = typeBuilder.DefineMethod(
                "op_Implicit",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Static,
                typeBuilder,
                new[] { this._baseType });
            conversionOperator.DefineParameter(1, ParameterAttributes.None, "src");
            var ilGenerator = conversionOperator.GetILGenerator();

            ilGenerator.Emit(OpCodes.Newobj, defaultCtor);

            foreach (var p in this._properties)
            {
                ilGenerator.Emit(OpCodes.Dup);
                p.Value.AddPropertyToBaseToProxyOperator(ilGenerator);
            }

            ilGenerator.Emit(OpCodes.Ret);
        }
    }
}
