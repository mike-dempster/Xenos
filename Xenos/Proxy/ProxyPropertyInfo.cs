using System;
using System.Collections.Generic;
using System.Reflection;

namespace Xenos.Proxy
{
    internal class ProxyPropertyInfo
    {
        internal string PropertyName { get; set; }
        internal PropertyInfo BasePropertyInfo { get; set; }
        internal Type ProxyPropertyType { get; set; }
        internal ICollection<Attribute> Attributes { get; set; }
        internal bool ExcludeFromProxy { get; set; }
        internal MethodInfo ConversionToProxyDelegate { get; set; }
        internal MethodInfo ConversionFromProxyDelegate { get; set; }
    }
}
