using System;
using BenchmarkDotNet.Toolchains.MonoAotLLVM;
using JetBrains.Annotations;


namespace BenchmarkDotNet.Toolchains.DotNetCli
{
    /// <summary>
    /// custom settings used in the auto-generated project.json / .csproj file
    /// </summary>
    [PublicAPI]
    public class NetCoreAppSettings
    {
        public static readonly TimeSpan DefaultBuildTimeout = TimeSpan.FromMinutes(2); // it should always be a few seconds, but some users might have a bad internet connection

        [PublicAPI] public static readonly NetCoreAppSettings NetCoreApp20 = new NetCoreAppSettings("netcoreapp2.0", null, ".NET Core 2.0");
        [PublicAPI] public static readonly NetCoreAppSettings NetCoreApp21 = new NetCoreAppSettings("netcoreapp2.1", null, ".NET Core 2.1");
        [PublicAPI] public static readonly NetCoreAppSettings NetCoreApp22 = new NetCoreAppSettings("netcoreapp2.2", null, ".NET Core 2.2");
        [PublicAPI] public static readonly NetCoreAppSettings NetCoreApp30 = new NetCoreAppSettings("netcoreapp3.0", null, ".NET Core 3.0");
        [PublicAPI] public static readonly NetCoreAppSettings NetCoreApp31 = new NetCoreAppSettings("netcoreapp3.1", null, ".NET Core 3.1");
        [PublicAPI] public static readonly NetCoreAppSettings NetCoreApp50 = new NetCoreAppSettings("net5.0", null, ".NET 5.0");
        [PublicAPI] public static readonly NetCoreAppSettings NetCoreApp60 = new NetCoreAppSettings("net6.0", null, ".NET 6.0");
        [PublicAPI] public static readonly NetCoreAppSettings NetCoreApp70 = new NetCoreAppSettings("net7.0", null, ".NET 7.0");

        /// <summary>
        /// <param name="targetFrameworkMoniker">
        /// sample values: netcoreapp2.0, netcoreapp2.1
        /// </param>
        /// <param name="runtimeFrameworkVersion">
        /// used in the auto-generated .csproj file
        /// simply ignored if null or empty
        /// </param>
        /// <param name="name">
        /// display name used for showing the results
        /// </param>
        /// <param name="customDotNetCliPath">
        /// customize dotnet cli path if default is not desired
        /// simply ignored if null
        /// </param>
        /// <param name="packagesPath">the directory to restore packages to</param>
        /// <param name="timeout">timeout to build the benchmark</param>
        /// <param name="customRuntimePack">path to a custom runtime pack</param>
        /// <param name="aotCompilerPath">path to Mono AOT compiler</param>
        /// <param name="aotCompilerMode">Mono AOT compiler moder</param>
        /// </summary>
        [PublicAPI]
        public NetCoreAppSettings(
            string targetFrameworkMoniker,
            string runtimeFrameworkVersion,
            string name,
            string customDotNetCliPath = null,
            string packagesPath = null,
            TimeSpan? timeout = null,
            string customRuntimePack = null,
            string aotCompilerPath = null,
            MonoAotCompilerMode aotCompilerMode = MonoAotCompilerMode.mini
            )
        {
            TargetFrameworkMoniker = targetFrameworkMoniker;
            RuntimeFrameworkVersion = runtimeFrameworkVersion;
            Name = name;

            CustomDotNetCliPath = customDotNetCliPath;
            PackagesPath = packagesPath;
            Timeout = timeout ?? DefaultBuildTimeout;
            CustomRuntimePack = customRuntimePack;
            AOTCompilerPath = aotCompilerPath;
            AOTCompilerMode = aotCompilerMode;
        }

        /// <summary>
        /// sample values: netcoreapp2.0, netcoreapp2.1
        /// </summary>
        public string TargetFrameworkMoniker { get; }

        public string RuntimeFrameworkVersion { get; }

        /// <summary>
        /// display name used for showing the results
        /// </summary>
        public string Name { get; }

        public string CustomDotNetCliPath { get; }

        /// <summary>
        /// The directory to restore packages to.
        /// </summary>
        public string PackagesPath { get; }

        /// <summary>
        /// timeout to build the benchmark
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Path to a custom runtime pack.
        /// </summary>
        public string CustomRuntimePack { get; }

        /// <summary>
        /// Path to the Mono AOT Compiler
        /// </summary>
        public string AOTCompilerPath { get; }

        /// <summary>
        /// Mono AOT Compiler mode, either 'mini' or 'llvm'
        /// </summary>
        public MonoAotCompilerMode AOTCompilerMode { get; }

        public NetCoreAppSettings WithCustomDotNetCliPath(string customDotNetCliPath, string displayName = null)
            => new NetCoreAppSettings(TargetFrameworkMoniker, RuntimeFrameworkVersion, displayName ?? Name, customDotNetCliPath, PackagesPath, Timeout);

        public NetCoreAppSettings WithCustomPackagesRestorePath(string packagesPath, string displayName = null)
            => new NetCoreAppSettings(TargetFrameworkMoniker, RuntimeFrameworkVersion, displayName ?? Name, CustomDotNetCliPath, packagesPath, Timeout);

        public NetCoreAppSettings WithTimeout(TimeSpan? timeOut)
            => new NetCoreAppSettings(TargetFrameworkMoniker, RuntimeFrameworkVersion, Name, CustomDotNetCliPath, PackagesPath, timeOut ?? Timeout);
    }
}
