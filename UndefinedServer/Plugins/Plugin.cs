using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Utils;
using Utils.AsyncOperations;
using Utils.Exceptions;

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
        public static async void LoadAllPlugins(AsyncOperationInfo<string> operation)
        {
            operation.Start("Loading plugins...");
            await Task.Run(() =>
            {
                if (!Directory.Exists(Paths.PluginsFolder)) Directory.CreateDirectory(Paths.PluginsFolder);
                foreach (var file in Directory.GetFiles(Paths.PluginsFolder))
                {
                    if(!file.EndsWith(".dll")) continue;
                    if(LoadFile(file) is not { } plugin) return;
                    LoadedPlugins.Add(plugin.Name, plugin);
                    operation.CurrentState = $"Enabling plugin {plugin.Name}...";
                    plugin.IsEnabled = true;
                    operation.CurrentState = $"Plugin {plugin.Name} enabled";
                }
                operation.CurrentState = $"{LoadedPlugins.Count} plugins loaded";
                operation.Finish();
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