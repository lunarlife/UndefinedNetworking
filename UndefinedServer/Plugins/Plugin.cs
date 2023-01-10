using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UndefinedServer.Plugins
{
    public abstract class Plugin
    {
        private static readonly Dictionary<string, Plugin> LoadedPlugins = new();
        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if(_isEnabled == value) return;
                _isEnabled = value;
                if (_isEnabled) OnEnable();
                else OnDisable();
            }
        }

        public abstract string Name { get; }
        public abstract PluginConfiguration Configuration { get; }
        
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }


        private static Plugin LoadFile(string filePath)
        {
            Assembly assembly;
            try
            {
               assembly = Assembly.LoadFrom(filePath);
            }
            catch (Exception e)
            {
                throw new PluginLoadException(filePath, $"Unknown error, message: {e.Message}");
            }
            var pluginType = assembly.GetTypes().FirstOrDefault(type => type.IsSubclassOf(typeof(Plugin)));
            if (pluginType is null)
                throw new PluginLoadException(filePath, $"Type extends {nameof(Plugin)} not founded");
            if (pluginType.IsAbstract || pluginType.IsInterface)
                throw new PluginLoadException(filePath, $"{pluginType.FullName} cant be abstract class or interface");
            if(!pluginType.IsSealed)
                throw new PluginLoadException(filePath, $"{pluginType.FullName} should be sealed class");
            try
            {
                var plugin = (Plugin)Activator.CreateInstance(pluginType)!;
                if (string.IsNullOrEmpty(plugin.Configuration.PluginVersion.ToString()))
                    throw new PluginLoadException(filePath, $"Plugin version cant be null or empty");
                return plugin;
            }
            catch (Exception e)
            {
                throw new PluginLoadException(filePath, $"Unknown error, message: {e.Message}");
            }
        }
        internal static async Task LoadAllPlugins()
        {
            Undefined.Logger.Info("Loading plugins...");
            await Task.Run(() =>
            {
                if (!Directory.Exists(Paths.PluginsFolder)) Directory.CreateDirectory(Paths.PluginsFolder);
                foreach (var file in Directory.GetFiles(Paths.PluginsFolder))
                {
                    if(!file.EndsWith(".dll")) continue;
                    if(LoadFile(file) is not { } plugin) return;
                    LoadedPlugins.Add(plugin.Name, plugin);
                    Undefined.Logger.Info($"Enabling plugin {plugin.Name}...");
                    plugin.IsEnabled = true;
                    Undefined.Logger.Info($"Plugin {plugin.Name} enabled");
                }
                Undefined.Logger.Info($"{LoadedPlugins.Count} plugins loaded");
            });
        }

        public static void DisableAll()
        {
            foreach (var plugin in LoadedPlugins.Values) plugin.IsEnabled = false;
        }
    }

    public class PluginException : Exception
    {
        public PluginException(string msg) : base(msg)
        {
            
        }
    }
    public class PluginLoadException : PluginException
    {
        public PluginLoadException(string pluginFile, string msg) : base($"Plugin at {pluginFile} not loaded. {msg}")
        {
            
        }
    }
}