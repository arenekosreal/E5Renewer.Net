using System.Reflection;
using System.Runtime.Loader;

namespace E5Renewer.Models.Modules
{
    /// <summary> AssemblyLoadContext for loading modules.</summary>
    public class ModuleLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver resolver;

        /// <summary>Initialize <c>ModuleLoadContext</c> with values given.</summary>
        /// <param name="file">The <see cref="FileInfo">FileInfo</see> instance which points to the module file.</param>
        public ModuleLoadContext(FileInfo file)
        {
            this.resolver = new(file.FullName);
        }

        /// <inheritdoc/>
        protected override Assembly? Load(AssemblyName assemblyName)
        {
            string? assemblyPath = this.resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return this.LoadFromAssemblyPath(assemblyPath);
            }
            return null;
        }

        /// <inheritdoc/>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string? unmanagedDllPath = this.resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (unmanagedDllPath != null)
            {
                return this.LoadUnmanagedDllFromPath(unmanagedDllPath);
            }
            return IntPtr.Zero;
        }
    }
}
