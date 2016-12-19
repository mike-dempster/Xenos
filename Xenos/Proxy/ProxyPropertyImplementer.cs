using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Xenos.Proxy
{
    /// <summary>
    /// Implements a property on a dynamic type.
    /// </summary>
    internal class ProxyPropertyImplementer
    {
        private readonly ProxyPropertyInfo _propertyInfo;

        private PropertyBuilder propertyBuilder;

        /// <summary>
        /// Initializes the property implementer for the given property.
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo of the property to implement.</param>
        internal ProxyPropertyImplementer(ProxyPropertyInfo propertyInfo)
        {
            if (null == propertyInfo)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            this._propertyInfo = propertyInfo;
        }

        /// <summary>
        /// Creates the property on the given type builder.
        /// </summary>
        /// <param name="typeBuilder">Type builder to create the property on.</param>
        internal void ImplementProperty(TypeBuilder typeBuilder)
        {
            if (null == typeBuilder)
            {
                throw new ArgumentNullException(nameof(typeBuilder));
            }

            var backingField = typeBuilder.DefineField($"<{this._propertyInfo.PropertyName}>k__BackingField", this._propertyInfo.ProxyPropertyType, FieldAttributes.Private);
            this.propertyBuilder = typeBuilder.DefineProperty(this._propertyInfo.PropertyName, PropertyAttributes.None, this._propertyInfo.ProxyPropertyType, null);
            var getter = this.BuildGetterMethod(typeBuilder, backingField);
            this.propertyBuilder.SetGetMethod(getter);
            var setter = this.BuildSetterMethod(typeBuilder, backingField);
            this.propertyBuilder.SetSetMethod(setter);
        }

        /// <summary>
        /// Implements a conversion from the base property type to the proxy property type and assignment of the value to the proxy property.
        /// </summary>
        /// <param name="ilGenerator">IL code generator to use to generate the conversion and assignment.</param>
        internal void AddPropertyToBaseToProxyOperator(ILGenerator ilGenerator)
        {
            if (null == ilGenerator)
            {
                throw new ArgumentNullException(nameof(ilGenerator));
            }

            if (false == this._propertyInfo.BasePropertyInfo.CanRead)
            {
                // If there is no getter on the base property then return here.
                return;
            }

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.EmitCall(OpCodes.Callvirt, this._propertyInfo.BasePropertyInfo.GetMethod, new[] { this._propertyInfo.BasePropertyInfo.PropertyType });

            if (null != this._propertyInfo.ConversionToProxyDelegate)
            {
                if (this._propertyInfo.ConversionToProxyDelegate.CallingConvention == CallingConventions.VarArgs)
                {
                    ilGenerator.EmitCall(OpCodes.Call, this._propertyInfo.ConversionToProxyDelegate, new[] { this._propertyInfo.BasePropertyInfo.PropertyType });
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Call, this._propertyInfo.ConversionToProxyDelegate);
                }
            }

            ilGenerator.EmitCall(OpCodes.Call, this.propertyBuilder.SetMethod, new[] { this.propertyBuilder.PropertyType });
        }

        /// <summary>
        /// Implements a conversion from the proxy property type to the base property type and assignment of the value to the base property.
        /// </summary>
        /// <param name="ilGenerator">IL code generator to use to generate the conversion and assignment.</param>
        internal void AddPropertyToProxyToBaseOperator(ILGenerator ilGenerator)
        {
            if (null == ilGenerator)
            {
                throw new ArgumentNullException(nameof(ilGenerator));
            }

            if (false == this._propertyInfo.BasePropertyInfo.CanWrite)
            {
                // If there is no setter on the base property then return here.
                return;
            }

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.EmitCall(OpCodes.Callvirt, this.propertyBuilder.GetMethod, new[] { this.propertyBuilder.PropertyType });

            if (null != this._propertyInfo.ConversionFromProxyDelegate)
            {
                if (this._propertyInfo.ConversionFromProxyDelegate.CallingConvention == CallingConventions.VarArgs)
                {
                    ilGenerator.EmitCall(OpCodes.Call, this._propertyInfo.ConversionFromProxyDelegate, new[] { this.propertyBuilder.PropertyType });
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Call, this._propertyInfo.ConversionFromProxyDelegate);
                }
            }

            ilGenerator.EmitCall(OpCodes.Callvirt, this._propertyInfo.BasePropertyInfo.SetMethod, new[] { this._propertyInfo.BasePropertyInfo.PropertyType });
        }

        /// <summary>
        /// Builds a getter method for a property.
        /// </summary>
        /// <param name="typeBuilder">Type builder for the dynamic type that is declaring the property.</param>
        /// <param name="backingField">Field used to hold the value of the property.</param>
        /// <returns>Built up getter method.</returns>
        private MethodBuilder BuildGetterMethod(TypeBuilder typeBuilder, FieldBuilder backingField)
        {
            var propertyGetter = typeBuilder.DefineMethod(
                $"get_{this._propertyInfo.PropertyName}",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                this._propertyInfo.ProxyPropertyType,
                Type.EmptyTypes);
            var ilGenerator = propertyGetter.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, backingField);
            ilGenerator.Emit(OpCodes.Ret);

            return propertyGetter;
        }

        /// <summary>
        /// Builds a setter method for a property.
        /// </summary>
        /// <param name="typeBuilder">Type builder for the dynamic type that is declaring the property.</param>
        /// <param name="backingField">Field used to hold the value of the property.</param>
        /// <returns>Built up setter method.</returns>
        private MethodBuilder BuildSetterMethod(TypeBuilder typeBuilder, FieldBuilder backingField)
        {
            var propertySetter = typeBuilder.DefineMethod(
                $"set_{this._propertyInfo.PropertyName}",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                null,
                new[] { this._propertyInfo.ProxyPropertyType });
            var ilGenerator = propertySetter.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Stfld, backingField);
            ilGenerator.Emit(OpCodes.Ret);

            return propertySetter;
        }
    }
}
