using UndefinedServer.Plugins;
using Version = Utils.Version;

namespace ServerPlugin;

public class MainPluginConfiguration : PluginConfiguration
{
    public override Version PluginVersion { get; } = new("2.0");
}