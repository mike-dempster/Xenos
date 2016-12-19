using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace Xenos.Proxy
{
    /// <summary>
    /// Managed dynamic module builder.
    /// </summary>
    internal class ProxyModuleBuilder
    {
        private readonly string _tempFilePath;
        private readonly AssemblyName _assemblyName;
        private readonly ModuleBuilder _moduleBuilder;
        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly TempFileCollection _temporaryAssemblyFiles;

        private static readonly IDictionary<Type, ProxyModuleBuilder> _proxyModuleBuilders = new Dictionary<Type, ProxyModuleBuilder>();

        /// <summary>
        /// Gets a singleton instance of the proxy module builder.
        /// </summary>
        internal static ProxyModuleBuilder GetInstance<T>()
            where T : XmlSerializerContext
        {
            ProxyModuleBuilder context;

            if (_proxyModuleBuilders.TryGetValue(typeof(T), out context))
            {
                return context;
            }

            context = new ProxyModuleBuilder();
            _proxyModuleBuilders.Add(typeof(T), context);
            return context;
        }

        /// <summary>
        /// Gets a singleton instance of the proxy module builder.
        /// </summary>
        internal static ProxyModuleBuilder GetInstance(Type contextType)
        {
            ProxyModuleBuilder context;

            if (_proxyModuleBuilders.TryGetValue(contextType, out context))
            {
                return context;
            }

            context = new ProxyModuleBuilder();
            _proxyModuleBuilders.Add(contextType, context);
            return context;
        }

        /// <summary>
        /// Initializes the internal state of the module builder with the default settings.
        /// </summary>
        private ProxyModuleBuilder()
        {
            string instanceId = Guid.NewGuid()
                                    .ToString()
                                    .Replace("-", "");
            this._temporaryAssemblyFiles = new TempFileCollection(this._tempFilePath);
            this._assemblyName = new AssemblyName($"Xenos.ProxyAsm_{instanceId}");
            this._tempFilePath = this._temporaryAssemblyFiles.BasePath;

            if (false == Directory.Exists(this._tempFilePath))
            {
                Directory.CreateDirectory(this._tempFilePath);
            }

            this._assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(this._assemblyName, AssemblyBuilderAccess.RunAndSave, this._tempFilePath);
            this._moduleBuilder = this._assemblyBuilder.DefineDynamicModule($"{this._assemblyName.Name}.Entities", $"{this._assemblyName.Name}.dll");
        }

        /// <summary>
        /// Cleans up before the module builder is picked up by the garbage collector.
        /// </summary>
        ~ProxyModuleBuilder()
        {
            this._temporaryAssemblyFiles.Delete();
        }

        /// <summary>
        /// Exposes the module builder to the internal application.
        /// </summary>
        /// <returns>Instance of the underlying module builder.</returns>
        internal ModuleBuilder GetModuleBuilder()
        {
            return this._moduleBuilder;
        }

        /// <summary>
        /// Saves the assembly to disk and returns the loaded assembly from the file on disk.
        /// </summary>
        /// <returns>The assembly loaded form file on the local disk.</returns>
        internal Assembly GetAssembly()
        {
            string assemblyName = $"{this._assemblyName.Name}.dll";
            this._assemblyBuilder.Save(assemblyName);
            string fullAssemblyName = $"{this._tempFilePath}{Path.DirectorySeparatorChar}{assemblyName}";
            this._temporaryAssemblyFiles.AddFile(fullAssemblyName, false);
            // TODO: When the assembly is loaded from the file system, the file is locked and cannot be cleaned up when the serializer is picked up by the garbage collector.
            var loadedAssembly = Assembly.LoadFile(fullAssemblyName);

            return loadedAssembly;
        }
    }
}
