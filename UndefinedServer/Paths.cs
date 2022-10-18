using System.IO;

namespace UndefinedServer
{
    public static class Paths
    {
        internal static string PluginsFolder { get; }

        internal static string ResourcesFolder { get; }

        internal static string TempData { get; }

        internal static string ConstantData { get; }

        internal static string ServerConfigurationFile { get; }
        
 
        static Paths()
        {
            var exeDir = Directory.GetCurrentDirectory();
            PluginsFolder = Path.Combine(exeDir, "Plugins");
            ResourcesFolder = Path.Combine(exeDir, "Resources");
            TempData = Path.Combine(exeDir, "TempData");
            ConstantData = Path.Combine(exeDir, "ConstantData");
            ServerConfigurationFile = Path.Combine(exeDir, "server.json");
        }

    }
}